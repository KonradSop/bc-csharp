using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when the certificate of a signer is not valid at the time of verification.</summary>
    public class CmsVerifierCertificateNotValidException
        : CmsException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsVerifierCertificateNotValidException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsVerifierCertificateNotValidException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsVerifierCertificateNotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsVerifierCertificateNotValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
