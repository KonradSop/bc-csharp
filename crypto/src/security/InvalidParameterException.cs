using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Serializable]
    /// <summary>Exception thrown when a parameter is invalid.</summary>
    public class InvalidParameterException
        : KeyException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public InvalidParameterException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public InvalidParameterException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public InvalidParameterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
