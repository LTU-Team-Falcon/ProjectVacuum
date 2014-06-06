Shader "Custom/Toon_Option3_SpecBounceSingular" 
{
	Properties
	{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
		_SpecColor("Specular Color (Alpha is Pow)", Color) = (1,1,1,1)
		_Range ("RangeFalloff (min, max, lowestLevel, sig)", Vector) = (1,10,0,1)
		_RimColor ("Rim Color", Color) = (0,0,0,1)
		
				
								
		_RampH ("LightBounce Ramp", 2D) = "white" {}
		_RampV ("ViewBounce Ramp", 2D) = "white" {}

	}
	
	SubShader
	{		
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;
			uniform float4 _Range;

			uniform float4 _SpecColor;
			
			
			
			uniform sampler2D _RampH;
			uniform float4 _RampH_ST;
			
			uniform sampler2D _RampV;
			uniform float4 _RampV_ST;

						
			//unity defined vars
			uniform float4 _LightColor0;
			
			//Base input structs
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct vertexOutput 
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
				

			};
			
			
			//vertexfunction
			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;															
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);								
				o.posWorld = mul(_Object2World, v.vertex);								
				o.normalDir = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);	

																						
				return o;																
			}
			
			//fragment function
			float4 frag(vertexOutput i) : COLOR
			{
				float3 normalDirection = normalize(i.normalDir);
				float3 viewDirection = _WorldSpaceCameraPos - i.posWorld.xyz;
				float dist = length(viewDirection);
				viewDirection = normalize(viewDirection);
				
				dist = 1 - saturate((dist - _Range.x)/(_Range.y - _Range.x));
				dist = saturate(dist - _Range.z)+_Range.z;
			
				float3 lightDirection;
				float atten;
				
				if(_WorldSpaceLightPos0.w == 0.0)
				{
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				 	atten = 1.0;
				}
				else
				{
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float dist = length(fragmentToLightSource);
					atten = 1/(dist);
					lightDirection = normalize(fragmentToLightSource);
				}

				float specBounce = saturate(dot( reflect( -lightDirection, normalDirection) , viewDirection ));
				specBounce = saturate(pow(specBounce, _SpecColor.a)*_Color.a);
				
				
				float lightBounce = 0;//saturate( dot(lightDirection, normalDirection)*0.5 + 0.5);
				
				lightBounce += specBounce;
				float2 lightCoord = float2(lightBounce, 0);
				float3 lightRamp = tex2D(_RampH, lightCoord*_RampH_ST.xy + _RampH_ST.zw ).rgb; //*_Ramp_ST.xy + _Ramp_ST.zw);
				
				//lightRamp = lerp(float3(0,0,0), _LightColor0.rgb, lightRamp.g*atten);
				

				float viewBounce = saturate(dot(viewDirection, normalDirection));
				float2 viewCoord = float2(0,viewBounce);
				float3 viewRamp = tex2D(_RampV, viewCoord*_RampV_ST.xy + _RampV_ST.zw ).rgb;
				viewRamp = pow(viewRamp.g, dist);

				//_Color.rgb = lerp(float3(0,0,0), _Color.rgb, _Color.a);
				
				viewRamp = lerp(_RimColor.rgb, float3(1,1,1) , viewRamp.g);
									
				lightRamp = lerp(_Color.rgb, _SpecColor.rgb, specBounce);
				
				float3 bounceCol = viewRamp * lightRamp ;
				
				float3 lightFinal = bounceCol; //rimLighting*rimDif*_RimColor.a;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
		
				return float4(lightFinal,1.0);
			
			}
			ENDCG
		}
		
	//	Pass  X2

	//	{
	//		Tags {"LightMode" = "ForwardAdd"}
	//		Blend One One
	//		CGPROGRAM
	//		#pragma vertex vert
	//		#pragma fragment frag
	// 
	//	}

		
	}
	//Fallback "Specular"
}
