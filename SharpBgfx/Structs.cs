using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBgfx {
    public struct Caps {
        public RendererType RendererType;
        public CapsFlags Supported;
        public CapsFlags Emulated;
        public short MaxTextureSize;
        public short MaxDrawCalls;
        public byte MaxFramebufferAttachements;
    }
}
