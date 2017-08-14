// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Northwind/Examples/MatEdit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TintColor ("Tint Color", Color) = (1,1,1,1)

		_ScrollingDuration ("Texture Scrolling Duration", float) = 5
		_ScrollingSpeed ("Texture Scrolling Speed", 2D) = "black" {}

		_TestVector ("Test Vector", Vector) = (0,0,0,0)

		_TestGradient ("Test Gradient", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _TintColor;
			
			float _ScrollingDuration;
			sampler2D _ScrollingSpeed;

			float4 _TestVector;

			sampler2D _TestGradient;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float lEvalPos = (_Time.y / _ScrollingDuration) % 1;

				float4 lScrollSpeedRaw = tex2D(_ScrollingSpeed, float2(lEvalPos, 0.5));
				float lScrollSpeed = lScrollSpeedRaw.x + lScrollSpeedRaw.y + lScrollSpeedRaw.z;

				float4 col = tex2D(_MainTex, i.uv + _Time.y + float2(1, 0) * lScrollSpeed) * _TintColor;
				col = tex2D(_TestGradient, i.uv);
				return col;
			}
			ENDCG
		}
	}
	CustomEditor "ExampleUnlitShader_Editor"
}
