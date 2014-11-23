using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    public unsafe struct Caps {
        const int TextureFormatCount = 48;

        public RendererBackend Backend;
        public CapsFlags Supported;
        public ushort MaxTextureSize;
        public ushort MaxDrawCalls;
        public byte MaxFramebufferAttachements;

        public fixed byte Formats[TextureFormatCount];
    }

    public struct TextureInfo {
        public TextureFormat Format;
        public int StorageSize;
        public short Width;
        public short Height;
        public short Depth;
        public byte MipCount;
        public byte BitsPerPixel;
    }
}
