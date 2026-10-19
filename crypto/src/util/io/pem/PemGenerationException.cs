using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
    /// <summary>Exception thrown when a PEM object cannot be generated.</summary>
    [Serializable]
    public class PemGenerationException
        : Exception
    {
        /// <summary>Create a new <see cref="PemGenerationException"/>.</summary>
        public PemGenerationException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="PemGenerationException"/>.</summary>
        /// <param name="message">The message.</param>
        public PemGenerationException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="PemGenerationException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PemGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="PemGenerationException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PemGenerationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
