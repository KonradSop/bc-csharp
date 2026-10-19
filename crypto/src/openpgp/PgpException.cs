using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
    /// <summary>Generic exception class for PGP encoding/decoding problems.</summary>
    [Serializable]
    public class PgpException
        : Exception
    {
        /// <summary>Create a new <see cref="PgpException"/>.</summary>
        public PgpException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PgpException"/>.</summary>
        /// <param name="message">The message.</param>
        public PgpException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PgpException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PgpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PgpException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected PgpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
