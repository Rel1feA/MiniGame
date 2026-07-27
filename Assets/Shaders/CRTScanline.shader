Shader "Hidden/CRTScanline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlineIntensity ("扫描线强度", Range(0, 1)) = 0.5
        _ScanlineCount    ("扫描线数量", Range(50, 500)) = 180
        _ScanlineSpeed    ("扫描线滚动速度", Range(-5, 5)) = 0
        _Curvature        ("屏幕曲率", Range(0, 0.1)) = 0.02
        _VignetteStrength ("暗角强度", Range(0, 1)) = 0.15
        _Brightness       ("亮度", Range(0.5, 1.5)) = 1.05
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _ScanlineSpeed;
            float _Curvature;
            float _VignetteStrength;
            float _Brightness;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // 简单桶形畸变，模拟 CRT 屏幕弧度
            float2 barrelDistort(float2 uv, float amount)
            {
                float2 centered = uv - 0.5;
                float r2 = dot(centered, centered);
                return uv + centered * amount * r2;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 1. 屏幕曲率（桶形畸变）
                float2 distortedUV = barrelDistort(i.uv, _Curvature);

                // 曲率超出屏幕范围的部分直接黑边
                if (distortedUV.x < 0 || distortedUV.x > 1 ||
                    distortedUV.y < 0 || distortedUV.y > 1)
                    return fixed4(0, 0, 0, 1);

                // 2. 采样原图
                fixed4 col = tex2D(_MainTex, distortedUV);

                // 3. 扫描线
                float scanline = sin((distortedUV.y + _Time.y * _ScanlineSpeed) * _ScanlineCount * UNITY_PI);
                scanline = saturate(scanline);
                // 让暗线更明显
                scanline = lerp(1.0, scanline, _ScanlineIntensity);
                col.rgb *= scanline;

                // 4. 暗角（边缘变暗）
                float2 vigUV = i.uv - 0.5;
                float vignette = 1.0 - dot(vigUV, vigUV) * 4.0 * _VignetteStrength;
                vignette = saturate(vignette);
                col.rgb *= vignette;

                // 5. 亮度
                col.rgb *= _Brightness;

                return col;
            }
            ENDCG
        }
    }
}
