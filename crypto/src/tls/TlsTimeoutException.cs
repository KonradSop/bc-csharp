using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Tls
{
    /// <summary>Exception thrown when a TLS operation times out.</summary>
    [Serializable]
    public class TlsTimeoutException
        : IOException
    {
        /// <summary>Create a new <see cref="TlsTimeoutException"/>.</summary>
        public TlsTimeoutException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="TlsTimeoutException"/>.</summary>
        /// <param name="message">The message.</param>
        public TlsTimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="TlsTimeoutException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TlsTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="TlsTimeoutException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected TlsTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
