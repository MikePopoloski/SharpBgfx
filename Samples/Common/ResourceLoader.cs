using System.Collections.Generic;
using System.IO;
using SharpBgfx;

namespace Common {
    public static class ResourceLoader {
        static string GetShaderPath () {
            switch (Bgfx.GetCurrentBackend()) {
                case RendererBackend.Direct3D11:
                    return "Assets/dx11/";

                case RendererBackend.OpenGL:
                    return "Assets/glsl/";

                case RendererBackend.OpenGLES:
                    return "Assets/gles/";

                default:
                    return "Assets/dx9/";
            }
        }

        public static Shader LoadShader (string name) {
            var path = Path.Combine(GetShaderPath(), name) + ".bin";
            var mem = MemoryBlock.FromArray(File.ReadAllBytes(path));
            return new Shader(mem);
        }

        public static Program LoadProgram (string vsName, string fsName) {
            var vsh = LoadShader(vsName);
            var fsh = LoadShader(fsName);

            return new Program(vsh, fsh, true);
        }

        public static Texture LoadTexture (string name) {
            var path = Path.Combine("Assets/textures/", name);
            var mem = MemoryBlock.FromArray(File.ReadAllBytes(path));
            return new Texture(mem, TextureFlags.None, 0);
        }

        //public static Mesh LoadMesh (string fileName) {
        //    var path = Path.Combine("Assets/meshes/", fileName);
        //    var groups = new List<MeshGroup>();
        //    var group = new MeshGroup();
        //    var decl = new VertexDeclaration();

        //    using (var stream = new ByteStream(File.ReadAllBytes(path))) {
        //        while (stream.RemainingBytes > 0) {
        //            var tag = stream.Read<uint>();
        //            if (tag == ChunkTagVB) {
        //                // skip bounding volume info
        //                stream.Skip(BoundingVolumeSize);

        //                decl = stream.Read<VertexDeclaration>();

        //                var vertexCount = stream.Read<ushort>();
        //                var vertexData = stream.ReadRange<byte>(vertexCount * decl.Stride);
        //                group.VertexBuffer = Bgfx.CreateVertexBuffer(MemoryBuffer.FromArray(vertexData), decl);
        //            }
        //            else if (tag == ChunkTagIB) {
        //                var indexCount = stream.Read<int>();
        //                var indexData = stream.ReadRange<ushort>(indexCount);
        //                group.IndexBuffer = Bgfx.CreateIndexBuffer(MemoryBuffer.FromArray(indexData));
        //            }
        //            else if (tag == ChunkTagPri) {
        //                // skip material name
        //                var len = stream.Read<ushort>();
        //                stream.Skip(len);

        //                // read primitive data
        //                var count = stream.Read<ushort>();
        //                for (int i = 0; i < count; i++) {
        //                    // skip name
        //                    len = stream.Read<ushort>();
        //                    stream.Skip(len);

        //                    var prim = stream.Read<Primitive>();
        //                    group.Primitives.Add(prim);

        //                    stream.Skip(BoundingVolumeSize);
        //                }

        //                groups.Add(group);
        //                group = new MeshGroup();
        //            }
        //        }
        //    }

        //    return new Mesh(decl, groups);
        //}

        //static uint MakeFourCC (char a, char b, char c, char d) {
        //    return a | ((uint)b << 8) | ((uint)c << 16) | ((uint)d << 24);
        //}

        //const int BoundingVolumeSize = 104;
        //static readonly uint ChunkTagVB = MakeFourCC('V', 'B', ' ', '\0');
        //static readonly uint ChunkTagIB = MakeFourCC('I', 'B', ' ', '\0');
        //static readonly uint ChunkTagPri = MakeFourCC('P', 'R', 'I', '\0');
    }
}
