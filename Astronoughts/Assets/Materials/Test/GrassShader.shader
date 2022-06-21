Shader "Custom/GrassShader"
{
    Properties
    {
        _ColorTex ("Color Seed Texture", 2D) = "white" {}
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _ColorTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_ColorTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        //Random rand;
        //half randR;
        //half randG;
        //half randB;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //fixed4 randomRGB = (randomize(randR,randG,randB), 1);
            // c is final albedo, multiplies main texture by random color from seed image
            //rand = new Random();
            //half randomU= rand.NextDouble;
            //half randomV= rand.NextDouble;
            //fixed2 randomUV= (randomU, randomV);
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * tex2D(_ColorTex, randomUV);
            //o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
