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
                    var count = bgfx_get_shader_uniforms(handle, null, 0);
                    uniforms = new Uniform[count];
                    bgfx_get_shader_uniforms(handle, uniforms, count);
                }

                return uniforms;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> struct.
        /// </summary>
        /// <param name="memory">The compiled shader memory.</param>
        public Shader (MemoryBlock memory) {
            handle = bgfx_create_shader(memory.ptr);
            uniforms = null;
        }

        /// <summary>
        /// Releases the shader.
        /// </summary>
        public void Dispose () {
            bgfx_destroy_shader(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_shader (MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_get_shader_uniforms (ushort handle, Uniform[] uniforms, ushort max);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_destroy_shader (ushort handle);
    }
}
