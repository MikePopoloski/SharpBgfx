using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    

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
