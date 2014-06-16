using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpBgfx;

namespace DrawStress {
    unsafe static class Program {
        static void Main () {
            var form = new Form {
                Text = "Draw Stress Test",
                ClientSize = new Size(920, 760)
            };

            Bgfx.SetWindowHandle(form.Handle);
            Bgfx.Init(RendererType.OpenGL, IntPtr.Zero, IntPtr.Zero);

            var renderers = Bgfx.GetSupportedRenderers();

            var current = Bgfx.GetCurrentRenderer();
            var name = Bgfx.GetRendererName(current);

            var caps = Bgfx.GetCaps();

            Application.Run(form);

            Bgfx.Shutdown();
        }
    }
}
