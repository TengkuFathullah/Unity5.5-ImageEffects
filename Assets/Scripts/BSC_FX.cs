using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BSC_FX : MonoBehaviour
{
    public Shader CurShader;
    [Range(0, 3)]
    public float BrightnessAmount = 1.0f;
    [Range(0, 5)]
    public float SaturationAmount = 1.0f;
    [Range(0, 3)]
    public float ContrastAmount = 1.0f;

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
            material.SetFloat("_BrightnessAmount", BrightnessAmount);
            material.SetFloat("_SatAmount", SaturationAmount);
            material.SetFloat("_ConAmount", ContrastAmount);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void OnDisable()
    {
        if (curMaterial)
            DestroyImmediate(curMaterial);
    }
}
