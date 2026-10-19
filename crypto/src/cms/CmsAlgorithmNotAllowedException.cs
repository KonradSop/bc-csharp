using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when an algorithm is not allowed by the configured policy.</summary>
    public class CmsAlgorithmNotAllowedException
        : CmsException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsAlgorithmNotAllowedException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsAlgorithmNotAllowedException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsAlgorithmNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsAlgorithmNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
