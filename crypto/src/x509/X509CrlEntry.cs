using System;
using System.Text;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
    /// <remarks>
    /// The following extensions are listed in RFC 2459 as relevant to CRL Entries:
    /// <list type="bullet">
    /// <item>Reason Code</item>
    /// <item>Hold Instruction Code</item>
    /// <item>Invalidity Date</item>
    /// <item>Certificate Issuer (critical)</item>
    /// </list>
    /// </remarks>
    public class X509CrlEntry
        : X509ExtensionBase
    {
        private CrlEntry c;
        private bool isIndirect;
        private X509Name previousCertificateIssuer;
        private X509Name certificateIssuer;

        private volatile bool hashValueSet;
        private volatile int hashValue;

        /// <summary>Create an entry for a CRL that is not an indirect CRL.</summary>
        /// <param name="c">The ASN.1 <see cref="CrlEntry"/> object.</param>
        public X509CrlEntry(CrlEntry c)
        {
            this.c = c;
            this.certificateIssuer = LoadCertificateIssuer();
        }

        /// <summary>
        /// Constructor for CRLEntries of indirect CRLs.
        /// </summary>
        /// <remarks>
        /// If <paramref name="isIndirect"/> is <c>false</c> <see cref="GetCertificateIssuer"/> will always return
        /// <c>null</c>, <paramref name="previousCertificateIssuer"/> is ignored. If <c>true</c> and this CRL entry
        /// has no Certificate Issuer extension, then <paramref name="previousCertificateIssuer"/> is returned by
        /// <see cref="GetCertificateIssuer"/>.
        /// </remarks>
        /// <param name="c">The ASN.1 <see cref="CrlEntry"/> object.</param>
        /// <param name="isIndirect"><c>true</c> if the corresponding CRL is an indirect CRL.</param>
        /// <param name="previousCertificateIssuer">Certificate issuer of the previous CRL entry.</param>
        public X509CrlEntry(CrlEntry c, bool isIndirect, X509Name previousCertificateIssuer)
        {
            this.c = c;
            this.isIndirect = isIndirect;
            this.previousCertificateIssuer = previousCertificateIssuer;
            this.certificateIssuer = LoadCertificateIssuer();
        }

        /// <summary>Gets the underlying ASN.1 structure.</summary>
        public virtual CrlEntry CrlEntry => c;

        private X509Name LoadCertificateIssuer()
        {
            if (!isIndirect)
                return null;

            var certificateIssuer = this.GetExtension(X509Extensions.CertificateIssuer, GeneralNames.GetInstance);
            if (certificateIssuer == null)
                return previousCertificateIssuer;

            try
            {
                foreach (var name in certificateIssuer.GetNames())
                {
                    if (name.TagNo == GeneralName.DirectoryName)
                        return X509Name.GetInstance(name.Name);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>Return the issuer of the revoked certificate.</summary>
        /// <returns>
        /// The issuer name, or null if the CRL is not an indirect CRL or no issuer can be determined.
        /// </returns>
        public X509Name GetCertificateIssuer() => certificateIssuer;

        /// <summary>Returns the extensions of the entry, or null if there are none.</summary>
        protected override X509Extensions GetX509Extensions() => c.Extensions;

        /// <summary>Return the DER encoding of the entry.</summary>
        /// <exception cref="CrlException">The entry could not be encoded.</exception>
        public byte[] GetEncoded()
        {
            try
            {
                return c.GetEncoded(Asn1Encodable.Der);
            }
            catch (Exception e)
            {
                throw new CrlException(e.ToString());
            }
        }

        /// <summary>Gets the serial number of the revoked certificate.</summary>
        public BigInteger SerialNumber => c.UserCertificate.Value;

        /// <summary>Gets the date on which the certificate was revoked.</summary>
        public DateTime RevocationDate => c.RevocationDate.ToDateTime();

        /// <summary>Gets whether the entry has any extensions.</summary>
        public bool HasExtensions => c.Extensions != null;

        /// <summary>Two entries are equal if their underlying ASN.1 structures are equal.</summary>
        public override bool Equals(object other)
        {
            if (this == other)
                return true;

            if (!(other is X509CrlEntry that))
                return false;

            if (this.hashValueSet && that.hashValueSet)
            {
                if (this.hashValue != that.hashValue)
                    return false;
            }

            return this.c.Equals(that.c);
        }

        /// <summary>Returns a hash code derived from the underlying ASN.1 structure.</summary>
        public override int GetHashCode()
        {
            if (!hashValueSet)
            {
                hashValue = this.c.GetHashCode();
                hashValueSet = true;
            }

            return hashValue;
        }

        /// <summary>Returns a textual description of the entry.</summary>
        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();

            buf.Append("        userCertificate: ").Append(this.SerialNumber).AppendLine();
            buf.Append("         revocationDate: ").Append(this.RevocationDate).AppendLine();
            buf.Append("      certificateIssuer: ").Append(this.GetCertificateIssuer()).AppendLine();

            X509Extensions extensions = c.Extensions;

            if (extensions != null)
            {
                var e = extensions.ExtensionOids.GetEnumerator();
                if (e.MoveNext())
                {
                    buf.AppendLine("   crlEntryExtensions:");

                    do
                    {
                        DerObjectIdentifier oid = e.Current;
                        X509Extension ext = extensions.GetExtension(oid);

                        if (ext.Value != null)
                        {
                            Asn1Object obj = X509ExtensionUtilities.FromExtensionValue(ext.Value);

                            buf.Append("                       critical(")
                                .Append(ext.IsCritical)
                                .Append(") ");
                            try
                            {
                                if (oid.Equals(X509Extensions.ReasonCode))
                                {
                                    buf.Append(new CrlReason(DerEnumerated.GetInstance(obj)));
                                }
                                else if (oid.Equals(X509Extensions.CertificateIssuer))
                                {
                                    buf.Append("Certificate issuer: ").Append(
                                        GeneralNames.GetInstance((Asn1Sequence)obj));
                                }
                                else
                                {
                                    buf.Append(oid.Id);
                                    buf.Append(" value = ").Append(Asn1Dump.DumpAsString(obj));
                                }
                                buf.AppendLine();
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
            }

            return buf.ToString();
        }
    }
}
