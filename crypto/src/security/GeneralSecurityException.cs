using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Base exception for security related failures.</summary>
    public class GeneralSecurityException
        : Exception
    {
        /// <summary>Create an exception with no detail message.</summary>
        public GeneralSecurityException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public GeneralSecurityException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public GeneralSecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="GeneralSecurityException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected GeneralSecurityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
