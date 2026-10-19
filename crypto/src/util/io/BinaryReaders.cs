using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
    /// <summary>
    /// Utility methods for reading fixed-size values from a <see cref="BinaryReader"/> in a given byte order.
    /// </summary>
    public static class BinaryReaders
    {
        internal static T Parse<T>(Func<BinaryReader, T> parse, Stream stream, bool leaveOpen)
        {
            using (var binaryReader = new BinaryReader(stream, Strings.UTF8, leaveOpen))
            {
                return parse(binaryReader);
            }
        }

        internal static T Parse<T>(Func<BinaryReader, T> parse, byte[] buf, int off, int len, string description)
        {
            using (var stream = new MemoryStream(buf, off, len, false))
            {
                T t = Parse(parse, stream, leaveOpen: true);
                if (stream.Position != stream.Length)
                    throw new InvalidDataException($"unexpected data found after {description}");
                return t;
            }
        }

        /// <summary>Read exactly the requested number of bytes from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The bytes read.</returns>
        /// <exception cref="EndOfStreamException">
        /// The stream ended before <paramref name="count"/> bytes could be read.
        /// </exception>
        public static byte[] ReadBytesFully(BinaryReader binaryReader, int count)
        {
            byte[] bytes = binaryReader.ReadBytes(count);
            if (bytes == null || bytes.Length != count)
                throw new EndOfStreamException();
            return bytes;
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static short ReadInt16BigEndian(BinaryReader binaryReader)
        {
            short n = binaryReader.ReadInt16();
            return BitConverter.IsLittleEndian ? Shorts.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static short ReadInt16LittleEndian(BinaryReader binaryReader)
        {
            short n = binaryReader.ReadInt16();
            return BitConverter.IsLittleEndian ? n : Shorts.ReverseBytes(n);
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static int ReadInt32BigEndian(BinaryReader binaryReader)
        {
            int n = binaryReader.ReadInt32();
            return BitConverter.IsLittleEndian ? Integers.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static int ReadInt32LittleEndian(BinaryReader binaryReader)
        {
            int n = binaryReader.ReadInt32();
            return BitConverter.IsLittleEndian ? n : Integers.ReverseBytes(n);
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static long ReadInt64BigEndian(BinaryReader binaryReader)
        {
            long n = binaryReader.ReadInt64();
            return BitConverter.IsLittleEndian ? Longs.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        public static long ReadInt64LittleEndian(BinaryReader binaryReader)
        {
            long n = binaryReader.ReadInt64();
            return BitConverter.IsLittleEndian ? n : Longs.ReverseBytes(n);
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static ushort ReadUInt16BigEndian(BinaryReader binaryReader)
        {
            ushort n = binaryReader.ReadUInt16();
            return BitConverter.IsLittleEndian ? Shorts.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static ushort ReadUInt16LittleEndian(BinaryReader binaryReader)
        {
            ushort n = binaryReader.ReadUInt16();
            return BitConverter.IsLittleEndian ? n : Shorts.ReverseBytes(n);
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static uint ReadUInt32BigEndian(BinaryReader binaryReader)
        {
            uint n = binaryReader.ReadUInt32();
            return BitConverter.IsLittleEndian ? Integers.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static uint ReadUInt32LittleEndian(BinaryReader binaryReader)
        {
            uint n = binaryReader.ReadUInt32();
            return BitConverter.IsLittleEndian ? n : Integers.ReverseBytes(n);
        }

        /// <summary>Read a big-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static ulong ReadUInt64BigEndian(BinaryReader binaryReader)
        {
            ulong n = binaryReader.ReadUInt64();
            return BitConverter.IsLittleEndian ? Longs.ReverseBytes(n) : n;
        }

        /// <summary>Read a little-endian value from a reader.</summary>
        /// <param name="binaryReader">The reader.</param>
        /// <returns>The value read.</returns>
        [CLSCompliant(false)]
        public static ulong ReadUInt64LittleEndian(BinaryReader binaryReader)
        {
            ulong n = binaryReader.ReadUInt64();
            return BitConverter.IsLittleEndian ? n : Longs.ReverseBytes(n);
        }
    }
}
