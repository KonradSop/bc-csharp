using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
    /// <summary>Thrown if the key checksum is invalid.</summary>
    [Serializable]
    public class PgpKeyValidationException
        : PgpException
    {
        /// <summary>Create a new <see cref="PgpKeyValidationException"/>.</summary>
        public PgpKeyValidationException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PgpKeyValidationException"/>.</summary>
        /// <param name="message">The message.</param>
        public PgpKeyValidationException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PgpKeyValidationException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PgpKeyValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PgpKeyValidationException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PgpKeyValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
