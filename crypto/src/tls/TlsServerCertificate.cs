using System;

namespace Org.BouncyCastle.Tls
{
    /// <summary>Server certificate carrier interface.</summary>
    // TODO[api] Turn this into a concrete sealed class (record)
    public interface TlsServerCertificate
    {
        Certificate Certificate { get; }

        CertificateStatus CertificateStatus { get; }
    }
}
