Shader "Custom/FireSim"
{
    Properties
    {
        _MapTex ("MapTex", 2D) = "white" {}
        
        _PlanetColor("PlanetColor",Color)=(1,1,1,1)
        _GroundColor("GroundColor",Color)=(1,1,1,1)
        _FrieColor("FrieColor",Color)=(1,1,1,1)
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv[9] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MapTex;
            float4 _MapTex_ST;
            float4 _MapTex_TexelSize;

            float4 _PlanetColor;
            float4 _GroundColor;
            float4 _FrieColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv[0] = v.uv+_MapTex_TexelSize.xy*half2(0,0);
                o.uv[1] = v.uv+_MapTex_TexelSize.xy*half2(1,0);
                o.uv[2] = v.uv+_MapTex_TexelSize.xy*half2(1,1);
                o.uv[3] = v.uv+_MapTex_TexelSize.xy*half2(0,1);
                o.uv[4] = v.uv+_MapTex_TexelSize.xy*half2(-1,1);
                o.uv[5] = v.uv+_MapTex_TexelSize.xy*half2(-1,0);
                o.uv[6] = v.uv+_MapTex_TexelSize.xy*half2(-1,-1);
                o.uv[7] = v.uv+_MapTex_TexelSize.xy*half2(0,-1);
                o.uv[8] = v.uv+_MapTex_TexelSize.xy*half2(1,-1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float height = tex2D(_MapTex, i.uv[0]).a;

                if(height>0.75f)
                    return _FrieColor;
                if(height>0.25f)
                    return _PlanetColor;
                return _GroundColor;
            }
            ENDCG
        }
    }
}
