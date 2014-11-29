using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpBgfx {
    [SuppressUnmanagedCodeSecurity]
    unsafe static class NativeMethods {
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_program (ushort vsh, ushort fsh, bool destroyShaders);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_program (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Capabilities.Caps* bgfx_get_caps ();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_decl_begin (ref VertexDeclaration.Data decl, RendererBackend backend);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_decl_add (ref VertexDeclaration.Data decl, VertexAttribute attribute, byte count, VertexAttributeType type, bool normalized, bool asInt);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_decl_skip (ref VertexDeclaration.Data decl, byte count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_decl_end (ref VertexDeclaration.Data decl);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_vertex_buffer (MemoryBlock.DataPtr* memory, ref VertexDeclaration.Data decl);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_vertex_buffer (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_uniform ([MarshalAs(UnmanagedType.LPStr)] string name, UniformType type, ushort arraySize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_uniform (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_texture (MemoryBlock.DataPtr* mem, TextureFlags flags, byte skip, out Texture.TextureInfo info);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_texture_2d (ushort width, ushort _height, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_texture_3d (ushort width, ushort _height, ushort _depth, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_texture_cube (ushort size, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_texture (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_calc_texture_size (ref Texture.TextureInfo info, ushort width, ushort height, ushort depth, byte mipCount, TextureFormat format);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_shader (MemoryBlock.DataPtr* memory);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_get_shader_uniforms (ushort handle, Uniform[] uniforms, ushort max);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_shader (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryBlock.DataPtr* bgfx_alloc (int size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryBlock.DataPtr* bgfx_copy (IntPtr data, int size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_index_buffer (MemoryBlock.DataPtr* memory);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_index_buffer (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_dynamic_index_buffer (int indexCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_dynamic_index_buffer_mem (MemoryBlock.DataPtr* memory);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_update_dynamic_index_buffer (ushort handle, MemoryBlock.DataPtr* memory);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_dynamic_index_buffer (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_dynamic_vertex_buffer (ushort indexCount, ref VertexDeclaration.Data decl);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_create_dynamic_vertex_buffer_mem (MemoryBlock.DataPtr* memory, ref VertexDeclaration.Data decl);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_update_dynamic_vertex_buffer (ushort handle, MemoryBlock.DataPtr* memory);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_dynamic_vertex_buffer (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_image_swizzle_bgra8 (int width, int height, int pitch, IntPtr src, IntPtr dst);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_image_rgba8_downsample_2x2 (int width, int height, int pitch, IntPtr src, IntPtr dst);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_win_set_hwnd (IntPtr hwnd);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererBackend bgfx_get_renderer_type ();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_shutdown ();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_reset (int width, int height, ResetFlags flags);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int bgfx_frame ();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_debug (DebugFlags flags);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_marker ([MarshalAs(UnmanagedType.LPStr)] string marker);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_dbg_text_clear (byte color, [MarshalAs(UnmanagedType.U1)] bool smallText);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int bgfx_set_transform (float* matrix, ushort count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_stencil (StencilFlags frontFace, StencilFlags backFace);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int bgfx_submit (byte id, int depth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_discard ();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_name (byte id, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_rect (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_scissor (byte id, ushort x, ushort y, ushort width, ushort height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_clear (byte id, ClearFlags flags, int rgba, float depth, byte stencil);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_seq (byte id, bool enabled);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_transform (byte id, float* view, float* proj);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_save_screen_shot ([MarshalAs(UnmanagedType.LPStr)] string filePath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_state (RenderState state, int rgba);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_pack (float* input, bool inputNormalized, VertexAttribute attribute, ref VertexDeclaration.Data decl, IntPtr data, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_unpack (float* output, VertexAttribute attribute, ref VertexDeclaration.Data decl, IntPtr data, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_vertex_convert (ref VertexDeclaration.Data destDecl, IntPtr destData, ref VertexDeclaration.Data srcDecl, IntPtr srcData, int num);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort bgfx_weld_vertices (ushort* output, ref VertexDeclaration.Data decl, IntPtr data, ushort num, float epsilon);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte bgfx_get_supported_renderers (RendererBackend[] backends);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern sbyte* bgfx_get_renderer_name (RendererBackend backend);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_init (RendererBackend backend, IntPtr callbacks, IntPtr allocator);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_dbg_text_printf (ushort x, ushort y, byte color, [MarshalAs(UnmanagedType.LPStr)] string format, [MarshalAs(UnmanagedType.LPStr)] string args);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_view_transform (byte id, float* view, float* proj, byte flags, IntPtr projR);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_program (ushort handle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_index_buffer (ushort handle, int firstIndex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_dynamic_index_buffer (ushort handle, int firstIndex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_vertex_buffer (ushort handle, int startVertex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_dynamic_vertex_buffer (ushort handle, int startVertex, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_set_uniform (ushort handle, void* value, ushort arraySize);

#if DEBUG
        const string DllName = "bgfx_debug.dll";
#else
        const string DllName = "bgfx.dll";
#endif
    }
}
