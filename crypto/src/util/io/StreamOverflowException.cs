using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Utilities.IO
{
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
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected StreamOverflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
