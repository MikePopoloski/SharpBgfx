using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct DynamicIndexBuffer : IDisposable {
        ushort handle;

        public DynamicIndexBuffer (int indexCount) {
            handle = bgfx_create_dynamic_index_buffer(indexCount);
        }

        public DynamicIndexBuffer (MemoryBlock memory) {
            handle = bgfx_create_dynamic_index_buffer_mem(memory.ptr);
        }

        public void Update (MemoryBlock memory) {
            bgfx_update_dynamic_index_buffer(handle, memory.ptr);
        }

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
        public static extern void bgfx_destroy_dynamic_index_buffer (ushort handle);
    }
}
