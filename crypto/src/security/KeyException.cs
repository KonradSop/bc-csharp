using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Exception thrown for problems with keys.</summary>
    public class KeyException
        : GeneralSecurityException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public KeyException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public KeyException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public KeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="KeyException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected KeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
