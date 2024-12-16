Shader "Custom/HitWhite"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _FlashColor("Flash Color", Color) = (1, 1, 1, 1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : POSITION;
                    float2 uv : TEXCOORD0;
                };

                sampler2D _MainTex;     // 纹理
                float4 _FlashColor;     // 闪烁颜色

                // 顶点着色器，将顶点坐标和UV坐标传递给片元着色器
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex); // 转换世界空间坐标
                    o.uv = v.uv;  // 传递UV坐标
                    return o;
                }

                // 片元着色器，计算纹理颜色和闪烁颜色的混合
                half4 frag(v2f i) : SV_Target
                {
                    half4 texColor = tex2D(_MainTex, i.uv);   // 获取纹理颜色
                    return texColor * _FlashColor;             // 将纹理颜色与闪烁颜色相乘
                }
                ENDCG
            }
        }
}


