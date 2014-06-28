using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using SharpBgfx;
using SlimMath;

namespace Common {
    public class Mesh : IDisposable {
        VertexDecl vertexDecl;
        List<MeshGroup> groups;

        public Mesh (MemoryBuffer vertices, VertexDecl decl, ushort[] indices) {
            var group = new MeshGroup();
            group.VertexBuffer = Bgfx.CreateVertexBuffer(vertices, decl);
            group.IndexBuffer = Bgfx.CreateIndexBuffer(MemoryBuffer.FromArray(indices));

            vertexDecl = decl;
            groups = new List<MeshGroup> { group };
        }

        internal Mesh (VertexDecl decl, List<MeshGroup> groups) {
            vertexDecl = decl;
            this.groups = groups;
        }

        public unsafe void Submit (byte viewId, ProgramHandle program, Matrix transform, RenderState state) {
            foreach (var group in groups) {
                Bgfx.SetTransform(&transform.M11, 1);
                Bgfx.SetProgram(program);
                Bgfx.SetIndexBuffer(group.IndexBuffer, 0, -1);
                Bgfx.SetVertexBuffer(group.VertexBuffer, 0, -1);
                Bgfx.SetRenderState(state, 0);
                Bgfx.Submit(viewId, 0);
            }
        }

        public void Dispose () {
            foreach (var group in groups) {
                Bgfx.DestroyVertexBuffer(group.VertexBuffer);
                Bgfx.DestroyIndexBuffer(group.IndexBuffer);
            }

            groups.Clear();
        }
    }

    class MeshGroup {
        public VertexBufferHandle VertexBuffer {
            get;
            set;
        }

        public IndexBufferHandle IndexBuffer {
            get;
            set;
        }

        public Collection<Primitive> Primitives {
            get;
            private set;
        }

        public MeshGroup () {
            Primitives = new Collection<Primitive>();
        }
    }

    struct Primitive {
        public int StartIndex;
        public int IndexCount;
        public int StartVertex;
        public int VertexCount;
    }
}
