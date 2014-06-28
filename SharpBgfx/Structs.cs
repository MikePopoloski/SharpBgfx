using System.Runtime.InteropServices;

namespace SharpBgfx {
    public struct Caps {
        public RendererType RendererType;
        public CapsFlags Supported;
        public CapsFlags Emulated;
        public ushort MaxTextureSize;
        public ushort MaxDrawCalls;
        public byte MaxFramebufferAttachements;
    }

    public unsafe struct VertexDecl {
        const int MaxAttribCount = 15;

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(VertexDecl));

        public uint Hash;
        public ushort Stride;
        public fixed ushort Offset[MaxAttribCount];
        public fixed byte Attributes[MaxAttribCount];
    }

    unsafe struct GraphicsMemory {
        public byte* Data;
        public int Size;
    }
}
