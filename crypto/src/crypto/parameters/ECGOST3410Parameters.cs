using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Parameters
{
    /// <summary>Domain parameters for GOST R 34.10 on an elliptic curve.</summary>
    public class ECGost3410Parameters
        : ECNamedDomainParameters
    {
        private readonly DerObjectIdentifier m_publicKeyParamSet;
        private readonly DerObjectIdentifier m_digestParamSet;
        private readonly DerObjectIdentifier m_encryptionParamSet;

        /// <summary>Create a new <see cref="ECGost3410Parameters"/>.</summary>
        /// <param name="dp">The dp.</param>
        /// <param name="publicKeyParamSet">The public key param set.</param>
        /// <param name="digestParamSet">The digest param set.</param>
        /// <param name="encryptionParamSet">The encryption param set.</param>
        public ECGost3410Parameters(ECNamedDomainParameters dp, DerObjectIdentifier publicKeyParamSet,
            DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet)
            : base(dp.Name, dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed())
        {
            m_publicKeyParamSet = publicKeyParamSet;
            m_digestParamSet = digestParamSet;
            m_encryptionParamSet = encryptionParamSet;
        }

        /// <summary>Create a new <see cref="ECGost3410Parameters"/>.</summary>
        /// <param name="dp">The dp.</param>
        /// <param name="publicKeyParamSet">The public key param set.</param>
        /// <param name="digestParamSet">The digest param set.</param>
        /// <param name="encryptionParamSet">The encryption param set.</param>
        public ECGost3410Parameters(ECDomainParameters dp, DerObjectIdentifier publicKeyParamSet,
            DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet)
            : base(dp is ECNamedDomainParameters ndp ? ndp.Name : publicKeyParamSet, dp.Curve, dp.G, dp.N, dp.H,
                  dp.GetSeed())
        {
            m_publicKeyParamSet = publicKeyParamSet;
            m_digestParamSet = digestParamSet;
            m_encryptionParamSet = encryptionParamSet;
        }

        /// <summary>Gets the public key param set.</summary>
        public DerObjectIdentifier PublicKeyParamSet => m_publicKeyParamSet;

        /// <summary>Gets the digest param set.</summary>
        public DerObjectIdentifier DigestParamSet => m_digestParamSet;

        /// <summary>Gets the encryption param set.</summary>
        public DerObjectIdentifier EncryptionParamSet => m_encryptionParamSet;
    }
}
