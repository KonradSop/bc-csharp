using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Tsp
{
    /// <summary>Exception thrown when a time stamp request, response or token cannot be processed.</summary>
    [Serializable]
    public class TspException
        : Exception
    {
        /// <summary>Create a new <see cref="TspException"/>.</summary>
        public TspException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="TspException"/>.</summary>
        /// <param name="message">The message.</param>
        public TspException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="TspException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TspException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="TspException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected TspException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
