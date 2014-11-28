using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    /// <summary>
    /// Represents a loaded texture.
    /// </summary>
    public unsafe sealed class Texture : IDisposable {
        ushort handle;

        /// <summary>
        /// The width of the texture.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height of the texture.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The depth of the texture, if 3D.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// The number of mip levels in the texture.
        /// </summary>
        public int MipLevels { get; }

        /// <summary>
        /// The number of bits per pixel.
        /// </summary>
        public int BitsPerPixel { get; }

        /// <summary>
        /// The size of the entire texture, in bytes.
        /// </summary>
        public int SizeInBytes { get; }

        /// <summary>
        /// The format of the image data.
        /// </summary>
        public TextureFormat Format { get; }

        Texture (ushort handle, ref TextureInfo info) {
            this.handle = handle;

            Width = info.Width;
            Height = info.Height;
            Depth = info.Depth;
            MipLevels = info.MipCount;
            BitsPerPixel = info.BitsPerPixel;
            SizeInBytes = info.StorageSize;
            Format = info.Format;
        }

        /// <summary>
        /// Creates a new texture from a file loaded in memory.
        /// </summary>
        /// <param name="memory">The content of the file.</param>
        /// <param name="flags">Flags that control texture behavior.</param>
        /// <param name="skipMips">A number of top level mips to skip when parsing texture data.</param>
        /// <returns>The newly created texture.</returns>
        /// <remarks>
        /// This function supports textures in the following container formats:
        /// - DDS
        /// - KTX
        /// - PVR
        /// </remarks>
        public static Texture FromFile (MemoryBlock memory, TextureFlags flags = TextureFlags.None, int skipMips = 0) {
            TextureInfo info;
            var handle = bgfx_create_texture(memory.ptr, flags, (byte)skipMips, out info);

            return new Texture(handle, ref info);
        }

        /// <summary>
        /// Creates a new 2D texture.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="mipCount">The number of mip levels.</param>
        /// <param name="format">The format of the texture data.</param>
        /// <param name="flags">Flags that control texture behavior.</param>
        /// <param name="memory">If not <c>null</c>, contains the texture's image data.</param>
        /// <returns>
        /// The newly created texture handle.
        /// </returns>
        public static Texture Create2D (int width, int height, int mipCount, TextureFormat format, TextureFlags flags = TextureFlags.None, MemoryBlock? memory = null) {
            var info = new TextureInfo();
            bgfx_calc_texture_size(ref info, (ushort)width, (ushort)height, 1, (byte)mipCount, format);

            var handle = bgfx_create_texture_2d(info.Width, info.Height, info.MipCount, format, flags, memory == null ? null : memory.Value.ptr);
            return new Texture(handle, ref info);
        }

        /// <summary>
        /// Creates a new 3D texture.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="mipCount">The number of mip levels.</param>
        /// <param name="format">The format of the texture data.</param>
        /// <param name="flags">Flags that control texture behavior.</param>
        /// <param name="memory">If not <c>null</c>, contains the texture's image data.</param>
        /// <returns>The newly created texture handle.</returns>
        public static Texture Create3D (int width, int height, int depth, int mipCount, TextureFormat format, TextureFlags flags = TextureFlags.None, MemoryBlock? memory = null) {
            var info = new TextureInfo();
            bgfx_calc_texture_size(ref info, (ushort)width, (ushort)height, (ushort)depth, (byte)mipCount, format);

            var handle = bgfx_create_texture_3d(info.Width, info.Height, info.Depth, info.MipCount, format, flags, memory == null ? null : memory.Value.ptr);
            return new Texture(handle, ref info);
        }

        /// <summary>
        /// Creates a new cube texture.
        /// </summary>
        /// <param name="size">The size of each cube face.</param>
        /// <param name="mipCount">The number of mip levels.</param>
        /// <param name="format">The format of the texture data.</param>
        /// <param name="flags">Flags that control texture behavior.</param>
        /// <param name="memory">If not <c>null</c>, contains the texture's image data.</param>
        /// <returns>
        /// The newly created texture handle.
        /// </returns>
        public static Texture CreateCube (int size, int mipCount, TextureFormat format, TextureFlags flags = TextureFlags.None, MemoryBlock? memory = null) {
            var info = new TextureInfo();
            bgfx_calc_texture_size(ref info, (ushort)size, (ushort)size, 1, (byte)mipCount, format);

            var handle = bgfx_create_texture_cube(info.Width, info.MipCount, format, flags, memory == null ? null : memory.Value.ptr);
            return new Texture(handle, ref info);
        }

        /// <summary>
        /// Releases the texture.
        /// </summary>
        public void Dispose () => bgfx_destroy_texture(handle);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture (MemoryBlock.DataPtr* mem, TextureFlags flags, byte skip, out TextureInfo info);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_2d (ushort width, ushort _height, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_3d (ushort width, ushort _height, ushort _depth, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern ushort bgfx_create_texture_cube (ushort size, byte numMips, TextureFormat format, TextureFlags flags, MemoryBlock.DataPtr* mem);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_destroy_texture (ushort handle);

        [DllImport(Bgfx.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void bgfx_calc_texture_size (ref TextureInfo info, ushort width, ushort height, ushort depth, byte mipCount, TextureFormat format);

        struct TextureInfo {
            public TextureFormat Format;
            public int StorageSize;
            public ushort Width;
            public ushort Height;
            public ushort Depth;
            public byte MipCount;
            public byte BitsPerPixel;
        }
    }
}
