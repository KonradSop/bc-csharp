using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Tls
{
    /// <summary>Base class for exceptions thrown by the TLS protocol implementation.</summary>
    [Serializable]
    public class TlsException
        : IOException
    {
        /// <summary>Create a new <see cref="TlsException"/>.</summary>
        public TlsException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="TlsException"/>.</summary>
        /// <param name="message">The message.</param>
        public TlsException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="TlsException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TlsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="TlsException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected TlsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
