using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.OpenSsl
{
    /// <summary>Exception thrown when a PEM object is malformed or cannot be parsed.</summary>
    [Serializable]
    public class PemException
        : IOException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public PemException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public PemException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public PemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
