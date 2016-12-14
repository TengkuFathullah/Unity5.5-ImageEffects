Shader "TFTM/ImageEffect/SepiaFX" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EffectAmount ("Effect Amount", Range(0,1)) = 1.0
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
			fixed _EffectAmount;

			fixed3 Sepia(float3 color)
			{
				color.r = (color.r * .393) + (color.g *.769) + (color.b * .189);
				color.g = (color.r * .349) + (color.g *.686) + (color.b * .168);
				color.b = (color.r * .272) + (color.g *.534) + (color.b * .131);

				return fixed4(color.r, color.g, color.b, 1);
			}

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 renderTex = tex2D(_MainTex, i.uv);

				//float luminosity = 0.299 * renderTex.r + 0.587 * renderTex.g + 0.114 * renderTex.b;
				fixed3 newColor = Sepia(renderTex.rgb);
				fixed4 sepiaColor = fixed4(newColor.r, newColor.g, newColor.b, 1);
				fixed4 finalColor = lerp(renderTex, sepiaColor, _EffectAmount);

				return finalColor;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
