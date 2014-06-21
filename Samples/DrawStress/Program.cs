using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using SharpBgfx;

namespace DrawStress {
    static class Program {
        static EventQueue eventQueue = new EventQueue();
        static int windowWidth = 1280;
        static int windowHeight = 720;

        static void Main () {
            var form = new Form {
                Text = "Draw Stress Test",
                ClientSize = new Size(windowWidth, windowHeight)
            };

            form.ClientSizeChanged += (o, e) => eventQueue.Post(new SizeEvent(form.ClientSize.Width, form.ClientSize.Height));
            form.FormClosed += (o, e) => eventQueue.Post(new Event(EventType.Exit));

            Bgfx.SetWindowHandle(form.Handle);

            eventQueue.Post(new SizeEvent(form.ClientSize.Width, form.ClientSize.Height));

            var thread = new Thread(RenderThread);
            thread.Start();

            Application.Run(form);

            thread.Join();
        }

        static void MainLoop () {
            Bgfx.SetViewRect(0, 0, 0, (ushort)windowWidth, (ushort)windowHeight);
            Bgfx.Submit(0, 0);

            Bgfx.Frame();
        }

        static void RenderThread () {
            Bgfx.Init(RendererType.OpenGL, IntPtr.Zero, IntPtr.Zero);
            Bgfx.Reset(windowWidth, windowHeight, ResetFlags.None);
            Bgfx.SetDebugFlags(DebugFlags.DisplayText);

            Bgfx.SetViewClear(0, ClearFlags.ColorBit | ClearFlags.DepthBit, 0x303030ff, 1.0f, 0);

            while (ProcessEvents())
                MainLoop();

            Bgfx.Shutdown();
        }

        static bool ProcessEvents () {
            Event ev;
            bool resizeRequired = false;

            while ((ev = eventQueue.Poll()) != null) {
                switch (ev.Type) {
                    case EventType.Exit:
                        return false;

                    case EventType.Size:
                        var size = (SizeEvent)ev;
                        windowWidth = size.Width;
                        windowHeight = size.Height;
                        resizeRequired = true;
                        break;
                }
            }

            if (resizeRequired)
                Bgfx.Reset(windowWidth, windowHeight, ResetFlags.None);

            return true;
        }
    }
}
