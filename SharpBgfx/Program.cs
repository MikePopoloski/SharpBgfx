using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public struct Program : IDisposable {
        internal ushort handle;

        public Program (Shader vertexShader, Shader fragmentShader, bool destroyShaders = false) {
            handle = bgfx_create_program(vertexShader.handle, fragmentShader.handle, destroyShaders);
        }

        public void Dispose () {
            bgfx_destroy_program(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_program (ushort vsh, ushort fsh, bool destroyShaders);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_program (ushort handle);
    }
}
