using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when a CMS stream cannot be read or written.</summary>
    public class CmsStreamException
        : IOException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsStreamException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsStreamException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsStreamException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsStreamException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
