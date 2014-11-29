using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBgfx {
    public unsafe struct InstanceDataBuffer {
        internal NativeStruct* ptr;

        public IntPtr Data => ptr->data;

        public int Count => ptr->size;

        public InstanceDataBuffer (int count, int stride) {
            ptr = NativeMethods.bgfx_alloc_instance_data_buffer(count, (ushort)stride);
        }

        internal struct NativeStruct {
            public IntPtr data;
            public int size;
            public int offset;
            public ushort stride;
            public ushort num;
            public ushort handle;
        }
    }
}
