Shader "UI/GreyscaleShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _GreyScale("Grey Scale", Float) = 1
        _Stencil("Stencil Reference", Float) = 1
        _StencilComp("Stencil Comparison", Float) = 8 // 8 = Always
        _StencilOp("Stencil Operation", Float) = 3  // 3 = Replace
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        // Stencil setup: customizable via properties
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GreyScale;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (_GreyScale > 0.5)
                {
                    float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                    col.rgb = float3(grey, grey, grey);
                }
                return col;
            }
            ENDCG
        }
    }
}