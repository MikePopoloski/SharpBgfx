using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a shader uniform.
    /// </summary>
    public struct Uniform : IDisposable {
        internal ushort handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Uniform"/> struct.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        /// <param name="type">The type of data represented by the uniform.</param>
        /// <param name="arraySize">Size of the array, if the uniform is an array type.</param>
        /// <remarks>
        /// Predefined uniform names:
        /// u_viewRect vec4(x, y, width, height) - view rectangle for current view.
        /// u_viewTexel vec4 (1.0/width, 1.0/height, undef, undef) - inverse width and height
        /// u_view mat4 - view matrix
        /// u_invView mat4 - inverted view matrix
        /// u_proj mat4 - projection matrix
        /// u_invProj mat4 - inverted projection matrix
        /// u_viewProj mat4 - concatenated view projection matrix
        /// u_invViewProj mat4 - concatenated inverted view projection matrix
        /// u_model mat4[BGFX_CONFIG_MAX_BONES] - array of model matrices.
        /// u_modelView mat4 - concatenated model view matrix, only first model matrix from array is used.
        /// u_modelViewProj mat4 - concatenated model view projection matrix.
        /// u_alphaRef float - alpha reference value for alpha test.
        /// </remarks>
        public Uniform (string name, UniformType type, int arraySize = 1) {
            handle = NativeMethods.bgfx_create_uniform(name, type, (ushort)arraySize);
        }

        /// <summary>
        /// Releases the uniform.
        /// </summary>
        public void Dispose () {
            NativeMethods.bgfx_destroy_uniform(handle);
        }
    }
}
