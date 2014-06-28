using System.Collections.Generic;
using System.IO;
using SharpBgfx;

namespace Common {
    public static class ResourceLoader {
        static string GetShaderPath () {
            switch (Bgfx.GetCurrentRenderer()) {
                case RendererType.Direct3D11:
                    return "Assets/dx11/";

                case RendererType.OpenGL:
                    return "Assets/glsl/";

                case RendererType.OpenGLES:
                    return "Assets/gles/";

                default:
                    return "Assets/dx9/";
            }
        }

        public static ShaderHandle LoadShader (string name) {
            var path = Path.Combine(GetShaderPath(), name) + ".bin";
            var mem = MemoryBuffer.FromArray(File.ReadAllBytes(path));
            return Bgfx.CreateShader(mem);
        }

        public static ProgramHandle LoadProgram (string vsName, string fsName) {
            var vsh = LoadShader(vsName);
            var fsh = LoadShader(fsName);

            return Bgfx.CreateProgram(vsh, fsh, true);
        }

        public static TextureHandle LoadTexture (string name) {
            var path = Path.Combine("Assets/textures/", name);
            var mem = MemoryBuffer.FromArray(File.ReadAllBytes(path));
            return Bgfx.CreateTexture(mem, TextureFlags.None, 0);
        }

        public static Mesh LoadMesh (string fileName) {
            var path = Path.Combine("Assets/meshes/", fileName);
            var groups = new List<MeshGroup>();
            var group = new MeshGroup();
            var decl = new VertexDecl();

            using (var stream = new ByteStream(File.ReadAllBytes(path))) {
                while (stream.RemainingBytes > 0) {
                    var tag = stream.Read<uint>();
                    if (tag == ChunkTagVB) {
                        // skip bounding volume info
                        stream.Skip(BoundingVolumeSize);

                        decl = stream.Read<VertexDecl>();

                        var vertexCount = stream.Read<ushort>();
                        var vertexData = stream.ReadRange<byte>(vertexCount * decl.Stride);
                        group.VertexBuffer = Bgfx.CreateVertexBuffer(MemoryBuffer.FromArray(vertexData), decl);
                    }
                    else if (tag == ChunkTagIB) {
                        var indexCount = stream.Read<int>();
                        var indexData = stream.ReadRange<ushort>(indexCount);
                        group.IndexBuffer = Bgfx.CreateIndexBuffer(MemoryBuffer.FromArray(indexData));
                    }
                    else if (tag == ChunkTagPri) {
                        // skip material name
                        var len = stream.Read<ushort>();
                        stream.Skip(len);

                        // read primitive data
                        var count = stream.Read<ushort>();
                        for (int i = 0; i < count; i++) {
                            // skip name
                            len = stream.Read<ushort>();
                            stream.Skip(len);

                            var prim = stream.Read<Primitive>();
                            group.Primitives.Add(prim);

                            stream.Skip(BoundingVolumeSize);
                        }

                        groups.Add(group);
                        group = new MeshGroup();
                    }
                }
            }

            return new Mesh(decl, groups);
        }

        static uint MakeFourCC (char a, char b, char c, char d) {
            return a | ((uint)b << 8) | ((uint)c << 16) | ((uint)d << 24);
        }

        const int BoundingVolumeSize = 104;
        static readonly uint ChunkTagVB = MakeFourCC('V', 'B', ' ', '\0');
        static readonly uint ChunkTagIB = MakeFourCC('I', 'B', ' ', '\0');
        static readonly uint ChunkTagPri = MakeFourCC('P', 'R', 'I', '\0');
    }
}
