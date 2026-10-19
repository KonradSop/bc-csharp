using System;

using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement
{
    /// <summary>ECDH key agreement followed by a key derivation function.</summary>
    // TODO[api] sealed, avoid inheritance
    public class ECDHWithKdfBasicAgreement
        : ECDHBasicAgreement
    {
        private readonly string m_algorithm;
        private readonly IDerivationFunction m_kdf;

        /// <summary>Create a new <see cref="ECDHWithKdfBasicAgreement"/>.</summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="kdf">The kdf.</param>
        public ECDHWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
        {
            m_algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            m_kdf = kdf ?? throw new ArgumentNullException(nameof(kdf));
        }

        /// <inheritdoc/>
        public override BigInteger CalculateAgreement(ICipherParameters pubKey)
        {
            BigInteger result = base.CalculateAgreement(pubKey);

            return BasicAgreementWithKdf.CalculateAgreementWithKdf(m_algorithm, m_kdf, GetFieldSize(), result);
        }
    }
}
