using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Obsolete("Use Org.BouncyCastle.OpenSsl.PasswordException instead")]
    [Serializable]
    /// <summary>Exception thrown when a password is missing or wrong.</summary>
    public class PasswordException
        : IOException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public PasswordException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public PasswordException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public PasswordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PasswordException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
