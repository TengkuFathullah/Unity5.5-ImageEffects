# UnityImageEffects
Unity3D Camera Effect, My journey to shaders study, *experimental*

1. Grayscale
2. CameraDepth
3. BSC(Brightness,Saturation,Contrast)
4. Vignette (Blendmode-Runtime)
5. Night Vision
6. ColorFX (Sepia)

![Alt text](https://cloud.githubusercontent.com/assets/17969112/21396090/e9b25e26-c7d9-11e6-9771-cad445be2ee1.png "All ImageFX")

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

PS: You can change blendmode for vignette shader on run time through script.

![Alt text](https://cloud.githubusercontent.com/assets/17969112/21396193/4ea7f3fe-c7da-11e6-8106-f182bdd05061.png "All ImageFX")