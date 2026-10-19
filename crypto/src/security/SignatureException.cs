using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Exception thrown when a signature cannot be generated or verified.</summary>
    public class SignatureException
        : GeneralSecurityException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public SignatureException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public SignatureException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public SignatureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SignatureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
