Shader "Unlit/BlackholeVolume"
{
	Properties
	{
		_MainColorGradient ("MainColorGradient", 2D) = "white" {}
		_BrightnessGradient ("BrightnessGradient", 2D) = "white" {}
		_MainNoiseSource ("MainNoiseSource", 2D) = "white" {}
		_SkyboxCube ("SkyboxCube", Cube) = "black" {}

		[Space]

		_BrightnessMultiplier ("BrightnessMultiplier", Range(0, 5)) = 3.2
		_SkyboxBrightnessMultiplier ("SkyboxBrightness", Float) = 800
		_EventHorizonBrightnessMultiplier ("EventHorizonBrightnessMultiplier", Range(0, 100)) = 20
		_EventHorizonColor ("EventHorizonColor", Color) = (1, 1, 1, 1)

		[Space]

		_FastSpinSpeed ("FastSpinSpeed", Range(0, 1)) = 0.37
		_SlowSpinSpeed ("SlowSpinSpeed", Range(0, 1)) = 0.09

		[Space]

		_MinStepSize ("MinStepSize", Range(0.0024, 0.032)) = 0.015 //Most important parameter; directly related to performance
		_MaxStepSize ("MaxStepSize", Range(0.1, 0.5)) = 0.32
	}

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "HDRenderPipeline"
			"RenderType" = "Opaque"
		}

		LOD 100
		Cull Off

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			//#include "UnityCG.cginc"

			#include "Assets/jp.keijiro.noiseshader/Shader/SimplexNoise3D.hlsl"

			#define MAX_STEPS 1000
			#define SCHWARZSCHILD 0.5

			#define DISK_INNER_RADIUS SCHWARZSCHILD * 3 //AKA the Innermost Stable Circular Orbit (ISCO) = schwarzschild radius * 3
			#define DISK_OUTER_RADIUS SCHWARZSCHILD * 8
			#define DISK_VOLUMETRIC_HEIGHT 0.08
			#define SKYBOX_MARCH_RADIUS DISK_OUTER_RADIUS * 1.3

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 rayOrigin : TEXCOORD0;
				float3 hitPosition : TEXCOORD1;
			};

			sampler2D _MainColorGradient;
			sampler2D _BrightnessGradient;
			sampler2D _MainNoiseSource;
			samplerCUBE _SkyboxCube;

			float _BrightnessMultiplier;
			float _SkyboxBrightnessMultiplier;
			float _EventHorizonBrightnessMultiplier;
			float4 _EventHorizonColor;

			float _FastSpinSpeed;
			float _SlowSpinSpeed;

			float _MinStepSize;
			float _MaxStepSize;

			float _Slider;

			float4x4 _ObjectToWorldMatrix;
			float4x4 _WorldToObjectMatrix;

			//Shader assumes blackhole is at object origin

			//Referenced articles/websites:
			//https://medium.com/dotcrossdot/raymarching-simulating-a-black-hole-53624a3684d3
			//https://www.shadertoy.com/view/XdjXDy
			//https://www.shadertoy.com/view/tsBXW3

			//Skybox generator: https://wwwtyro.github.io/space-3d/#animationSpeed=2.951618060622956&fov=90&nebulae=true&pointStars=true&resolution=2048&seed=2bjl0mehpyf4&stars=true&sun=false
			//Good seeds:
			//2bjl0mehpyf4

			float inverselerp(float a, float b, float value)
			{
				return (value - a) / (b - a);
			}

			float sdSphere(float3 p, float radius)
			{
				return length(p) - radius;
			}

			float sdCircle(float2 p, float radius)
			{
				return length(p) - radius;
			}

			float sdTorus(float3 p, float width, float radius)
			{
				return length(float2(length(p.xz) - radius, p.y)) - width;
			}

			float sdPlaneBox(float3 p, float height)
			{
				return abs(p.y) - height;
			}

			float sdDiskBox(float3 p, float inner, float outer, float height)
			{
				float ring = max(-sdCircle(p.xz, inner), sdCircle(p.xz, outer));
				return max(ring, sdPlaneBox(p, height));
			}

			float sdAccretion(float3 p)
			{
				return sdDiskBox(p, DISK_INNER_RADIUS, DISK_OUTER_RADIUS, DISK_VOLUMETRIC_HEIGHT);
			}

			float to0to1(float value)
			{
				return (value + 1) / 2;
			}

			float to1to1(float value)
			{
				return value * 2 - 1;
			}

			float corner(float value, float power)
			{
				return saturate(1 / (1 - min(value, 1)) / (power * power));
			}

			float3 SampleSkybox(float3 direction) //direction should be in object/local space, this method will convert it
			{
				float4 worldDirection = mul(_ObjectToWorldMatrix, float4(direction, 0));
				float intensity = GetCurrentExposureMultiplier() * _SkyboxBrightnessMultiplier;

				return texCUBElod(_SkyboxCube, worldDirection).xyz * intensity;
			}

			float3 SampleNoiseTexture(float u, float v)
			{
				return tex2Dlod(_MainNoiseSource, float4(u, v, 0, 0)).rgb;
			}

			float3 SampleNoiseTexture(float2 uv)
			{
				return SampleNoiseTexture(uv.x, uv.y);
			}

			float3 SampleAccretionDiskVolume(const float3 position)
			{
				float u = saturate(degrees(atan2(position.z, position.x)) / 360 + 0.5);
				float v = saturate(inverselerp(DISK_INNER_RADIUS, DISK_OUTER_RADIUS, length(position.xz)));

				float2 uShift = float2(_FastSpinSpeed, _SlowSpinSpeed) * _Time.yx;

				float3 mainColor = tex2Dlod(_MainColorGradient, float4(v, 0.5, 0, 0)).rgb;
				float brightness = tex2Dlod(_BrightnessGradient, float4(v, 0.5, 0, 0)).r;

				float mainNoise = to0to1(snoise(float3((u + uShift.x) * 5.5, v * 11.3, position.y * 14)));

				//return mainNoise;

				float bitsNoise = SampleNoiseTexture((u + uShift.y * 7) / 5, v / 6).r;
				float backNoise = SampleNoiseTexture((u - uShift.y * 3) / 7, v / 11).g;
				float diveNoise = SampleNoiseTexture((u + uShift.y * 4) / 8, (pow(v, 1.7) * 1.2 + uShift.y * 8) / 11).b;

				brightness = to1to1(brightness);
				mainNoise *= smoothstep(v * v / 2, 1, bitsNoise);

				float3 accumulation = 0;

				float back = smoothstep(min(0.66 - v * 0.18, 1), 1, backNoise) * 3.7; //The part that goes in reverse direction
				float dive = pow(smoothstep(min(mainNoise * 0.08 + 0.53, 1), 1, diveNoise), 1.4) * 9; //The part that slowly dives into the blackhole
				float extra = (1 - mainNoise) * max(brightness * 0.65 - v, 0); //Extra bits of light to make the inner ring smoother

				accumulation += mainColor * (mainNoise + extra + back + dive);
				accumulation *= exp((accumulation + brightness) * _BrightnessMultiplier);

				//return accumulation;

				float threshold = pow(saturate(abs(position.y) / DISK_VOLUMETRIC_HEIGHT), 2) * v;
				return accumulation * smoothstep(threshold - 0.15, threshold + 0.15, mainNoise);
			}

			void MarchForward(inout float3 position, inout float3 direction, inout float blackholeDistance, const float stepSize)
			{
				direction = normalize(direction + -position * stepSize / pow(blackholeDistance, 3) * SCHWARZSCHILD);

				//The previous line is identical to this, it just avoids using an extra normalize function
				//direction = normalize(direction + -normalize(position) * stepSize / pow(blackholeDistance, 2) * SCHWARZSCHILD);

				//Moves ray forward
				position += direction * stepSize;
				blackholeDistance = length(position);
			}

			float3 RayMarch(const float3 rayOrigin, const float3 rayDirection)
			{
				float3 position = rayOrigin;
				float3 direction = rayDirection; //Should always be normalized

				float3 light = 0;

				//First march positions rays really close to accretion disk
				position += direction * max(sdSphere(position, DISK_OUTER_RADIUS), 0);

				float blackholeDistance = length(position);
				float sdValue = sdAccretion(position);

				for (int i = 0; i < MAX_STEPS; i++)
				{
					//Gets step size
					float stepSize = clamp(sdValue, _MinStepSize, _MaxStepSize);
					float sdValueOld = sdValue;

					//Distorts ray and moves it forward
					MarchForward(position, direction, blackholeDistance, stepSize);

					sdValue = sdAccretion(position);
					float sdValueMin = min(sdValue, sdValueOld);

					if (sdValueMin < 0) //If inside accretion disk volume
					{
						//An estimated travel distance inside the volume for this step.
						//This value becomes more inaccurate as the vector becomes more parallel to the surface.
						//This value is used to roughly correct the discontinuation of the start tracing position inside disk volume.
						float volumetric = min(stepSize, -sdValueMin);
						float3 accumulation = SampleAccretionDiskVolume(position);

						//return accumulation;

						light += accumulation * (volumetric / DISK_VOLUMETRIC_HEIGHT);
					}

					//If the ray is exiting the accretion disk volume sphere, we stop the march
					if (blackholeDistance >= DISK_OUTER_RADIUS && dot(-position, direction) < 0)
					{
						MarchForward(position, direction, blackholeDistance, SKYBOX_MARCH_RADIUS - blackholeDistance);
						light += SampleSkybox(direction); //Samples skybox

						break;
					}

					//If the ray entered the event horizon
					if (blackholeDistance <= SCHWARZSCHILD)
					{
						//NOTE: Since the distortion at the event horizon is extremely volatile, the event horizon would appear
						//to be a dotted ring because rays at some pixels will hit the accretion disk, while others would not.
						//In order to remove this artifact, we place a solid ring at the event horizon using the following lines.

						float horizonStep = smoothstep(0.1, 0.15, dot(position, direction));
						light += horizonStep * _EventHorizonColor.rgb * _EventHorizonBrightnessMultiplier;

						break;
					}
				}

				return light;

				//If we are not branching out from the march
				//return light + SampleSkybox(direction); //Samples skybox
			}

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_VP, mul(UNITY_MATRIX_M, v.vertex)); //For some reason UNITY_MATRIX_MVP is missing so we have to do the multiplication mannually

				//Must use float4 for point transformation in matrix multiplication instead of vector transformation
				float4 cameraPoint = float4(_WorldSpaceCameraPos, 1);

				//Both in object space so the origin is the object pivot
				o.rayOrigin = mul(_WorldToObjectMatrix, cameraPoint).xyz; //Convert world space into object space
				o.hitPosition = v.vertex.xyz;

				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float3 rayOrigin = i.rayOrigin;
				float3 rayDirection = normalize(i.hitPosition - rayOrigin);

				return float4(RayMarch(rayOrigin, rayDirection), 1);
			}
			ENDHLSL
		}
	}
	Fallback Off
}