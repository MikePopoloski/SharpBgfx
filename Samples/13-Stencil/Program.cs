using System;
using Common;
using SharpBgfx;
using SlimMath;

static class Program {
    static void Main () {
        // create a UI thread and kick off a separate render thread
        var sample = new Sample("Stencil Test", 1280, 720);
        sample.Run(RenderThread);
    }

    static unsafe void RenderThread (Sample sample) {
        // initialize the renderer
        Bgfx.Init(RendererType.OpenGL, IntPtr.Zero, IntPtr.Zero);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.Vsync);

        // enable debug text
        Bgfx.SetDebugFlags(DebugFlags.DisplayText);

        // set view 0 clear state
        Bgfx.SetViewClear(0, ClearFlags.ColorBit | ClearFlags.DepthBit, 0x30303000, 1.0f, 0);

        // create vertex and index buffers
        PosColorVertex.Init();
        var vbh = Bgfx.CreateVertexBuffer(MemoryBuffer.FromArray(cubeVertices), PosColorVertex.Decl);
        var ibh = Bgfx.CreateIndexBuffer(MemoryBuffer.FromArray(cubeIndices));

        // load shaders
        var programTextureLightning = ResourceLoader.LoadProgram("vs_stencil_texture_lightning", "fs_stencil_texture_lightning");
        var programColorLightning = ResourceLoader.LoadProgram("vs_stencil_color_lightning", "fs_stencil_color_lightning");
        var programColorTexture = ResourceLoader.LoadProgram("vs_stencil_color_texture", "fs_stencil_color_texture");
        var programColorBlack = ResourceLoader.LoadProgram("vs_stencil_color", "fs_stencil_color_black");
        var programTexture = ResourceLoader.LoadProgram("vs_stencil_texture", "fs_stencil_texture");

        // load meshes
        var bunnyMesh = ResourceLoader.LoadMesh("bunny.bin");
        var columnMesh = ResourceLoader.LoadMesh("column.bin");
        var hplaneMesh = new Mesh(MemoryBuffer.FromArray(StaticMeshes.HorizontalPlane), PosNormalTexcoordVertex.Decl, StaticMeshes.PlaneIndices);
        var vplaneMesh = new Mesh(MemoryBuffer.FromArray(StaticMeshes.VerticalPlane), PosNormalTexcoordVertex.Decl, StaticMeshes.PlaneIndices);



        // start the frame clock
        var clock = new Clock();
        clock.Start();

        // main loop
        while (sample.ProcessEvents(ResetFlags.Vsync)) {
            // tick the clock
            var elapsed = clock.Frame();
            var time = clock.TotalTime();

            // write some debug text
            Bgfx.DebugTextClear(0, false);
            Bgfx.DebugTextWrite(0, 1, 0x4f, "SharpBgfx/Samples/13-Stencil");
            Bgfx.DebugTextWrite(0, 2, 0x6f, "Description: Stencil reflections.");
            Bgfx.DebugTextWrite(0, 3, 0x6f, string.Format("Frame: {0:F3} ms", elapsed * 1000));

            // clear the view



            // set view 0 viewport
            Bgfx.SetViewRect(0, 0, 0, (ushort)sample.WindowWidth, (ushort)sample.WindowHeight);

            // view transforms
            var viewMatrix = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -35.0f), Vector3.Zero, Vector3.UnitY);
            var projMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 3, (float)sample.WindowWidth / sample.WindowHeight, 0.1f, 100.0f);
            Bgfx.SetViewTransform(0, &viewMatrix.M11, &projMatrix.M11);

            // advance to the next frame. Rendering thread will be kicked to
            // process submitted rendering primitives.
            Bgfx.Frame();
        }

        // clean up
        Bgfx.DestroyIndexBuffer(ibh);
        Bgfx.DestroyVertexBuffer(vbh);
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