using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NightVisionFX : MonoBehaviour
{
    public Shader CurShader;
    public Texture vignetteTexture;

    [Header("Night Vision")]
    public Color color = Color.white;
    [Range(0f, 4f)]
    public float contrast = 2.0f;
    [Range(0f, 2f)]
    public float brightness = 1.0f;

    [Header("Scan Line")]
    public Texture2D scanLineTexture;
    public float scanLineTileAmount = 4.0f;

    [Header("Noise")]
    public Texture2D nightVisionNoise;
    public float noiseXSpeed = 100.0f;
    public float noiseYSpeed = 100.0f;

    [Header("Distortion")]
    [Range(-1f, 1f)]
    public float distortion = 0.2f;
    [Range(0f, 3f)]
    public float scale = 0.8f;

    float randomValue = 0.0f;
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

    void Update()
    {
        randomValue = Random.Range(-1f, 1f);
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
            material.SetFloat("_Contrast", contrast);
            material.SetFloat("_Brightness", brightness);
            material.SetColor("_NightVisionColor", color);
            material.SetFloat("_RandomValue", randomValue);
            material.SetFloat("_distortion", distortion);
            material.SetFloat("_scale", scale);

            if (vignetteTexture)
            {
                material.SetTexture("_Vignette", vignetteTexture);
            }

            if (scanLineTexture)
            {
                material.SetTexture("_ScanLineTex", scanLineTexture);
                material.SetFloat("_ScanLineTileAmount", scanLineTileAmount);
            }

            if (nightVisionNoise)
            {
                material.SetTexture("_NoiseTex", nightVisionNoise);
                material.SetFloat("_NoiseXSpeed", noiseXSpeed);
                material.SetFloat("_NoiseYSpeed", noiseYSpeed);
            }

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
