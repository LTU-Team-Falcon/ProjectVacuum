Shader "Custom/CustomToon" 
{
	Properties
	{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
		_Octaves ("Octaves", float) = 4
		_RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1.0)
		_RimPow ("Rim Power", Range(0.1,10)) = 3
		
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

			uniform float _RimPow;
			uniform float _Octaves;


			
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
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
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
				float3 normalDirection = i.normalDir;
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
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
					atten = 1/(dist*dist);
					lightDirection = normalize(fragmentToLightSource);
				}
				
				//lighting
				float3 diffuseReflection = atten*_LightColor0.rgb * saturate( dot( normalDirection, lightDirection ) );
			
				//float3 specularReflection = atten * _LightColor0.rgb * _SpecColor* saturate( dot( normalDirection, lightDirection ) ) *
				//							pow( saturate(dot( reflect( -lightDirection, normalDirection) , viewDirection )) , _Shininess);
				
				
				
					
				//float rimDif = 1;//length(rimLighting);
				//rimDif = (ceil(rimDif * _Octaves) / _Octaves)/rimDif;
				
				float diffDif = length(diffuseReflection);
				diffDif = (round(diffDif * _Octaves) / _Octaves)/diffDif;
				
				float diffMultiplier = length(diffuseReflection);

				float3 lightFinal = saturate(diffuseReflection * diffDif)  * diffMultiplier + UNITY_LIGHTMODEL_AMBIENT.rgb; //rimLighting*rimDif*_RimColor.a;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
				//float3 lightFinal = rimLighting*(_RimColor.a) + diffuseReflection*(_Color.a) + specularReflection*(_SpecColor.a) + UNITY_LIGHTMODEL_AMBIENT.rgb;
				return float4(lightFinal * _Color,1.0);
			
			}
			ENDCG
		}

		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;

			uniform float _RimPow;
			uniform float _Octaves;


			
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
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
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
				float3 normalDirection = i.normalDir;
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
				

			   	
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
					atten = 1/(dist*dist);
					lightDirection = normalize(fragmentToLightSource);
				}
				
				//lighting
				float3 diffuseReflection = atten*_LightColor0.rgb * saturate( dot( normalDirection, lightDirection ) );
				
				float diffMultiplier = length(diffuseReflection);
				//diffuseReflection = normalize(diffuseReflection);
				
				float diffDif = length(diffuseReflection);
				diffDif = (ceil(diffDif * _Octaves) / _Octaves)/diffDif;
				
				float3 lightFinal = saturate(diffuseReflection * diffDif)  * diffMultiplier;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
					//float3 lightFinal = rimLighting*(_RimColor.a) + diffuseReflection*(_Color.a) + specularReflection*(_SpecColor.a) + UNITY_LIGHTMODEL_AMBIENT.rgb;

					
				return float4(max(lightFinal * _Color, 0.0),1.0);
			
			}
			ENDCG
		}
		
		Pass
		{
			Tags {"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;

			uniform float _RimPow;
			uniform float _Octaves;


			
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
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
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
				float3 normalDirection = i.normalDir;
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
				

			   	
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
					atten = 1/(dist*dist);
					lightDirection = normalize(fragmentToLightSource);
				}
				
				//lighting
				float3 diffuseReflection = atten*_LightColor0.rgb * saturate( dot( normalDirection, lightDirection ) );
				float diffMultiplier = length(diffuseReflection);
				
				float diffDif = length(diffuseReflection);
				diffDif = (ceil(diffDif * _Octaves) / _Octaves)/diffDif;
				
				float3 lightFinal = saturate(diffuseReflection * diffDif)  * diffMultiplier;

					
				return float4(max(lightFinal * _Color, 0.0),1.0);
			
			}
			ENDCG
		}

		
	}
	//Fallback "Specular"
}
