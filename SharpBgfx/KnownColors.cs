using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBgfx {
    /// <summary>
    /// Provides a set of predefined color values for convenience.
    /// </summary>
    public static class KnownColors {
        public static readonly Color4 Red = new Color4(255, 0, 0);
        public static readonly Color4 Green = new Color4(0, 255, 0);
        public static readonly Color4 Blue = new Color4(0, 0, 255);
        public static readonly Color4 White = new Color4(255, 255, 255);
        public static readonly Color4 Black = new Color4(0, 0, 0);
        public static readonly Color4 TransparentBlack = new Color4(0, 0, 0, 0);
        public static readonly Color4 TransparentWhite = new Color4(255, 255, 255, 0);

        public static readonly Color4 Yellow = new Color4(255, 255, 0);
        public static readonly Color4 Purple = new Color4(128, 0, 128);
    }
}
