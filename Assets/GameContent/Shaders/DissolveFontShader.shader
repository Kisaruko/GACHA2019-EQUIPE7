// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Dissolve Text" {
	Properties{
		_MainTex("Font Texture", 2D) = "white" {}
		_Color("Text Color", Color) = (1,1,1,1)
		_DissolveAmount("Dissolve Amount", Range(0,1)) = 0
		_DissolveTexture("Dissolve Texture", 2D) = "black"
	}

		SubShader{

			Tags {
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
			}
			Lighting Off Cull Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float2 texcoord1 : TEXCOORD1;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float2 texcoord1 : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				sampler2D _DissolveTexture;
				uniform float4 _MainTex_ST;
				uniform float4 _DissolveTexture_ST;
				uniform fixed4 _Color;
				float _DissolveAmount;

				v2f vert(appdata_t v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color * _Color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _DissolveTexture);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = i.color;
					fixed dissolve = round((1 - tex2D(_DissolveTexture, i.texcoord1).r) - _DissolveAmount + 0.5f);
					col.a *= tex2D(_MainTex, i.texcoord).a * i.color.a * - dissolve;
					return col;
				}
				ENDCG
			}
		}
}
