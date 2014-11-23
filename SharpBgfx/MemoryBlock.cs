using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct MemoryBlock {
        internal DataPtr* ptr;

        public IntPtr Data {
            get { return ptr == null ? IntPtr.Zero : ptr->Data; }
        }

        public int Size {
            get { return ptr == null ? 0 : ptr->Size; }
        }

        public MemoryBlock (int size) {
            ptr = bgfx_alloc(size);
        }

        public MemoryBlock (IntPtr data, int size) {
            ptr = bgfx_copy(data, size);
        }

        public static MemoryBlock FromArray<T> (T[] data) where T : struct {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("data");

            var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var block = new MemoryBlock(gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(typeof(T)) * data.Length);

            gcHandle.Free();
            return block;
        }

        internal struct DataPtr {
            public IntPtr Data;
            public int Size;
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern DataPtr* bgfx_alloc (int size);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern DataPtr* bgfx_copy (IntPtr data, int size);
    }
}
