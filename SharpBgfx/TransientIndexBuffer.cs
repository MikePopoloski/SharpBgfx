using System;

namespace SharpBgfx {
    public unsafe sealed class TransientIndexBuffer {
        internal NativeStruct tib;

        public IntPtr Data => tib.data;

        public int Count => tib.size;

        public void Allocate (int count) {
            NativeMethods.bgfx_alloc_transient_index_buffer(ref tib, count);
        }

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
