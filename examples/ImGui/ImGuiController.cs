/*	This implementation is based on the implementation in bgfx: https://github.com/bkaradzic/bgfx/blob/master/examples/common/imgui/imgui.cpp
 *	It is not fully implemented, but should help people which use SharpBgfx and want to use ImGui.
 *
 * Things that are not implemented:
 *		Input forwarding (Engine specific)
 *		Drawing textures (Maybe buggy with mipmaps)
 *		Adding different fonts (Fonts must be known when creating the ImGuiController)
 *		Transient Buffer size check (Not wrapped?)
 *		Implement UserCallback 
 */

using System;
using System.Collections.Generic;
using System.Numerics;
using Common;
using ImGuiNET;
using Matrix4x4 = Common.Matrix4x4;

namespace SharpBgfx
{
	public class ImGuiController
	{
		private Texture FontAtlas;

		// This is my own implementation how to include fonts and textures. If you have a better one feel free to improve this!
		private readonly Dictionary<string, ImFontPtr> _fonts = new Dictionary<string, ImFontPtr>();
		private readonly Dictionary<IntPtr, Texture> _textures = new Dictionary<IntPtr, Texture>();

		private Program _imguiProgram;
		private Program _textureProgram;

		private VertexLayout _vertexLayout;
		private Uniform _texUniform;

		private IntPtr _imguiContext;

		private readonly ushort _bgfxId;

		public ImGuiController(ushort bgfxId)
		{
			_bgfxId = bgfxId;
			Init();
		}

