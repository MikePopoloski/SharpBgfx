using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBgfx;
using SlimMath;

unsafe class Uniforms : IDisposable {
    const int MaxLights = 5;

    Uniform parametersHandle;
    Uniform ambientHandle;
    Uniform diffuseHandle;
    Uniform specularHandle;
    Uniform colorHandle;
    Uniform timeHandle;
    Uniform lightPosRadiusHandle;
    Uniform lightRgbInnerRHandle;

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
        parametersHandle = new Uniform("u_params", UniformType.Float4Array);
        ambientHandle = new Uniform("u_ambient", UniformType.Float4Array);
        diffuseHandle = new Uniform("u_diffuse", UniformType.Float4Array);
        specularHandle = new Uniform("u_specular_shininess", UniformType.Float4Array);
        colorHandle = new Uniform("u_color", UniformType.Float4Array);
        timeHandle = new Uniform("u_time", UniformType.Float);
        lightPosRadiusHandle = new Uniform("u_lightPosRadius", UniformType.Float4Array, MaxLights);
        lightRgbInnerRHandle = new Uniform("u_lightRgbInnerR", UniformType.Float4Array, MaxLights);
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
        parametersHandle.Dispose();
        ambientHandle.Dispose();
        diffuseHandle.Dispose();
        specularHandle.Dispose();
        colorHandle.Dispose();
        timeHandle.Dispose();
        lightPosRadiusHandle.Dispose();
        lightRgbInnerRHandle.Dispose();
    }
}