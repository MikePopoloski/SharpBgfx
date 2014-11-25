using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Describes the layout of data in a vertex stream.
    /// </summary>
    public sealed class VertexDeclaration {
        internal Data data;

        /// <summary>
        /// Starts a stream of vertex attribute additions to the declaration.
        /// </summary>
        /// <param name="backend">The rendering backend with which to associate the attributes.</param>
        /// <returns>This instance, for use in a fluent API.</returns>
        public VertexDeclaration Begin (RendererBackend backend = RendererBackend.Null) {
            bgfx_vertex_decl_begin(ref data, backend);
            return this;
        }

        /// <summary>
        /// Starts a stream of vertex attribute additions to the declaration.
        /// </summary>
        /// <param name="attribute">The kind of attribute to add.</param>
        /// <param name="count">The number of elements in the attribute (1, 2, 3, or 4).</param>
        /// <param name="type">The type of data described by the attribute.</param>
        /// <param name="normalized">if set to <c>true</c>, values will be normalized from a 0-255 range to 0.0 - 0.1 in the shader.</param>
        /// <param name="asInt">if set to <c>true</c>, the attribute is packaged as an integer in the shader.</param>
        /// <returns>
        /// This instance, for use in a fluent API.
        /// </returns>
        public VertexDeclaration Add (VertexAttribute attribute, int count, VertexAttributeType type, bool normalized = false, bool asInt = false) {
            bgfx_vertex_decl_add(ref data, attribute, (byte)count, type, normalized, asInt);
            return this;
        }

        /// <summary>
        /// Skips the specified number of bytes in the vertex stream.
        /// </summary>
        /// <param name="count">The number of bytes to skip.</param>
        /// <returns>This instance, for use in a fluent API.</returns>
        public VertexDeclaration Skip (int count) {
            bgfx_vertex_decl_skip(ref data, (byte)count);
            return this;
        }

        /// <summary>
        /// Marks the end of the vertex stream.
        /// </summary>
        public void End () {
            bgfx_vertex_decl_end(ref data);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_vertex_decl_begin (ref Data decl, RendererBackend backend);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_vertex_decl_add (ref Data decl, VertexAttribute attribute, byte count, VertexAttributeType type, bool normalized, bool asInt);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_vertex_decl_skip (ref Data decl, byte count);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_vertex_decl_end (ref Data decl);

        internal unsafe struct Data {
            const int MaxAttribCount = 16;

            public uint Hash;
            public ushort Stride;
            public fixed ushort Offset[MaxAttribCount];
            public fixed byte Attributes[MaxAttribCount];
        }
    }
}
