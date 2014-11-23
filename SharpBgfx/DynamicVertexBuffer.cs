using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct DynamicVertexBuffer : IDisposable {
        ushort handle;

        public DynamicVertexBuffer (int vertexCount, ref VertexDeclaration decl) {
            handle = bgfx_create_dynamic_vertex_buffer((ushort)vertexCount, ref decl);
        }

        public DynamicVertexBuffer (MemoryBlock memory, ref VertexDeclaration decl) {
            handle = bgfx_create_dynamic_vertex_buffer_mem(memory.ptr, ref decl);
        }

        public void Update (MemoryBlock memory) {
            bgfx_update_dynamic_vertex_buffer(handle, memory.ptr);
        }

        public void Dispose () {
            bgfx_destroy_dynamic_vertex_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_vertex_buffer (ushort indexCount, ref VertexDeclaration decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_dynamic_vertex_buffer_mem (MemoryBlock.DataPtr* memory, ref VertexDeclaration decl);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_update_dynamic_vertex_buffer (ushort handle, MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_dynamic_vertex_buffer (ushort handle);
    }
}
