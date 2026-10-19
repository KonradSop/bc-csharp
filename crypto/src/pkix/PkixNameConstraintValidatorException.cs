using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Pkix
{
    /// <summary>Exception thrown when a name does not satisfy the name constraints being checked.</summary>
    [Serializable]
    public class PkixNameConstraintValidatorException
        : Exception
    {
        /// <summary>Create a new <see cref="PkixNameConstraintValidatorException"/>.</summary>
        public PkixNameConstraintValidatorException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PkixNameConstraintValidatorException"/>.</summary>
        /// <param name="message">The message.</param>
        public PkixNameConstraintValidatorException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PkixNameConstraintValidatorException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PkixNameConstraintValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PkixNameConstraintValidatorException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected PkixNameConstraintValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
