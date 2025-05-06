Shader "Custom/GPTOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness (in UV)", Range(0.001, 0.05)) = 0.01
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;

                // 외곽선 샘플링 (8방향)
                float outline = 0.0;
                float2 offset = float2(_OutlineThickness, 0);
                float2 offsetY = float2(0, _OutlineThickness);

                float surrounding =
                    tex2D(_MainTex, i.uv + offset).a +
                    tex2D(_MainTex, i.uv - offset).a +
                    tex2D(_MainTex, i.uv + offsetY).a +
                    tex2D(_MainTex, i.uv - offsetY).a +
                    tex2D(_MainTex, i.uv + offset + offsetY).a +
                    tex2D(_MainTex, i.uv - offset + offsetY).a +
                    tex2D(_MainTex, i.uv + offset - offsetY).a +
                    tex2D(_MainTex, i.uv - offset - offsetY).a;

                // 알파가 0인데 주변 픽셀이 있으면 외곽선
                if (alpha == 0 && surrounding > 0)
                {
                    return _OutlineColor;
                }

                // 아니면 원래 텍스처 반환
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}