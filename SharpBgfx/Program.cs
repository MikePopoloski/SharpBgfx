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
            handle = NativeMethods.bgfx_create_program(vertexShader.handle, fragmentShader.handle, destroyShaders);
        }

        /// <summary>
        /// Releases the program.
        /// </summary>
        public void Dispose () {
            NativeMethods.bgfx_destroy_program(handle);
        }
    }
}
