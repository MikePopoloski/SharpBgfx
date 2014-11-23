using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct VertexBuffer : IDisposable {
        internal ushort handle;

        public VertexBuffer (MemoryBlock memory, VertexDeclaration decl) {
            handle = bgfx_create_vertex_buffer(memory.ptr, ref decl.data);
        }

        public void Dispose () {
            bgfx_destroy_vertex_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_vertex_buffer (MemoryBlock.DataPtr* memory, ref VertexDeclaration.Data decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_vertex_buffer (ushort handle);
    }
}
