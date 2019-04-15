Shader "Custom/Dissolve Texture Surface Rough Metallic"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset]_NormalMap("Normal Map", 2D) = "bump" {}
		[NoScaleOffset]_RoughMap ("Roughness", 2D) = "gray" {}
		[NoScaleOffset]_MetalMap ("Metallic", 2D) = "gray" {}
		[Header(Dissolve), Space]
		_DissolveMap("Dissolve Map", 2D) = "black" {}
		[NoScaleOffset]_DissolvePath("Dissolve Path", 2D) = "white" {}
		_DissolveAmount ("Dissolve Amount", Range(0,1)) = 0
		_HiddenColor("Hidden Color", Color) = (1,1,1,1)
		_HiddenEmissive("Hidden Emissive", Color) = (0,0,0,0)
		_ScrollSpeed("Scroll Speed", Range(0,1)) = 0.2
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
        sampler2D _NormalMap;
		sampler2D _RoughMap;
        sampler2D _MetalMap;
		sampler2D _DissolveMap;
		sampler2D _DissolvePath;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_DissolveMap;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed _DissolveAmount;
		fixed4 _HiddenColor;
		fixed4 _HiddenEmissive;
		float _ScrollSpeed;


		float2 Panner(float2 texcoord)
		{
			texcoord.x += _Time * _ScrollSpeed;
			return texcoord;
		}

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			fixed4 dissolveMap = tex2D(_DissolveMap, Panner(IN.uv_MainTex)) * 2;
			fixed4 dissolveMapInv = tex2D(_DissolveMap, Panner(-IN.uv_MainTex * 2)) * 2 ;

			fixed4 dissolvePath = tex2D(_DissolvePath, IN.uv_MainTex);

			_DissolveAmount = lerp(0.5, 1, _DissolveAmount);
			fixed dissolveAmount = round(1 - dissolveMap * dissolveMapInv * dissolvePath - _DissolveAmount + 0.5);

            o.Albedo = dissolveAmount * c.rgb + (1 - dissolveAmount) * _HiddenColor;
            // Metallic and smoothness come from slider variables
            o.Metallic = tex2D(_MetalMap, IN.uv_MainTex).r * dissolveAmount;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            o.Smoothness = tex2D(_RoughMap, IN.uv_MainTex).r * dissolveAmount;
            o.Alpha = c.a;
			o.Emission = (1 - dissolveAmount) * _HiddenEmissive;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
