using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a single compiled shader component.
    /// </summary>
    public unsafe struct Shader : IDisposable {
        Uniform[] uniforms;
        internal ushort handle;

        /// <summary>
        /// The set of uniforms exposed by the shader.
        /// </summary>
        public Uniform[] Uniforms {
            get {
                if (uniforms == null) {
                    var count = NativeMethods.bgfx_get_shader_uniforms(handle, null, 0);
                    uniforms = new Uniform[count];
                    NativeMethods.bgfx_get_shader_uniforms(handle, uniforms, count);
                }

                return uniforms;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> struct.
        /// </summary>
        /// <param name="memory">The compiled shader memory.</param>
        public Shader (MemoryBlock memory) {
            handle = NativeMethods.bgfx_create_shader(memory.ptr);
            uniforms = null;
        }

        /// <summary>
        /// Releases the shader.
        /// </summary>
        public void Dispose () {
            NativeMethods.bgfx_destroy_shader(handle);
        }
    }
}
