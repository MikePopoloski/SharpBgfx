using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct IndexBuffer : IDisposable {
        internal ushort handle;

        public IndexBuffer (MemoryBlock memory) {
            handle = bgfx_create_index_buffer(memory.ptr);
        }

        public void Dispose () {
            bgfx_destroy_index_buffer(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_index_buffer (MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_index_buffer (ushort handle);
    }
}
