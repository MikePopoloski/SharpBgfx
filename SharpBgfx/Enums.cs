using System;

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

    public enum TextureFormat {
        BC1,
        BC2,
        BC3,
        BC4,
        BC5,
        ETC1,
        ETC2,
        ETC2A,
        ETC2A1,
        PTC12,
        PTC14,
        PTC12A,
        PTC14A,
        PTC22,
        PTC24,

        Unknown,

        R8,
        R16,
        R16F,
        BGRA8,
        RGBA16,
        RGBA16F,
        R5G6B5,
        RGBA4,
        RGB5A1,
        RGB10A2,

        UnknownDepth,

        D16,
        D24,
        D24S8,
        D32,
        D16F,
        D24F,
        D32F,
        D0S8
    }

    public enum UniformType {
        Int,
        Float,
        Int1Array,
        Float1Array,
        Float2Array,
        Float3Array,
        Float4Array,
        Matrix3x3Array,
        Matrix4x4Array
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

        TextureCompareAll = DepthTextureCompare | ShadowSamplerCompare,

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

    [Flags]
    public enum RenderState : long {
        None = 0,
        ColorWrite = 0x0000000000000001,
        AlphaWrite = 0x0000000000000002,
        DepthWrite = 0x0000000000000004,
        DepthTestLess = 0x0000000000000010,
        DepthTestLessEqual = 0x0000000000000020,
        DepthTestEqual = 0x0000000000000030,
        DepthTestGreaterEqual = 0x0000000000000040,
        DepthTestGreater = 0x0000000000000050,
        DepthTestNotEqual = 0x0000000000000060,
        DepthTestNever = 0x0000000000000070,
        DepthTestAlways = 0x0000000000000080,
        BlendZero = 0x0000000000001000,
        BlendOne = 0x0000000000002000,
        BlendSourceColor = 0x0000000000003000,
        BlendInvSourceColor = 0x0000000000004000,
        BlendSourceAlpha = 0x0000000000005000,
        BlendInvSourceAlpha = 0x0000000000006000,
        BlendDestAlpha = 0x0000000000007000,
        BlendInvDestAlpha = 0x0000000000008000,
        BlendDestColor = 0x0000000000009000,
        BlendInvDestColor = 0x000000000000a000,
        BlendSourceAlphaSaturate = 0x000000000000b000,
        BlendFactor = 0x000000000000c000,
        BlendInvFactor = 0x000000000000d000,
        BlendEquationSub = 0x0000000010000000,
        BlendEquationReverseSub = 0x0000000020000000,
        BlendEquationMin = 0x0000000030000000,
        BlendEquationMax = 0x0000000040000000,
        BlendIndependent = 0x0000000400000000,
        CullClockwise = 0x0000001000000000,
        CullCounterclockwise = 0x0000002000000000,
        PrimitiveLineStrip = 0x0001000000000000,
        PrimitiveLines = 0x0002000000000000,
        PrimitivePoints = 0x0003000000000000,
        Multisampling = 0x1000000000000000,

        Default =
            ColorWrite |
            AlphaWrite |
            DepthWrite |
            DepthTestLess |
            CullClockwise |
            Multisampling
    }

    [Flags]
    public enum TextureFlags {
        None = 0x00000000,
        MirrorU = 0x00000001,
        ClampU = 0x00000002,
        MirrorV = 0x00000004,
        ClampV = 0x00000008,
        MirrorW = 0x00000010,
        ClampW = 0x00000020,
        MinFilterPoint = 0x00000040,
        MinFilterAnisotropic = 0x00000080,
        MagFilterPoint = 0x00000100,
        MagFilterAnisotropic = 0x00000200,
        MipFilterPoint = 0x00000400,
        RenderTarget = 0x00001000,
        RenderTargetMultisampleX2 = 0x00002000,
        RenderTargetMultisampleX4 = 0x00003000,
        RenderTargetMultisampleX8 = 0x00004000,
        RenderTargetMultisampleX16 = 0x00005000,
        RenderTargetBufferOnly = 0x00008000,
        CompareLess = 0x00010000,
        CompareLessEqual = 0x00020000,
        CompareEqual = 0x00030000,
        CompareGreaterEqual = 0x00040000,
        CompareGreater = 0x00050000,
        CompareNotEqual = 0x00060000,
        CompareNever = 0x00070000,
        CompareAlways = 0x00080000,
    }

    [Flags]
    public enum StencilFlags {
        None = 0,
        TestLess = 0x00010000,
        TestLessEqual = 0x00020000,
        TestEqual = 0x00030000,
        TestGreaterEqual = 0x00040000,
        TestGreater = 0x00050000,
        TestNotEqual = 0x00060000,
        TestNever = 0x00070000,
        TestAlways = 0x00080000,
        FailSZero = 0x00000000,
        FailSKeep = 0x00100000,
        FailSReplace = 0x00200000,
        FailSIncrement = 0x00300000,
        FailSIncrementSaturate = 0x00400000,
        FailSDecrement = 0x00500000,
        FailSDecrementSaturate = 0x00600000,
        FailSInvert = 0x00700000,
        FailZZero = 0x00000000,
        FailZKeep = 0x01000000,
        FailZReplace = 0x02000000,
        FailZIncrement = 0x03000000,
        FailZIncrementSaturate = 0x04000000,
        FailZDecrement = 0x05000000,
        FailZDecrementSaturate = 0x06000000,
        FailZInvert = 0x07000000,
        PassZZero = 0x00000000,
        PassZKeep = 0x10000000,
        PassZReplace = 0x20000000,
        PassZIncrement = 0x30000000,
        PassZIncrementSaturate = 0x40000000,
        PassZDecrement = 0x50000000,
        PassZDecrementSaturate = 0x60000000,
        PassZInvert = 0x70000000
    }
}
