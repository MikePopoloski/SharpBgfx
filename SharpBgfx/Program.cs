using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a compiled and linked shader program.
    /// </summary>
    public struct Program : IDisposable {
        internal ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> struct.
        /// </summary>
        /// <param name="vertexShader">The vertex shader.</param>
        /// <param name="fragmentShader">The fragment shader.</param>
        /// <param name="destroyShaders">if set to <c>true</c>, the shaders will be released after creating the program.</param>
        public Program (Shader vertexShader, Shader fragmentShader, bool destroyShaders = false) {
            handle = bgfx_create_program(vertexShader.handle, fragmentShader.handle, destroyShaders);
        }

        /// <summary>
        /// Releases the program.
        /// </summary>
        public void Dispose () {
            bgfx_destroy_program(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_program (ushort vsh, ushort fsh, bool destroyShaders);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_destroy_program (ushort handle);
    }
}
