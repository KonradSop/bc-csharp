using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
    /// <summary>The algorithm parameters of a GOST R 34.10 public key.</summary>
    public class Gost3410PublicKeyAlgParameters
        : Asn1Encodable
    {
        /// <summary>Return an <see cref="Gost3410PublicKeyAlgParameters"/> from the given object.</summary>
        /// <param name="obj">
        /// An <see cref="Gost3410PublicKeyAlgParameters"/>, an object from which one can be constructed, or null.
        /// </param>
        /// <returns>
        /// The <see cref="Gost3410PublicKeyAlgParameters"/>, or null if <paramref name="obj"/> is null.
        /// </returns>
        public static Gost3410PublicKeyAlgParameters GetInstance(object obj)
        {
            if (obj == null)
                return null;
            if (obj is Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters)
                return gost3410PublicKeyAlgParameters;
            return new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(obj));
        }

        /// <summary>Return an <see cref="Gost3410PublicKeyAlgParameters"/> from a tagged object.</summary>
        /// <param name="taggedObject">The tagged object.</param>
        /// <param name="declaredExplicit">true if the tagging is explicit, false if implicit.</param>
        /// <returns>The <see cref="Gost3410PublicKeyAlgParameters"/>.</returns>
        public static Gost3410PublicKeyAlgParameters GetInstance(Asn1TaggedObject taggedObject, bool declaredExplicit) =>
            new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(taggedObject, declaredExplicit));

        /// <summary>Return an <see cref="Gost3410PublicKeyAlgParameters"/> from a tagged object.</summary>
        /// <param name="taggedObject">The tagged object.</param>
        /// <param name="declaredExplicit">true if the tagging is explicit, false if implicit.</param>
        /// <returns>The <see cref="Gost3410PublicKeyAlgParameters"/>.</returns>
        public static Gost3410PublicKeyAlgParameters GetTagged(Asn1TaggedObject taggedObject, bool declaredExplicit) =>
            new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetTagged(taggedObject, declaredExplicit));

        private readonly DerObjectIdentifier m_publicKeyParamSet;
        private readonly DerObjectIdentifier m_digestParamSet;
        private readonly DerObjectIdentifier m_encryptionParamSet;

        private Gost3410PublicKeyAlgParameters(Asn1Sequence seq)
        {
            int count = seq.Count, pos = 0;
            if (count < 2 || count > 3)
                throw new ArgumentException("Bad sequence size: " + count, nameof(seq));

            m_publicKeyParamSet = Asn1Utilities.Read(seq, ref pos, DerObjectIdentifier.GetInstance);
            m_digestParamSet = Asn1Utilities.Read(seq, ref pos, DerObjectIdentifier.GetInstance);
            m_encryptionParamSet = Asn1Utilities.ReadOptional(seq, ref pos, DerObjectIdentifier.GetOptional);

            if (pos != count)
                throw new ArgumentException("Unexpected elements in sequence", nameof(seq));
        }

        /// <summary>Create a new <see cref="Gost3410PublicKeyAlgParameters"/>.</summary>
        /// <param name="publicKeyParamSet">The public key param set.</param>
        /// <param name="digestParamSet">The digest param set.</param>
        public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet)
            : this(publicKeyParamSet, digestParamSet, null)
        {
        }

        /// <summary>Create a new <see cref="Gost3410PublicKeyAlgParameters"/>.</summary>
        /// <param name="publicKeyParamSet">The public key param set.</param>
        /// <param name="digestParamSet">The digest param set.</param>
        /// <param name="encryptionParamSet">The encryption param set.</param>
        public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet,
            DerObjectIdentifier encryptionParamSet)
        {
            m_publicKeyParamSet = publicKeyParamSet ?? throw new ArgumentNullException(nameof(publicKeyParamSet));
            m_digestParamSet = digestParamSet ?? throw new ArgumentNullException(nameof(digestParamSet));
            m_encryptionParamSet = encryptionParamSet;
        }

        /// <summary>Gets the public key param set.</summary>
        public DerObjectIdentifier PublicKeyParamSet => m_publicKeyParamSet;

        /// <summary>Gets the digest param set.</summary>
        public DerObjectIdentifier DigestParamSet => m_digestParamSet;

        /// <summary>Gets the encryption param set.</summary>
        public DerObjectIdentifier EncryptionParamSet => m_encryptionParamSet;

        /// <inheritdoc/>
        public override Asn1Object ToAsn1Object()
        {
            return m_encryptionParamSet == null
                ?  new DerSequence(m_publicKeyParamSet, m_digestParamSet)
                :  new DerSequence(m_publicKeyParamSet, m_digestParamSet, m_encryptionParamSet);
        }
    }
}
