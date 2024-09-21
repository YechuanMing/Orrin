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
				Tags { "Queue" = "Transparent" "RenderType" = "Transparent" } //切换为transparent透明渲染队列
				LOD 100

				ZWrite off  //透明效果不需要写入深度缓冲区
				Blend SrcAlpha OneMinusSrcAlpha  //alpha混合

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


				sampler2D _BlurTex;//需要模糊的贴图
				float _Radius;//需要模糊的半径
				float4 _BlurTex_TexelSize;//贴图的像素大小
				float _Trans;//透明的程度
				float _BlurIntensity;//需要模糊的程度


				fixed4 Blur(sampler2D tex, half2 uv, half2 blurSize) //计算模糊的函数方法，tex为需要模糊的图像，uv为需要模糊图像的uv坐标，blurSize为需要取周围像素的范围
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
