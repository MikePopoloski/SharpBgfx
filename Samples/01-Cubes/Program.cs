using System;
using Common;
using SharpBgfx;

static class Program {
    static void Main () {
        // create a UI thread and kick off a separate render thread
        var sample = new Sample("Cubes", 1280, 720);
        sample.Run(RenderThread);
    }

    static void RenderThread (Sample sample) {
        // initialize the renderer
        Bgfx.Init(RendererType.OpenGL, IntPtr.Zero, IntPtr.Zero);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.Vsync);

        // enable debug text
        Bgfx.SetDebugFlags(DebugFlags.DisplayText);

        // set view 0 clear state
        Bgfx.SetViewClear(0, ClearFlags.ColorBit | ClearFlags.DepthBit, 0x303030ff, 1.0f, 0);

        // main loop
        while (sample.ProcessEvents(ResetFlags.Vsync)) {
            // set view 0 viewport
            Bgfx.SetViewRect(0, 0, 0, (ushort)sample.WindowWidth, (ushort)sample.WindowHeight);

            // dummy draw call to make sure view 0 is cleared if no other draw calls are submitted
            Bgfx.Submit(0, 0);

            // write some debug text
            Bgfx.DebugTextClear(0, false);
            Bgfx.DebugTextWrite(0, 1, 0x4f, "SharpBgfx/Samples/HelloWorld");
            Bgfx.DebugTextWrite(0, 2, 0x6f, "Description: Initialization and debug text.");

            // advance to the next frame. Rendering thread will be kicked to
            // process submitted rendering primitives.
            Bgfx.Frame();
        }

        // clean up
        Bgfx.Shutdown();
    }
}
