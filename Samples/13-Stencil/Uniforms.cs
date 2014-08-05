using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBgfx;
using SlimMath;

unsafe class Uniforms : IDisposable {
    const int MaxLights = 5;

    UniformHandle parametersHandle;
    UniformHandle ambientHandle;
    UniformHandle diffuseHandle;
    UniformHandle specularHandle;
    UniformHandle colorHandle;
    UniformHandle timeHandle;
    UniformHandle lightPosRadiusHandle;
    UniformHandle lightRgbInnerRHandle;

    public bool AmbientPass {
        get;
        set;
    }

    public bool LightingPass {
        get;
        set;
    }

    public int LightCount {
        get;
        set;
    }

    public Color4 Color {
        get;
        set;
    }

    public Lights Lights {
        get;
        set;
    }

    public Uniforms () {
        parametersHandle = Bgfx.CreateUniform("u_params", UniformType.Float4Array);
        ambientHandle = Bgfx.CreateUniform("u_ambient", UniformType.Float4Array);
        diffuseHandle = Bgfx.CreateUniform("u_diffuse", UniformType.Float4Array);
        specularHandle = Bgfx.CreateUniform("u_specular_shininess", UniformType.Float4Array);
        colorHandle = Bgfx.CreateUniform("u_color", UniformType.Float4Array);
        timeHandle = Bgfx.CreateUniform("u_time", UniformType.Float);
        lightPosRadiusHandle = Bgfx.CreateUniform("u_lightPosRadius", UniformType.Float4Array, MaxLights);
        lightRgbInnerRHandle = Bgfx.CreateUniform("u_lightRgbInnerR", UniformType.Float4Array, MaxLights);
    }

    public void SetConstUniforms () {
        var color = new Color4(0.02f, 0.02f, 0.02f, 0.0f);
        var shininess = new Vector4(1.0f, 1.0f, 1.0f, 10.0f);

        Bgfx.SetUniform(ambientHandle, &color);
        Bgfx.SetUniform(diffuseHandle, &color);
        Bgfx.SetUniform(specularHandle, &shininess);
    }

    public void SetPerFrameUniforms (float time) {
        Bgfx.SetUniform(timeHandle, &time);
    }

    public void SetPerDrawUniforms () {
        var color = Color;
        var param = new Vector4(
            AmbientPass ? 1.0f : 0.0f,
            LightingPass ? 1.0f : 0.0f,
            LightCount,
            0.0f
        );

        Bgfx.SetUniform(parametersHandle, &param);
        Bgfx.SetUniform(colorHandle, &color);
    }

    public void Dispose () {
        Bgfx.DestroyUniform(parametersHandle);
        Bgfx.DestroyUniform(ambientHandle);
        Bgfx.DestroyUniform(diffuseHandle);
        Bgfx.DestroyUniform(specularHandle);
        Bgfx.DestroyUniform(colorHandle);
        Bgfx.DestroyUniform(timeHandle);
        Bgfx.DestroyUniform(lightPosRadiusHandle);
        Bgfx.DestroyUniform(lightRgbInnerRHandle);
    }
}