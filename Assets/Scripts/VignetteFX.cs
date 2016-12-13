using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VignetteFX : MonoBehaviour
{
    public enum BlendMode
    {
        Darken,
        Multiply,
        ColorBurn,
        LinearBurn,
        Screen,
        ColorDodge,
        LinearDodge,
        Overlay,
        SoftLight,
        HardLight,
        VividLight,
        LinearLight,
        PinLight,
        Difference,
        Exclusion
    }

    public Shader CurShader;
    public Texture vignetteTexture;
    public BlendMode blendMode;
    [Range(0, 1)]
    public float VignetteAmount = 1.0f;

    Material curMaterial;

    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(CurShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    private void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!CurShader && !CurShader.isSupported)
            enabled = false;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CurShader != null)
        {
            material.SetTexture("_Vignette", vignetteTexture);
            material.SetFloat("_VignetteAmount", VignetteAmount);

            DisableAll();

            switch (blendMode)
            {
                case BlendMode.Darken:
                    Shader.EnableKeyword("DARKEN");
                    break;
                case BlendMode.Multiply:
                    Shader.EnableKeyword("MULTIPLY");
                    break;
                case BlendMode.ColorBurn:
                    Shader.EnableKeyword("COLORBURN");
                    break;
                case BlendMode.LinearBurn:
                    Shader.EnableKeyword("LINEARBURN");
                    break;
                case BlendMode.Screen:
                    Shader.EnableKeyword("SCREEN");
                    break;
                case BlendMode.ColorDodge:
                    Shader.EnableKeyword("COLORDODGE");
                    break;
                case BlendMode.LinearDodge:
                    Shader.EnableKeyword("LINEARDODGE");
                    break;
                case BlendMode.Overlay:
                    Shader.EnableKeyword("OVERLAY");
                    break;
                case BlendMode.SoftLight:
                    Shader.EnableKeyword("SOFTLIGHT");
                    break;
                case BlendMode.HardLight:
                    Shader.EnableKeyword("HARDLIGHT");
                    break;
                case BlendMode.VividLight:
                    Shader.EnableKeyword("VIVIDLIGHT");
                    break;
                case BlendMode.LinearLight:
                    Shader.EnableKeyword("LINEARLIGHT");
                    break;
                case BlendMode.PinLight:
                    Shader.EnableKeyword("PINLIGHT");
                    break;
                case BlendMode.Difference:
                    Shader.EnableKeyword("DIFFERENCE");
                    break;
                case BlendMode.Exclusion:
                    Shader.EnableKeyword("EXCLUSION");
                    break;
            }

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    void DisableAll()
    {
        Shader.DisableKeyword("DARKEN");
        Shader.DisableKeyword("MULTIPLY");
        Shader.DisableKeyword("COLORBURN");
        Shader.DisableKeyword("LINEARBURN");
        Shader.DisableKeyword("SCREEN");
        Shader.DisableKeyword("COLORDODGE");
        Shader.DisableKeyword("LINEARDODGE");
        Shader.DisableKeyword("OVERLAY");
        Shader.DisableKeyword("SOFTLIGHT");
        Shader.DisableKeyword("HARDLIGHT");
        Shader.DisableKeyword("VIVIDLIGHT");
        Shader.DisableKeyword("LINEARLIGHT");
        Shader.DisableKeyword("PINLIGHT");
        Shader.DisableKeyword("DIFFERENCE");
        Shader.DisableKeyword("EXCLUSION");
    }

    private void OnDisable()
    {
        if (curMaterial)
            DestroyImmediate(curMaterial);
    }
}
