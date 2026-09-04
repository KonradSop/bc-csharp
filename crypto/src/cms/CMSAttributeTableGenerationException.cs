using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    /// <summary>Exception thrown when a CMS signed or authenticated attribute table cannot be generated.</summary>
    [Serializable]
    public class CmsAttributeTableGenerationException
        : CmsException
    {
        public CmsAttributeTableGenerationException()
            : base()
        {
        }

        public CmsAttributeTableGenerationException(string message)
            : base(message)
        {
        }

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
