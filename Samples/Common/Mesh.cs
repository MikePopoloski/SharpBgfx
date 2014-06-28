using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using SharpBgfx;
using SlimMath;

namespace Common {
    public class Mesh {
        VertexDecl vertexDecl;
        IEnumerable<MeshGroup> groups;

        internal Mesh (VertexDecl decl, IEnumerable<MeshGroup> groups) {
            vertexDecl = decl;
            this.groups = groups;
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
