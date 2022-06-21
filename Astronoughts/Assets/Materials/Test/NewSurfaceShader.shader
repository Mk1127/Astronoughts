Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _BaseColor ("Base Color", 2D) = "white" {}
        _Roughness ("Roughness", 2D) = "white" {}
        _Metallic ("Metallic", 2D) = "white" {}
        _Normal ("Normal", 2D) = "white" {}
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

        sampler2D _BaseColor;
        sampler2D _Roughness;
        sampler2D _Metallic;
        sampler2D _Normal;

        struct Input
        {
            float2 uv_BaseColor;
            float2 uv_Roughness;
            float2 uv_Metallic;
            float2 uv_Normal;
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_BaseColor, IN.uv_BaseColor) * _Color;
            half m = tex2D (_Metallic, IN.uv_Metallic);
            half s = tex2D(_Roughness, IN.uv_Roughness);
            half n = tex2D(_Normal, IN.uv_Normal);
            o.Albedo = c.rgb;
            o.Metallic = m;
            o.Smoothness = s;
            o.Normal = n;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
