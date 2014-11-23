using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpBgfx {
    [SuppressUnmanagedCodeSecurity]
    public unsafe static class Bgfx {
        internal const string DllName = "bgfx.dll";
        const int RendererCount = 6;

        #region Vertex Data

        [DllImport(DllName, EntryPoint = "bgfx_vertex_pack", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexPack (float* input, bool inputNormalized, VertexAttribute attribute, ref VertexDeclaration decl, IntPtr data, int index = 0);

        [DllImport(DllName, EntryPoint = "bgfx_vertex_unpack", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexUnpack (float* output, VertexAttribute attribute, ref VertexDeclaration decl, IntPtr data, int index = 0);

        [DllImport(DllName, EntryPoint = "bgfx_vertex_convert", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VertexConvert (ref VertexDeclaration destDecl, IntPtr destData, ref VertexDeclaration srcDecl, IntPtr srcData, int num = 1);

        [DllImport(DllName, EntryPoint = "bgfx_weld_vertices", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort WeldVertices (ushort* output, ref VertexDeclaration decl, IntPtr data, ushort num, float epsilon = 0.001f);

        #endregion

        #region Image Data

        [DllImport(DllName, EntryPoint = "bgfx_image_swizzle_bgra8", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImageSwizzleBgra8 (int width, int height, int pitch, IntPtr src, IntPtr dst);

        [DllImport(DllName, EntryPoint = "bgfx_image_rgba8_downsample_2x2", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImageRgba8Downsample2x2 (int width, int height, int pitch, IntPtr src, IntPtr dst);

        #endregion

        #region Renderer

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern byte bgfx_get_supported_renderers (RendererBackend[] backends);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern sbyte* bgfx_get_renderer_name (RendererBackend backend);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_init (RendererBackend backend, IntPtr callbacks, IntPtr allocator);

        [DllImport(DllName, EntryPoint = "bgfx_get_caps", CallingConvention = CallingConvention.Cdecl)]
        public static extern Caps* GetCaps ();

        [DllImport(DllName, EntryPoint = "bgfx_win_set_hwnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetWindowHandle (IntPtr hwnd);

        [DllImport(DllName, EntryPoint = "bgfx_get_renderer_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererBackend GetCurrentBackend ();

        [DllImport(DllName, EntryPoint = "bgfx_shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown ();

        [DllImport(DllName, EntryPoint = "bgfx_reset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Reset (int width, int height, ResetFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_frame", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Frame ();

        public static void Init () {
            Init((RendererBackend)RendererCount);
        }

        public static void Init (RendererBackend backend) {
            bgfx_init(backend, IntPtr.Zero, IntPtr.Zero);
        }

        public static RendererBackend[] GetSupportedBackends () {
            var types = new RendererBackend[RendererCount];
            var count = bgfx_get_supported_renderers(types);

            return types.Take(count).ToArray();
        }

        public static string GetBackendName (RendererBackend backend) {
            return new string(bgfx_get_renderer_name(backend));
        }

        #endregion

        #region Debug

        [DllImport(DllName, EntryPoint = "bgfx_set_debug", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDebugFlags (DebugFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_set_marker", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDebugMarker (string marker);

        [DllImport(DllName, EntryPoint = "bgfx_dbg_text_clear", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DebugTextClear (byte color, bool smallText);

        [DllImport(DllName, EntryPoint = "bgfx_dbg_text_printf", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DebugTextWrite (ushort x, ushort y, byte color, string text);

        #endregion

        #region Views

        [DllImport(DllName, EntryPoint = "bgfx_set_view_name", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewName (byte id, string name);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_rect", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewRect (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_rect_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewRectMask (int viewMask, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_scissor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewScissor (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_scissor_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewScissorMask (int viewMask, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_clear", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewClear (byte id, ClearFlags flags, int rgba, float depth, byte stencil);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_clear_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewClearMask (int viewMask, ClearFlags flags, int rgba, float depth, byte stencil);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_seq", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewSequential (byte id, bool enabled);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_seq_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewSequentialMask (int viewMask, bool enabled);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_transform", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewTransform (byte id, float* view, float* proj);

        [DllImport(DllName, EntryPoint = "bgfx_set_view_transform_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetViewTransformMask (int viewMask, float* view, float* proj);

        #endregion

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_set_program (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_set_index_buffer (ushort handle, int firstIndex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_set_vertex_buffer (ushort handle, int startVertex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_set_uniform (ushort handle, void* value, ushort arraySize = 1);

        public static void SetProgram (Program program) {
            bgfx_set_program(program.handle);
        }

        public static void SetIndexBuffer (IndexBuffer indexBuffer, int firstIndex = 0, int count = -1) {
            bgfx_set_index_buffer(indexBuffer.handle, firstIndex, count);
        }

        public static void SetVertexBuffer (VertexBuffer vertexBuffer, int firstVertex = 0, int count = -1) {
            bgfx_set_vertex_buffer(vertexBuffer.handle, firstVertex, count);
        }

        public static void SetUniform (Uniform uniform, void* value, int arraySize = 1) {
            SetUniform(uniform, new IntPtr(value), arraySize);
        }

        public static void SetUniform (Uniform uniform, IntPtr value, int arraySize = 1) {
            bgfx_set_uniform(uniform.handle, value.ToPointer(), (ushort)arraySize);
        }

        [DllImport(DllName, EntryPoint = "bgfx_set_transform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetTransform (float* matrix, ushort count);

        [DllImport(DllName, EntryPoint = "bgfx_set_stencil", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetStencil (StencilFlags frontFace, StencilFlags backFace);

        [DllImport(DllName, EntryPoint = "bgfx_submit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Submit (byte id, int depth = 0);

        [DllImport(DllName, EntryPoint = "bgfx_submit_mask", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SubmitMask (int viewMask, int depth);

        [DllImport(DllName, EntryPoint = "bgfx_discard", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Discard ();

        [DllImport(DllName, EntryPoint = "bgfx_save_screen_shot", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SaveScreenShot (string filePath);

        [DllImport(DllName, EntryPoint = "bgfx_set_state", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRenderState (RenderState state, uint rgba);

        // **** methods below are internal, since they're exposed by a wrapper to make them more convenient for .NET callers ****


        [DllImport(DllName, EntryPoint = "bgfx_calc_texture_size", CallingConvention = CallingConvention.Cdecl)]
        static extern void CalcTextureSize (ref TextureInfo info, ushort width, ushort height, ushort depth, byte mipCount, TextureFormat format);
    }
}
