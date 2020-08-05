// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Caustic_transparent" {
	Properties {
		_Color ("main color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_CausticTex ("caustic texture", 2D) = "white" {}
		_CausticIntensity ("caustic intensity", Range(0.1, 1.0)) = 0.5
	}
	SubShader {
		//set lightmode as forwardbase 
		//Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "LightMode"="ForwardBase"}
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "LightMode"="ForwardBase"}
		ZWrite off
		LOD 200
		
		Pass
		{
			Blend  SrcAlpha OneMinusSrcAlpha
			offset -1, -1
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile  UVAnimation BlendCaustic ProjectorCaustic

			float4 _Color;
			sampler2D _MainTex;
			sampler2D _CausticTex;
			float4 _MainTex_ST;
			float4 _CausticTex_ST;
			float _CausticIntensity;
			//define in script
			float4x4 _MyProjector;
			float4 _CausticFrameVar;

			struct appdata
			{
				float4 vertex:POSITION;
				float3 normal:NORMAL;
				float2 texcoord:TEXCOORD0;
			};
			struct v2f
			{
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
				float intensity:TEXCOORD1;
				float2 uvCaustic:TEXCOORD2;
			};

			v2f vert(appdata v)
			{
				v2f o;
				
				//#define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)
				//equal: o.uv = v.texcoord.xy * tiling + offset
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);	
				_MyProjector = mul(_MyProjector, unity_ObjectToWorld);
				o.pos = mul(_MyProjector, v.vertex);
				o.uvCaustic = float2(o.pos.x/o.pos.w, o.pos.y/o.pos.w);//ignor z
				o.uvCaustic = o.uvCaustic*0.5+0.5;
				o.uvCaustic = TRANSFORM_TEX(o.uvCaustic, _CausticTex);
				
				//directional light: mul(_World2Object, _WorldSpaceLightPos0).xyz
				float3 toLight = ObjSpaceLightDir(v.vertex);

				o.intensity = dot(v.normal, toLight);
				o.pos = UnityObjectToClipPos(v.vertex);

				return o;
			}

			float4 frag(v2f i):COLOR
			{
				float4 result;

				int mask = _CausticFrameVar.x;
				float2 aniUV = float2(_CausticFrameVar.y, _CausticFrameVar.z);
				aniUV = frac(i.uvCaustic)*float2(0.25, 0.25)+aniUV;
				
				float4 causticCol = tex2D(_CausticTex, aniUV);
				float causticIntensity = 1.0;
				if(mask == 0)
				{
					causticIntensity = causticCol.r;
				}
				else if(mask == 1)
				{
					causticIntensity = causticCol.g;
				}
				else if(mask == 2)
				{
					causticIntensity = causticCol.b;
				}
				causticIntensity=1.0+causticIntensity*_CausticIntensity;
				result = tex2D(_MainTex, i.uv);
				result.rgb = result.rgb * _Color * i.intensity * causticIntensity;
				
				return result;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
