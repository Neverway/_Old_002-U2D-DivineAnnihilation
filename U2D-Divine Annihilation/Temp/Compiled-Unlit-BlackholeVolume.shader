// Compiled shader for PC, Mac & Linux Standalone

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "Unlit/BlackholeVolume" {
Properties {
 _MainColorGradient ("MainColorGradient", 2D) = "white" { }
 _BrightnessGradient ("BrightnessGradient", 2D) = "white" { }
 _MainNoiseSource ("MainNoiseSource", 2D) = "white" { }
 _SkyboxCube ("SkyboxCube", CUBE) = "black" { }
[Space]  _BrightnessMultiplier ("BrightnessMultiplier", Range(0.000000,5.000000)) = 3.200000
 _SkyboxBrightnessMultiplier ("SkyboxBrightness", Float) = 800.000000
 _EventHorizonBrightnessMultiplier ("EventHorizonBrightnessMultiplier", Range(0.000000,100.000000)) = 20.000000
 _EventHorizonColor ("EventHorizonColor", Color) = (1.000000,1.000000,1.000000,1.000000)
[Space]  _FastSpinSpeed ("FastSpinSpeed", Range(0.000000,1.000000)) = 0.370000
 _SlowSpinSpeed ("SlowSpinSpeed", Range(0.000000,1.000000)) = 0.090000
[Space]  _MinStepSize ("MinStepSize", Range(0.002400,0.032000)) = 0.015000
 _MaxStepSize ("MaxStepSize", Range(0.100000,0.500000)) = 0.320000
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" "RenderPipeline"="HDRenderPipeline" }
 Pass {
  Tags { "RenderType"="Opaque" "RenderPipeline"="HDRenderPipeline" }
  Cull Off
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Global Keywords: <none>
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "glcore":
// Compile errors generating this shader.

-- Hardware tier variant: Tier 1
-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

//////////////////////////////////////////////////////
Global Keywords: <none>
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "vulkan":
// Compile errors generating this shader.

-- Hardware tier variant: Tier 1
-- Fragment shader for "vulkan":
Shader Disassembly:
 

 }
}
Fallback Off
}