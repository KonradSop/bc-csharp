using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Utilities.IO
{
    /// <summary>Exception thrown when a stream holds more data than a configured limit allows.</summary>
    [Serializable]
    public class StreamOverflowException
        : IOException
    {
        /// <summary>Create a new <see cref="StreamOverflowException"/>.</summary>
        public StreamOverflowException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="StreamOverflowException"/>.</summary>
        /// <param name="message">The message.</param>
        public StreamOverflowException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="StreamOverflowException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public StreamOverflowException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="StreamOverflowException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected StreamOverflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
