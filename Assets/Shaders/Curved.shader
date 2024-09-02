// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Mobile/VADE STUDIO/Curved" {
	    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _QOffset ("Offset", Vector) = (0,0,0,0)
        _Dist ("Distance", Float) = 100.0
        _Color ("Fog Color", Color) = (1,1,1,1)
        _FogStart("Fog Start", float) = 40
        _FogEnd("Fog End", float) = 150
 
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
       
        CGPROGRAM
        #pragma surface surf Lambert vertex:myvert finalcolor:mycolor 
 
        sampler2D _MainTex;
        uniform half4 _Color;
        uniform half _FogStart;
        uniform half _FogEnd;
 		uniform float4 _QOffset;
        uniform float _Dist;
        float4 color : COLOR;

        struct Input
        {
            float2 uv_MainTex;
            half Myfog;
        };

        void myvert (inout appdata_full v, out Input data)
        {
        	float4 vv = mul( unity_ObjectToWorld, v.vertex );
            vv.xyz -= _WorldSpaceCameraPos.xyz;
			float zOff = vv.z/_Dist;
			vv = _QOffset*zOff*zOff;
            v.vertex += mul(unity_WorldToObject, vv);
            UNITY_INITIALIZE_OUTPUT(Input,data);
            float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
 
            float diff = _FogEnd - _FogStart;
            float invDiff = 1.0f / diff;
            data.Myfog = clamp ((_FogEnd - pos) * invDiff, 0.0, 1.0);
        }
        void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
        {
            fixed3 fogColor = _Color.rgb;
            #ifdef UNITY_PASS_FORWARDADD
            fogColor = 0;
            #endif
            color.rgb = lerp (fogColor, color.rgb, IN.Myfog);
        }
 
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Mobile/Diffuse"
}
