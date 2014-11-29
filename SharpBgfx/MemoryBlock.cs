using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a block of memory managed by the graphics API.
    /// </summary>
    public unsafe struct MemoryBlock {
        internal DataPtr* ptr;

        /// <summary>
        /// The pointer to the raw data.
        /// </summary>
        public IntPtr Data {
            get { return ptr == null ? IntPtr.Zero : ptr->Data; }
        }

        /// <summary>
        /// The size of the block, in bytes.
        /// </summary>
        public int Size {
            get { return ptr == null ? 0 : ptr->Size; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBlock"/> struct.
        /// </summary>
        /// <param name="size">The size of the block, in bytes.</param>
        public MemoryBlock (int size) {
            ptr = NativeMethods.bgfx_alloc(size);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBlock"/> struct.
        /// </summary>
        /// <param name="data">A pointer to the initial data to copy into the new block.</param>
        /// <param name="size">The size of the block, in bytes.</param>
        public MemoryBlock (IntPtr data, int size) {
            ptr = NativeMethods.bgfx_copy(data, size);
        }

        /// <summary>
        /// Copies a managed array into a native graphics memory block.
        /// </summary>
        /// <typeparam name="T">The type of data in the array.</typeparam>
        /// <param name="data">The array to copy.</param>
        /// <returns>The native memory block containing the copied data.</returns>
        public static MemoryBlock FromArray<T> (T[] data) where T : struct {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("data");

            var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var block = new MemoryBlock(gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(typeof(T)) * data.Length);

            gcHandle.Free();
            return block;
        }

#pragma warning disable 649
        internal struct DataPtr {
            public IntPtr Data;
            public int Size;
        }
#pragma warning restore 649
    }
}
