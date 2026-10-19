using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Exception thrown when a key is invalid or unsuitable for the operation.</summary>
    public class InvalidKeyException
        : KeyException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public InvalidKeyException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public InvalidKeyException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public InvalidKeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="InvalidKeyException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected InvalidKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
