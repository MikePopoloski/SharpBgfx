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

    static class MathHelpers {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if (value.CompareTo(min) < 0)
                return min;

            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }

        public static byte Lerp (byte start, byte end, float amount) {
            return (byte)(start + (byte)(amount * (end - start)));
        }
    }
}
