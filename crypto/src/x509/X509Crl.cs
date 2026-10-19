using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
    /// <remarks>
    /// The following extensions are listed in RFC 2459 as relevant to CRLs:
    /// <list type="bullet">
    /// <item>Authority Key Identifier</item>
    /// <item>Issuer Alternative Name</item>
    /// <item>CRL Number</item>
    /// <item>Delta CRL Indicator (critical)</item>
    /// <item>Issuing Distribution Point (critical)</item>
    /// </list>
    /// </remarks>
    public class X509Crl
        : X509ExtensionBase
    {
        private class CachedEncoding
        {
            private readonly byte[] m_encoding;
            private readonly CrlException m_exception;

            internal CachedEncoding(byte[] encoding, CrlException exception)
            {
                m_encoding = encoding;
                m_exception = exception;
            }

            internal byte[] Encoding => m_encoding;

            internal byte[] GetEncoded()
            {
                if (null != m_exception)
                    throw m_exception;

                if (null == m_encoding)
                    throw new CrlException();

                return m_encoding;
            }
        }

        private readonly CertificateList c;
        private readonly byte[] sigAlgParams;
        private readonly bool isIndirect;

        private string m_sigAlgName = null;

        private CachedEncoding cachedEncoding;

        private volatile bool hashValueSet;
        private volatile int hashValue;

        /// <summary>Parse a CRL from its encoding.</summary>
        /// <param name="encoding">The encoded CRL.</param>
        public X509Crl(byte[] encoding)
            : this(CertificateList.GetInstance(encoding))
        {
        }

        /// <summary>Wrap an ASN.1 CertificateList structure.</summary>
        /// <param name="c">The ASN.1 structure.</param>
        /// <exception cref="CrlException">The contents of the CRL are invalid.</exception>
        public X509Crl(CertificateList c)
        {
            this.c = c;

            try
            {
                Asn1Encodable parameters = c.SignatureAlgorithm.Parameters;
                this.sigAlgParams = parameters?.GetEncoded(Asn1Encodable.Der);

                this.isIndirect = IsIndirectCrl;
            }
            catch (Exception e)
            {
                throw new CrlException("CRL contents invalid: " + e);
            }
        }

        /// <summary>Gets the underlying ASN.1 structure.</summary>
        public virtual CertificateList CertificateList => c;

        /// <summary>Returns the extensions of the CRL, or null for a version 1 CRL.</summary>
        protected override X509Extensions GetX509Extensions() => c.Version >= 2 ? c.TbsCertList.Extensions : null;

        // TODO[api] Rename 'key' to 'publicKey'
        /// <summary>Check whether the CRL signature is valid for the given public key.</summary>
        /// <param name="key">The public key of the issuer.</param>
        /// <returns>true if the signature is valid, false otherwise.</returns>
        /// <exception cref="CrlException">The signature algorithms in the CRL do not match.</exception>
        public virtual bool IsSignatureValid(AsymmetricKeyParameter key) =>
            CheckSignatureValid(new Asn1VerifierFactory(c.SignatureAlgorithm, key));

        /// <summary>Check whether the CRL signature is valid, using a verifier from the given provider.</summary>
        /// <param name="verifierProvider">A provider of signature verifier factories.</param>
        /// <returns>true if the signature is valid, false otherwise.</returns>
        /// <exception cref="CrlException">The signature algorithms in the CRL do not match.</exception>
        public virtual bool IsSignatureValid(IVerifierFactoryProvider verifierProvider) =>
            CheckSignatureValid(verifierProvider.CreateVerifierFactory(c.SignatureAlgorithm));

        /// <summary>Check whether the alternative signature carried in the CRL's extensions is valid.</summary>
        /// <param name="verifierProvider">A provider of signature verifier factories.</param>
        /// <returns>true if the alternative signature is valid, false otherwise.</returns>
        public virtual bool IsAlternativeSignatureValid(IVerifierFactoryProvider verifierProvider)
        {
            var tbsCertList = c.TbsCertList;
            var extensions = tbsCertList.Extensions;

            AltSignatureAlgorithm altSigAlg = AltSignatureAlgorithm.FromExtensions(extensions);
            AltSignatureValue altSigValue = AltSignatureValue.FromExtensions(extensions);

            var verifier = verifierProvider.CreateVerifierFactory(altSigAlg.Algorithm);

            Asn1Sequence tbsSeq = Asn1Sequence.GetInstance(tbsCertList.ToAsn1Object());
            Asn1EncodableVector v = new Asn1EncodableVector();

            int start = 1;    //  want to skip signature field
            if (tbsSeq[0] is DerInteger version)
            {
                v.Add(version);
                start++;
            }

            for (int i = start; i < tbsSeq.Count - 1; i++)
            {
                v.Add(tbsSeq[i]);
            }

            v.Add(new DerTaggedObject(true, 0, extensions.ToAsn1ObjectTrimmed()));

            return X509Utilities.VerifySignature(verifier, new DerSequence(v), altSigValue.Signature);
        }

        /// <summary>Verify the CRL signature with the given public key.</summary>
        /// <param name="publicKey">The public key of the issuer.</param>
        /// <exception cref="InvalidKeyException">The CRL does not verify with the key.</exception>
        /// <exception cref="CrlException">The signature algorithms in the CRL do not match.</exception>
        public virtual void Verify(AsymmetricKeyParameter publicKey) =>
            CheckSignature(new Asn1VerifierFactory(c.SignatureAlgorithm, publicKey));

        /// <summary>Verify the CRL signature, using a verifier from the given provider.</summary>
        /// <param name="verifierProvider">A provider of signature verifier factories.</param>
        /// <exception cref="InvalidKeyException">The CRL does not verify.</exception>
        /// <exception cref="CrlException">The signature algorithms in the CRL do not match.</exception>
        public virtual void Verify(IVerifierFactoryProvider verifierProvider) =>
            CheckSignature(verifierProvider.CreateVerifierFactory(c.SignatureAlgorithm));

        /// <summary>Verify the alternative signature carried in the CRL's extensions.</summary>
        /// <param name="verifierProvider">A provider of signature verifier factories.</param>
        /// <exception cref="InvalidKeyException">The alternative signature does not verify.</exception>
        public virtual void VerifyAltSignature(IVerifierFactoryProvider verifierProvider)
        {
            if (!IsAlternativeSignatureValid(verifierProvider))
                throw new InvalidKeyException("CRL alternative signature does not verify with supplied public key.");
        }

        /// <summary>Verify the CRL signature using the given verifier factory.</summary>
        /// <param name="verifier">The verifier factory.</param>
        /// <exception cref="InvalidKeyException">The CRL does not verify.</exception>
        protected virtual void CheckSignature(IVerifierFactory verifier)
        {
            if (!CheckSignatureValid(verifier))
                throw new InvalidKeyException("CRL does not verify with supplied public key.");
        }

        /// <summary>Check the CRL signature using the given verifier factory.</summary>
        /// <param name="verifier">The verifier factory.</param>
        /// <returns>true if the signature is valid, false otherwise.</returns>
        /// <exception cref="CrlException">The signature algorithms in the CRL do not match.</exception>
        protected virtual bool CheckSignatureValid(IVerifierFactory verifier)
        {
            var tbsCertList = c.TbsCertList;

            if (!X509Utilities.AreEquivalentAlgorithms(c.SignatureAlgorithm, tbsCertList.Signature))
                throw new CrlException("Signature algorithm on CertificateList does not match TbsCertList.");

            return X509Utilities.VerifySignature(verifier, tbsCertList, c.Signature);
        }

        /// <summary>Gets the version number of the CRL (1 or 2).</summary>
        public virtual int Version => c.Version;

        /// <summary>Gets the distinguished name of the CRL issuer.</summary>
        public virtual X509Name IssuerDN => c.Issuer;

        /// <summary>Gets the date this CRL was issued.</summary>
        public virtual DateTime ThisUpdate => c.ThisUpdate.ToDateTime();

        /// <summary>Gets the date by which the next CRL will be issued, or null if not specified.</summary>
        public virtual DateTime? NextUpdate => c.NextUpdate?.ToDateTime();

        private ISet<X509CrlEntry> LoadCrlEntries()
        {
            var entrySet = new HashSet<X509CrlEntry>();
            var revoked = c.GetRevokedCertificateEnumeration();

            X509Name previousCertificateIssuer = IssuerDN;
            foreach (CrlEntry entry in revoked)
            {
                X509CrlEntry crlEntry = new X509CrlEntry(entry, isIndirect, previousCertificateIssuer);
                entrySet.Add(crlEntry);
                previousCertificateIssuer = crlEntry.GetCertificateIssuer();
            }

            return entrySet;
        }

        /// <summary>Return the revocation entry for the given certificate serial number.</summary>
        /// <param name="serialNumber">The serial number of the certificate.</param>
        /// <returns>The entry, or null if the certificate is not listed.</returns>
        public virtual X509CrlEntry GetRevokedCertificate(
            BigInteger serialNumber)
        {
            var certs = c.GetRevokedCertificateEnumeration();

            X509Name previousCertificateIssuer = IssuerDN;
            foreach (CrlEntry entry in certs)
            {
                X509CrlEntry crlEntry = new X509CrlEntry(entry, isIndirect, previousCertificateIssuer);

                if (serialNumber.Equals(entry.UserCertificate.Value))
                    return crlEntry;

                previousCertificateIssuer = crlEntry.GetCertificateIssuer();
            }

            return null;
        }

        /// <summary>Return the entries of all the revoked certificates listed in the CRL.</summary>
        /// <returns>The entries, or null if there are none.</returns>
        public virtual ISet<X509CrlEntry> GetRevokedCertificates()
        {
            var entrySet = LoadCrlEntries();

            if (entrySet.Count > 0)
                return entrySet;

            return null;
        }

        /// <summary>Return the DER encoding of the tbsCertList field.</summary>
        /// <exception cref="CrlException">The field could not be encoded.</exception>
        public virtual byte[] GetTbsCertList()
        {
            try
            {
                return c.TbsCertList.GetDerEncoded();
            }
            catch (Exception e)
            {
                throw new CrlException(e.ToString());
            }
        }

        /// <summary>Return the signature octets.</summary>
        public virtual byte[] GetSignature() => c.GetSignatureOctets();

        /// <summary>
        /// A meaningful version of the Signature Algorithm. (e.g. SHA1WITHRSA)
        /// </summary>
        /// <returns>A string representing the signature algorithm.</returns>
        public virtual string SigAlgName => Objects.EnsureSingletonInitialized(ref m_sigAlgName, SignatureAlgorithm,
            X509SignatureUtilities.GetSignatureName);

        /// <summary>Gets the object identifier of the signature algorithm.</summary>
        public virtual string SigAlgOid => c.SignatureAlgorithm.Algorithm.GetID();

        /// <summary>Return a copy of the DER encoding of the signature algorithm parameters, or null if there are none.
        /// </summary>
        public virtual byte[] GetSigAlgParams() => Arrays.Clone(sigAlgParams);

        /// <summary>Gets the signature algorithm identifier.</summary>
        public virtual AlgorithmIdentifier SignatureAlgorithm => c.SignatureAlgorithm;

        /// <summary>
        /// Return the DER encoding of this CRL.
        /// </summary>
        /// <returns>A byte array containing the DER encoding of this CRL.</returns>
        /// <exception cref="CrlException">If there is an error encoding the CRL.</exception>
        public virtual byte[] GetEncoded() => Arrays.Clone(GetCachedEncoding().GetEncoded());

        /// <summary>Two CRLs are equal if their DER encodings are equal.</summary>
        public override bool Equals(object other)
        {
            if (this == other)
                return true;

            if (!(other is X509Crl that))
                return false;

            if (this.hashValueSet && that.hashValueSet)
            {
                if (this.hashValue != that.hashValue)
                    return false;
            }
            else if (null == this.cachedEncoding || null == that.cachedEncoding)
            {
                DerBitString signature = c.Signature;
                if (null != signature && !signature.Equals(that.c.Signature))
                    return false;
            }

            byte[] thisEncoding = this.GetCachedEncoding().Encoding;
            byte[] thatEncoding = that.GetCachedEncoding().Encoding;

            return null != thisEncoding
                && null != thatEncoding
                && Arrays.AreEqual(thisEncoding, thatEncoding);
        }

        /// <summary>Returns a hash code derived from the DER encoding.</summary>
        public override int GetHashCode()
        {
            if (!hashValueSet)
            {
                byte[] thisEncoding = this.GetCachedEncoding().Encoding;

                hashValue = Arrays.GetHashCode(thisEncoding);
                hashValueSet = true;
            }

            return hashValue;
        }

        /// <summary>Returns a string representation of this CRL.</summary>
        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();

            buf.Append("              Version: ").Append(this.Version).AppendLine();
            buf.Append("             IssuerDN: ").Append(this.IssuerDN).AppendLine();
            buf.Append("          This update: ").Append(this.ThisUpdate).AppendLine();
            buf.Append("          Next update: ").Append(this.NextUpdate).AppendLine();
            buf.Append("  Signature Algorithm: ").Append(this.SigAlgName).AppendLine();

            byte[] sig = this.GetSignature();

            buf.Append("            Signature: ");
            buf.AppendLine(Hex.ToHexString(sig, 0, 20));

            for (int i = 20; i < sig.Length; i += 20)
            {
                int count = System.Math.Min(20, sig.Length - i);
                buf.Append("                       ");
                buf.AppendLine(Hex.ToHexString(sig, i, count));
            }

            X509Extensions extensions = c.TbsCertList.Extensions;

            if (extensions != null)
            {
                var e = extensions.ExtensionOids.GetEnumerator();

                if (e.MoveNext())
                {
                    buf.AppendLine("           Extensions:");
                }

                do
                {
                    DerObjectIdentifier oid = e.Current;
                    X509Extension ext = extensions.GetExtension(oid);

                    if (ext.Value != null)
                    {
                        Asn1Object asn1Value = X509ExtensionUtilities.FromExtensionValue(ext.Value);

                        buf.Append("                       critical(").Append(ext.IsCritical).Append(") ");
                        try
                        {
                            if (oid.Equals(X509Extensions.CrlNumber))
                            {
                                buf.Append(new CrlNumber(DerInteger.GetInstance(asn1Value).PositiveValue)).AppendLine();
                            }
                            else if (oid.Equals(X509Extensions.DeltaCrlIndicator))
                            {
                                buf.Append(
                                    "Base CRL: "
                                    + new CrlNumber(DerInteger.GetInstance(
                                    asn1Value).PositiveValue))
                                    .AppendLine();
                            }
                            else if (oid.Equals(X509Extensions.IssuingDistributionPoint))
                            {
                                buf.Append(IssuingDistributionPoint.GetInstance((Asn1Sequence)asn1Value)).AppendLine();
                            }
                            else if (oid.Equals(X509Extensions.CrlDistributionPoints))
                            {
                                buf.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Value)).AppendLine();
                            }
                            else if (oid.Equals(X509Extensions.FreshestCrl))
                            {
                                buf.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Value)).AppendLine();
                            }
                            else
                            {
                                buf.Append(oid.Id);
                                buf.Append(" value = ").Append(
                                    Asn1Dump.DumpAsString(asn1Value))
                                    .AppendLine();
                            }
                        }
                        catch (Exception)
                        {
                            buf.Append(oid.Id);
                            buf.Append(" value = ").Append("*****").AppendLine();
                        }
                    }
                    else
                    {
                        buf.AppendLine();
                    }
                }
                while (e.MoveNext());
            }

            var certSet = GetRevokedCertificates();
            if (certSet != null)
            {
                foreach (X509CrlEntry entry in certSet)
                {
                    buf.Append(entry);
                    buf.AppendLine();
                }
            }

            return buf.ToString();
        }

        /// <summary>Checks whether the given certificate is on this CRL.</summary>
        /// <param name="cert">The certificate to check for.</param>
        /// <returns><c>true</c> if the given certificate is on this CRL, <c>false</c> otherwise.</returns>
        public virtual bool IsRevoked(X509Certificate cert)
        {
            CrlEntry[] certs = c.GetRevokedCertificates();

            if (certs != null)
            {
                BigInteger serial = cert.SerialNumber;

                for (int i = 0; i < certs.Length; i++)
                {
                    if (certs[i].UserCertificate.HasValue(serial))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets whether this is an indirect CRL, according to its issuing distribution point extension.
        /// </summary>
        /// <exception cref="CrlException">The extension could not be read.</exception>
        protected virtual bool IsIndirectCrl
        {
            get
            {
                IssuingDistributionPoint idp;
                try
                {
                    idp = this.GetExtension(X509Extensions.IssuingDistributionPoint,
                        IssuingDistributionPoint.GetInstance);
                }
                catch (Exception e)
                {
                    throw new CrlException("Exception reading IssuingDistributionPoint" + e);
                }

                return idp != null && idp.IsIndirectCrl;
            }
        }

        internal byte[] GetEncodedInternal() => GetCachedEncoding().GetEncoded();

        private CachedEncoding GetCachedEncoding() =>
            Objects.EnsureSingletonInitialized(ref cachedEncoding, c, CreateCachedEncoding);

        private static CachedEncoding CreateCachedEncoding(CertificateList c)
        {
            byte[] encoding = null;
            CrlException exception = null;
            try
            {
                encoding = c.GetEncoded(Asn1Encodable.Der);
            }
            catch (IOException e)
            {
                exception = new CrlException("Failed to DER-encode CRL", e);
            }

            return new CachedEncoding(encoding, exception);
        }
    }
}
