using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBgfx;

class RenderStateGroup {
    public RenderState State;
    public uint BlendFactorRgba;
    public StencilFlags FrontFace;
    public StencilFlags BackFace;

    public RenderStateGroup (RenderState state, uint blendFactor, StencilFlags frontFace, StencilFlags backFace) {
        State = state;
        BlendFactorRgba = blendFactor;
        FrontFace = frontFace;
        BackFace = backFace;
    }

    public static Dictionary<PrebuiltRenderState, RenderStateGroup> Groups = new Dictionary<PrebuiltRenderState, RenderStateGroup> {
        { PrebuiltRenderState.CraftStencil, new RenderStateGroup(
            RenderState.DepthWrite |
            RenderState.DepthTestLess |
            RenderState.Multisampling |
            RenderState.ColorWrite,
            uint.MaxValue,
            StencilFlags.TestAlways |
            StencilFlags.ReferenceValue(1) |
            StencilFlags.ReadMask(0xff) |
            StencilFlags.FailSReplace |
            StencilFlags.FailZReplace |
            StencilFlags.PassZReplace,
            StencilFlags.None)
        },
        { PrebuiltRenderState.DrawReflected, new RenderStateGroup(
            RenderState.ColorWrite |
            RenderState.AlphaWrite |
            RenderState.DepthWrite |
            RenderState.DepthTestLess |
            RenderState.CullClockwise |
            RenderState.Multisampling |
            RenderState.BlendAlpha,
            uint.MaxValue,
            StencilFlags.TestEqual |
            StencilFlags.ReferenceValue(1) |
            StencilFlags.ReadMask(1) |
            StencilFlags.FailSKeep |
            StencilFlags.FailZKeep |
            StencilFlags.PassZKeep,
            StencilFlags.None)
        },
        { PrebuiltRenderState.BlendPlane, new RenderStateGroup(
            RenderState.ColorWrite |
            RenderState.DepthWrite |
            RenderState.DepthTestLess |
            RenderState.BlendFunction(RenderState.BlendOne, RenderState.BlendSourceColor) |
            RenderState.CullCounterclockwise |
            RenderState.Multisampling,
            uint.MaxValue,
            StencilFlags.None,
            StencilFlags.None)
        },
        { PrebuiltRenderState.DrawScene, new RenderStateGroup(
            RenderState.ColorWrite |
            RenderState.DepthWrite |
            RenderState.DepthTestLess |
            RenderState.CullCounterclockwise |
            RenderState.Multisampling,
            uint.MaxValue,
            StencilFlags.None,
            StencilFlags.None)
        }
    };
}

enum PrebuiltRenderState {
    CraftStencil,
    DrawReflected,
    BlendPlane,
    DrawScene
}
