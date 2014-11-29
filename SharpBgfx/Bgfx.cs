using System;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace SharpBgfx {
    public unsafe static class Bgfx {
        /// <summary>
        /// Packs a vector into vertex stream format.
        /// </summary>
        /// <param name="input">The vector to pack.</param>
        /// <param name="inputNormalized"><c>true</c> if the input vector is normalized.</param>
        /// <param name="attribute">The attribute usage of the vector data.</param>
        /// <param name="layout">The layout of the vertex stream.</param>
        /// <param name="data">The pointer to the vertex data stream.</param>
        /// <param name="index">The index of the vertex within the stream.</param>
        public static void VertexPack (Vector4 input, bool inputNormalized, VertexAttributeUsage attribute, VertexLayout layout, IntPtr data, int index = 0) {
            NativeMethods.bgfx_vertex_pack((float*)&input, inputNormalized, attribute, ref layout.data, data, index);
        }

        /// <summary>
        /// Unpack a vector from a vertex stream.
        /// </summary>
        /// <param name="attribute">The usage of the vertex attribute.</param>
        /// <param name="layout">The layout of the vertex stream.</param>
        /// <param name="data">A pointer to the vertex data stream.</param>
        /// <param name="index">The index of the vertex within the stream.</param>
        /// <returns>The unpacked vector.</returns>
        public static Vector4 VertexUnpack (VertexAttributeUsage attribute, VertexLayout layout, IntPtr data, int index = 0) {
            Vector4 output;
            NativeMethods.bgfx_vertex_unpack((float*)&output, attribute, ref layout.data, data, index);

            return output;
        }

        /// <summary>
        /// Converts a stream of vertex data from one format to another.
        /// </summary>
        /// <param name="destinationLayout">The destination format.</param>
        /// <param name="destinationData">A pointer to the output location.</param>
        /// <param name="sourceLayout">The source format.</param>
        /// <param name="sourceData">A pointer to the source vertex data to convert.</param>
        /// <param name="count">The number of vertices to convert.</param>
        public static void VertexConvert (VertexLayout destinationLayout, IntPtr destinationData, VertexLayout sourceLayout, IntPtr sourceData, int count = 1) {
            NativeMethods.bgfx_vertex_convert(ref destinationLayout.data, destinationData, ref sourceLayout.data, sourceData, count);
        }

        /// <summary>
        /// Welds vertices that are close together.
        /// </summary>
        /// <param name="output">Pointer to an output remapping table for welded vertices. The size of the buffer must match the number of vertices.</param>
        /// <param name="layout">The layout of the vertex stream.</param>
        /// <param name="data">A pointer to the vertex data stream.</param>
        /// <param name="count">The number of vertices in the stream.</param>
        /// <param name="epsilon">The tolerance for welding vertex positions.</param>
        /// <returns>The number of unique vertices after welding.</returns>
        public static int WeldVertices (ushort* output, VertexLayout layout, IntPtr data, int count, float epsilon = 0.001f) {
            return NativeMethods.bgfx_weld_vertices(output, ref layout.data, data, (ushort)count, epsilon);
        }

        /// <summary>
        /// Swizzles an RGBA8 image to BGRA8.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="pitch">The pitch of the image (in bytes).</param>
        /// <param name="source">The source image data.</param>
        /// <param name="destination">The destination image data.</param>
        /// <remarks>
        /// This method can operate in-place on the image (i.e. src == dst).
        /// </remarks>
        public static void ImageSwizzleBgra8 (int width, int height, int pitch, IntPtr source, IntPtr destination) {
            NativeMethods.bgfx_image_swizzle_bgra8(width, height, pitch, source, destination);
        }

        /// <summary>
        /// Downsamples an RGBA8 image with a 2x2 pixel average filter.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="pitch">The pitch of the image (in bytes).</param>
        /// <param name="source">The source image data.</param>
        /// <param name="destination">The destination image data.</param>
        /// <remarks>
        /// This method can operate in-place on the image (i.e. src == dst).
        /// </remarks>
        public static void ImageRgba8Downsample2x2 (int width, int height, int pitch, IntPtr source, IntPtr destination) {
            NativeMethods.bgfx_image_rgba8_downsample_2x2(width, height, pitch, source, destination);
        }

        /// <summary>
        /// Sets the handle of the main rendering window.
        /// </summary>
        /// <param name="windowHandle">The handle of the native OS window.</param>
        public static void SetWindowHandle (IntPtr windowHandle) {
            NativeMethods.bgfx_win_set_hwnd(windowHandle);
        }

        /// <summary>
        /// Gets the currently active rendering backend API.
        /// </summary>
        /// <returns>The currently active rendering backend.</returns>
        public static RendererBackend GetCurrentBackend () {
            return NativeMethods.bgfx_get_renderer_type();
        }

        /// <summary>
        /// Closes the library and releases all resources.
        /// </summary>
        public static void Shutdown () {
            NativeMethods.bgfx_shutdown();
        }

        /// <summary>
        /// Gets the capabilities of the rendering device.
        /// </summary>
        /// <returns>Information about the capabilities of the device.</returns>
        public static Capabilities GetCaps () {
            return new Capabilities();
        }

        /// <summary>
        /// Resets graphics settings and surfaces.
        /// </summary>
        /// <param name="width">The width of the main window.</param>
        /// <param name="height">The height of the main window.</param>
        /// <param name="flags">Flags used to configure rendering output.</param>
        public static void Reset (int width, int height, ResetFlags flags = ResetFlags.None) {
            NativeMethods.bgfx_reset(width, height, flags);
        }

        /// <summary>
        /// Advances to the next frame.
        /// </summary>
        /// <returns>The current frame number.</returns>
        /// <remarks>
        /// When using a multithreaded renderer, this call
        /// just swaps internal buffers, kicks render thread, and returns. In a
        /// singlethreaded renderer this call does frame rendering.
        /// </remarks>
        public static int Frame () {
            return NativeMethods.bgfx_frame();
        }

        /// <summary>
        /// Initializes the graphics library with a default backend.
        /// </summary>
        public static void Init () {
            Init((RendererBackend)RendererCount);
        }

        /// <summary>
        /// Initializes the graphics library with a specific backend API.
        /// </summary>
        /// <param name="backend">The backend API to use for rendering.</param>
        public static void Init (RendererBackend backend) {
            NativeMethods.bgfx_init(backend, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Gets the set of supported rendering backends.
        /// </summary>
        /// <returns></returns>
        public static RendererBackend[] GetSupportedBackends () {
            var types = new RendererBackend[RendererCount];
            var count = NativeMethods.bgfx_get_supported_renderers(types);

            return types.Take(count).ToArray();
        }

        /// <summary>
        /// Gets the friendly name of a specific rendering backend.
        /// </summary>
        /// <param name="backend">The backend for which to retrieve a name.</param>
        /// <returns>The friendly name of the specified backend.</returns>
        public static string GetBackendName (RendererBackend backend) {
            return new string(NativeMethods.bgfx_get_renderer_name(backend));
        }

        /// <summary>
        /// Enables debugging features.
        /// </summary>
        /// <param name="features">The set of debug features to enable.</param>
        public static void SetDebugFeatures (DebugFeatures features) {
            NativeMethods.bgfx_set_debug(features);
        }

        /// <summary>
        /// Sets a marker that can be used for debugging purposes.
        /// </summary>
        /// <param name="marker">The user-defined name of the marker.</param>
        public static void SetDebugMarker (string marker) {
            NativeMethods.bgfx_set_marker(marker);
        }

        /// <summary>
        /// Clears the debug text buffer.
        /// </summary>
        /// <param name="color">The color with which to clear the background.</param>
        /// <param name="smallText"><c>true</c> to use a small font for debug output; <c>false</c> to use normal sized text.</param>
        public static void DebugTextClear (byte color, bool smallText) {
            NativeMethods.bgfx_dbg_text_clear(color, smallText);
        }

        /// <summary>
        /// Writes debug text to the screen.
        /// </summary>
        /// <param name="x">The X position, in cells.</param>
        /// <param name="y">The Y position, in cells.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="message">The message to write.</param>
        public static void DebugTextWrite (int x, int y, byte color, string message) {
            NativeMethods.bgfx_dbg_text_printf((ushort)x, (ushort)y, color, "%s", message);
        }

        /// <summary>
        /// Writes debug text to the screen.
        /// </summary>
        /// <param name="x">The X position, in cells.</param>
        /// <param name="y">The Y position, in cells.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="format">The format of the message.</param>
        /// <param name="args">The arguments with which to format the message.</param>
        public static void DebugTextWrite (int x, int y, byte color, string format, params object[] args) {
            NativeMethods.bgfx_dbg_text_printf((ushort)x, (ushort)y, color, "%s", string.Format(CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Sets the name of a rendering view, for debugging purposes.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="name">The name of the view.</param>
        public static void SetViewName (byte id, string name) {
            NativeMethods.bgfx_set_view_name(id, name);
        }

        /// <summary>
        /// Sets the viewport for the given rendering view.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="x">The X coordinate of the viewport.</param>
        /// <param name="y">The Y coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport, in pixels.</param>
        /// <param name="height">The height of the viewport, in pixels.</param>
        public static void SetViewRect (byte id, int x, int y, int width, int height) {
            NativeMethods.bgfx_set_view_rect(id, (ushort)x, (ushort)y, (ushort)width, (ushort)height);
        }

        /// <summary>
        /// Sets the scissor rectangle for a specific view.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="x">The X coordinate of the scissor rectangle.</param>
        /// <param name="y">The Y coordinate of the scissor rectangle.</param>
        /// <param name="width">The width of the scissor rectangle.</param>
        /// <param name="height">The height of the scissor rectangle.</param>
        /// <remarks>
        /// Set all values to zero to disable the scissor test.
        /// </remarks>
        public static void SetViewScissor (byte id, int x, int y, int width, int height) {
            NativeMethods.bgfx_set_view_scissor(id, (ushort)x, (ushort)y, (ushort)width, (ushort)height);
        }

        /// <summary>
        /// Sets view clear flags.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="targets">The target surfaces that should be cleared.</param>
        /// <param name="rgba">The color to clear the backbuffer.</param>
        /// <param name="depth">The value to fill the depth buffer.</param>
        /// <param name="stencil">The value to fill the stencil buffer.</param>
        public static void SetViewClear (byte id, ClearTargets targets, Color4 color, float depth = 1.0f, byte stencil = 0) {
            NativeMethods.bgfx_set_view_clear(id, targets, color.ToRgba(), depth, stencil);
        }

        /// <summary>
        /// Enables or disables sequential mode for a view. Sequential mode issues draw calls in the order they are received.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="enabled"><c>true</c> to enable sequential mode; otherwise, <c>false</c>.</param>
        public static void SetViewSequential (byte id, bool enabled) {
            NativeMethods.bgfx_set_view_seq(id, enabled);
        }

        /// <summary>
        /// Sets the view and projection transforms for the given rendering view.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="view">The 4x4 view transform matrix.</param>
        /// <param name="projection">The 4x4 projection transform matrix.</param>
        public static void SetViewTransform (byte id, Matrix4x4 view, Matrix4x4 projection) {
            NativeMethods.bgfx_set_view_transform(id, (float*)&view, (float*)&projection);
        }

        /// <summary>
        /// Sets the view and projection transforms for the given rendering view.
        /// </summary>
        /// <param name="id">The index of the view.</param>
        /// <param name="view">The 4x4 view transform matrix.</param>
        /// <param name="projection">The 4x4 projection transform matrix.</param>
        [CLSCompliant(false)]
        public static void SetViewTransform (byte id, float* view, float* projection) {
            NativeMethods.bgfx_set_view_transform(id, view, projection);
        }

        public static int SetTransform (Matrix4x4 matrix, int count) {
            return NativeMethods.bgfx_set_transform((float*)&matrix, (ushort)count);
        }

        [CLSCompliant(false)]
        public static int SetTransform (float* matrix, int count) {
            return NativeMethods.bgfx_set_transform(matrix, (ushort)count);
        }

        /// <summary>
        /// Sets the shader program to use for drawing primitives.
        /// </summary>
        /// <param name="program">The shader program to set.</param>
        public static void SetProgram (Program program) {
            NativeMethods.bgfx_set_program(program.handle);
        }

        /// <summary>
        /// Sets the index buffer to use for drawing primitives.
        /// </summary>
        /// <param name="indexBuffer">The index buffer to set.</param>
        /// <param name="firstIndex">The first index in the buffer to use.</param>
        /// <param name="count">The number of indices to pull from the buffer.</param>
        public static void SetIndexBuffer (IndexBuffer indexBuffer, int firstIndex = 0, int count = -1) {
            NativeMethods.bgfx_set_index_buffer(indexBuffer.handle, firstIndex, count);
        }

        /// <summary>
        /// Sets the vertex buffer to use for drawing primitives.
        /// </summary>
        /// <param name="vertexBuffer">The vertex buffer to set.</param>
        /// <param name="firstVertex">The index of the first vertex to use.</param>
        /// <param name="count">The number of vertices to pull from the buffer.</param>
        public static void SetVertexBuffer (VertexBuffer vertexBuffer, int firstVertex = 0, int count = -1) {
            NativeMethods.bgfx_set_vertex_buffer(vertexBuffer.handle, firstVertex, count);
        }

        /// <summary>
        /// Sets the index buffer to use for drawing primitives.
        /// </summary>
        /// <param name="indexBuffer">The index buffer to set.</param>
        /// <param name="firstIndex">The first index in the buffer to use.</param>
        /// <param name="count">The number of indices to pull from the buffer.</param>
        public static void SetIndexBuffer (DynamicIndexBuffer indexBuffer, int firstIndex = 0, int count = -1) {
            NativeMethods.bgfx_set_dynamic_index_buffer(indexBuffer.handle, firstIndex, count);
        }

        /// <summary>
        /// Sets the vertex buffer to use for drawing primitives.
        /// </summary>
        /// <param name="vertexBuffer">The vertex buffer to set.</param>
        /// <param name="firstVertex">The index of the first vertex to use.</param>
        /// <param name="count">The number of vertices to pull from the buffer.</param>
        public static void SetVertexBuffer (DynamicVertexBuffer vertexBuffer, int firstVertex = 0, int count = -1) {
            NativeMethods.bgfx_set_dynamic_vertex_buffer(vertexBuffer.handle, firstVertex, count);
        }

        /// <summary>
        /// Sets the value of a uniform parameter.
        /// </summary>
        /// <param name="uniform">The uniform to set.</param>
        /// <param name="value">A pointer to the uniform's data.</param>
        /// <param name="arraySize">The size of the data array, if the uniform is an array.</param>
        public static void SetUniform (Uniform uniform, float value, int arraySize = 1) {
            NativeMethods.bgfx_set_uniform(uniform.handle, &value, (ushort)arraySize);
        }

        /// <summary>
        /// Sets the value of a uniform parameter.
        /// </summary>
        /// <param name="uniform">The uniform to set.</param>
        /// <param name="value">A pointer to the uniform's data.</param>
        /// <param name="arraySize">The size of the data array, if the uniform is an array.</param>
        [CLSCompliant(false)]
        public static void SetUniform (Uniform uniform, void* value, int arraySize = 1) {
            NativeMethods.bgfx_set_uniform(uniform.handle, value, (ushort)arraySize);
        }

        /// <summary>
        /// Sets the value of a uniform parameter.
        /// </summary>
        /// <param name="uniform">The uniform to set.</param>
        /// <param name="value">A pointer to the uniform's data.</param>
        /// <param name="arraySize">The size of the data array, if the uniform is an array.</param>
        public static void SetUniform (Uniform uniform, IntPtr value, int arraySize = 1) {
            NativeMethods.bgfx_set_uniform(uniform.handle, value.ToPointer(), (ushort)arraySize);
        }

        /// <summary>
        /// Submits the current batch of primitives for rendering.
        /// </summary>
        /// <param name="id">The index of the view to submit.</param>
        /// <param name="depth">A depth value to use for sorting the batch.</param>
        /// <returns>The number of draw calls.</returns>
        public static int Submit (byte id, int depth = 0) {
            return NativeMethods.bgfx_submit(id, depth);
        }

        /// <summary>
        /// Discards all previously set state for the draw call.
        /// </summary>
        public static void Discard () {
            NativeMethods.bgfx_discard();
        }

        /// <summary>
        /// Requests that a screenshot be saved. The ScreenshotTaken event will be fired to save the result.
        /// </summary>
        /// <param name="filePath">The file path that will be passed to the callback event.</param>
        public static void SaveScreenShot (string filePath) {
            NativeMethods.bgfx_save_screen_shot(filePath);
        }

        /// <summary>
        /// Set rendering states used to draw primitives.
        /// </summary>
        /// <param name="state">The set of states to set.</param>
        public static void SetRenderState (RenderState state) {
            NativeMethods.bgfx_set_state(state, 0);
        }

        /// <summary>
        /// Set rendering states used to draw primitives.
        /// </summary>
        /// <param name="state">The set of states to set.</param>
        /// <param name="color">The color used for "factor" blending modes.</param>
        public static void SetRenderState (RenderState state, Color4 color) {
            NativeMethods.bgfx_set_state(state, color.ToRgba());
        }

        const int RendererCount = 6;
    }
}
