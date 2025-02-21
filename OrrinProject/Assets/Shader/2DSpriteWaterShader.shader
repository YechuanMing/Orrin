Shader "Custom/2DSpriteLocalWaveShader"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _WaterColor("Water Color", Color) = (0, 0.5, 1, 1)
        _ShallowColor("Shallow Color", Color) = (0, 0.7, 1, 1)
        _DeepColor("Deep Color", Color) = (0, 0.3, 0.6, 1)
        _WaveSpeed("Wave Speed", Range(0, 10)) = 2
        _WaveAmplitude("Wave Amplitude", Range(0, 0.5)) = 0.1
        _WaveFrequency("Wave Frequency", Range(1, 20)) = 5
        _DepthFactor("Depth Factor", Range(0, 10)) = 2
        _NoiseScale("Noise Scale", Range(0.1, 10)) = 2
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

            // Perlin 噪声函数
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            float perlinNoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);

                float a = rand(i);
                float b = rand(i + float2(1, 0));
                float c = rand(i + float2(0, 1));
                float d = rand(i + float2(1, 1));

                float2 u = f * f * (3 - 2 * f);

                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float depth : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _WaterColor;
            fixed4 _ShallowColor;
            fixed4 _DeepColor;
            float _WaveSpeed;
            float _WaveAmplitude;
            float _WaveFrequency;
            float _DepthFactor;
            float _NoiseScale;

            v2f vert(appdata v)
            {
                v2f o;

                // 计算包含时间的噪声坐标
                float2 noiseCoord = v.uv * _NoiseScale + float2(_Time.y * _WaveSpeed, 0);

                // 生成 Perlin 噪声
                float noiseValue = perlinNoise(noiseCoord);

                // 计算波浪偏移
                float waveOffset = _WaveAmplitude * sin((v.uv.x * UNITY_PI * 2 + _Time.y * _WaveSpeed) * _WaveFrequency) * noiseValue;

                v.vertex.y += waveOffset;

                // 将顶点从模型空间转换到裁剪空间
                o.vertex = UnityObjectToClipPos(v.vertex);

                // 计算深度信息
                o.depth = v.uv.y * _DepthFactor;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 采样基础纹理
                fixed4 texCol = tex2D(_MainTex, i.uv);

            // 根据深度插值颜色
            fixed t = saturate(i.depth);
            fixed4 depthCol = lerp(_ShallowColor, _DeepColor, t);

            // 混合颜色
            fixed4 finalCol = _WaterColor * texCol * depthCol;
            finalCol.a = _WaterColor.a * texCol.a * depthCol.a;

            return finalCol;
        }
        ENDCG
    }
        }
            FallBack "Diffuse"
}