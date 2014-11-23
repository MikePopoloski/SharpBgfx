using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpBgfx {
    public unsafe class Texture : IDisposable {
        ushort handle;

        public Texture (MemoryBlock memory, TextureFlags flags = TextureFlags.None, int skipMips = 0) {
            TextureInfo info;
            handle = bgfx_create_texture(memory.ptr, flags, (byte)skipMips, out info);
        }

        public void Dispose () {
            bgfx_destroy_texture(handle);
        }

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture (MemoryBlock.DataPtr* mem, TextureFlags flags, byte skip, out TextureInfo info);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_2d (ushort width, ushort _height, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_3d (ushort width, ushort _height, ushort _depth, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_cube (ushort size, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void bgfx_destroy_texture (ushort handle);
    }
}
