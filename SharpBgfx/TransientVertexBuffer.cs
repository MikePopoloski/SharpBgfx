using System;

namespace SharpBgfx {
    /// <summary>
    /// Maintains a transient vertex buffer.
    /// </summary>
    /// <remarks>
    /// The contents of the buffer are valid for the current frame only.
    /// You must call Allocate() anew on each frame.
    /// </remarks>
    public unsafe sealed class TransientVertexBuffer {
        internal NativeStruct tvb;

        /// <summary>
        /// A pointer that can be filled with vertex data.
        /// </summary>
        public IntPtr Data => tvb.data;

        /// <summary>
        /// The size of the buffer.
        /// </summary>
        public int Count => tvb.size;

        /// <summary>
        /// Allocates space in the buffer.
        /// </summary>
        /// <param name="count">The number of vertices for which to make room.</param>
        /// <param name="layout">The layout of each vertex.</param>
        public void Allocate (int count, VertexLayout layout) {
            NativeMethods.bgfx_alloc_transient_vertex_buffer(ref tvb, count, ref layout.data);
        }

        /// <summary>
        /// Check if there is available space in the global transient vertex buffer.
        /// </summary>
        /// <param name="count">The number of vertices to allocate.</param>
        /// <param name="layout">The layout of each vertex.</param>
        /// <returns>
        ///   <c>true</c> if there is sufficient space for the give number of vertices.
        /// </returns>
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
