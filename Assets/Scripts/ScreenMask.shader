Shader "ScreenMask"{
	Properties {
        _Color ("Main Color", Color) = (1.0,1.0,1.0)
        Scale ("Mask Scale", Float ) = 1.0
        MaskTex("Mask Image" , 2D ) = "test" {}
    }
    
    SubShader {
    Tags { "Queue" = "transparent" }
    Pass {
    		Blend SrcAlpha OneMinusSrcAlpha
    
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            
            uniform float3 _Color;
            uniform float  Scale;
            uniform sampler2D MaskTex;
            
           struct vIn
           {
           float4 vertex : POSITION;
           float2 uv: TEXCOORD0;
           };
           
           		struct v2f {
   				float4 pos : POSITION;
  			 	float2 uv : TEXCOORD0;
  			};
 
			v2f vert (vIn v) 
			{
    			v2f o;
    			o.pos = mul(UNITY_MATRIX_MVP, float4( v.vertex.xyz , v.vertex.w ) );
    			o.uv = v.uv;
    			
    			return o;
			}
			
	
       	 	float4 frag(v2f i) : COLOR
        	{

            	if ( Scale <= 0 )
            		discard;
            		
            	float4 output;
            	//float2 uv = i.uv;
            	float2 uv = clamp( 0.5 + ( i.uv - 0.5 ) * Scale , 0 , 1.0 );

            	

            	output.rgb = _Color;
            	//output.rgb = float3( uv, 0 );
            	if ( Scale > 2 )
            		output.a   = 1;
            	else
            	    output.a   = 1- tex2D( MaskTex , uv ).r;
            	    
            	               	

            //output.rgb = 0;
            	return output;
        	}
            ENDCG
        }
    }
}

