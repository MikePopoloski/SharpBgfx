using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe struct Shader : IDisposable {
        Uniform[] uniforms;
        internal ushort handle;

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

        public Shader (MemoryBlock memory) {
            handle = bgfx_create_shader(memory.ptr);
            uniforms = null;
        }

        public void Dispose () {
            bgfx_destroy_shader(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_shader (MemoryBlock.DataPtr* memory);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_get_shader_uniforms (ushort handle, Uniform[] uniforms, ushort max);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_shader (ushort handle);
    }
}
