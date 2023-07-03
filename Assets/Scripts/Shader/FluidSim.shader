Shader "Custom/Fluid"
{
    Properties
    {
        _MapTex ("MapTex", 2D) = "white" {}
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
            float4 _MapTex_ST;
            float4 _MapTex_TexelSize;
            float4 _HighColor;
            float4 _LowColor;
            bool _showY;
            bool _showZ;
            bool _showW;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MapTex, i.uv);
            }
            ENDCG
        }
    }
}