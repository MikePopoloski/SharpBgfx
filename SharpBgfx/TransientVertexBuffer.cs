using System;

namespace SharpBgfx {
    public unsafe sealed class TransientVertexBuffer {
        internal NativeStruct tvb;

        public IntPtr Data => tvb.data;

        public int Count => tvb.size;

        public void Allocate (int count, VertexLayout layout) {
            NativeMethods.bgfx_alloc_transient_vertex_buffer(ref tvb, count, ref layout.data);
        }

        public static bool CheckAvailableSpace (int count, VertexLayout layout) {
            return NativeMethods.bgfx_check_avail_transient_vertex_buffer(count, ref layout.data);
        }

        internal struct NativeStruct {
            public IntPtr data;
            public int size;
            public int startVertex;
            public ushort stride;
            public ushort handle;
            public ushort decl;
        }
    }
}
