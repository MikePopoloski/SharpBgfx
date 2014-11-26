using System;

namespace SharpBgfx {
    /// <summary>
    /// Specifies the supported rendering backend APIs.
    /// </summary>
    public enum RendererBackend {
        /// <summary>
        /// No backend given.
        /// </summary>
        Null,

        /// <summary>
        /// Direct3D 9
        /// </summary>
        Direct3D9,

        /// <summary>
        /// Direct3D 11
        /// </summary>
        Direct3D11,

        /// <summary>
        /// OpenGL ES
        /// </summary>
        OpenGLES = 4,

        /// <summary>
        /// OpenGL
        /// </summary>
        OpenGL
    }

    /// <summary>
    /// Specifies vertex attribute usages.
    /// </summary>
    public enum VertexAttribute {
        /// <summary>
        /// Position data.
        /// </summary>
        Position,

        /// <summary>
        /// Normals.
        /// </summary>
        Normal,

        /// <summary>
        /// Tangents.
        /// </summary>
        Tangent,

        /// <summary>
        /// Bitangents.
        /// </summary>
        Bitangent,

        /// <summary>
        /// First color channel.
        /// </summary>
        Color0,

        /// <summary>
        /// Second color channel.
        /// </summary>
        Color1,

        /// <summary>
        /// Indices.
        /// </summary>
        Indices,

        /// <summary>
        /// Animation weights.
        /// </summary>
        Weight,

        /// <summary>
        /// First texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord0,

        /// <summary>
        /// Second texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord1,

        /// <summary>
        /// Third texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord2,

        /// <summary>
        /// Fourth texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord3,

        /// <summary>
        /// Fifth texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord4,

        /// <summary>
        /// Sixth texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord5,

        /// <summary>
        /// Seventh texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord6,

        /// <summary>
        /// Eighth texture coordinate channel (arbitrary data).
        /// </summary>
        TexCoord7
    }

    /// <summary>
    /// Specifies data types for vertex attributes.
    /// </summary>
    public enum VertexAttributeType {
        /// <summary>
        /// One-byte unsigned integer.
        /// </summary>
        UInt8,

        /// <summary>
        /// Two-byte signed integer.
        /// </summary>
        Int16,

        /// <summary>
        /// Two-byte float.
        /// </summary>
        Half,

        /// <summary>
        /// Four-byte float.
        /// </summary>
        Float
    }

    /// <summary>
    /// Specifies the format of a texture's data.
    /// </summary>
    public enum TextureFormat {
        /// <summary>
        /// Block compression with three color channels, 1 bit alpha.
        /// </summary>
        BC1,

        /// <summary>
        /// Block compression with three color channels, 4 bits alpha.
        /// </summary>
        BC2,

        /// <summary>
        /// Block compression with three color channels, 8 bits alpha.
        /// </summary>
        BC3,

        /// <summary>
        /// Block compression for 1-channel color.
        /// </summary>
        BC4,

        /// <summary>
        /// Block compression for 2-channel color.
        /// </summary>
        BC5,

        /// <summary>
        /// Block compression for three-channel HDR color.
        /// </summary>
        BC6H,

        /// <summary>
        /// Highest quality block compression.
        /// </summary>
        BC7,

        /// <summary>
        /// Original ETC block compression.
        /// </summary>
        ETC1,

        /// <summary>
        /// Improved ETC block compression (no alpha).
        /// </summary>
        ETC2,

        /// <summary>
        /// Improved ETC block compression with full alpha.
        /// </summary>
        ETC2A,

        /// <summary>
        /// Improved ETC block compression with 1-bit punchthrough alpha.
        /// </summary>
        ETC2A1,

        /// <summary>
        /// PVRTC1 compression (2 bits per pixel)
        /// </summary>
        PTC12,

        /// <summary>
        /// PVRTC1 compression (4 bits per pixel)
        /// </summary>
        PTC14,

