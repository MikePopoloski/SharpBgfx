using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

        public uint Hash;
        public ushort Stride;
        public fixed ushort Offset[MaxAttribCount];
        public fixed byte Attributes[MaxAttribCount];
    }
}
