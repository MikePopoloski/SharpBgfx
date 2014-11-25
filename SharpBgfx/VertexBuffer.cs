using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a static vertex buffer.
    /// </summary>
    public unsafe struct VertexBuffer : IDisposable {
        internal ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBuffer"/> struct.
        /// </summary>
        /// <param name="memory">The vertex data with which to populate the buffer.</param>
        /// <param name="decl">A declaration describing the layout of the vertex data.</param>
        public VertexBuffer (MemoryBlock memory, VertexDeclaration decl) {
            handle = bgfx_create_vertex_buffer(memory.ptr, ref decl.data);
        }

        /// <summary>
        /// Releases the vertex buffer.
        /// </summary>
        public void Dispose () {
            bgfx_destroy_vertex_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_vertex_buffer (MemoryBlock.DataPtr* memory, ref VertexDeclaration.Data decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_destroy_vertex_buffer (ushort handle);
    }
}
