Shader "Custom/InvertedHolo" 
{
	Properties
	{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
		_Octaves ("Octaves", float) = 4
		_RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1.0)
		_RimPow ("Rim Power", float) = 0.5
		_SizeMod ("SizeMod", float) = 1.2
		
		_MainTex ("Main Texture", 2D) = "white" {}

		
	}
	
	SubShader
	{	
		Tags {"Queue" = "Transparent"}	
		Pass
		{
        	Cull Front // first render the back faces
       	 	
       	 	ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
        	
        	Blend SrcAlpha OneMinusSrcAlpha 
            // blend based on the fragment's alpha value
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0


			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;

			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;

			uniform float _RimPow;
			uniform float _Octaves;

			uniform float _SizeMod;

			
			//unity defined vars
			uniform float4 _LightColor0;
			
			//Base input structs
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;

			};
			
			struct vertexOutput 
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;

				float3 normalDir : TEXCOORD2;
				float4 posObject : TEXCOORD3;
				

			};
			
			
			//vertexfunction
			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;															
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);	
				
				o.posObject = v.vertex*_SizeMod;							
				o.posWorld = mul(_Object2World, v.vertex);								
				o.normalDir = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);	
				
				o.tex = v.texcoord;
																		
				return o;																
			}
			
			//fragment function
			float4 frag(vertexOutput i) : COLOR
			{
				float3 normalDirection = i.normalDir;
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
				float3 lightDirection;
				float atten;
				
				float4 NoiseMapValue = tex2D(_MainTex, _MainTex_ST.xy * i.tex.xy + _MainTex_ST.zw);
				NoiseMapValue.a = length(NoiseMapValue.ba);
				NoiseMapValue.r = length(NoiseMapValue.rg);

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
				float3 diffuseReflection = atten*_LightColor0.rgb * saturate( dot( normalDirection, viewDirection ) );
			
				

				float3 lightFinal = saturate(diffuseReflection*0.7) + UNITY_LIGHTMODEL_AMBIENT.rgb; //rimLighting*rimDif*_RimColor.a;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
				
				//pulsations
				float pulsations = 0.4 + 0.4*cos(_Time.y + i.posObject.y) +  0.5*cos(_Time.x + .23 + i.posObject.y); //+ cos(_Time.w+5);
				pulsations*= pulsations;

				
				//lightFinal *= pulsations;
				float lines = max(0, abs((0.4*cos(_Time.z/2 + i.posObject.x + 0.5)) + 1.7) * cos(_Time.y + (i.posObject.y*2 + i.posObject.z  + 0.7)*15) );
				lines *= saturate(floor(lines + 0.4*(1 + cos( _Time.z/2 + i.posObject.y )) ));
				
				lines = saturate(lines - 0.5);
				lines *= lines;
				lines *= lines;

				lightFinal * pulsations;
				 
				float alpher = _Color.a* (1 - lines);
				alpher = min(1, alpher/pow(dot(viewDirection,normalDirection), _RimPow));
				return float4(_Color.rgb * lightFinal, alpher);
			 
			}
			ENDCG
		}
		
				
		Pass
		{
        	Cull Back // first render the back faces
       	 	ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
        	Blend SrcAlpha OneMinusSrcAlpha 
            // blend based on the fragment's alpha value
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			//user defined variables
			uniform float4 _Color;
			uniform float4 _RimColor;

			uniform float _RimPow;
			uniform float _Octaves;
			uniform float _SizeMod;


			
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
				float4 posObject : TEXCOORD2;

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
				float3 diffuseReflection = atten*_LightColor0.rgb * saturate( dot( normalDirection, viewDirection ) );
			
				

				float3 lightFinal = saturate(diffuseReflection * 0.7) + UNITY_LIGHTMODEL_AMBIENT.rgb; //rimLighting*rimDif*_RimColor.a;// X >> Y is the same as X / 2^Y  ERGO: this is the same as specRef*4)/
				
				//pulsations
				float pulsations = 0.4 + 0.4*cos(_Time.y + i.posObject.y) +  0.5*cos(_Time.x + .23 + i.posObject.y); //+ cos(_Time.w+5);
				pulsations*= pulsations;
				
				//lightFinal *= pulsations;
				float lines = max(0, abs((0.4*cos(_Time.z/2 + i.posObject.x + 0.5)) + 1.7) * cos(_Time.y + (i.posObject.y*2 + i.posObject.z  + 0.7)*15) );
				lines *= saturate(floor(lines + 0.4*(1 + cos( _Time.z/2 + i.posObject.y )) ));
				
				lines = saturate(lines - 0.5);
				lines *= lines;
				lines *= lines;

				lightFinal * pulsations;
				 
				float alpher = _Color.a* (1 - lines);
				alpher = min(1, alpher/pow(dot(viewDirection,normalDirection), _RimPow));
				return float4(_Color.rgb * lightFinal, alpher);
			
			}
			ENDCG
		}

	}
	//Fallback "Unlit/Transparent"
}
