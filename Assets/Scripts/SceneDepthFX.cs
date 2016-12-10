using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneDepthFX : MonoBehaviour
{
    public Shader CurShader;
    [Range(0, 5)]
    public float DepthPower = 1.0f;

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
            material.SetFloat("_DepthPower", DepthPower);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void Update()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    private void OnDisable()
    {
        if (curMaterial)
            DestroyImmediate(curMaterial);
    }
}
