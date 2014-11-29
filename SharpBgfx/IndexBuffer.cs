using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a static index buffer.
    /// </summary>
    /// <remarks>Indices are always 16-bits.</remarks>
    public unsafe struct IndexBuffer : IDisposable {
        internal ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexBuffer"/> struct.
        /// </summary>
        /// <param name="memory">The 16-bit index data used to populate the buffer.</param>
        public IndexBuffer (MemoryBlock memory) {
            handle = NativeMethods.bgfx_create_index_buffer(memory.ptr);
        }

        /// <summary>
        /// Releases the index buffer.
        /// </summary>
        public void Dispose () {
            NativeMethods.bgfx_destroy_index_buffer(handle);
        }
    }
}
