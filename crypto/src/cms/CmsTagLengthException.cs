using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when a tag or length in a CMS structure is not valid.</summary>
    public class CmsTagLengthException
        : CmsException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsTagLengthException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsTagLengthException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsTagLengthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsTagLengthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
