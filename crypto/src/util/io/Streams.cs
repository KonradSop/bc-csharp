using System;
using System.IO;
#if NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
using System.Runtime.InteropServices;
#endif
#if NETCOREAPP1_0_OR_GREATER || NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Org.BouncyCastle.Utilities.IO
{
    /// <summary>Utility methods for working with streams.</summary>
    public static class Streams
    {
        private static readonly int MaxStackAlloc = Platform.Is64BitProcess ? 4096 : 1024;

        /// <summary>Gets the default buffer size.</summary>
        public static int DefaultBufferSize => MaxStackAlloc;

        /// <summary>Read a stream to its end, discarding the data.</summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyTo(Stream source, Stream destination) => CopyTo(source, destination, DefaultBufferSize);

        /// <summary>Read a stream to its end, discarding the data.</summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="bufferSize">The size of the buffer used for reading.</param>
        public static void CopyTo(Stream source, Stream destination, int bufferSize)
        {
            int bytesRead;
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<byte> buffer = bufferSize <= MaxStackAlloc
                ? stackalloc byte[bufferSize]
                : new byte[bufferSize];
            while ((bytesRead = source.Read(buffer)) != 0)
            {
                destination.Write(buffer[..bytesRead]);
            }
#else
            byte[] buffer = new byte[bufferSize];
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                destination.Write(buffer, 0, bytesRead);
            }
#endif
        }

#if NETCOREAPP1_0_OR_GREATER || NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER
        /// <summary>Asynchronously copy all the data from one stream to another.</summary>
        /// <param name="source">The stream to read from.</param>
        /// <param name="destination">The stream to write to.</param>
        /// <returns>A task representing the copy operation.</returns>
        public static Task CopyToAsync(Stream source, Stream destination) =>
            CopyToAsync(source, destination, DefaultBufferSize);

        /// <summary>Asynchronously copy all the data from one stream to another.</summary>
        /// <param name="source">The stream to read from.</param>
        /// <param name="destination">The stream to write to.</param>
        /// <param name="bufferSize">The size of the copy buffer.</param>
        /// <returns>A task representing the copy operation.</returns>
        public static Task CopyToAsync(Stream source, Stream destination, int bufferSize) =>
            CopyToAsync(source, destination, bufferSize, CancellationToken.None);

        /// <summary>Asynchronously copy all the data from one stream to another.</summary>
        /// <param name="source">The stream to read from.</param>
        /// <param name="destination">The stream to write to.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>A task representing the copy operation.</returns>
        public static Task CopyToAsync(Stream source, Stream destination, CancellationToken cancellationToken) =>
            CopyToAsync(source, destination, DefaultBufferSize, cancellationToken);

        /// <summary>Asynchronously copy all the data from one stream to another.</summary>
        /// <param name="source">The stream to read from.</param>
        /// <param name="destination">The stream to write to.</param>
        /// <param name="bufferSize">The size of the copy buffer.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>A task representing the copy operation.</returns>
        public static async Task CopyToAsync(Stream source, Stream destination, int bufferSize,
            CancellationToken cancellationToken)
        {
            int bytesRead;
            byte[] buffer = new byte[bufferSize];
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            while ((bytesRead = await ReadAsync(source, new Memory<byte>(buffer), cancellationToken)
                .ConfigureAwait(false)) != 0)
            {
                await WriteAsync(destination, new ReadOnlyMemory<byte>(buffer, 0, bytesRead), cancellationToken)
                    .ConfigureAwait(false);
            }
#else
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken)
                .ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
            }
#endif
        }
