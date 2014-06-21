using System;
using Common;
using SharpBgfx;

static class Program {
    static void Main () {
        var sample = new Sample("Draw Stress Test", 1280, 720);
        sample.Run(RenderThread);
    }

    static void RenderThread (Sample sample) {
        Bgfx.Init(RendererType.OpenGL, IntPtr.Zero, IntPtr.Zero);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.None);
        Bgfx.SetDebugFlags(DebugFlags.DisplayText);

        Bgfx.SetViewClear(0, ClearFlags.ColorBit | ClearFlags.DepthBit, 0x303030ff, 1.0f, 0);

        while (sample.ProcessEvents(ResetFlags.None))
            MainLoop(sample.WindowWidth, sample.WindowHeight);

        Bgfx.Shutdown();
    }

    static void MainLoop (int windowWidth, int windowHeight) {
        Bgfx.SetViewRect(0, 0, 0, (ushort)windowWidth, (ushort)windowHeight);
        Bgfx.Submit(0, 0);

        Bgfx.Frame();
    }
}
