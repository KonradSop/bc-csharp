using System;
using System.IO;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
    /// <summary>General class for generating a compressed CMS message.</summary>
    public class CmsCompressedDataGenerator
    {
        /// <summary>The object identifier (as a string) of the ZLIB compression algorithm.</summary>
        public static readonly string ZLib = CmsObjectIdentifiers.ZlibCompress.Id;

        private static readonly AlgorithmIdentifier ZLibCompressionAlgorithm =
            new AlgorithmIdentifier(CmsObjectIdentifiers.ZlibCompress);

        /// <summary>Create a generator.</summary>
        public CmsCompressedDataGenerator()
        {
        }

        /// <summary>Generate an object that contains an CMS CompressedData.</summary>
        [Obsolete("Use 'Generate(CmsTypedData, string)' instead")]
        public CmsCompressedData Generate(CmsProcessable content, string compressionOid) =>
            Generate(CmsUtilities.GetTypedData(content), compressionOid);

        /// <summary>Generate an object that contains a CMS CompressedData.</summary>
        /// <param name="content">The content to compress.</param>
        /// <param name="compressionOid">The compression algorithm; only <see cref="ZLib"/> is supported.</param>
        /// <exception cref="ArgumentException">The compression algorithm is not supported.</exception>
        /// <exception cref="CmsException">The content could not be compressed.</exception>
        public CmsCompressedData Generate(CmsTypedData content, string compressionOid)
        {
            if (ZLib != compressionOid)
                throw new ArgumentException("Unsupported compression algorithm: " + compressionOid,
                    nameof(compressionOid));

            Asn1OctetString encapContent;
            try
            {
                MemoryStream bOut = new MemoryStream();
                using (var zOut = Utilities.IO.Compression.ZLib.CompressOutput(bOut, -1))
                {
                    content.Write(zOut);
                }
                encapContent = BerOctetString.WithContents(bOut.ToArray());
            }
            catch (IOException e)
            {
                throw new CmsException("exception encoding data.", e);
            }

            var encapContentInfo = new ContentInfo(content.ContentType, encapContent);

            var compressedData = new CompressedData(ZLibCompressionAlgorithm, encapContentInfo);

            var contentInfo = new ContentInfo(CmsObjectIdentifiers.CompressedData, compressedData);

            return new CmsCompressedData(contentInfo);
        }
    }
}