        /// <summary>
        /// PVRTC1 compression with alpha (2 bits per pixel)
        /// </summary>
        PTC12A,

        /// <summary>
        /// PVRTC1 compression with alpha (4 bits per pixel)
        /// </summary>
        PTC14A,

        /// <summary>
        /// PVRTC2 compression with alpha (2 bits per pixel)
        /// </summary>
        PTC22,

        /// <summary>
        /// PVRTC2 compression with alpha (4 bits per pixel)
        /// </summary>
        PTC24,

        /// <summary>
        /// Unknown texture format.
        /// </summary>
        Unknown,

        /// <summary>
        /// 1-bit single channel.
        /// </summary>
        R1,

        /// <summary>
        /// 8-bit single channel.
        /// </summary>
        R8,

        /// <summary>
        /// 16-bit single channel.
        /// </summary>
        R16,

        /// <summary>
        /// 16-bit single channel (float).
        /// </summary>
        R16F,

        /// <summary>
        /// 32-bit single channel.
        /// </summary>
        R32,

        /// <summary>
        /// 32-bit single channel (float).
        /// </summary>
        R32F,

        /// <summary>
        /// 8-bit two channel.
        /// </summary>
        RG8,

        /// <summary>
        /// 16-bit two channel.
        /// </summary>
        RG16,

        /// <summary>
        /// 16-bit two channel (float).
        /// </summary>
        RG16F,

        /// <summary>
        /// 32-bit two channel.
        /// </summary>
        RG32,

        /// <summary>
        /// 32-bit two channel (float).
        /// </summary>
        RG32F,

        /// <summary>
        /// 8-bit BGRA color.
        /// </summary>
        BGRA8,

        /// <summary>
        /// 16-bit RGBA color.
        /// </summary>
        RGBA16,

        /// <summary>
        /// 16-bit RGBA color (float).
        /// </summary>
        RGBA16F,

        /// <summary>
        /// 32-bit RGBA color.
        /// </summary>
        RGBA32,

        /// <summary>
        /// 32-bit RGBA color (float).
        /// </summary>
        RGBA32F,

        /// <summary>
        /// 5-6-6 color.
        /// </summary>
        R5G6B5,

        /// <summary>
        /// 4-bit RGBA color.
        /// </summary>
        RGBA4,

        /// <summary>
        /// 5-bit RGB color with 1-bit alpha.
        /// </summary>
        RGB5A1,

        /// <summary>
        /// 10-bit RGB color with 2-bit alpha.
        /// </summary>
        RGB10A2,

        /// <summary>
        /// 11-11-10 color (float).
        /// </summary>
        R11G11B10F,

        /// <summary>
        /// Unknown depth format.
        /// </summary>
        UnknownDepth,

        /// <summary>
        /// 16-bit depth.
        /// </summary>
        D16,

        /// <summary>
        /// 24-bit depth.
        /// </summary>
        D24,

        /// <summary>
        /// 24-bit depth, 8-bit stencil.
        /// </summary>
        D24S8,

        /// <summary>
        /// 32-bit depth.
        /// </summary>
        D32,

        /// <summary>
        /// 16-bit depth (float).
        /// </summary>
        D16F,

        /// <summary>
        /// 24-bit depth (float).
        /// </summary>
        D24F,

        /// <summary>
        /// 32-bit depth (float).
        /// </summary>
        D32F,

        /// <summary>
        /// 8-bit stencil.
        /// </summary>
        D0S8
    }

    /// <summary>
    /// Specifies the type of uniform data.
    /// </summary>
    public enum UniformType {
        /// <summary>
        /// Single integer.
        /// </summary>
        Int,

        /// <summary>
        /// Single float.
        /// </summary>
        Float,

        /// <summary>
        /// Array of integers.
        /// </summary>
        Int1Array = 3,

        /// <summary>
        /// Array of floats.
        /// </summary>
        Float1Array,

        /// <summary>
        /// Array of 2D vectors.
        /// </summary>
        Float2Array,

