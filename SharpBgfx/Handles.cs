using System;
using System.Runtime.InteropServices;

namespace SharpBgfx {
    // 2-byte opaque handles

    [StructLayout(LayoutKind.Sequential, Size = 2)]
    public struct IndexBufferHandle {
    }

    [StructLayout(LayoutKind.Sequential, Size = 2)]
    public struct VertexBufferHandle {
    }

    [StructLayout(LayoutKind.Sequential, Size = 2)]
    public struct ShaderHandle {
    }

    [StructLayout(LayoutKind.Sequential, Size = 2)]
    public struct ProgramHandle {
    }
}
