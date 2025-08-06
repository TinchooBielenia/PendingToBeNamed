Shader "Custom/TreeWind"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WindStrength ("Wind Strength", Float) = 0.1
        _WindSpeed ("Wind Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        float _WindStrength;
        float _WindSpeed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert (inout appdata_full v)
        {
            // Agitá en Y según una sinusoide que depende del tiempo y posición
            float sway = sin(_Time.y * _WindSpeed + v.vertex.x * 0.5 + v.vertex.z * 0.5);
            // Solo afecta las partes que van para arriba (normal.y)
            v.vertex.y += sway * _WindStrength * v.normal.y;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
