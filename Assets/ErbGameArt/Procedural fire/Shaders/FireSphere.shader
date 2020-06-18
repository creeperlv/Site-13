Shader "EGA/Particles/FireSphere"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Emission("Emission", Float) = 2
		_StartFrequency("Start Frequency", Float) = 4
		_Frequency("Frequency", Float) = 10
		_Amplitude("Amplitude", Float) = 1
		[Toggle(_USEDEPTH_ON)] _Usedepth("Use depth?", Float) = 0
		_Depthpower("Depth power", Float) = 1
		[Toggle(_USEBLACK_ON)] _Useblack("Use black", Float) = 0
		_Opacity("Opacity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  "PreviewType" = "Plane" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature _USEBLACK_ON
		#pragma shader_feature _USEDEPTH_ON
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float _Emission;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float _StartFrequency;
		uniform float _Amplitude;
		uniform float _Frequency;
		uniform float _Opacity;
		uniform sampler2D _CameraDepthTexture;
		uniform float _Depthpower;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 temp_output_121_0 = ( _Emission * _Color * i.vertexColor );
			float2 temp_output_8_0 = ( ( ( float2( 0.2,0 ) * _Time.y ) + i.uv_texcoord ) * _StartFrequency );
			float2 break18 = floor( temp_output_8_0 );
			float temp_output_21_0 = ( break18.x + ( break18.y * 57.0 ) );
			float2 temp_output_10_0 = frac( temp_output_8_0 );
			float2 temp_cast_0 = (3.0).xx;
			float2 break17 = ( temp_output_10_0 * temp_output_10_0 * ( temp_cast_0 - ( temp_output_10_0 * 2.0 ) ) );
			float lerpResult39 = lerp( frac( ( 473.5 * sin( temp_output_21_0 ) ) ) , frac( ( 473.5 * sin( ( 1.0 + temp_output_21_0 ) ) ) ) , break17.x);
			float lerpResult38 = lerp( frac( ( 473.5 * sin( ( 57.0 + temp_output_21_0 ) ) ) ) , frac( ( 473.5 * sin( ( 58.0 + temp_output_21_0 ) ) ) ) , break17.x);
			float lerpResult40 = lerp( lerpResult39 , lerpResult38 , break17.y);
			float2 temp_output_51_0 = ( ( ( float2( 0.5,0.5 ) * _Time.y ) + ( i.uv_texcoord * ( lerpResult40 * _Amplitude ) ) ) * _Frequency );
			float2 break87 = floor( temp_output_51_0 );
			float temp_output_90_0 = ( break87.x + ( break87.y * 57.0 ) );
			float2 temp_output_52_0 = frac( temp_output_51_0 );
			float2 temp_cast_1 = (3.0).xx;
			float2 break110 = ( temp_output_52_0 * temp_output_52_0 * ( temp_cast_1 - ( temp_output_52_0 * 2.0 ) ) );
			float lerpResult109 = lerp( frac( ( 473.5 * sin( temp_output_90_0 ) ) ) , frac( ( 473.5 * sin( ( 1.0 + temp_output_90_0 ) ) ) ) , break110.x);
			float lerpResult105 = lerp( frac( ( 473.5 * sin( ( 57.0 + temp_output_90_0 ) ) ) ) , frac( ( 473.5 * sin( ( 58.0 + temp_output_90_0 ) ) ) ) , break110.x);
			float lerpResult106 = lerp( lerpResult109 , lerpResult105 , break110.y);
			float Amp114 = _Amplitude;
			float4 tex2DNode117 = tex2D( _MainTex, ( i.uv_texcoord + ( 0.2 * ( lerpResult106 * Amp114 ) ) ) );
			#ifdef _USEBLACK_ON
				float4 staticSwitch128 = ( temp_output_121_0 * tex2DNode117 );
			#else
				float4 staticSwitch128 = temp_output_121_0;
			#endif
			o.Emission = staticSwitch128.rgb;
			float4 clampResult132 = clamp( ( i.vertexColor.a * tex2DNode117 * _Opacity ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth137 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float distanceDepth137 = abs( ( screenDepth137 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depthpower ) );
			float clampResult136 = clamp( distanceDepth137 , 0.0 , 1.0 );
			#ifdef _USEDEPTH_ON
				float4 staticSwitch140 = ( clampResult132 * clampResult136 );
			#else
				float4 staticSwitch140 = clampResult132;
			#endif
			o.Alpha = staticSwitch140.r;
		}

		ENDCG
	}
}