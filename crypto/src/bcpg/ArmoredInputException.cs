using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Bcpg
{
    /// <summary>Exception thrown when armored input is malformed.</summary>
    [Serializable]
    public class ArmoredInputException
        : IOException
    {
        /// <summary>Create a new <see cref="ArmoredInputException"/>.</summary>
        public ArmoredInputException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="ArmoredInputException"/>.</summary>
        /// <param name="message">The message.</param>
        public ArmoredInputException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="ArmoredInputException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArmoredInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="ArmoredInputException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ArmoredInputException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
