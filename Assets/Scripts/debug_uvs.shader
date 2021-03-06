Shader "Unlit/debug_uvs"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            //vert data
            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };


            struct VertexOutput //my vertex output parameters (custom naming for clipSpacePos (originally called vertex))
            {
                float2 uv : TEXCOORD0;
                float4 clipSpacePos : SV_POSITION;
                float3 normal : TEXCOORD1;
            };


            float4 _MainTex_ST;


            //prepare/pass input data to be sent to our fragment function
            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;

                return o;
            }


            fixed4 frag(VertexOutput o) : SV_Target
            {
                float2 uv = o.uv;
                return float4((uv),0,0); //x,y,z,alpha
            }
            ENDCG
        }
    }

        //go to clip 1:32:35
}
