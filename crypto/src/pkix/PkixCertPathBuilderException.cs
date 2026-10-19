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
        public PkixCertPathBuilderException()
            : base()
        {
        }

        public PkixCertPathBuilderException(string message)
            : base(message)
        {
        }

        public PkixCertPathBuilderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PkixCertPathBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
