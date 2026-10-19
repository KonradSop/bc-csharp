using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Exception thrown when a requested algorithm or mechanism is not recognised or supported.</summary>
    public class SecurityUtilityException
        : Exception
    {
        /// <summary>Create an exception with no detail message.</summary>
        public SecurityUtilityException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public SecurityUtilityException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public SecurityUtilityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="SecurityUtilityException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected SecurityUtilityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
