using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Crypto
{
    /// <summary>General exception thrown for errors in cryptographic operations.</summary>
    [Serializable]
    public class CryptoException
        : Exception
    {
        /// <summary>Create a new <see cref="CryptoException"/>.</summary>
        public CryptoException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="CryptoException"/>.</summary>
        /// <param name="message">The message.</param>
        public CryptoException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="CryptoException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CryptoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="CryptoException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected CryptoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
