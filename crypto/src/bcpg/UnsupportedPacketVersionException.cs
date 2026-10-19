using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg
{
    [Serializable]
    public class UnsupportedPacketVersionException
        : Exception
    {
        /// <summary>Create a new <see cref="UnsupportedPacketVersionException"/>.</summary>
        public UnsupportedPacketVersionException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="UnsupportedPacketVersionException"/>.</summary>
        /// <param name="message">The message.</param>
        public UnsupportedPacketVersionException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="UnsupportedPacketVersionException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UnsupportedPacketVersionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="UnsupportedPacketVersionException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected UnsupportedPacketVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
