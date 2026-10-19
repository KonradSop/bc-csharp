using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Crypto
{
    /// <summary>This exception is thrown if a buffer that is meant to have output copied into it turns out to be too
    /// short, or if we've been given insufficient input.</summary>
    /// <remarks>
    /// In general this exception will get thrown rather than an <see cref="IndexOutOfRangeException"/>.
    /// </remarks>
    [Serializable]
    public class DataLengthException
        : CryptoException
    {
        /// <summary>Create a new <see cref="DataLengthException"/>.</summary>
        public DataLengthException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="DataLengthException"/>.</summary>
        /// <param name="message">The message.</param>
        public DataLengthException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="DataLengthException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataLengthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="DataLengthException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected DataLengthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
