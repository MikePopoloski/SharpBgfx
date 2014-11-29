using System;

namespace SharpBgfx {
    /// <summary>
    /// Specifies state information used to configure rendering operations.
    /// </summary>
    public struct StencilFlags : IEquatable<StencilFlags> {
        const int ReadMaskShift = 8;
        const uint RefMask = 0x000000ff;
        const uint ReadMaskMask = 0x0000ff00;

        uint value;

        public static readonly StencilFlags None = 0;
        public static readonly StencilFlags TestLess = 0x00010000;
        public static readonly StencilFlags TestLessEqual = 0x00020000;
        public static readonly StencilFlags TestEqual = 0x00030000;
        public static readonly StencilFlags TestGreaterEqual = 0x00040000;
        public static readonly StencilFlags TestGreater = 0x00050000;
        public static readonly StencilFlags TestNotEqual = 0x00060000;
        public static readonly StencilFlags TestNever = 0x00070000;
        public static readonly StencilFlags TestAlways = 0x00080000;
        public static readonly StencilFlags FailSZero = 0x00000000;
        public static readonly StencilFlags FailSKeep = 0x00100000;
        public static readonly StencilFlags FailSReplace = 0x00200000;
        public static readonly StencilFlags FailSIncrement = 0x00300000;
        public static readonly StencilFlags FailSIncrementSaturate = 0x00400000;
        public static readonly StencilFlags FailSDecrement = 0x00500000;
        public static readonly StencilFlags FailSDecrementSaturate = 0x00600000;
        public static readonly StencilFlags FailSInvert = 0x00700000;
        public static readonly StencilFlags FailZZero = 0x00000000;
        public static readonly StencilFlags FailZKeep = 0x01000000;
        public static readonly StencilFlags FailZReplace = 0x02000000;
        public static readonly StencilFlags FailZIncrement = 0x03000000;
        public static readonly StencilFlags FailZIncrementSaturate = 0x04000000;
        public static readonly StencilFlags FailZDecrement = 0x05000000;
        public static readonly StencilFlags FailZDecrementSaturate = 0x06000000;
        public static readonly StencilFlags FailZInvert = 0x07000000;
        public static readonly StencilFlags PassZZero = 0x00000000;
        public static readonly StencilFlags PassZKeep = 0x10000000;
        public static readonly StencilFlags PassZReplace = 0x20000000;
        public static readonly StencilFlags PassZIncrement = 0x30000000;
        public static readonly StencilFlags PassZIncrementSaturate = 0x40000000;
        public static readonly StencilFlags PassZDecrement = 0x50000000;
        public static readonly StencilFlags PassZDecrementSaturate = 0x60000000;
        public static readonly StencilFlags PassZInvert = 0x70000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="StencilFlags"/> struct.
        /// </summary>
        /// <param name="value">The integer value of the state.</param>
        public StencilFlags (int value) {
            this.value = (uint)value;
        }

        public static StencilFlags ReferenceValue (byte reference) => reference & RefMask;

        public static StencilFlags ReadMask (byte mask) => (((uint)mask) << ReadMaskShift) & ReadMaskMask;

        public override int GetHashCode () => value.GetHashCode();

        public bool Equals (StencilFlags other) => value == other.value;

        public override bool Equals (object obj) {
            var state = obj as StencilFlags?;
            if (state == null)
                return false;

            return Equals(state);
        }

        public static bool operator ==(StencilFlags left, StencilFlags right) => left.Equals(right);

        public static bool operator !=(StencilFlags left, StencilFlags right) => !left.Equals(right);

        [CLSCompliant(false)]
        public static implicit operator StencilFlags (uint value) => new StencilFlags((int)value);

        [CLSCompliant(false)]
        public static explicit operator uint (StencilFlags state) => state.value;

        public static StencilFlags operator |(StencilFlags left, StencilFlags right) => left.value | right.value;

        public static StencilFlags operator &(StencilFlags left, StencilFlags right) => left.value & right.value;

        public static StencilFlags operator ~(StencilFlags state) => ~state.value;

        public static StencilFlags operator <<(StencilFlags state, int amount) => state.value << amount;

        public static StencilFlags operator >>(StencilFlags state, int amount) => state.value >> amount;
    }
}