#endif

        /// <summary>Read a stream to its end, discarding the data.</summary>
        /// <param name="inStr">The stream to drain.</param>
        public static void Drain(Stream inStr) => Drain(inStr, DefaultBufferSize);

        /// <summary>Read a stream to its end, discarding the data.</summary>
        /// <param name="inStr">The stream to drain.</param>
        /// <param name="bufferSize">The size of the buffer used for reading.</param>
        public static void Drain(Stream inStr, int bufferSize) => CopyTo(inStr, Stream.Null, bufferSize);

        /// <summary>Write the full contents of inStr to the destination stream outStr.</summary>
        /// <param name="inStr">Source stream.</param>
        /// <param name="outStr">Destination stream.</param>
        /// <exception cref="IOException">In case of IO failure.</exception>
        public static void PipeAll(Stream inStr, Stream outStr) => PipeAll(inStr, outStr, DefaultBufferSize);

        /// <summary>Write the full contents of inStr to the destination stream outStr.</summary>
        /// <param name="inStr">Source stream.</param>
        /// <param name="outStr">Destination stream.</param>
        /// <param name="bufferSize">The size of temporary buffer to use.</param>
        /// <exception cref="IOException">In case of IO failure.</exception>
        public static void PipeAll(Stream inStr, Stream outStr, int bufferSize) => CopyTo(inStr, outStr, bufferSize);

        /// <summary>
        /// Pipe all bytes from <c>inStr</c> to <c>outStr</c>, throwing <c>StreamFlowException</c> if greater
        /// than <c>limit</c> bytes in <c>inStr</c>.
        /// </summary>
        /// <param name="inStr">
        /// A <see cref="Stream"/>
        /// </param>
        /// <param name="limit">
        /// A <see cref="System.Int64"/>
        /// </param>
        /// <param name="outStr">
        /// A <see cref="Stream"/>
        /// </param>
        /// <returns>The number of bytes actually transferred, if not greater than <c>limit</c></returns>
        /// <exception cref="IOException"></exception>
        public static long PipeAllLimited(Stream inStr, long limit, Stream outStr) =>
            PipeAllLimited(inStr, limit, outStr, DefaultBufferSize);

        /// <summary>
        /// Pipe all bytes from <c>inStr</c> to <c>outStr</c>, throwing <c>StreamFlowException</c> if greater than
        /// <c>limit</c> bytes in <c>inStr</c>.
        /// </summary>
        /// <param name="inStr"> A <see cref="Stream"/> </param>
        /// <param name="limit"> A <see cref="System.Int64"/> </param>
        /// <param name="outStr"> A <see cref="Stream"/> </param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <returns>The number of bytes actually transferred, if not greater than <c>limit</c></returns>
        /// <exception cref="IOException"></exception>
        public static long PipeAllLimited(Stream inStr, long limit, Stream outStr, int bufferSize)
        {
            using (var limited = new LimitedInputStream(limit, inStr, leaveOpen: true))
            {
                CopyTo(limited, outStr, bufferSize);
                return limit - limited.CurrentLimit;
            }
        }

        /// <summary>Read a stream to its end and return the data.</summary>
        /// <param name="inStr">The stream to read.</param>
        /// <returns>The data read.</returns>
        public static byte[] ReadAll(Stream inStr) => ReadAll(inStr, DefaultBufferSize);

        /// <summary>Read a stream to its end and return the data.</summary>
        /// <param name="inStr">The stream to read.</param>
        /// <param name="bufferSize">The size of the buffer used for reading.</param>
        /// <returns>The data read.</returns>
        public static byte[] ReadAll(Stream inStr, int bufferSize)
        {
            MemoryStream buf = new MemoryStream();
            using (buf)
            {
                CopyTo(inStr, buf, bufferSize);
            }
            return buf.ToArray();
        }

        /// <summary>Read a stream to its end and return the data.</summary>
        /// <param name="inStr">The stream to read.</param>
        /// <returns>The data read.</returns>
        [Obsolete("Will be removed")]
        public static byte[] ReadAll(MemoryStream inStr) => inStr.ToArray();

        /// <summary>
        /// Read a stream to its end and return the data, failing if more than a limited amount is available.
        /// </summary>
        /// <param name="inStr">The stream to read.</param>
        /// <param name="limit">The maximum number of bytes to accept.</param>
        /// <returns>The data read.</returns>
        /// <exception cref="StreamOverflowException">
        /// The stream holds more than <paramref name="limit"/> bytes.
        /// </exception>
        public static byte[] ReadAllLimited(Stream inStr, int limit) => ReadAllLimited(inStr, limit, DefaultBufferSize);

        /// <summary>
        /// Read a stream to its end and return the data, failing if more than a limited amount is available.
        /// </summary>
        /// <param name="inStr">The stream to read.</param>
        /// <param name="limit">The maximum number of bytes to accept.</param>
        /// <param name="bufferSize">The size of the buffer used for reading.</param>
        /// <returns>The data read.</returns>
        /// <exception cref="StreamOverflowException">
        /// The stream holds more than <paramref name="limit"/> bytes.
        /// </exception>
        public static byte[] ReadAllLimited(Stream inStr, int limit, int bufferSize)
        {
            MemoryStream buf = new MemoryStream();
            using (buf)
            {
                PipeAllLimited(inStr, limit, buf, bufferSize);
            }
            return buf.ToArray();
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>Asynchronously read bytes from a stream into a memory region.</summary>
        /// <param name="source">The stream to read from.</param>
        /// <param name="buffer">The region of memory to read into.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>The number of bytes read, or zero if the end of the stream has been reached.</returns>
        public static ValueTask<int> ReadAsync(Stream source, Memory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            if (MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> array))
            {
                return new ValueTask<int>(
                    source.ReadAsync(array.Array!, array.Offset, array.Count, cancellationToken));
            }

            byte[] sharedBuffer = new byte[buffer.Length];
            var readTask = source.ReadAsync(sharedBuffer, 0, buffer.Length, cancellationToken);
            return ReadAsyncCompletion(readTask, sharedBuffer, buffer);
        }

        internal static async ValueTask<int> ReadAsyncCompletion(Task<int> readTask, byte[] localBuffer,
            Memory<byte> localDestination)
        {
            try
            {
                int result = await readTask.ConfigureAwait(false);
                new ReadOnlySpan<byte>(localBuffer, 0, result).CopyTo(localDestination.Span);
                return result;
            }
            finally
            {
                Array.Clear(localBuffer, 0, localBuffer.Length);
            }
        }
