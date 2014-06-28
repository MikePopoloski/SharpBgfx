using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe sealed class ByteStream : IDisposable {
        GCHandle handle;
        byte* readPtr;
        byte* endPtr;

        public int RemainingBytes {
            get { return (int)(endPtr - readPtr); }
        }

        public ByteStream (byte[] array) {
            handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            readPtr = (byte*)handle.AddrOfPinnedObject();
            endPtr = readPtr + array.Length;
        }

        public void Dispose () {
            handle.Free();
            readPtr = null;
        }

        public void Skip (int count) {
            CheckBounds(count);
            readPtr += count;
        }

        /// <summary>
        /// Reads memory from the stream.
        /// </summary>
        /// <typeparam name="T">The type of data to read.</typeparam>
        /// <returns>The read data.</returns>
        public T Read<T>() where T : struct {
            var size = RewriteStubs.SizeOfInline<T>();
            CheckBounds(size);

            var result = RewriteStubs.ReadInline<T>(readPtr);
            readPtr += size;

            return result;
        }

        /// <summary>
        /// Reads a range of memory from the stream.
        /// </summary>
        /// <typeparam name="T">The type of data to read.</typeparam>
        /// <param name="count">The number of elements to read.</param>
        /// <returns>The read data.</returns>
        public T[] ReadRange<T>(int count) where T : struct {
            var dest = new T[count];
            ReadRange(dest, 0, count);

            return dest;
        }

        /// <summary>
        /// Reads a range of memory from the stream.
        /// </summary>
        /// <typeparam name="T">The type of data to read.</typeparam>
        /// <param name="destination">The destination array.</param>
        /// <param name="startIndex">The start index in which to start copying data.</param>
        /// <param name="count">The number of elements to read.</param>
        public void ReadRange<T>(T[] destination, int startIndex, int count) where T : struct {
            var size = RewriteStubs.SizeOfInline<T>() * count;
            CheckBounds(size);

            ReadHelper(new IntPtr(readPtr), destination, startIndex, count);
            readPtr += size;
        }

        [Conditional("DEBUG")]
        void CheckBounds (int bytes) {
            if (bytes > RemainingBytes)
                throw new InvalidOperationException("Tried to read past the end of the buffer.");
        }

        // this method is a helper wrapper around the RewriteStubs method, which will
        // get replaced inline once the ILRewriter runs on over the assembly
        static void ReadHelper<T>(IntPtr src, T[] data, int startIndex, int count) where T : struct {
            RewriteStubs.ReadArray(src, data, startIndex, count);
        }
    }
}
