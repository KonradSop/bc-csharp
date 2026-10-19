using System;

namespace Org.BouncyCastle.Tls
{
    /// <summary>Constants for the certificate status types of RFC 6066.</summary>
    public abstract class CertificateStatusType
    {
        /*
         *  RFC 6066
         */
        public const short ocsp = 1;

        /*
         *  RFC 6961
         */
        public const short ocsp_multi = 2;
    }
}
