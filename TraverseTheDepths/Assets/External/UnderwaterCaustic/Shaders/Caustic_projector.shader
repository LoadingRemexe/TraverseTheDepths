// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Caustic_projector" {
	Properties
	{
		_MainTex("cautics texture", 2D) = "caustic" {}
		_CausticIntensity("caustics strength", Range(0.0, 1.0)) = 0.5
	}
	Subshader
	{
		//Opaque
		Tags {"RenderType"="Opaque"  "LightMode"="ForwardBase"}
		Pass
		{
			Blend DstColor one
			//Blend one OneMinusSrcAlpha
			//Blend one one
			offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _CausticIntensity;

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			float4 _CausticFrameVar;

			struct appdata
			{
				float4 vertex:POSITION;
				float2 texcoord:TEXCOORD0;
				float4 normal : NORMAL;
			};
			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				half  intensity : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				//calculate uv
				o.pos = mul(unity_Projector, v.vertex);
				o.uv = float2(o.pos.x/o.pos.w, o.pos.y/o.pos.w);//ignore z
				o.uv = TRANSFORM_TEX(o.uv, _MainTex);
				//
				float3 lightDir = ObjSpaceLightDir(v.vertex);
				o.intensity = dot(normalize(v.normal), normalize(lightDir));
				o.intensity *= (1-mul(unity_ProjectorClip, v.vertex).x)*5;
				//
				o.pos = UnityObjectToClipPos(v.vertex);
				
				return o;
			}
			
			half4 frag(v2f i):COLOR
			{
				half4 result = float4(1.0, 1.0, 1.0, 1.0);
				int mask = _CausticFrameVar.x;
				float2 aniUV = float2(_CausticFrameVar.y, _CausticFrameVar.z);
				aniUV = frac(i.uv)*float2(0.25, 0.25)+aniUV;
				
				float4 causticCol = tex2D(_MainTex, aniUV);
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
				causticIntensity=i.intensity*causticIntensity*_CausticIntensity;

				return result*causticIntensity;
			}

			ENDCG
		}
	}
}
