using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Security
{
    [Obsolete("Use Org.BouncyCastle.OpenSsl.EncryptionException instead")]
    [Serializable]
    /// <summary>Exception thrown when data cannot be encrypted or decrypted.</summary>
    public class EncryptionException
        : IOException
    {
        /// <summary>Create an exception with no detail message.</summary>
        public EncryptionException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public EncryptionException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public EncryptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EncryptionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
