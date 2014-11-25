using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a dynamically updateable vertex buffer.
    /// </summary>
    public unsafe struct DynamicVertexBuffer : IDisposable {
        ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVertexBuffer"/> struct.
        /// </summary>
        /// <param name="vertexCount">The number of vertices that fit in the buffer.</param>
        /// <param name="decl">A declaration describing the layout of the vertex data.</param>
        public DynamicVertexBuffer (int vertexCount, VertexDeclaration decl) {
            handle = bgfx_create_dynamic_vertex_buffer((ushort)vertexCount, ref decl.data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVertexBuffer"/> struct.
        /// </summary>
        /// <param name="memory">The initial vertex data with which to populate the buffer.</param>
        /// <param name="decl">A declaration describing the layout of the vertex data.</param>
        public DynamicVertexBuffer (MemoryBlock memory, VertexDeclaration decl) {
            handle = bgfx_create_dynamic_vertex_buffer_mem(memory.ptr, ref decl.data);
        }

        /// <summary>
        /// Updates the data in the buffer.
        /// </summary>
        /// <param name="memory">The new vertex data with which to fill the buffer.</param>
        public void Update (MemoryBlock memory) {
            bgfx_update_dynamic_vertex_buffer(handle, memory.ptr);
        }

        /// <summary>
        /// Releases the vertex buffer.
        /// </summary>
        public void Dispose () {
            bgfx_destroy_dynamic_vertex_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_vertex_buffer (ushort indexCount, ref VertexDeclaration.Data decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_vertex_buffer_mem (MemoryBlock.DataPtr* memory, ref VertexDeclaration.Data decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_update_dynamic_vertex_buffer (ushort handle, MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_dynamic_vertex_buffer (ushort handle);
    }
}
