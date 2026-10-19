using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg
{
    /// <summary>Exception thrown when a packet has a version that is not supported.</summary>
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
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected UnsupportedPacketVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
