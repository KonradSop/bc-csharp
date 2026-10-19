using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Asn1
{
    /// <summary>Exception thrown when an ASN.1 structure cannot be parsed.</summary>
    [Serializable]
    public class Asn1ParsingException
        : InvalidOperationException
    {
        /// <summary>Create a new <see cref="Asn1ParsingException"/>.</summary>
        public Asn1ParsingException()
            : base()
        {
        }

        /// <summary>Create a new <see cref="Asn1ParsingException"/>.</summary>
        /// <param name="message">The message.</param>
        public Asn1ParsingException(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="Asn1ParsingException"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public Asn1ParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="Asn1ParsingException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected Asn1ParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
