using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Asn1
{
    /// <summary>Exception thrown for errors in ASN.1 encodings.</summary>
    [Serializable]
    public class Asn1Exception
        : IOException
    {
        /// <summary>Create a new <see cref="Asn1Exception"/>.</summary>
        public Asn1Exception()
            : base()
        {
        }

        /// <summary>Create a new <see cref="Asn1Exception"/>.</summary>
        /// <param name="message">The message.</param>
        public Asn1Exception(string message)
            : base(message)
        {
        }

        /// <summary>Create a new <see cref="Asn1Exception"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public Asn1Exception(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Create a new <see cref="Asn1Exception"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected Asn1Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
