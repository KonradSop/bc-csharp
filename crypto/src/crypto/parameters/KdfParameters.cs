namespace Org.BouncyCastle.Crypto.Parameters
{
    /// <summary>Parameters for Key Derivation Functions for IEEE P1363a.</summary>
    public class KdfParameters
        : IDerivationParameters
    {
        private readonly byte[] m_iv;
        private readonly byte[] m_shared;

        /// <summary>Create a new <see cref="KdfParameters"/>.</summary>
        /// <param name="shared">The shared.</param>
        /// <param name="iv">The initialization vector.</param>
        public KdfParameters(byte[] shared, byte[] iv)
        {
            m_shared = shared;
            m_iv = iv;
        }

        /// <summary>Return the IV.</summary>
        public byte[] GetIV() => m_iv;

        /// <summary>Return the shared secret.</summary>
        public byte[] GetSharedSecret() => m_shared;
    }
}
