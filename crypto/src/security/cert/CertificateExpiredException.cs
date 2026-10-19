using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security.Certificates
{
    /// <summary>Exception thrown when a certificate has expired.</summary>
    [Serializable]
    public class CertificateExpiredException
        : CertificateException
    {
        /// <summary>Create a new <see cref="CertificateExpiredException"/>.</summary>
        public CertificateExpiredException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="CertificateExpiredException"/>.</summary>
        /// <param name="message">The message.</param>
        public CertificateExpiredException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="CertificateExpiredException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CertificateExpiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="CertificateExpiredException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected CertificateExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
