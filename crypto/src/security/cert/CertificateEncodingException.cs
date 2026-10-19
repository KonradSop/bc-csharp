using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security.Certificates
{
    /// <summary>Exception thrown when a certificate cannot be encoded.</summary>
    [Serializable]
    public class CertificateEncodingException
        : CertificateException
    {
        /// <summary>Create a new <see cref="CertificateEncodingException"/>.</summary>
        public CertificateEncodingException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="CertificateEncodingException"/>.</summary>
        /// <param name="message">The message.</param>
        public CertificateEncodingException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="CertificateEncodingException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CertificateEncodingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="CertificateEncodingException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected CertificateEncodingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
