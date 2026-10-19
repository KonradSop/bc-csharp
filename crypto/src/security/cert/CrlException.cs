using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security.Certificates
{
    [Serializable]
    public class CrlException
        : GeneralSecurityException
    {
        /// <summary>Create a new <see cref="CrlException"/>.</summary>
        public CrlException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="CrlException"/>.</summary>
        /// <param name="message">The message.</param>
        public CrlException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="CrlException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CrlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="CrlException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected CrlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
