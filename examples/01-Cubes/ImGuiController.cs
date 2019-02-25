using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using ImGuiNET;
using Matrix4x4 = Common.Matrix4x4;


namespace SharpBgfx
{
	public class ImGuiController
	{
		private Program imguiProgram;
		private VertexLayout vertexLayout;
		private Uniform texUniform;
		public Texture FontAtlas;
		private IntPtr imguiContext;
		private ushort BgfxID;

		public ImGuiController(ushort bgfxID)
		{
			BgfxID = bgfxID;
			Init();
		}

		private void Init()
		{
			imguiContext = ImGui.CreateContext();
			var io = ImGui.GetIO();

			imguiProgram = ResourceLoader.LoadProgram("vs_ocornut_imgui", "fs_ocornut_imgui");
			vertexLayout = new VertexLayout();
			vertexLayout.Begin()
				.Add(VertexAttributeUsage.Position, 2, VertexAttributeType.Float)
				.Add(VertexAttributeUsage.TexCoord0, 2, VertexAttributeType.Float)
				.Add(VertexAttributeUsage.Color0, 4, VertexAttributeType.UInt8, true)
				.End();

			io.Fonts.AddFontDefault();

			unsafe
			{
				io.Fonts.GetTexDataAsRGBA32(out var data, out var width, out var height, out var bytesPerPixel);

				FontAtlas = Texture.Create2D(width, height, false, 1, TextureFormat.BGRA8, 0,
					new MemoryBlock((IntPtr)data, width * height * bytesPerPixel));
			}
		}

		public void StartFrame()
		{
			ImGui.NewFrame();
		}

		public void EndFrame(float elapsedInSeconds, Vector2 resolution)
		{
			var io = ImGui.GetIO();
			io.DeltaTime = elapsedInSeconds;
			io.DisplaySize = resolution;
			ImGui.EndFrame();
			ImGui.Render();

			BgfxRender(ImGui.GetDrawData());
		}

		private void BgfxRender(ImDrawDataPtr drawData)
		{
			var io = ImGui.GetIO();
			Bgfx.SetViewRect(BgfxID, 0, 0, (int)io.DisplaySize.X, (int)io.DisplaySize.Y);

			unsafe
			{
				var identityMatrix = Matrix4x4.Identity;
				Bgfx.SetViewTransform(BgfxID, &identityMatrix.M11, &identityMatrix.M11);
			}

			for (var ii = 0; ii < drawData.CmdListsCount; ii++)
			{
				var drawList = drawData.CmdListsRange[ii];
				var numVertices = drawList.VtxBuffer.Size;
				var numIndices = drawList.IdxBuffer.Size;

				Bgfx.AllocateTransientBuffers(numVertices, vertexLayout, numIndices, out var tvb, out var tib);

				unsafe
				{
					var vertices = tvb.Data;
					Buffer.MemoryCopy(drawList.VtxBuffer.Data.ToPointer(), vertices.ToPointer(),
						numVertices * sizeof(ImDrawVert), numVertices * sizeof(ImDrawVert));

					var indices = tib.Data;
					Buffer.MemoryCopy(drawList.IdxBuffer.Data.ToPointer(), indices.ToPointer(),
						numIndices * sizeof(ushort), numIndices * sizeof(ushort));
				}

				var offset = 0;
				for (var cmdIndex = 0; cmdIndex < drawList.CmdBuffer.Size; cmdIndex++)
				{
					var cmd = drawList.CmdBuffer[cmdIndex];

					if (cmd.UserCallback != IntPtr.Zero) throw new NotImplementedException();

					if (cmd.ElemCount != 0)
					{
						var state = 0
									| RenderState.WriteRGB
									| RenderState.WriteA
									| RenderState.Multisampling
							;
						var th = FontAtlas;
						var program = imguiProgram;

						if (cmd.TextureId != (IntPtr)FontAtlas.GetHashCode())
						{
							state |= RenderState.BlendFunction(RenderState.BlendSourceAlpha,
								RenderState.BlendInverseSourceAlpha);

							// ToDo: Implement if texture is not fonttexture

						}
						else
						{
							state |= RenderState.BlendFunction(RenderState.BlendSourceAlpha,
								RenderState.BlendInverseSourceAlpha);
						}
						var x = (int)Math.Max(cmd.ClipRect.X, 0.0f);
						var y = (int)Math.Max(cmd.ClipRect.Y, 0.0f);
						var z = (int)Math.Min(cmd.ClipRect.Z, 65535.0f);
						var w = (int)Math.Min(cmd.ClipRect.W, 65535.0f);

						Bgfx.SetScissor(x, y, z - x, w - y);

						Bgfx.SetRenderState(state);
						Bgfx.SetTexture(0, texUniform, th);
						Bgfx.SetVertexBuffer(0, tvb, 0, numVertices);
						Bgfx.SetIndexBuffer(tib, offset, (int)cmd.ElemCount);
						Bgfx.Submit(BgfxID, program);
					}

					offset += (int)cmd.ElemCount;
				}

			}
		}

		public void Shutdown()
		{
			ImGui.DestroyContext(imguiContext);
		}
	}
}
