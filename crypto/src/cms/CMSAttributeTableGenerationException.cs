using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    [Serializable]
    /// <summary>Exception thrown when a CMS attribute table cannot be generated.</summary>
    public class CmsAttributeTableGenerationException
        : CmsException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public CmsAttributeTableGenerationException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public CmsAttributeTableGenerationException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public CmsAttributeTableGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CmsAttributeTableGenerationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
