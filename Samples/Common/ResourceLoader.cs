using System.IO;
using SharpBgfx;

namespace Common {
    public static class ResourceLoader {
        static MemoryHandle LoadMemory (string fileName) {
            var bytes = File.ReadAllBytes(fileName);
            return Memory.Copy(bytes);
        }

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
            var mem = LoadMemory(path);
            return Bgfx.CreateShader(mem);
        }

        public static ProgramHandle LoadProgram (string vsName, string fsName) {
            var vsh = LoadShader(vsName);
            var fsh = LoadShader(fsName);

            return Bgfx.CreateProgram(vsh, fsh, true);
        }
    }
}
