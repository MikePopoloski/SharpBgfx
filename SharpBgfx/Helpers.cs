using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBgfx {
    public static class RenderStates {
        public static readonly RenderState BlendAdd = BlendFunction(RenderState.BlendOne, RenderState.BlendOne);
        public static readonly RenderState BlendAlpha = BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInvSourceAlpha);
        public static readonly RenderState BlendDarken = BlendFunction(RenderState.BlendOne, RenderState.BlendOne) | BlendEquation(RenderState.BlendEquationMin);
        public static readonly RenderState BlendLighten = BlendFunction(RenderState.BlendOne, RenderState.BlendOne) | BlendEquation(RenderState.BlendEquationMax);
        public static readonly RenderState BlendMultiply = BlendFunction(RenderState.BlendDestColor, RenderState.BlendZero);
        public static readonly RenderState BlendNormal = BlendFunction(RenderState.BlendOne, RenderState.BlendInvSourceAlpha);
        public static readonly RenderState BlendScreen = BlendFunction(RenderState.BlendOne, RenderState.BlendInvSourceColor);
        public static readonly RenderState BlendLinearBurn = BlendFunction(RenderState.BlendDestColor, RenderState.BlendInvDestColor) | BlendEquation(RenderState.BlendEquationSub);

        public static RenderState AlphaRef (byte alpha) {
            return (RenderState)((((ulong)alpha) << AlphaRefShift) & AlphaRefMask);
        }

        public static RenderState PointSize (byte size) {
            return (RenderState)((((ulong)size) << PointSizeShift) & PointSizeMask);
        }

        public static RenderState BlendFunction (RenderState source, RenderState destination) {
            return BlendFunction(source, destination, source, destination);
        }

        public static RenderState BlendFunction (RenderState sourceColor, RenderState destinationColor, RenderState sourceAlpha, RenderState destinationAlpha) {
            return (RenderState)(
                (((ulong)sourceColor) | ((ulong)destinationColor) << 4) |
                ((((ulong)sourceAlpha) | ((ulong)destinationAlpha) << 4) << 8));
        }

        public static RenderState BlendEquation (RenderState equation) {
            return BlendEquation(equation, equation);
        }

        public static RenderState BlendEquation (RenderState sourceEquation, RenderState alphaEquation) {
            return (RenderState)(((ulong)sourceEquation) | (((ulong)alphaEquation) << 3));
        }

        const int AlphaRefShift = 40;
        const int PointSizeShift = 52;
        const ulong AlphaRefMask = 0x0000ff0000000000;
        const ulong PointSizeMask = 0x0ff0000000000000;
    }

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
