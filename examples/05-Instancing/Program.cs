﻿using System;
using Common;
using SharpBgfx;

static class Program {
    static void Main () {
        // create a UI thread and kick off a separate render thread
        var sample = new Sample("Instancing", 1280, 720);
        sample.Run(RenderThread);
    }

    static unsafe void RenderThread (Sample sample) {
        // initialize the renderer
        Bgfx.Init(RendererBackend.OpenGL);
        Bgfx.Reset(sample.WindowWidth, sample.WindowHeight, ResetFlags.Vsync);

        // enable debug text
        Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

        // set view 0 clear state
        Bgfx.SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);

        // create vertex and index buffers
        var vbh = Cube.CreateVertexBuffer();
        var ibh = Cube.CreateIndexBuffer();

        // load shaders
        var program = ResourceLoader.LoadProgram("vs_instancing", "fs_instancing");

        // start the frame clock
        var clock = new Clock();
        clock.Start();
        
        // getting caps
        var caps = Bgfx.GetCaps();

        // main loop
        while (sample.ProcessEvents(ResetFlags.Vsync)) {
            // set view 0 viewport
            Bgfx.SetViewRect(0, 0, 0, sample.WindowWidth, sample.WindowHeight);

            // view transforms
            var viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -35.0f), Vector3.Zero, Vector3.UnitY);
            var projMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float)Math.PI / 3, (float)sample.WindowWidth / sample.WindowHeight, 0.1f, 100.0f);
            Bgfx.SetViewTransform(0, &viewMatrix.M11, &projMatrix.M11);

            // make sure view 0 is cleared if no other draw calls are submitted
            Bgfx.Touch(0);

            // tick the clock
            var elapsed = clock.Frame();
            var time = clock.TotalTime();

            // write some debug text
            Bgfx.DebugTextClear();
            Bgfx.DebugTextWrite(0, 1, DebugColor.White, DebugColor.Blue, "SharpBgfx/Samples/05-Instancing");
            Bgfx.DebugTextWrite(0, 2, DebugColor.White, DebugColor.Cyan, "Description: Geometry instancing.");
            Bgfx.DebugTextWrite(0, 3, DebugColor.White, DebugColor.Cyan, "Frame: {0:F3} ms", elapsed * 1000);

            // check caps
            if ((caps.SupportedFeatures & DeviceFeatures.Instancing) != DeviceFeatures.Instancing) {
                // instancing not supported
                Bgfx.DebugTextWrite(0, 3, DebugColor.White, DebugColor.Red, "Instancing not supported!");
            } else {
                const int instanceStride = 80;
                const int instanceCount = 121;
                
                var idb = new InstanceDataBuffer(instanceCount, instanceStride);
                
                // fill in InstanceDataBuffer
                byte *dataPtr = (byte *)idb.Data.ToPointer();
                // TODO: extract idb->data->num
                for (int y = 0; y < 11; y++) {
                    for (int x = 0; x < 11; x++) {
                        float *matrix = (float *)dataPtr;
                        var realMatrix = Matrix4x4.CreateFromYawPitchRoll(time + x * 0.21f, time + y * 0.37f, 0f);
                        realMatrix.M41 = -15.0f + x * 3.0f;
                        realMatrix.M42 = -15.0f + y * 3.0f;
                        realMatrix.M43 = 0.0f;
                        // TODO: use proper copy function, not a bycicle
                        float *realMatrixPtr = &realMatrix.M11;
                        for (int i = 0; i < 16; i++)
                            matrix[i] = realMatrixPtr[i];
                        
                        float *color = (float *)(dataPtr + 64);
                        color[0] = (float)Math.Sin(time + x / 11.0f) * 0.5f + 0.5f;
                        color[1] = (float)Math.Cos(time + y / 11.0f) * 0.5f + 0.5f;
                        color[2] = (float)Math.Sin(time * 3.0f) * 0.5f + 0.5f;
                        color[3] = 1.0f;
                        
                        dataPtr += instanceStride;
                    }
                }
                
                // set pipeline states
                Bgfx.SetVertexBuffer(0, vbh);
                Bgfx.SetIndexBuffer(ibh);
                Bgfx.SetInstanceDataBuffer(idb);
                Bgfx.SetRenderState(RenderState.Default);

                // submit primitives
                Bgfx.Submit(0, program);
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
}