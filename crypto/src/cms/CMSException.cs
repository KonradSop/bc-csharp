using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when a CMS message cannot be created or processed.</summary>
    public class CmsException
        : Exception
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
