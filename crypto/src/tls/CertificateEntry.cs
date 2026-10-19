using System;
using System.Collections.Generic;

using Org.BouncyCastle.Tls.Crypto;

namespace Org.BouncyCastle.Tls
{
    public sealed class CertificateEntry
    {
        private readonly TlsCertificate m_certificate;
        private readonly IDictionary<int, byte[]> m_extensions;

        /// <summary>Create a new <see cref="CertificateEntry"/>.</summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="extensions">The extensions.</param>
        public CertificateEntry(TlsCertificate certificate, IDictionary<int, byte[]> extensions)
        {
            if (null == certificate)
                throw new ArgumentNullException("certificate");

            this.m_certificate = certificate;
            this.m_extensions = extensions;
        }

        public TlsCertificate Certificate
        {
            get { return m_certificate; }
        }

        /// <summary>Gets the extensions.</summary>
        public IDictionary<int, byte[]> Extensions
        {
            get { return m_extensions; }
        }
    }
}
