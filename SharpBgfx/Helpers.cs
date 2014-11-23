using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBgfx {
    public static class StencilBits {
        public static StencilFlags ReferenceValue (byte reference) {
            return (StencilFlags)(((uint)reference) & RefMask);
        }

        public static StencilFlags ReadMask (byte mask) {
            return (StencilFlags)((((uint)mask) << ReadMaskShift) & ReadMaskMask);
        }

        const int ReadMaskShift = 8;
        const int RefMask = 0x000000ff;
        const int ReadMaskMask = 0x0000ff00;
    }
}
