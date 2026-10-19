using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cmp
{
    /// <summary>Exception thrown when a CMP message cannot be created or processed.</summary>
    [Serializable]
    public class CmpException
        : Exception
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmpException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmpException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="CmpException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected CmpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
