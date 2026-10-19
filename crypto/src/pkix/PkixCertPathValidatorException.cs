using System;
using System.Runtime.Serialization;

using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
    /// <summary>
    /// An exception indicating one of a variety of problems encountered when validating a certification path.
    /// </summary>
    /// <remarks>
    /// The exception may wrap the exception that caused it, available through <see cref="Exception.InnerException"/>.
    /// It may also include the index of the certificate in the certification path that caused the exception to be
    /// thrown, available through <see cref="Index"/>.
    /// <para>
    /// Unless otherwise specified, the methods defined in this class are not thread-safe. Multiple threads that need
    /// to access a single object concurrently should synchronize amongst themselves and provide the necessary
    /// locking. Multiple threads each manipulating separate objects need not synchronize.
    /// </para>
    /// </remarks>
    /// <seealso cref="PkixCertPathValidator"/>
    [Serializable]
    public class PkixCertPathValidatorException
        : GeneralSecurityException
    {
        /// <summary>The index of the offending certificate in the path, or -1 if none has been set.</summary>
        protected readonly int m_index = -1;

        /// <summary>Create an exception with no detail message.</summary>
        public PkixCertPathValidatorException()
            : base()
        {
        }

        /// <summary>Create an exception with the specified detail message.</summary>
        /// <param name="message">The detail message.</param>
        public PkixCertPathValidatorException(string message)
            : base(message)
        {
        }

        /// <summary>Create an exception with the specified detail message and cause.</summary>
        /// <param name="message">The detail message.</param>
        /// <param name="innerException">The cause.</param>
        public PkixCertPathValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a <c>PkixCertPathValidatorException</c> with the specified detail message, cause and index.
        /// </summary>
        /// <param name="message">The detail message (or null if none).</param>
        /// <param name="innerException">The cause (or null if none).</param>
        /// <param name="index">The index of the certificate in the path that caused the exception.</param>
        /// <exception cref="ArgumentException"><paramref name="index"/> is less than -1.</exception>
        public PkixCertPathValidatorException(string message, Exception innerException, int index)
            : base(message, innerException)
        {
            if (index < -1)
                throw new ArgumentException("cannot be < -1", nameof(index));

            m_index = index;
        }

        /// <summary>Create a new <see cref="PkixCertPathValidatorException"/>.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PkixCertPathValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_index = info.GetInt32("index");
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("index", m_index);
        }

        /// <summary>
        /// Returns the index of the certificate in the certification path that caused the exception to be thrown.
        /// </summary>
        /// <remarks>
        /// Note that the list of certificates in a <see cref="PkixCertPath"/> is zero based. If no index has been set,
        /// -1 is returned.
        /// </remarks>
        /// <returns>The index that has been set, or -1 if none has been set.</returns>
        public int Index => m_index;
    }
}
