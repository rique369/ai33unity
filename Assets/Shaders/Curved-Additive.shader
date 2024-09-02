// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Mobile/Aladdin Project/Curved-Additive" {
    
   Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	_QOffset ("Offset", Vector) = (0,0,0,0)
    _Dist ("Distance", Float) = 100.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off
	
	 SubShader {
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert alpha

        uniform sampler2D _MainTex;
        uniform float4 _QOffset;
        uniform float _Dist;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void vert( inout appdata_full v)
        {
            float4 vv = mul( unity_ObjectToWorld, v.vertex );
            vv.xyz -= _WorldSpaceCameraPos.xyz;
			float zOff = vv.z/_Dist;
			vv = _QOffset*zOff*zOff;
            v.vertex += mul(unity_WorldToObject, vv);
        }
 
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
}
    
}
