using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Crypto
{
    /// <summary>This exception is thrown whenever we find something we don't expect in a message.</summary>
    [Serializable]
    public class InvalidCipherTextException
        : CryptoException
    {
        /// <summary>Create a new <see cref="InvalidCipherTextException"/>.</summary>
        public InvalidCipherTextException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="InvalidCipherTextException"/>.</summary>
        /// <param name="message">The message.</param>
        public InvalidCipherTextException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="InvalidCipherTextException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidCipherTextException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="InvalidCipherTextException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected InvalidCipherTextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
