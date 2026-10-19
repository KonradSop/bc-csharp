using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.OpenSsl
{
    /// <summary>Exception thrown when a required password is missing or wrong.</summary>
    [Serializable]
    public class PasswordException
        // TODO[api] Change to IOException
#pragma warning disable CS0618 // Type or member is obsolete
        : Security.PasswordException
#pragma warning restore CS0618 // Type or member is obsolete
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

        protected PasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
