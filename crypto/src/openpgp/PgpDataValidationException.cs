using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
    /// <summary>Thrown if the IV at the start of a data stream indicates the wrong key is being used.</summary>
    [Serializable]
    public class PgpDataValidationException
        : PgpException
    {
        /// <summary>Create a new <see cref="PgpDataValidationException"/>.</summary>
        public PgpDataValidationException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PgpDataValidationException"/>.</summary>
        /// <param name="message">The message.</param>
        public PgpDataValidationException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PgpDataValidationException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PgpDataValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PgpDataValidationException"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected PgpDataValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
