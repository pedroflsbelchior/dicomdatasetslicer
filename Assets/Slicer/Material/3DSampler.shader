Shader "Unlit/3DSampler"
{
    Properties
    {
        _MainTex ("Texture", 3D) = "white" {}
    }
    SubShader
    {
        Cull Off

        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler3D _MainTex;
            float4 _MainTex_ST;
            float3 _flipAxis;
            float3 _min;
            float3 _max;
            float2 _window;
            float4x4 _worldToLocal;

            float map(float s, float a1, float a2, float b1, float b2)
            {
                return b1 + (s-a1)*(b2-b1)/(a2-a1);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float3 wpos = mul(unity_ObjectToWorld, v.vertex);
                wpos = mul(_worldToLocal,wpos);

                wpos.x = map(wpos.x, _min.x, _max.x, 0, 1);
                wpos.y = map(wpos.y, _min.y, _max.y, 0, 1);
                wpos.z = map(wpos.z, _min.z, _max.z, 0, 1);

                if (_flipAxis.x == 1) wpos.x = 1 - wpos.x;
                if (_flipAxis.y == 1) wpos.y = 1 - wpos.y;
                if (_flipAxis.z == 1) wpos.z = 1 - wpos.z;

                o.uv = wpos.xzy;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (i.uv.x < 0 || i.uv.y < 0 || i.uv.z < 0 || 
                    i.uv.x > 1 || i.uv.y > 1 || i.uv.z > 1)
                { 
                    discard;
                    return fixed4(0, 0, 0, 0);
                }

                float r = tex3D(_MainTex, i.uv).r;
                r = map(r, _window.x, _window.y, 0, 1);
                fixed4 col = fixed4(r, r, r, 1);
                return col;
            }
            ENDCG
        }
    }
}
