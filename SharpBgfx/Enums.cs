using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBgfx {
    public enum RendererType {
        Null,
        Direct3D9,
        Direct3D11,
        OpenGLES,
        OpenGL
    }

    public enum VertexAttribute {
        Position,
        Normal,
        Tangent,
        Color0,
        Color1,
        Indices,
        Weight,
        TexCoord0,
        TexCoord1,
        TexCoord2,
        TexCoord3,
        TexCoord4,
        TexCoord5,
        TexCoord6,
        TexCoord7
    }

    public enum VertexAttributeType {
        UInt8,
        Int16,
        Half,
        Float
    }

    [Flags]
    public enum ResetFlags {
        None = 0,
        Fullscreen = 0x1,
        MSAA_X2 = 0x10,
        MSAA_X4 = 0x20,
        MSAA_X8 = 0x30,
        MSAA_X16 = 0x40,
        Vsync = 0x80,
        Capture = 0x100
    }

    [Flags]
    public enum DebugFlags {
        None = 0,
        Wireframe = 0x1,
        InfinitelyFastHardware = 0x2,
        DisplayStatistics = 0x4,
        DisplayText = 0x8
    }

    [Flags]
    public enum ClearFlags {
        None = 0,
        ColorBit = 0x1,
        DepthBit = 0x2,
        StencilBit = 0x4
    }

    [Flags]
    public enum CapsFlags : long {
        None = 0,
        TextureFormatBC1 = 0x00000001,
        TextureFormatBC2 = 0x00000002,
        TextureFormatBC3 = 0x00000004,
        TextureFormatBC4 = 0x00000008,
        TextureFormatBC5 = 0x00000010,
        TextureFormatETC1 = 0x00000020,
        TextureFormatETC2 = 0x00000040,
        TextureFormatETC2A = 0x00000080,
        TextureFormatETC2A1 = 0x00000100,
        TextureFormatPTC12 = 0x00000200,
        TextureFormatPTC14 = 0x00000400,
        TextureFormatPTC14A = 0x00000800,
        TextureFormatPTC12A = 0x00001000,
        TextureFormatPTC22 = 0x00002000,
        TextureFormatPTC24 = 0x00004000,
        TextureFormatD16 = 0x00008000,
        TextureFormatD24 = 0x00010000,
        TextureFormatD24S8 = 0x00020000,
        TextureFormatD32 = 0x00040000,
        TextureFormatD16F = 0x00080000,
        TextureFormatD24F = 0x00100000,
        TextureFormatD32F = 0x00200000,
        TextureFormatD0S8 = 0x00400000,
        DepthTextureCompare = 0x01000000,
        ShadowSamplerCompare = 0x02000000,
        Texture3D = 0x04000000,
        VertexAttributeHalf = 0x08000000,
        Instancing = 0x10000000,
        RendererMultithreaded = 0x20000000,
        FragmentDepth = 0x40000000,
        BlendIndependent = 0x80000000,

        TextureDepthMask =
            TextureFormatD16 |
            TextureFormatD16F |
            TextureFormatD24 |
            TextureFormatD24F |
            TextureFormatD24S8 |
            TextureFormatD32 |
            TextureFormatD32F |
            TextureFormatD0S8
    }
}