        /// <summary>
        /// Array of 3D vectors.
        /// </summary>
        Float3Array,

        /// <summary>
        /// Array of 4D vectors.
        /// </summary>
        Float4Array,

        /// <summary>
        /// Array of 3x3 matrices.
        /// </summary>
        Matrix3x3Array,

        /// <summary>
        /// Array of 4x4 matrices.
        /// </summary>
        Matrix4x4Array
    }

    /// <summary>
    /// Specifies various settings to change during a reset call.
    /// </summary>
    [Flags]
    public enum ResetFlags {
        /// <summary>
        /// No features to change.
        /// </summary>
        None = 0,

        /// <summary>
        /// Not yet supported.
        /// </summary>
        Fullscreen = 0x1,

        /// <summary>
        /// Enable 2x multisampling.
        /// </summary>
        MSAA_X2 = 0x10,

        /// <summary>
        /// Enable 4x multisampling.
        /// </summary>
        MSAA_X4 = 0x20,

        /// <summary>
        /// Enable 8x multisampling.
        /// </summary>
        MSAA_X8 = 0x30,

        /// <summary>
        /// Enable 16x multisampling.
        /// </summary>
        MSAA_X16 = 0x40,

        /// <summary>
        /// Enable v-sync.
        /// </summary>
        Vsync = 0x80,

        /// <summary>
        /// Begin screen capture.
        /// </summary>
        Capture = 0x100
    }

    /// <summary>
    /// Specifies various debug options.
    /// </summary>
    [Flags]
    public enum DebugFlags {
        /// <summary>
        /// Don't enable any debugging features.
        /// </summary>
        None = 0,

        /// <summary>
        /// Force wireframe rendering.
        /// </summary>
        Wireframe = 0x1,

        /// <summary>
        /// When set, all rendering calls are skipped. This is useful when profiling to
        /// find bottlenecks between the CPU and GPU.
        /// </summary>
        InfinitelyFastHardware = 0x2,

        /// <summary>
        /// Display internal statistics.
        /// </summary>
        DisplayStatistics = 0x4,

        /// <summary>
        /// Display debug text.
        /// </summary>
        DisplayText = 0x8
    }

    /// <summary>
    /// Specifies flags for clearing surfaces.
    /// </summary>
    [Flags]
    public enum ClearFlags : byte {
        /// <summary>
        /// Don't clear anything.
        /// </summary>
        None = 0,

        /// <summary>
        /// Clear the color channels.
        /// </summary>
        ColorBit = 0x1,

        /// <summary>
        /// Clear the depth buffer.
        /// </summary>
        DepthBit = 0x2,

        /// <summary>
        /// Clear the stencil buffer.
        /// </summary>
        StencilBit = 0x4
    }

    /// <summary>
    /// Specifies various capabilities supported by the rendering device.
    /// </summary>
    [Flags]
    public enum DeviceFeatures : long {
        /// <summary>
        /// No extra features supported.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Device supports "Less than or equal to" texture comparison mode.
        /// </summary>
        TextureCompareLessEqual = 0x1,

        /// <summary>
        /// Device supports all texture comparison modes.
        /// </summary>
        TextureCompareAll = 0x3,

        /// <summary>
        /// Device supports 3D textures.
        /// </summary>
        Texture3D = 0x4,

        /// <summary>
        /// Device supports 16-bit floats as vertex attributes.
        /// </summary>
        VertexAttributeHalf = 0x8,

        /// <summary>
        /// Device supports instancing.
        /// </summary>
        Instancing = 0x10,

        /// <summary>
        /// Device supports multithreaded rendering.
        /// </summary>
        RendererMultithreaded = 0x20,

        /// <summary>
        /// Fragment shaders can access depth values.
        /// </summary>
        FragmentDepth = 0x40,

        /// <summary>
        /// Device supports independent blending of simultaneous render targets.
        /// </summary>
        BlendIndependent = 0x80,

        /// <summary>
        /// Device supports compute shaders.
        /// </summary>
        Compute = 0x100,

