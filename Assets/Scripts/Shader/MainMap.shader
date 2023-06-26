Shader "Custom/MainMap"
{
    Properties
    {
        _MapTex ("MapTex", 2D) = "black" {}
        _ColorTex ("ColorTex", 2D) = "black" {}
        [Toggle(_True)]_debugY("debugY", float) = 0
        [Toggle(_True)]_debugZ("debugZ", float) = 0
        [Toggle(_True)]_debugW("debugW", float) = 0
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
            Texture2D _ColorTex;
            float4 _MapTex_ST;
            float4 _MapTex_TexelSize;
            bool _debugY;
            bool _debugZ;
            bool _debugW;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int kind=tex2D(_MapTex, i.uv).x*32;
                if(_debugY)
                    kind=tex2D(_MapTex, i.uv).y;
                else if(_debugZ)
                    kind=tex2D(_MapTex, i.uv).z;
                else if(_debugW)
                    kind=tex2D(_MapTex, i.uv).w;
                float4 color=_ColorTex[uint2(kind,0)];
                return color;
            }
            ENDCG
        }
    }
}