
Shader "Universal Render Pipeline/Unlit Cutout Shader (With shadows)" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1) // Color property
        _MainTex ("Main Texture", 2D) = "white" {} // Main Texture property
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5 // Alpha cutoff property
    }

    SubShader {
        Tags {
            "Queue"="Transparent" // Set the render queue to Transparent
            "RenderType"="Transparent" // Set the render type to Transparent
        }

        LOD 100 // Set the level of detail

        // Include the Core.hlsl file, which has common definitions and functions used in URP
        CGINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDCG

        Pass {
            Tags {
                "LightMode"="UniversalForward" // Use UniversalForward for the lighting mode
            }

            HLSLPROGRAM
            #pragma vertex vert // Vertex shader function
            #pragma fragment frag // Fragment shader function
            #pragma multi_compile _ _ALPHATEST_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" // Include Lighting.hlsl

            // Input struct for vertex shader
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Output struct for vertex shader and input struct for fragment shader
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Properties for the shader
            sampler2D _MainTex;
            float4 _Color;
            float _Cutoff;

            // Vertex shader function
            v2f vert (appdata v) {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader function
            half4 frag (v2f i) : SV_Target {
                half4 col = tex2D(_MainTex, i.uv) * _Color; // Sample the texture and multiply by color
                clip(col.a - _Cutoff); // Clip fragments based on the alpha cutoff value
                col.a = step(_Cutoff, col.a); // Set alpha to 1 if it's greater than or equal to the cutoff value
                return col;
            }
            ENDHLSL
        }

        Pass {
            Tags {
                "LightMode"="ShadowCaster" // Set the lighting mode to ShadowCaster
            }

            HLSLPROGRAM
            #pragma vertex vertShadow // Vertex shader function for shadow casting
            #pragma fragment fragShadow // Fragment shader function for shadow casting
            #pragma multi_compile_shadowcaster
            #pragma multi_compile _ _ALPHATEST_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" // Include Core.hlsl

            // Input struct for the shadow casting vertex shader
            struct appdataShadow {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Output struct for the shadow casting vertex shader and input struct for the shadow casting fragment shader
            struct v2fShadow {
                float2 uv : TEXCOORD0;
               
                float4 vertex : SV_POSITION;
            };

            // Properties for the shadow casting shader
            sampler2D _MainTex;
            float _Cutoff;

            // Shadow casting vertex shader function
            v2fShadow vertShadow (appdataShadow v) {
                v2fShadow o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            // Shadow casting fragment shader function
            float fragShadow (v2fShadow i) : SV_Depth {
                float alpha = tex2D(_MainTex, i.uv).a; // Sample the alpha channel from the texture
                clip(alpha - _Cutoff); // Clip fragments based on the alpha cutoff value
                return 1.0;
            }
            ENDHLSL
        }
    }

    FallBack "Diffuse"
}
