using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBgfx {
    /// <summary>
    /// Specifies state information used to configure rendering operations.
    /// </summary>
    public struct RenderState : IEquatable<RenderState> {
        const int AlphaRefShift = 40;
        const int PointSizeShift = 52;
        const ulong AlphaRefMask = 0x0000ff0000000000;
        const ulong PointSizeMask = 0x0ff0000000000000;

        ulong value;

        /// <summary>
        /// No state bits set.
        /// </summary>
        public static readonly RenderState None = 0;

        /// <summary>
        /// Enable writing color data to the framebuffer.
        /// </summary>
        public static readonly RenderState ColorWrite = 0x0000000000000001;

        /// <summary>
        /// Enable writing alpha data to the framebuffer.
        /// </summary>
        public static readonly RenderState AlphaWrite = 0x0000000000000002;

        /// <summary>
        /// Enable writing to the depth buffer.
        /// </summary>
        public static readonly RenderState DepthWrite = 0x0000000000000004;

        /// <summary>
        /// Use a "less than" comparison to pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestLess = 0x0000000000000010;

        /// <summary>
        /// Use a "less than or equal to" comparison to pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestLessEqual = 0x0000000000000020;

        /// <summary>
        /// Pass the depth test if both values are equal.
        /// </summary>
        public static readonly RenderState DepthTestEqual = 0x0000000000000030;

        /// <summary>
        /// Use a "greater than or equal to" comparison to pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestGreaterEqual = 0x0000000000000040;

        /// <summary>
        /// Use a "greater than" comparison to pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestGreater = 0x0000000000000050;

        /// <summary>
        /// Pass the depth test if both values are not equal.
        /// </summary>
        public static readonly RenderState DepthTestNotEqual = 0x0000000000000060;

        /// <summary>
        /// Never pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestNever = 0x0000000000000070;

        /// <summary>
        /// Always pass the depth test.
        /// </summary>
        public static readonly RenderState DepthTestAlways = 0x0000000000000080;

        /// <summary>
        /// Use a value of 0 as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendZero = 0x0000000000001000;

        /// <summary>
        /// Use a value of 1 as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendOne = 0x0000000000002000;

        /// <summary>
        /// Use the source pixel color as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendSourceColor = 0x0000000000003000;

        /// <summary>
        /// Use one minus the source pixel color as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendInvSourceColor = 0x0000000000004000;

        /// <summary>
        /// Use the source pixel alpha as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendSourceAlpha = 0x0000000000005000;

        /// <summary>
        /// Use one minus the source pixel alpha as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendInvSourceAlpha = 0x0000000000006000;

        /// <summary>
        /// Use the destination pixel alpha as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendDestAlpha = 0x0000000000007000;

        /// <summary>
        /// Use one minus the destination pixel alpha as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendInvDestAlpha = 0x0000000000008000;

        /// <summary>
        /// Use the destination pixel color as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendDestColor = 0x0000000000009000;

        /// <summary>
        /// Use one minus the destination pixel color as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendInvDestColor = 0x000000000000a000;

        /// <summary>
        /// Use the source pixel alpha (saturated) as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendSourceAlphaSaturate = 0x000000000000b000;

        /// <summary>
        /// Use an application supplied blending factor as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendFactor = 0x000000000000c000;

        /// <summary>
        /// Use one minus an application supplied blending factor as an input to a blend equation.
        /// </summary>
        public static readonly RenderState BlendInvFactor = 0x000000000000d000;

        /// <summary>
        /// Blend equation: A + B
        /// </summary>
        public static readonly RenderState BlendEquationAdd = 0x0000000000000000;

        /// <summary>
        /// Blend equation: B - A
        /// </summary>
        public static readonly RenderState BlendEquationSub = 0x0000000010000000;

        /// <summary>
        /// Blend equation: A - B
        /// </summary>
        public static readonly RenderState BlendEquationReverseSub = 0x0000000020000000;

        /// <summary>
        /// Blend equation: min(a, b)
        /// </summary>
        public static readonly RenderState BlendEquationMin = 0x0000000030000000;

        /// <summary>
        /// Blend equation: max(a, b)
        /// </summary>
        public static readonly RenderState BlendEquationMax = 0x0000000040000000;

        /// <summary>
        /// Enable independent blending of simultaenous render targets.
        /// </summary>
        public static readonly RenderState BlendIndependent = 0x0000000400000000;

        /// <summary>
        /// Don't perform culling of back faces.
        /// </summary>
        public static readonly RenderState NoCulling = 0x0000000000000000;

        /// <summary>
        /// Perform culling of clockwise faces.
        /// </summary>
        public static readonly RenderState CullClockwise = 0x0000001000000000;

        /// <summary>
        /// Perform culling of counter-clockwise faces.
        /// </summary>
        public static readonly RenderState CullCounterclockwise = 0x0000002000000000;

        /// <summary>
        /// Primitive topology: triangle list.
        /// </summary>
        public static readonly RenderState PrimitiveTriangles = 0x0000000000000000;

        /// <summary>
        /// Primitive topology: triangle strip.
        /// </summary>
        public static readonly RenderState PrimitiveTriangleStrip = 0x0001000000000000;

        /// <summary>
        /// Primitive topology: line list.
        /// </summary>
        public static readonly RenderState PrimitiveLines = 0x0002000000000000;

        /// <summary>
        /// Primitive topology: line strip.
        /// </summary>
        public static readonly RenderState PrimitiveLineStrip = 0x0003000000000000;

        /// <summary>
        /// Primitive topology: point list.
        /// </summary>
        public static readonly RenderState PrimitivePoints = 0x0004000000000000;

        /// <summary>
        /// Enable multisampling.
        /// </summary>
        public static readonly RenderState Multisampling = 0x1000000000000000;

        /// <summary>
        /// Provides a set of sane defaults.
        /// </summary>
        public static readonly RenderState Default =
            ColorWrite |
            AlphaWrite |
            DepthWrite |
            DepthTestLess |
            CullClockwise |
            Multisampling;

        /// <summary>
        /// Predefined blend effect: additive blending.
        /// </summary>
        public static readonly RenderState BlendAdd = BlendFunction(RenderState.BlendOne, RenderState.BlendOne);

        /// <summary>
        /// Predefined blend effect: alpha blending.
        /// </summary>
        public static readonly RenderState BlendAlpha = BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInvSourceAlpha);

        /// <summary>
        /// Predefined blend effect: "darken" blending.
        /// </summary>
        public static readonly RenderState BlendDarken = BlendFunction(RenderState.BlendOne, RenderState.BlendOne) | BlendEquation(RenderState.BlendEquationMin);

        /// <summary>
        /// Predefined blend effect: "lighten" blending.
        /// </summary>
        public static readonly RenderState BlendLighten = BlendFunction(RenderState.BlendOne, RenderState.BlendOne) | BlendEquation(RenderState.BlendEquationMax);

        /// <summary>
        /// Predefined blend effect: multiplicative blending.
        /// </summary>
        public static readonly RenderState BlendMultiply = BlendFunction(RenderState.BlendDestColor, RenderState.BlendZero);

        /// <summary>
        /// Predefined blend effect: normal blending based on alpha.
        /// </summary>
        public static readonly RenderState BlendNormal = BlendFunction(RenderState.BlendOne, RenderState.BlendInvSourceAlpha);

        /// <summary>
        /// Predefined blend effect: "screen" blending.
        /// </summary>
        public static readonly RenderState BlendScreen = BlendFunction(RenderState.BlendOne, RenderState.BlendInvSourceColor);

        /// <summary>
        /// Predefined blend effect: "linear burn" blending.
        /// </summary>
        public static readonly RenderState BlendLinearBurn = BlendFunction(RenderState.BlendDestColor, RenderState.BlendInvDestColor) | BlendEquation(RenderState.BlendEquationSub);

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderState"/> struct.
        /// </summary>
        /// <param name="value">The integer value of the state.</param>
        public RenderState (ulong value) {
            this.value = value;
        }

        public static RenderState AlphaRef (byte alpha) => (((ulong)alpha) << AlphaRefShift) & AlphaRefMask;

        public static RenderState PointSize (byte size) => (((ulong)size) << PointSizeShift) & PointSizeMask;

        public static RenderState BlendFunction (RenderState source, RenderState destination) => BlendFunction(source, destination, source, destination);

        public static RenderState BlendFunction (RenderState sourceColor, RenderState destinationColor, RenderState sourceAlpha, RenderState destinationAlpha) {
            return (sourceColor | (destinationColor << 4)) | ((sourceAlpha | (destinationAlpha << 4)) << 8);
        }

        public static RenderState BlendEquation (RenderState equation) => BlendEquation(equation, equation);

        public static RenderState BlendEquation (RenderState sourceEquation, RenderState alphaEquation) => sourceEquation | (alphaEquation << 3);

        public override int GetHashCode () => value.GetHashCode();

        public bool Equals (RenderState other) => value == other.value;

        public override bool Equals (object obj) {
            var state = obj as RenderState?;
            if (state == null)
                return false;

            return Equals(state);
        }

        public static bool operator ==(RenderState left, RenderState right) => left.Equals(right);

        public static bool operator !=(RenderState left, RenderState right) => !left.Equals(right);

        public static implicit operator RenderState (ulong value) => new RenderState(value);

        public static explicit operator ulong (RenderState state) => state.value;

        public static RenderState operator |(RenderState left, RenderState right) => left.value | right.value;

        public static RenderState operator &(RenderState left, RenderState right) => left.value & right.value;

        public static RenderState operator ~(RenderState state) => ~state.value;

        public static RenderState operator <<(RenderState state, int amount) => state.value << amount;

        public static RenderState operator >>(RenderState state, int amount) => state.value >> amount;
    }
}
