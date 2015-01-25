Shader "Background" {
	Properties {
        _Color ("Main Color", Color) = (1.0,1.0,1.0)
        TopTex("Top Image", 2D ) = "image1" {}
        DownTex("Down Image" , 2D ) = "image2" {}
        TimeFactor("Time Factor" , Range( 0.0 , 1.0  ) ) = 0.0
        
    }
    
    SubShader{
    //Tags { "Queue" = "transparent" }
    Pass {    
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            
            uniform float3 _Color;
            uniform sampler2D TopTex;
            uniform sampler2D DownTex;
            uniform float TimeFactor;
            uniform float uvOffset;
            
           struct vIn
           {
           float4 vertex : POSITION;
           float2 uv: TEXCOORD0;
           
           };
           
           	struct v2f 
           	{
   				float4 pos : POSITION;
  			 	float2 uv : TEXCOORD0;
  			};
 
			v2f vert (vIn v) 
			{
    			v2f o;
    			o.pos = mul(UNITY_MATRIX_MVP, float4( v.vertex.xyz , v.vertex.w ) );
    			o.uv = v.uv;
    			o.uv.y += TimeFactor;
    			
    			return o;
			}
			
	
       	 	float4 frag(v2f i) : COLOR
        	{
        	
        		//return float4( _Color , 1.0 );
        		if ( i.uv.y < 1 )
        		{
        			return float4( tex2D( DownTex , i.uv ).rgb , 1 );
        		}
  
            	float2 uv = i.uv;
            	uv.y -= 1.0;
            	return float4( tex2D( TopTex , uv ).rgb , 1 );

        	}
            ENDCG
        }
    }
}
