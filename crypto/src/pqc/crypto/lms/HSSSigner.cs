using System;
using System.IO;

using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pqc.Crypto.Lms
{
    /// <summary>A signer for HSS.</summary>
    public sealed class HssSigner
        : IMessageSigner
    {
        private HssPrivateKeyParameters m_privateKey;
        private HssPublicKeyParameters m_publicKey;

        /// <inheritdoc/>
        public void Init(bool forSigning, ICipherParameters param)
        {
            if (forSigning)
            {
                m_privateKey = (HssPrivateKeyParameters)param;
                m_publicKey = null;
            }
            else
            {
                m_publicKey = (HssPublicKeyParameters)param;
                m_privateKey = null;
            }
        }

        /// <inheritdoc/>
        public byte[] GenerateSignature(byte[] message)
        {
            try
            {
                return Hss.GenerateSignature(m_privateKey, message).GetEncoded();
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("unable to encode signature", e);
            }
        }

        /// <inheritdoc/>
        public bool VerifySignature(byte[] message, byte[] signature)
        {
            try
            {
                return Hss.VerifySignature(m_publicKey, HssSignature.GetInstance(signature, m_publicKey.Level), message);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