#endif

        /// <summary>
        /// Read from a stream until the buffer (or the given range of it) is full or the end of the stream is reached.
        /// </summary>
        /// <param name="inStr">The stream to read from.</param>
        /// <param name="buf">The buffer to fill.</param>
        /// <returns>
        /// The number of bytes read, which is less than requested only if the end of the stream was reached.
        /// </returns>
        public static int ReadFully(Stream inStr, byte[] buf) => ReadFully(inStr, buf, 0, buf.Length);

        /// <summary>
        /// Read from a stream until the buffer (or the given range of it) is full or the end of the stream is reached.
        /// </summary>
        /// <param name="inStr">The stream to read from.</param>
        /// <param name="buf">The buffer to fill.</param>
        /// <param name="off">The offset into <paramref name="buf"/> at which to start writing.</param>
        /// <param name="len">The maximum number of bytes to read.</param>
        /// <returns>
        /// The number of bytes read, which is less than requested only if the end of the stream was reached.
        /// </returns>
        public static int ReadFully(Stream inStr, byte[] buf, int off, int len)
        {
            int totalRead = 0;
            while (totalRead < len)
            {
                int numRead = inStr.Read(buf, off + totalRead, len - totalRead);
                if (numRead < 1)
                    break;
                totalRead += numRead;
            }
            return totalRead;
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// Read from a stream until the buffer (or the given range of it) is full or the end of the stream is reached.
        /// </summary>
        /// <param name="inStr">The stream to read from.</param>
        /// <param name="buffer">The buffer to fill.</param>
        /// <returns>
        /// The number of bytes read, which is less than requested only if the end of the stream was reached.
        /// </returns>
        public static int ReadFully(Stream inStr, Span<byte> buffer)
        {
            int totalRead = 0;
            while (totalRead < buffer.Length)
            {
                int numRead = inStr.Read(buffer[totalRead..]);
                if (numRead < 1)
                    break;
                totalRead += numRead;
            }
            return totalRead;
        }
#endif

        /// <summary>Try to determine how many bytes remain to be read from a stream.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="available">On success, the number of bytes available.</param>
        /// <returns>true if the number of bytes could be determined, false otherwise.</returns>
        public static bool TryGetAvailable(Stream stream, out long available)
        {
            try
            {
                if (stream.CanSeek)
                {
                    available = System.Math.Max(0L, stream.Length - stream.Position);
                    return true;
                }
            }
            catch (Exception)
            {
                // Ignore; this method is best-effort only
            }

            available = default;
            return false;
        }

        /// <summary>
        /// Read exactly <paramref name="exactLength"/> bytes from <paramref name="stream"/>, allocated incrementally.
        /// </summary>
        /// <remarks>
        /// The resulting <paramref name="bytes"/> array (if any) is grown incrementally as data arrives rather than
        /// allocated at the full length up front. A caller passing an untrusted (possibly hostile) length therefore
        /// cannot drive an extremely large allocation from a short input.
        /// </remarks>
        internal static bool TryReadExactIncremental(Stream stream, int exactLength, out byte[] bytes)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (exactLength < 0)
                throw new ArgumentOutOfRangeException("cannot be negative", nameof(exactLength));
            if (exactLength > Arrays.MaxLength)
                throw new ArgumentOutOfRangeException("exceeds maximum length for an array", nameof(exactLength));

            int initialAlloc = exactLength;
            while (initialAlloc > DefaultBufferSize)
            {
                initialAlloc = (int)(((uint)initialAlloc + 3U) >> 2);
            }

            byte[] buf = new byte[initialAlloc];
            int totalRead = 0;
            while (totalRead < exactLength)
            {
                if (totalRead == buf.Length)
                {
                    int expandedAlloc = (int)System.Math.Min(exactLength, 4L * buf.Length);
                    buf = Arrays.CopyOf(buf, expandedAlloc);
                }

                int numRead = stream.Read(buf, totalRead, buf.Length - totalRead);
                if (numRead < 1)
                {
                    bytes = default;
                    return false;
                }

                totalRead += numRead;
            }

            bytes = buf;
            return true;
        }