		private void Init()
		{
			_imguiContext = ImGui.CreateContext();
			var io = ImGui.GetIO();

			// load the shaders for imgui
			_imguiProgram = ResourceLoader.LoadProgram("vs_ocornut_imgui", "fs_ocornut_imgui");
			_textureProgram = ResourceLoader.LoadProgram("vs_imgui_image", "fs_imgui_image");

			_vertexLayout = new VertexLayout();
			_vertexLayout.Begin()
				.Add(VertexAttributeUsage.Position, 2, VertexAttributeType.Float)
				.Add(VertexAttributeUsage.TexCoord0, 2, VertexAttributeType.Float)
				.Add(VertexAttributeUsage.Color0, 4, VertexAttributeType.UInt8, true)
				.End();

			_texUniform = new Uniform("s_tex", UniformType.Sampler);

			_fonts.Add("default", io.Fonts.AddFontDefault());

			// Fonts: All fonts need to be known here, because we compile the fontatlas only once
			// Example code:
			/*foreach (var font in fonts)
			{
				_fonts.Add(font.name, io.Fonts.AddFontFromFileTTF(font.path, font.size));
			}*/

			// Build and save the fontatlas
			unsafe
			{
				io.Fonts.GetTexDataAsRGBA32(out var data, out var width, out var height, out var bytesPerPixel);

				FontAtlas = Texture.Create2D(width, height, false, 1, TextureFormat.BGRA8, 0,
					new MemoryBlock((IntPtr)data, width * height * bytesPerPixel));

				_textures.Add((IntPtr)FontAtlas.GetHashCode(), FontAtlas);
			}

			io.Fonts.SetTexID((IntPtr)FontAtlas.GetHashCode());

			// ToDo: Set KeyMappings
			// -> Engine specific
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

			var width = io.DisplaySize.X;
			var height = io.DisplaySize.Y;

			Bgfx.SetViewName(_bgfxId, "ImGui");
			Bgfx.SetViewMode(_bgfxId, ViewMode.Sequential);

			// Bgfx implementation now gets the capabilities and creates a orthogonal matrix with bx. Bx is not wrapped, so I just create a identity matrix.
			// Works as intended on my side.
			// Bgfx Cpp:
			//
			/*const bgfx::Caps* caps = bgfx::getCaps();
			{
				float ortho[16];
				bx::mtxOrtho(ortho, 0.0f, width, height, 0.0f, 0.0f, 1000.0f, 0.0f, caps->homogeneousDepth);
				bgfx::setViewTransform(m_viewId, NULL, ortho);
				bgfx::setViewRect(m_viewId, 0, 0, uint16_t(width), uint16_t(height));
			}*/

			unsafe
			{
				var identityMatrix = Matrix4x4.Identity;
				Bgfx.SetViewTransform(_bgfxId, null, &identityMatrix.M11);
			}

			Bgfx.SetViewRect(_bgfxId, 0, 0, (int)width, (int)height);

			for (var ii = 0; ii < drawData.CmdListsCount; ++ii)
			{
				TransientVertexBuffer tvb;
				TransientIndexBuffer tib;

				var drawList = drawData.CmdListsRange[ii];
				var numVertices = drawList.VtxBuffer.Size;
				var numIndices = drawList.IdxBuffer.Size;

				// Bgfx now checks if the transient buffer has enough space to draw the rest.
				// The Method uses bgfx::getAvailTransientVertexBuffer and bgfx::getAvailTransientVertexBuffer, which are not wrapped?
				// So I just skip the test.

				Bgfx.AllocateTransientBuffers(numVertices, _vertexLayout, numIndices, out tvb, out tib);

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

					if (cmd.UserCallback != IntPtr.Zero)
					{
						//cmd->UserCallback(drawList, cmd);
						// I have no idea how to do the above in C#.
						throw new NotImplementedException();
					}
					else if (0 != cmd.ElemCount)
					{
						var state = 0
									| RenderState.WriteRGB
									| RenderState.WriteA
									| RenderState.Multisampling
							;

						var th = FontAtlas;
						var program = _imguiProgram;

						// Check if the TextureId is not the one of the FontAtlas
						// Short: We give imgui a IntPtr to identify the texture. In c++ we can give the pointer to the texture, 
						// but I've dont't know if thats possible to do in c#.
						// My solution: Cast the hashcode of the texture to a IntPtr and use it as an identifier and store the IntPtrs and the Textures in a dictionary.
						// See imgui textureID/texId for more information.
						if (cmd.TextureId != (IntPtr)FontAtlas.GetHashCode())
						{
							// Bgfx sets the state dependent on the texture flags. Getting these flags is not implemented?, so I ignore the check.
							state |= RenderState.BlendFunction(RenderState.BlendSourceAlpha,
								RenderState.BlendInverseSourceAlpha);

							th = _textures[cmd.TextureId];
							if (0 != th.MipLevels)
							{
								float[] lodEnabled = { 1.0f, 0.0f, 0.0f, 0.0f };
								var imageLodEnabled = new Uniform("u_params", UniformType.Vector4);
								unsafe
								{
									fixed (void* lodEnabledPtr = lodEnabled)
									{
										Bgfx.SetUniform(imageLodEnabled, lodEnabledPtr);
									}
								}
								// If I use the texture shader from bgfx, the images aren't drawn
								//program = _textureProgram;
							}
						}
						else
						{
							state |= RenderState.BlendFunction(RenderState.BlendSourceAlpha,
								RenderState.BlendInverseSourceAlpha);
						}

						// I've splitted the declaration and the usage, so it's clearer
						var x = (int)Math.Max(cmd.ClipRect.X, 0.0f);
						var y = (int)Math.Max(cmd.ClipRect.Y, 0.0f);
						var z = (int)Math.Min(cmd.ClipRect.Z, 65535.0f);
						var w = (int)Math.Min(cmd.ClipRect.W, 65535.0f);
						Bgfx.SetScissor(x, y, z - x, w - y);

						Bgfx.SetRenderState(state);
						Bgfx.SetTexture(0, _texUniform, th);
						Bgfx.SetVertexBuffer(0, tvb, 0, numVertices);
						Bgfx.SetIndexBuffer(tib, offset, (int)cmd.ElemCount);
						Bgfx.Submit(_bgfxId, program);
					}

					offset += (int)cmd.ElemCount;
				}

			}
		}

		public void Shutdown()
		{
			ImGui.DestroyContext(_imguiContext);
		}

		public IntPtr AddTexture(Texture texture)
		{
			var textureId = (IntPtr)texture.GetHashCode();
			_textures.Add(textureId, texture);
			return textureId;
		}
	}
}
