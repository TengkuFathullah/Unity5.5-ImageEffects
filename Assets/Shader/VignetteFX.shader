Shader "TFTM/ImageEffect/VignetteFX" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Vignette("Vignette Texture", 2D) = "white" {}
		_VignetteAmount ("Grayscale Amount", Range(0,1)) = 1.0
	}
	SubShader {
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			#include "BlendMode.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _Vignette;
			fixed _VignetteAmount;

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				fixed4 vignette = tex2D(_Vignette, i.uv);

				fixed4 finalColor = Overlay(renderTex, vignette);
				finalColor = lerp(renderTex, finalColor, _VignetteAmount);

				return finalColor;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
