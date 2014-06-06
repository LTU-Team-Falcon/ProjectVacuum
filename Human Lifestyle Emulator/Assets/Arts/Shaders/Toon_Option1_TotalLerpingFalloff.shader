Shader "Custom/Toon_Option1_TotalLerpingFalloff" 
{
	Properties
	{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
		
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
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;
			uniform float4 _Range;

			
			
			
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
				lightDirection *= 10 - _Color.a * 9; 


				float lightBounce = saturate( dot(lightDirection, normalDirection)*0.5 + 0.5);
				float2 lightCoord = float2(lightBounce, 0);
				float3 lightRamp = tex2D(_RampH, lightCoord*_RampH_ST.xy + _RampH_ST.zw ); //*_Ramp_ST.xy + _Ramp_ST.zw);
				
				lightRamp = lerp(float3(0,0,0), _LightColor0.rgb, lightRamp.g*atten);
				

				float viewBounce = saturate(dot(viewDirection, normalDirection));
				float2 viewCoord = float2(0,viewBounce);
				float3 viewRamp = tex2D(_RampV, viewCoord*_RampV_ST.xy + _RampV_ST.zw );
			
				_Color.rgb = lerp(float3(0,0,0), _Color.rgb, _Color.a);
				viewRamp = lerp(_RimColor.rgb, _Color.rgb, viewRamp.g);
					
				float3 bounceCol = viewRamp * lightRamp;
				bounceCol *= dist;//lerp(1, dist, _Range.w);
							
				//float3 specularReflection = atten * _LightColor0.rgb * _SpecColor* saturate( dot( normalDirection, lightDirection ) ) *
				//							pow( saturate(dot( reflect( -lightDirection, normalDirection) , viewDirection )) , _Shininess);
								
				//float specBounce = saturate(dot( reflect( -lightDirection, normalDirection) , viewDirection ));
				
				
				float3 lightFinal = bounceCol * _Color.rgb; //rimLighting*rimDif*_RimColor.a;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
		
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
