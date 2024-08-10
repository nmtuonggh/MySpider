Shader "FX/Fres"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Fres ("Fres", Float) = 1
		_Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200
		
		ZWrite Off
		Lighting Off
		Blend OneMinusDstColor One

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

        half _Fres;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = _Color;
            o.Alpha = c.r *_Color.rgb * pow (rim, _Fres);
        }
        ENDCG
    }
    FallBack ""
}
