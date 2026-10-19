using System;
using System.IO;

using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Pqc.Crypto.Lms
{
    /**
    * Type to assist in build LMS messages.
    */
    public sealed class Composer
    {
        //Todo make sure MemoryStream works properly (not sure about byte arrays as inputs)
        private readonly MemoryStream bos = new MemoryStream();

        private Composer()
        {
        }

        /// <summary>Start composing a message.</summary>
        /// <returns>A new composer.</returns>
        public static Composer Compose()
        {
            return new Composer();
        }

        /// <summary>Append an unsigned 64-bit value in big-endian form.</summary>
        /// <param name="n">The value.</param>
        /// <returns>This composer.</returns>
        public Composer U64Str(long n)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<byte> buf = stackalloc byte[8];
            Pack.UInt64_To_BE((ulong)n, buf);
            bos.Write(buf);
#else
            U32Str((int)(n >> 32));
            U32Str((int)n);
#endif
            return this;
        }

        /// <summary>Append an unsigned 32-bit value in big-endian form.</summary>
        /// <param name="n">The value.</param>
        /// <returns>This composer.</returns>
        public Composer U32Str(int n)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<byte> buf = stackalloc byte[4];
            Pack.UInt32_To_BE((uint)n, buf);
            bos.Write(buf);
#else
            bos.WriteByte((byte)(n >> 24));
            bos.WriteByte((byte)(n >> 16));
            bos.WriteByte((byte)(n >> 8));
            bos.WriteByte((byte)n);
#endif
            return this;
        }

        /// <summary>Append an unsigned 16-bit value in big-endian form.</summary>
        /// <param name="n">The value.</param>
        /// <returns>This composer.</returns>
        public Composer U16Str(int n)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<byte> buf = stackalloc byte[2];
            Pack.UInt16_To_BE((ushort)n, buf);
            bos.Write(buf);
#else
            n &= 0xFFFF;
            bos.WriteByte((byte)(n >> 8));
            bos.WriteByte((byte)n);
#endif
            return this;
        }

        /// <summary>Append the encodings of several objects.</summary>
        /// <param name="encodable">The objects.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes(IEncodable[] encodable)
        {
            foreach (var e in encodable)
            {
                byte[] encoding = e.GetEncoded();
                bos.Write(encoding, 0, encoding.Length);// todo count?
            }
            return this;
        }

        /// <summary>Append the encoding of an object.</summary>
        /// <param name="encodable">The object.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes(IEncodable encodable)
        {
            byte[] encoding = encodable.GetEncoded();
            bos.Write(encoding, 0, encoding.Length);
            return this;
        }

        /// <summary>Append zero bytes.</summary>
        /// <param name="v">The value of the padding bytes.</param>
        /// <param name="len">The number of bytes.</param>
        /// <returns>This composer.</returns>
        public Composer Pad(int v, int len)
        {
            for (; len >= 0; len--)
            {
                bos.WriteByte((byte)v);
            }
            return this;
        }

        /// <summary>Append several arrays.</summary>
        /// <param name="arrays">The arrays.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes2(byte[][] arrays)
        {
            foreach (byte[] array in arrays)
            {
                bos.Write(array, 0, array.Length); //todo count?
            }
            return this;
        }

        /// <summary>Append a range of an array of arrays.</summary>
        /// <param name="arrays">The arrays.</param>
        /// <param name="start">The index of the first array to append.</param>
        /// <param name="end">The index after the last array to append.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes2(byte[][] arrays, int start, int end)
        {
            int j = start;
            while (j != end)
            {
                bos.Write(arrays[j], 0, arrays[j].Length);//todo count?
                j++;
            }
            return this;
        }

        /// <summary>Append an array.</summary>
        /// <param name="array">The array.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes(byte[] array)
        {
            bos.Write(array, 0, array.Length);//todo count?
            return this;
        }

        /// <summary>Append a range of an array.</summary>
        /// <param name="array">The array.</param>
        /// <param name="start">The index of the first byte.</param>
        /// <param name="len">The number of bytes.</param>
        /// <returns>This composer.</returns>
        public Composer Bytes(byte[] array, int start, int len)
        {
            bos.Write(array, start, len);
            return this;
        }

        /// <summary>Get the composed message.</summary>
        /// <returns>The message.</returns>
        public byte[] Build()
        {
            return bos.ToArray();
        }

        /// <summary>Append padding until the message has the required length.</summary>
        /// <param name="v">The value of the padding bytes.</param>
        /// <param name="requiredLen">The required length.</param>
        /// <returns>This composer.</returns>
        public Composer PadUntil(int v, int requiredLen)
        {
            while (bos.Length < requiredLen)
            {
                bos.WriteByte((byte)v);
            }
            return this;
        }

        /// <summary>Append a boolean as one byte.</summary>
        /// <param name="v">The value.</param>
        /// <returns>This composer.</returns>
        public Composer Boolean(bool v)
        {
            bos.WriteByte((byte)(v ? 1 : 0));
            return this;
        }
    }
}