        /// <summary>
        /// Device supports ordering of fragment output.
        /// </summary>
        FragmentOrdering = 0x200,

        /// <summary>
        /// Indicates whether the device can render to multiple swap chains.
        /// </summary>
        SwapChain = 0x400
    }

    /// <summary>
    /// Indicates the level of support for a specific texture format.
    /// </summary>
    public enum TextureFormatSupport {
        /// <summary>
        /// The format is unsupported.
        /// </summary>
        Unsupported,

        /// <summary>
        /// The format is fully supported.
        /// </summary>
        Supported,

        /// <summary>
        /// The format is supported through library emulation.
        /// </summary>
        Emulated
    }

    /// <summary>
    /// Specifies various texture flags.
    /// </summary>
    [Flags]
    public enum TextureFlags {
        /// <summary>
        /// No flags set.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Mirror the texture in the U coordinate.
        /// </summary>
        MirrorU = 0x00000001,

        /// <summary>
        /// Clamp the texture in the U coordinate.
        /// </summary>
        ClampU = 0x00000002,

        /// <summary>
        /// Mirror the texture in the V coordinate.
        /// </summary>
        MirrorV = 0x00000004,

        /// <summary>
        /// Clamp the texture in the V coordinate.
        /// </summary>
        ClampV = 0x00000008,

        /// <summary>
        /// Mirror the texture in the W coordinate.
        /// </summary>
        MirrorW = 0x00000010,

        /// <summary>
        /// Clamp the texture in the W coordinate.
        /// </summary>
        ClampW = 0x00000020,

        /// <summary>
        /// Use point filtering for texture minification.
        /// </summary>
        MinFilterPoint = 0x00000040,

        /// <summary>
        /// Use anisotropic filtering for texture minification.
        /// </summary>
        MinFilterAnisotropic = 0x00000080,

        /// <summary>
        /// Use point filtering for texture magnification.
        /// </summary>
        MagFilterPoint = 0x00000100,

        /// <summary>
        /// Use anisotropic filtering for texture magnification.
        /// </summary>
        MagFilterAnisotropic = 0x00000200,

        /// <summary>
        /// Use point filtering for texture mipmaps.
        /// </summary>
        MipFilterPoint = 0x00000400,

        /// <summary>
        /// The texture will be used as a render target.
        /// </summary>
        RenderTarget = 0x00001000,

        /// <summary>
        /// The render target texture support 2x multisampling.
        /// </summary>
        RenderTargetMultisample2x = 0x00002000,

        /// <summary>
        /// The render target texture support 4x multisampling.
        /// </summary>
        RenderTargetMultisample4x = 0x00003000,

        /// <summary>
        /// The render target texture support 8x multisampling.
        /// </summary>
        RenderTargetMultisample8x = 0x00004000,

        /// <summary>
        /// The render target texture support 16x multisampling.
        /// </summary>
        RenderTargetMultisample16x = 0x00005000,
        RenderTargetBufferOnly = 0x00008000,

        /// <summary>
        /// Use a "less than" operator when comparing textures.
        /// </summary>
        CompareLess = 0x00010000,

        /// <summary>
        /// Use a "less than or equal" operator when comparing textures.
        /// </summary>
        CompareLessEqual = 0x00020000,

        /// <summary>
        /// Use an equality operator when comparing textures.
        /// </summary>
        CompareEqual = 0x00030000,

        /// <summary>
        /// Use a "greater than or equal" operator when comparing textures.
        /// </summary>
        CompareGreaterEqual = 0x00040000,

        /// <summary>
        /// Use a "greater than" operator when comparing textures.
        /// </summary>
        CompareGreater = 0x00050000,

        /// <summary>
        /// Use an inequality operator when comparing textures.
        /// </summary>
        CompareNotEqual = 0x00060000,

        /// <summary>
        /// Never compare two textures as equal.
        /// </summary>
        CompareNever = 0x00070000,

        /// <summary>
        /// Always compare two textures as equal.
        /// </summary>
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
