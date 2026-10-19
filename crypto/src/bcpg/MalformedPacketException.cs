using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg
{
    [Serializable]
    public class MalformedPacketException
        : IOException
    {
        /// <summary>Create a new <see cref="MalformedPacketException"/>.</summary>
        public MalformedPacketException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="MalformedPacketException"/>.</summary>
        /// <param name="message">The message.</param>
        public MalformedPacketException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="MalformedPacketException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MalformedPacketException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="MalformedPacketException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected MalformedPacketException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
