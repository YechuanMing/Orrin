Shader "Custom/text"
{
	Properties
	{

		_BlurTex("BlurTex",2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_Radius("Radius",Range(0,10)) = 1
		_Trans("Trans",Range(0,1)) = 1
		_BlurIntensity("BlurIntensity",Range(0,10)) = 3
	}
		SubShader
		{
				Tags { "Queue" = "Transparent" "RenderType" = "Transparent" } //�л�Ϊtransparent͸����Ⱦ����
				LOD 100

				ZWrite off  //͸��Ч������Ҫд����Ȼ�����
				Blend SrcAlpha OneMinusSrcAlpha  //alpha���

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
					float4 color : COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 color : COLOR;
				};


				sampler2D _BlurTex;//��Ҫģ������ͼ
				float _Radius;//��Ҫģ���İ뾶
				float4 _BlurTex_TexelSize;//��ͼ�����ش�С
				float _Trans;//͸���ĳ̶�
				float _BlurIntensity;//��Ҫģ���ĳ̶�


				fixed4 Blur(sampler2D tex, half2 uv, half2 blurSize) //����ģ���ĺ���������texΪ��Ҫģ����ͼ��uvΪ��Ҫģ��ͼ���uv���꣬blurSizeΪ��Ҫȡ��Χ���صķ�Χ
				{
					int KERNEL_SIZE = _BlurIntensity;
					float4 o = 0;
					float sum = 0;
					float weight;
					half2 texcoord;
					for (int x = -KERNEL_SIZE / 2; x <= KERNEL_SIZE / 2; x++)
					{
						for (int y = -KERNEL_SIZE / 2; y <= KERNEL_SIZE / 2; y++)
						{
							texcoord = uv;
							texcoord.x += blurSize.x * x;
							texcoord.y += blurSize.y * y;
							weight = 1.0 / (abs(x) + abs(y) + 2);
							o += tex2D(tex, texcoord) * weight;
							sum += weight;
						}
					}
					return o / sum;
				}


				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = Blur(_BlurTex, i.uv,_Radius * _BlurTex_TexelSize.xy);
					col.a = _Trans;
					return col;
				}
				ENDCG
			}
		}

		
}
