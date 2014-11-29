using System;
using System.Numerics;
using Common;
using SharpBgfx;

static class Program {
    static void Main () {
        // create a UI thread and kick off a separate render thread
        var sample = new Sample("Stencil Test", 1280, 720);
        sample.Run(RenderThread);
    }

    static unsafe void RenderThread (Sample sample) {
        // initialize the renderer
        Bgfx.Init(RendererBackend.OpenGL);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.Vsync);

        // enable debug text
        Bgfx.SetDebugFlags(DebugFlags.DisplayText);

        // load shaders
        var programTextureLightning = ResourceLoader.LoadProgram("vs_stencil_texture_lightning", "fs_stencil_texture_lightning");
        var programColorLightning = ResourceLoader.LoadProgram("vs_stencil_color_lightning", "fs_stencil_color_lightning");
        var programColorTexture = ResourceLoader.LoadProgram("vs_stencil_color_texture", "fs_stencil_color_texture");
        var programColorBlack = ResourceLoader.LoadProgram("vs_stencil_color", "fs_stencil_color_black");
        var programTexture = ResourceLoader.LoadProgram("vs_stencil_texture", "fs_stencil_texture");

        // load meshes
        PosNormalTexcoordVertex.Init();
        //var bunnyMesh = ResourceLoader.LoadMesh("bunny.bin");
        //var columnMesh = ResourceLoader.LoadMesh("column.bin");
        //var hplaneMesh = new Mesh(MemoryBuffer.FromArray(StaticMeshes.HorizontalPlane), PosNormalTexcoordVertex.Decl, StaticMeshes.PlaneIndices);
        //var vplaneMesh = new Mesh(MemoryBuffer.FromArray(StaticMeshes.VerticalPlane), PosNormalTexcoordVertex.Decl, StaticMeshes.PlaneIndices);

        // load textures
        var figureTex = ResourceLoader.LoadTexture("figure-rgba.dds");
        var flareTex = ResourceLoader.LoadTexture("flare.dds");
        var fieldstoneTex = ResourceLoader.LoadTexture("fieldstone-rgba.dds");

        // create uniforms
        var uniforms = new Uniforms();
        uniforms.SetConstUniforms();

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
            ClearView(0, ClearFlags.ColorBit | ClearFlags.DepthBit | ClearFlags.StencilBit, sample.WindowWidth, sample.WindowHeight);
            Bgfx.Submit(0);

            // draw ground plane
            //hplaneMesh.Submit(PassId0, programColorBlack, FloorTransform, GetRenderState(PrebuiltRenderState.CraftStencil));

            // clear depth from previous pass
            //ClearView(PassId1, ClearFlags.DepthBit);

            // advance to the next frame. Rendering thread will be kicked to
            // process submitted rendering primitives.
            Bgfx.Frame();
        }

        // clean up
        uniforms.Dispose();

        //bunnyMesh.Dispose();
        //columnMesh.Dispose();
        //hplaneMesh.Dispose();
        //vplaneMesh.Dispose();

        figureTex.Dispose();
        fieldstoneTex.Dispose();
        flareTex.Dispose();

        programTextureLightning.Dispose();
        programColorLightning.Dispose();
        programColorTexture.Dispose();
        programColorBlack.Dispose();
        programTexture.Dispose();

        Bgfx.Shutdown();
    }

    static void ClearView (byte pass, ClearFlags flags, int width, int height) {
        Bgfx.SetViewClear(pass, flags, 0x30303000, 1.0f, 0);
        Bgfx.SetViewRect(pass, 0, 0, (ushort)width, (ushort)height);
    }

    static RenderState GetRenderState (PrebuiltRenderState selector) {
        return RenderStateGroup.Groups[selector].State;
    }
    
    static readonly Matrix4x4 FloorTransform = Matrix4x4.CreateScale(20.0f);

    const byte PassId0 = 1;
    const byte PassId1 = 2;
    const byte PassId2 = 3;
}