using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SharpBgfx;

namespace Common {
    public class Sample {
        EventQueue eventQueue = new EventQueue();
        Form form;

        public int WindowWidth {
            get;
            private set;
        }

        public int WindowHeight {
            get;
            private set;
        }

        public Sample (string name, int windowWidth, int windowHeight) {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            form = new Form {
                Text = name,
                ClientSize = new Size(windowWidth, windowHeight)
            };

            form.ClientSizeChanged += (o, e) => eventQueue.Post(new SizeEvent(windowWidth, windowHeight));
            form.FormClosed += (o, e) => eventQueue.Post(new Event(EventType.Exit));

            Bgfx.SetWindowHandle(form.Handle);
        }

        public void Run (Action<Sample> renderThread) {
            var thread = new Thread(() => renderThread(this));
            thread.Start();

            Application.Run(form);

            thread.Join();
        }

        public bool ProcessEvents (ResetFlags resetFlags) {
            Event ev;
            bool resizeRequired = false;

            while ((ev = eventQueue.Poll()) != null) {
                switch (ev.Type) {
                    case EventType.Exit:
                        return false;

                    case EventType.Size:
                        var size = (SizeEvent)ev;
                        WindowWidth = size.Width;
                        WindowHeight = size.Height;
                        resizeRequired = true;
                        break;
                }
            }

            if (resizeRequired)
                Bgfx.Reset(WindowWidth, WindowHeight, resetFlags);

            return true;
        }
    }
}
