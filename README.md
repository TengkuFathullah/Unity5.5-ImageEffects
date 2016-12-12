# UnityImageEffects
Unity3D Camera Effect

1. Grayscale
2. CameraDepth
3. BSC(Brightness,Saturation,Contrast)
4. Vignette

Custom shader include "BlendMode.cginc" which provide blend mode function.
- Darken
- Multiply
- ColorBurn
- LinearBurn
- Lighten
- Screen
- ColorDodge
- LinearDodge
- Overlay
- SoftLight
- HardLight
- VividLight
- LinearLight
- PinLight
- Difference
- Exclusion

PS: Change to appropiate blend function `fixed4 finalColor = Overlay(renderTex, vignette);`