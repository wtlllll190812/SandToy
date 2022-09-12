Shader "Custom/model"
{
    Properties
    {
        _MainTex("MainTex",2D) = "White"{}
        _BaseColor("BaseColor",Color) = (1,1,1,1)
    }
    SubShader
    {
        Cull off
        Tags
        {"RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque"}

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        CBUFFER_START(UnityPerMaterial)         
            float4 _MainTex_ST;
            half4 _BaseColor;
            float4 _OutlineColor;
            float _Outline;
        CBUFFER_END
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);


        struct a2v
        {
            float4 vertex:POSITION;
            float3 normal:NORMAL;
            float2 uv:TEXCOORD;
        };
        struct v2f
        {
            float4 pos:SV_POSITION;
            float2 uv:TEXCOORD;
            float3 worldNormal:TEXCOORD1;
            float3 worldPos:TEXCOORD2;
        };

        v2f vert(a2v v)
        {
            v2f o;
            o.pos = TransformObjectToHClip(v.vertex);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
            o.worldNormal=TransformObjectToWorldNormal(v.normal);
            o.uv = TRANSFORM_TEX(v.uv,_MainTex);
            return o;
        }

        ENDHLSL
        pass
        {
            Tags{ "LightMode" = "UniversalForward" }
            Cull back
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS //主光源阴影
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE //主光源层级阴影是否开启
            #pragma multi_compile _ _SHADOWS_SOFT //软阴影


            half4 frag(v2f i) :SV_TARGET
            {
                float4 SHADOW_COORDS = TransformWorldToShadowCoord(i.worldPos);
                Light mainLight = GetMainLight(SHADOW_COORDS);
                half shadow = mainLight.shadowAttenuation;
                
                half3 lightDir=normalize(_MainLightPosition.xyz);
                half3 worldNormal=normalize(i.worldNormal);
                half lambert=saturate(dot(worldNormal,lightDir)*shadow);

                _BaseColor=_BaseColor*SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
                half4 finalRGB=_BaseColor*lambert;//(SAMPLE_TEXTURE2D(_RampMap,sampler_RampMap,half2(lambert, ))+0.1f);
                return finalRGB;
            }
            ENDHLSL
        }
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}