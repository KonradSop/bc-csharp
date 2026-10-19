using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Crypto
{
    /// <summary>Exception thrown when an output buffer is too short to hold the result.</summary>
    [Serializable]
    public class OutputLengthException
        : DataLengthException
    {
        /// <summary>Create a new <see cref="OutputLengthException"/>.</summary>
        public OutputLengthException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="OutputLengthException"/>.</summary>
        /// <param name="message">The message.</param>
        public OutputLengthException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="OutputLengthException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public OutputLengthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="OutputLengthException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected OutputLengthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
