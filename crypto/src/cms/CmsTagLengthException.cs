using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Cms
{
    /// <summary>Exception thrown when an authenticated CMS content tag length is invalid.</summary>
    [Serializable]
    public class CmsTagLengthException
        : CmsException
    {
        public CmsTagLengthException()
            : base()
        {
        }

        public CmsTagLengthException(string message)
            : base(message)
        {
        }

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
