using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a dynamically updateable index buffer.
    /// </summary>
    /// <remarks>Indices are always 16-bits.</remarks>
    public unsafe struct DynamicIndexBuffer : IDisposable {
        internal ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicIndexBuffer"/> struct.
        /// </summary>
        /// <param name="indexCount">The number of indices that can fit in the buffer.</param>
        public DynamicIndexBuffer (int indexCount) {
            handle = bgfx_create_dynamic_index_buffer(indexCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicIndexBuffer"/> struct.
        /// </summary>
        /// <param name="memory">The initial index data with which to populate the buffer.</param>
        public DynamicIndexBuffer (MemoryBlock memory) {
            handle = bgfx_create_dynamic_index_buffer_mem(memory.ptr);
        }

        /// <summary>
        /// Updates the data in the buffer.
        /// </summary>
        /// <param name="memory">The new index data with which to fill the buffer.</param>
        public void Update (MemoryBlock memory) {
            bgfx_update_dynamic_index_buffer(handle, memory.ptr);
        }

        /// <summary>
        /// Releases the index buffer.
        /// </summary>
        public void Dispose () {
            bgfx_destroy_dynamic_index_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_index_buffer (int indexCount);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_index_buffer_mem (MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_update_dynamic_index_buffer (ushort handle, MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_destroy_dynamic_index_buffer (ushort handle);
    }
}
