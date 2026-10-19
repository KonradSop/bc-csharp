using System;
using System.Runtime.Serialization;

namespace Org.BouncyCastle.Tls
{
    /// <summary>Exception for a fatal TLS alert received from the peer.</summary>
    [Serializable]
    public class TlsFatalAlertReceived
        : TlsException
    {
        /// <summary>The alert description.</summary>
        protected readonly byte m_alertDescription;

        /// <summary>Create a new <see cref="TlsFatalAlertReceived"/>.</summary>
        /// <param name="alertDescription">The alert description.</param>
        public TlsFatalAlertReceived(short alertDescription)
            : base(Tls.AlertDescription.GetText(alertDescription))
        {
            if (!TlsUtilities.IsValidUint8(alertDescription))
                throw new ArgumentOutOfRangeException(nameof(alertDescription));

            m_alertDescription = (byte)alertDescription;
        }

        /// <summary>Create a new <see cref="TlsFatalAlertReceived"/>.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected TlsFatalAlertReceived(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_alertDescription = info.GetByte("alertDescription");
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("alertDescription", m_alertDescription);
        }

        /// <summary>Gets the alert description.</summary>
        public virtual short AlertDescription
        {
            get { return m_alertDescription; }
        }
    }
}
