using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Pkix
{
    /// <summary>Exception thrown when a name does not satisfy the name constraints being checked.</summary>
    [Serializable]
    public class PkixNameConstraintValidatorException
        : Exception
    {
        public PkixNameConstraintValidatorException()
            : base()
        {
        }

        public PkixNameConstraintValidatorException(string message)
            : base(message)
        {
        }

        public PkixNameConstraintValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PkixNameConstraintValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
