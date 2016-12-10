Shader "TFTM/ImageEffect/BSCFX" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BrightnessAmount ("Brightness Amount", Range(0,1)) = 1
		_SatAmount ("Saturation Amount", Range(0,1)) = 1
		_ConAmount("Contrast Amount", Range(0,1)) = 1
	}
	SubShader {
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			fixed _BrightnessAmount;
			fixed _SatAmount;
			fixed _ConAmount;

			float3 ContrastSaturationBrightness(float3 color, float brt, float sat, float con)
			{
				// Increase or decrease these values to
				// adjust r, g, b color channel separately
				float AvgLumR = 0.5;
				float AvgLumG = 0.5;
				float AvgLumB = 0.5;

				// Luminance coeffiecients for getting luminance from the image
				float3 LuminanceCoeff = float3(0.2125, 0.7154, 0.072);

				// Operation for brightness
				float3 AvgLumin = float3(AvgLumR, AvgLumG, AvgLumB);
				float3 brtColor = color * brt;
				float intensityF = dot(brtColor, LuminanceCoeff);
				float3 intensity = float3(intensityF, intensityF, intensityF);

				// Operation for saturation
				float3 satColor = lerp(intensity, brtColor, sat);

				// Operation for contrast
				float3 conColor = lerp(AvgLumin, satColor, con);

				return conColor;
			}

			fixed4 frag(v2f_img i) : COLOR 
			{
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				
				renderTex.rgb = ContrastSaturationBrightness(renderTex.rgb, _BrightnessAmount, _SatAmount, _ConAmount);

				return renderTex;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
