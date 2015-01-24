Shader "UIFace"{
	Properties {
        _Color ("Main Color", Color) = (1.0,1.0,1.0)
        _Scale ("Circle Scale", Float ) = 1.0
        MainTex   ("Image" , 2D ) = "test" {}
    }
    
    SubShader {
    Pass {
    
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            
            uniform float3 _Color;
            uniform float  _Scale;
            uniform sampler2D MainTex;
            
     		 struct vertexInput {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0;
            };
            struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        	};
  
            v2f vert(vertexInput vi){
                v2f vOut;
                vOut.pos = mul( UNITY_MATRIX_MVP, vi.vertex);
                vOut.uv  = _Scale * vi.uv;
                return vOut;
            }

            float4 frag( v2f fi ) : COLOR 
            {
                return tex2D( MainTex , fi.uv );
            }
            ENDCG
        }
    }
}

