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

                sampler2D _MainTex;     // ����
                float4 _FlashColor;     // ��˸��ɫ

                // ������ɫ���������������UV���괫�ݸ�ƬԪ��ɫ��
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex); // ת������ռ�����
                    o.uv = v.uv;  // ����UV����
                    return o;
                }

                // ƬԪ��ɫ��������������ɫ����˸��ɫ�Ļ��
                half4 frag(v2f i) : SV_Target
                {
                    half4 texColor = tex2D(_MainTex, i.uv);   // ��ȡ������ɫ
                    return texColor * _FlashColor;             // ��������ɫ����˸��ɫ���
                }
                ENDCG
            }
        }
}


