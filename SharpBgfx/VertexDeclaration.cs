using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe class VertexDeclaration {
        internal Data data;

        public VertexDeclaration Begin (RendererBackend backend = RendererBackend.Null) {
            bgfx_vertex_decl_begin(ref data, backend);
            return this;
        }

        public VertexDeclaration Add (VertexAttribute attribute, int count, VertexAttributeType type, bool normalized = false, bool asInt = false) {
            bgfx_vertex_decl_add(ref data, attribute, (byte)count, type, normalized, asInt);
            return this;
        }

        public VertexDeclaration Skip (int count) {
            bgfx_vertex_decl_skip(ref data, (byte)count);
            return this;
        }

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

        internal struct Data {
            const int MaxAttribCount = 16;

            public uint Hash;
            public ushort Stride;
            public fixed ushort Offset[MaxAttribCount];
            public fixed byte Attributes[MaxAttribCount];
        }
    }
}
