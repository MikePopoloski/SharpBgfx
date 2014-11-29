using System;

namespace SharpBgfx {
    /// <summary>
    /// Maintains a transient index buffer.
    /// </summary>
    /// <remarks>
    /// The contents of the buffer are valid for the current frame only.
    /// You must call Allocate() anew on each frame.
    /// </remarks>
    public unsafe sealed class TransientIndexBuffer {
        internal NativeStruct tib;

        /// <summary>
        /// A pointer that can be filled with index data.
        /// </summary>
        public IntPtr Data => tib.data;

        /// <summary>
        /// The size of the buffer.
        /// </summary>
        public int Count => tib.size;

        /// <summary>
        /// Allocates space in the buffer.
        /// </summary>
        /// <param name="count">The number of 16-bit indices for which to make room.</param>
        public void Allocate (int count) {
            NativeMethods.bgfx_alloc_transient_index_buffer(ref tib, count);
        }

        /// <summary>
        /// Check if there is available space in the global transient index buffer.
        /// </summary>
        /// <param name="count">The number of 16-bit indices to allocate.</param>
        /// <returns><c>true</c> if there is sufficient space for the give number of indices.</returns>
        public static bool CheckAvailableSpace (int count) {
            return NativeMethods.bgfx_check_avail_transient_index_buffer(count);
        }

        internal struct NativeStruct {
            public IntPtr data;
            public int size;
            public ushort handle;
            public int startIndex;
        }
    }
}