#if NETSTANDARD1_0_OR_GREATER || NETCOREAPP1_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        /// <summary>Validate the buffer arguments of a read or write operation.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset into the buffer.</param>
        /// <param name="count">The number of bytes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="count"/> is out of range.
        /// </exception>
        public static void ValidateBufferArguments(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if ((uint)count > buffer.Length - offset)
                throw new ArgumentOutOfRangeException(nameof(count));
        }

#if NETCOREAPP1_0_OR_GREATER || NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER
        internal static async Task WriteAsyncCompletion(Task writeTask, byte[] localBuffer)
        {
            try
            {
                await writeTask.ConfigureAwait(false);
            }
            finally
            {
                Array.Clear(localBuffer, 0, localBuffer.Length);
            }
        }

        internal static Task WriteAsyncDirect(Stream destination, byte[] buffer, int offset, int count,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            destination.Write(buffer, offset, count);
            return Task.CompletedTask;
        }
#endif

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>Asynchronously write a region of memory to a stream.</summary>
        /// <param name="destination">The stream to write to.</param>
        /// <param name="buffer">The region of memory to write.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>A task representing the write operation.</returns>
        public static ValueTask WriteAsync(Stream destination, ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            if (MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> array))
            {
                return new ValueTask(
                    destination.WriteAsync(array.Array!, array.Offset, array.Count, cancellationToken));
            }

            byte[] sharedBuffer = buffer.ToArray();
            var writeTask = destination.WriteAsync(sharedBuffer, 0, buffer.Length, cancellationToken);
            return new ValueTask(WriteAsyncCompletion(writeTask, sharedBuffer));
        }

        internal static async ValueTask WriteAsyncCompletion(ValueTask writeTask, byte[] localBuffer)
        {
            try
            {
                await writeTask.ConfigureAwait(false);
            }
            finally
            {
                Array.Clear(localBuffer, 0, localBuffer.Length);
            }
        }

        internal static ValueTask WriteAsyncDirect(Stream destination, ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return ValueTask.FromCanceled(cancellationToken);

            destination.Write(buffer.Span);
            return ValueTask.CompletedTask;
        }
#endif

        /// <exception cref="IOException"></exception>
        public static int WriteBufTo(MemoryStream buf, byte[] output, int offset)
        {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            if (buf.TryGetBuffer(out var buffer))
            {
                buffer.CopyTo(output, offset);
                return buffer.Count;
            }
#endif

            int size = Convert.ToInt32(buf.Length);
            using (var segment = new MemoryStream(output, offset, size))
            {
                buf.WriteTo(segment);
            }
            return size;
        }
    }
}
