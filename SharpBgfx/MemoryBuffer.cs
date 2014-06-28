using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe sealed class MemoryBuffer {
        GraphicsMemory* mem;
        byte* writePtr;

        internal GraphicsMemory* Native {
            get { return mem; }
        }

        public int Length {
            get { return mem->Size; }
        }

        public MemoryBuffer (int sizeInBytes)
            : this(Bgfx.Alloc(sizeInBytes)) {
        }

        MemoryBuffer (GraphicsMemory* mem) {
            this.mem = mem;
        }

        public static MemoryBuffer FromArray<T>(T[] array) where T : struct {
            if (array == null || array.Length == 0)
                throw new ArgumentNullException("array");

            var gcHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            var memoryHandle = Bgfx.Copy(gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(typeof(T)) * array.Length);

            gcHandle.Free();
            return new MemoryBuffer(memoryHandle);
        }

        /// <summary>
        /// Writes a value into the buffer.
        /// </summary>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        /// <param name="value">The value to write.</param>
        public void Write<T>(ref T value) where T : struct {
            var size = RewriteStubs.SizeOfInline<T>();
            CheckBounds(size);

            RewriteStubs.WriteInline(writePtr, ref value);
            writePtr += size;
        }

        /// <summary>
        /// Writes a value into the buffer.
        /// </summary>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        /// <param name="value">The value to write.</param>
        public void Write<T>(T value) where T : struct {
            var size = RewriteStubs.SizeOfInline<T>();
            CheckBounds(size);

            RewriteStubs.WriteInline(writePtr, ref value);
            writePtr += size;
        }

        /// <summary>
        /// Writes a range of values into the buffer.
        /// </summary>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        /// <param name="values">The range of values to write.</param>
        public void WriteRange<T>(params T[] values) where T : struct {
            if (values == null)
                return;

            WriteRange(values, 0, values.Length);
        }

        /// <summary>
        /// Writes a range of values into the buffer.
        /// </summary>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        /// <param name="values">The range of values to write.</param>
        /// <param name="startOffset">The start offset in the array to begin writing.</param>
        /// <param name="count">The number of elements to write.</param>
        public void WriteRange<T>(T[] values, int startOffset, int count) where T : struct {
            var size = RewriteStubs.SizeOfInline<T>() * count;
            CheckBounds(size);

            WriteHelper(new IntPtr(writePtr), values, startOffset, count);
            writePtr += size;
        }

        [Conditional("DEBUG")]
        void CheckBounds (int bytes) {
            if (writePtr + bytes > mem->Data + Length)
                throw new InvalidOperationException("Tried to write past the end of the buffer.");
        }

        // this method is a helper wrapper around the RewriteStubs method, which will
        // get replaced inline once the ILRewriter runs on over the assembly
        static void WriteHelper<T>(IntPtr dest, T[] data, int startIndex, int count) where T : struct {
            RewriteStubs.WriteArray(dest, data, startIndex, count);
        }
    }
}
