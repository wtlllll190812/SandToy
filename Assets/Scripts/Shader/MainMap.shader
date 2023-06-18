Shader "Custom/MainMap"
{
    Properties
    {
        _MapTex ("MapTex", 2D) = "white" {}
        _ColorTex ("ColorTex", 2D) = "white" {}
    }	
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MapTex;
            sampler2D _ColorTex;
            float4 _MapTex_ST;
            float4 _MapTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float kind=tex2D(_MapTex, i.uv).r;
                float4 color=tex2D(_ColorTex, float2(kind+1/128.0,kind));
                return color;
            }
            ENDCG
        }
    }
}