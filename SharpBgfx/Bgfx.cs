using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SharpBgfx {
    [SuppressUnmanagedCodeSecurity]
    public unsafe static class Bgfx {
        const string DllName = "bgfx.dll";

        [DllImport(DllName, EntryPoint = "bgfx_get_supported_renderers", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte GetSupportedRenderers (RendererType[] types);

        [DllImport(DllName, EntryPoint = "bgfx_get_renderer_name", CallingConvention = CallingConvention.Cdecl)]
        public static extern string GetRendererName (RendererType rendererType);

        [DllImport(DllName, EntryPoint = "bgfx_get_renderer_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererType GetCurrentRenderer ();

        [DllImport(DllName, EntryPoint = "bgfx_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init (RendererType rendererType, IntPtr p, IntPtr d);

        [DllImport(DllName, EntryPoint = "bgfx_shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown ();

        [DllImport(DllName, EntryPoint = "bgfx_reset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Reset (int width, int height, ResetFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_set_debug", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDebugFlags (DebugFlags flags);

        [DllImport(DllName, EntryPoint = "bgfx_frame", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Frame ();

        [DllImport(DllName, EntryPoint = "bgfx_get_caps", CallingConvention = CallingConvention.Cdecl)]
        public static extern Caps* GetCaps ();

        [DllImport(DllName, EntryPoint = "bgfx_win_set_hwnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetWindowHandle (IntPtr hwnd);
    }
}
