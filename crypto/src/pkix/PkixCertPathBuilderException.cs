using System;
using System.Runtime.Serialization;

using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
    /// <summary>Exception thrown when a certification path cannot be built.</summary>
    [Serializable]
    public class PkixCertPathBuilderException
        : GeneralSecurityException
    {
        /// <summary>Create a new <see cref="PkixCertPathBuilderException"/>.</summary>
        public PkixCertPathBuilderException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PkixCertPathBuilderException"/>.</summary>
        /// <param name="message">The message.</param>
        public PkixCertPathBuilderException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PkixCertPathBuilderException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PkixCertPathBuilderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PkixCertPathBuilderException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PkixCertPathBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
