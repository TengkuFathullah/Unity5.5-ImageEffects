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
			#pragma shader_feature DARKEN MULTIPLY COLORBURN LINEARBURN SCREEN COLORDODGE LINEARDODGE OVERLAY SOFTLIGHT HARDLIGHT VIVIDLIGHT LINEARLIGHT PINLIGHT DIFFERENCE EXCLUSION
			#include "UnityCG.cginc"
			#include "BlendMode.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _Vignette;
			fixed _VignetteAmount;

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				fixed4 vignette = tex2D(_Vignette, i.uv);

				fixed4 finalColor = fixed4(0,0,0,0);
				#if DARKEN
				finalColor = Darken(renderTex, vignette);
				#endif
				#if MULTIPLY
				finalColor = Multiply(renderTex, vignette);
				#endif
				#if COLORBURN
				finalColor = ColorBurn(renderTex, vignette);
				#endif
				#if LINEARBURN
				finalColor = LinearBurn(renderTex, vignette);
				#endif
				#if SCREEN
				finalColor = Screen(renderTex, vignette);
				#endif
				#if COLORDODGE
				finalColor = ColorDodge(renderTex, vignette);
				#endif
				#if LINEARDODGE
				finalColor = LinearDodge(renderTex, vignette);
				#endif
				#if OVERLAY
				finalColor = Overlay(renderTex, vignette);
				#endif
				#if SOFTLIGHT
				finalColor = SoftLight(renderTex, vignette);
				#endif
				#if HARDLIGHT
				finalColor = HardLight(renderTex, vignette);
				#endif
				#if VIVIDLIGHT
				finalColor = VividLight(renderTex, vignette);
				#endif
				#if LINEARLIGHT
				finalColor = LinearLight(renderTex, vignette);
				#endif
				#if PINLIGHT
				finalColor = PinLight(renderTex, vignette);
				#endif
				#if DIFFERENCE
				finalColor = Difference(renderTex, vignette);
				#endif
				#if EXCLUSION
				finalColor = Exclusion(renderTex, vignette);
				#endif
				finalColor = lerp(renderTex, finalColor, _VignetteAmount);

				return finalColor;
			}
			ENDCG
		}

	}
	FallBack "Diffuse"
}
