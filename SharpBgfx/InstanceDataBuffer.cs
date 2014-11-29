using System;

namespace SharpBgfx {
    public unsafe struct InstanceDataBuffer {
        internal NativeStruct* ptr;

        public IntPtr Data => ptr->data;

        public int Count => ptr->size;

        public InstanceDataBuffer (int count, int stride) {
            ptr = NativeMethods.bgfx_alloc_instance_data_buffer(count, (ushort)stride);
        }

        public static bool CheckAvailableSpace (int count, int stride) {
            return NativeMethods.bgfx_check_avail_instance_data_buffer(count, (ushort)stride);
        }

#pragma warning disable 649
        internal struct NativeStruct {
            public IntPtr data;
            public int size;
            public int offset;
            public ushort stride;
            public ushort num;
            public ushort handle;
        }
#pragma warning restore 649
    }
}
