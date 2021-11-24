// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Effects/SH_Energy"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Texture2("Texture 2", 2D) = "white" {}
		_Texture3("Texture 3", 2D) = "white" {}
		_Alpha("Alpha", 2D) = "white" {}
		_intensity("intensity", Range( 0 , 30)) = 1
		_Opacity("Opacity", Range( 0 , 100)) = 1
		_ColorOuter("Color Outer", Vector) = (1,0.5,0.5,0)
		_Emmision("Emmision", Range( 0 , 5)) = 0.2633788
		_Refract("Refract %", Range( 0 , 1)) = 0.05
		_Colorinner("Color inner", Vector) = (20,13,20,1)
		_T_Blobs("T_Blobs", 2D) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
			float2 texcoord_1;
			float4 vertexColor : COLOR;
		};

		uniform float4 _Colorinner;
		uniform float3 _ColorOuter;
		uniform sampler2D _Alpha;
		uniform float _Refract;
		uniform sampler2D _T_Blobs;
		uniform sampler2D _Texture2;
		uniform sampler2D _Texture3;
		uniform float _intensity;
		uniform float _Emmision;
		uniform float _Opacity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			o.texcoord_1.xy = v.texcoord.xy * float2( 1,0.5 ) + float2( 0,0 );
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_output_67_0 = ( i.texcoord_0 + ( _Refract * tex2D( _T_Blobs, (abs( i.texcoord_1+_Time[1] * float2(0.04,-0.2 ))) ).g ) );
			float cos86 = cos( _SinTime );
			float sin86 = sin( _SinTime );
			float2 rotator86 = mul(temp_output_67_0 - float2( 0,1 ), float2x2(cos86,-sin86,sin86,cos86)) + float2( 0,1 );
			float cos71 = cos( _SinTime.y );
			float sin71 = sin( _SinTime.y );
			float2 rotator71 = mul(temp_output_67_0 - float2( 1,0 ), float2x2(cos71,-sin71,sin71,cos71)) + float2( 1,0 );
			float4 temp_output_18_0 = ( tex2D( _Alpha, temp_output_67_0 ) * ( ( tex2D( _Texture2, rotator86 ).r * tex2D( _Texture3, rotator71 ) ) * _intensity ) );
			o.Emission = ( ( ( ( _Colorinner * float4( _ColorOuter , 0.0 ) ) * temp_output_18_0 ) * _Emmision ) * i.vertexColor ).xyz;
			float4 clampResult26 = clamp( ( i.vertexColor.a * ( temp_output_18_0 * _Opacity ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Alpha = clampResult26.x;
		}

		ENDCG
	}
	//CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=12001
176;552;1565;808;4918.143;106.1791;2.5;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-4345.996,449.5005;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.PannerNode;19;-3964.802,456.9005;Float;False;0.04;-0.2;2;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;64;-3731.487,309.7986;Float;False;Property;_Refract;Refract %;7;0;0.05;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;63;-3749.587,454.6004;Float;True;Property;_T_Blobs;T_Blobs;9;0;Assets/VFX/Textures/T_Blobs.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-3416.786,428.9995;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-3426.986,275.6993;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;67;-3105.185,219.5995;Float;True;2;2;0;FLOAT2;0.0;False;1;FLOAT;0.0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.SinTimeNode;69;-2741.284,568.9982;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SinTimeNode;87;-2698.443,265.4969;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RotatorNode;71;-2533.784,497.3995;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1.0;False;1;FLOAT2
Node;AmplifyShaderEditor.RotatorNode;86;-2537.243,172.9976;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,1;False;2;FLOAT;1.0;False;1;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;7;-2292.602,436.2002;Float;True;Property;_Texture3;Texture 3;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-2284.201,190.2;Float;True;Property;_Texture2;Texture 2;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1935.101,313.9999;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;12;-1953.3,546.7001;Float;False;Property;_intensity;intensity;3;0;1;0;30;0;1;FLOAT
Node;AmplifyShaderEditor.Vector3Node;31;-1438.501,-569.1993;Float;True;Property;_ColorOuter;Color Outer;5;0;1,0.5,0.5;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector4Node;30;-1640.499,-739.9991;Float;True;Property;_Colorinner;Color inner;8;0;20,13,20,1;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1681.6,311.4;Float;True;2;2;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;15;-2115.799,-164.9001;Float;True;Property;_Alpha;Alpha;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;25;-1111.799,525.5007;Float;False;Property;_Opacity;Opacity;4;0;1;0;100;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1103.299,-621.5995;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1423.699,209.0002;Float;True;2;2;0;FLOAT4;0.0;False;1;FLOAT4;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-844.2985,226.5006;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-736.299,-516.2996;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;27;-749.5983,-283.7997;Float;False;Property;_Emmision;Emmision;6;0;0.2633788;0;5;0;1;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;88;-1084.942,-87.27937;Float;False;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-622.5413,2.3206;Float;True;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT4;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-421.9973,-489.7001;Float;True;2;2;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ClampOpNode;26;-377.3993,181.3006;Float;True;3;0;FLOAT4;0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;1,1,1,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-363.3423,-196.0794;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;Unlit;FX_Shaders/SH_Launcher_Energy;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Off;0;0;False;0;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;4;One;One;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;21;0
WireConnection;63;1;19;0
WireConnection;65;0;64;0
WireConnection;65;1;63;2
WireConnection;67;0;66;0
WireConnection;67;1;65;0
WireConnection;71;0;67;0
WireConnection;71;2;69;2
WireConnection;86;0;67;0
WireConnection;86;2;87;0
WireConnection;7;1;71;0
WireConnection;4;1;86;0
WireConnection;10;0;4;1
WireConnection;10;1;7;0
WireConnection;11;0;10;0
WireConnection;11;1;12;0
WireConnection;15;1;67;0
WireConnection;29;0;30;0
WireConnection;29;1;31;0
WireConnection;18;0;15;0
WireConnection;18;1;11;0
WireConnection;24;0;18;0
WireConnection;24;1;25;0
WireConnection;28;0;29;0
WireConnection;28;1;18;0
WireConnection;89;0;88;4
WireConnection;89;1;24;0
WireConnection;32;0;28;0
WireConnection;32;1;27;0
WireConnection;26;0;89;0
WireConnection;90;0;32;0
WireConnection;90;1;88;0
WireConnection;0;2;90;0
WireConnection;0;9;26;0
ASEEND*/
//CHKSM=B2C49CA9FB852E937B7A4D788409D3375269AD41