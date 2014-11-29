using System;
using System.Numerics;
using Common;
using SharpBgfx;

static class Program {
    static void Main () {
        // create a UI thread and kick off a separate render thread
        var sample = new Sample("Cubes", 1280, 720);
        sample.Run(RenderThread);
    }

    static unsafe void RenderThread (Sample sample) {
        // initialize the renderer
        Bgfx.Init(RendererBackend.OpenGL);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.Vsync);

        // enable debug text
        Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

        // set view 0 clear state
        Bgfx.SetViewClear(0, ClearTargets.ColorBit | ClearTargets.DepthBit, 0x303030ff);

        // create vertex and index buffers
        PosColorVertex.Init();
        var vbh = new VertexBuffer(MemoryBlock.FromArray(cubeVertices), PosColorVertex.Layout);
        var ibh = new IndexBuffer(MemoryBlock.FromArray(cubeIndices));

        // load shaders
        var program = ResourceLoader.LoadProgram("vs_cubes", "fs_cubes");

        // start the frame clock
        var clock = new Clock();
        clock.Start();

        // main loop
        while (sample.ProcessEvents(ResetFlags.Vsync)) {
            // set view 0 viewport
            Bgfx.SetViewRect(0, 0, 0, sample.WindowWidth, sample.WindowHeight);

            // view transforms
            var viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -35.0f), Vector3.Zero, Vector3.UnitY);
            var projMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float)Math.PI / 3, (float)sample.WindowWidth / sample.WindowHeight, 0.1f, 100.0f);
            Bgfx.SetViewTransform(0, viewMatrix, projMatrix);

            // dummy draw call to make sure view 0 is cleared if no other draw calls are submitted
            Bgfx.Submit(0);

            // tick the clock
            var elapsed = clock.Frame();
            var time = clock.TotalTime();

            // write some debug text
            Bgfx.DebugTextClear();
            Bgfx.DebugTextWrite(0, 1, 0x4f, "SharpBgfx/Samples/01-Cubes");
            Bgfx.DebugTextWrite(0, 2, 0x6f, "Description: Rendering simple static mesh.");
            Bgfx.DebugTextWrite(0, 3, 0x6f, string.Format("Frame: {0:F3} ms", elapsed * 1000));

            // submit 11x11 cubes
            for (int y = 0; y < 11; y++) {
                for (int x = 0; x < 11; x++) {
                    // model matrix
                    var transform = Matrix4x4.CreateFromYawPitchRoll(time + x * 0.21f, time + y * 0.37f, 0.0f);
                    transform.M41 = -15.0f + x * 3.0f;
                    transform.M42 = -15.0f + y * 3.0f;
                    transform.M43 = 0.0f;
                    Bgfx.SetTransform(transform);

                    // set pipeline states
                    Bgfx.SetProgram(program);
                    Bgfx.SetVertexBuffer(vbh);
                    Bgfx.SetIndexBuffer(ibh);
                    Bgfx.SetRenderState(RenderState.Default);

                    // submit primitives
                    Bgfx.Submit(0);
                }
            }

            // advance to the next frame. Rendering thread will be kicked to
            // process submitted rendering primitives.
            Bgfx.Frame();
        }

        // clean up
        ibh.Dispose();
        vbh.Dispose();
        program.Dispose();
        Bgfx.Shutdown();
    }

    static readonly PosColorVertex[] cubeVertices = {
        new PosColorVertex(-1.0f,  1.0f,  1.0f, 0xff000000),
        new PosColorVertex( 1.0f,  1.0f,  1.0f, 0xff0000ff),
        new PosColorVertex(-1.0f, -1.0f,  1.0f, 0xff00ff00),
        new PosColorVertex( 1.0f, -1.0f,  1.0f, 0xff00ffff),
        new PosColorVertex(-1.0f,  1.0f, -1.0f, 0xffff0000),
        new PosColorVertex( 1.0f,  1.0f, -1.0f, 0xffff00ff),
        new PosColorVertex(-1.0f, -1.0f, -1.0f, 0xffffff00),
        new PosColorVertex( 1.0f, -1.0f, -1.0f, 0xffffffff)
    };

    static readonly ushort[] cubeIndices = {
        0, 1, 2, // 0
        1, 3, 2,
        4, 6, 5, // 2
        5, 6, 7,
        0, 2, 4, // 4
        4, 2, 6,
        1, 5, 3, // 6
        5, 7, 3,
        0, 4, 1, // 8
        4, 5, 1,
        2, 3, 6, // 10
        6, 3, 7
    };
}