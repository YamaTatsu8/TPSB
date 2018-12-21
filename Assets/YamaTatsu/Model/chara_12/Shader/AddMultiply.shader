//-----------------------------------------------------------------
/*!
    @file   AddMultiply.shader
    
    Copyright(C) BANDAI NAMCO Entertainment Inc. All rights reserved.
*/
//-----------------------------------------------------------------
Shader "Custom/AddMultiply+" {

Properties {
     _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
     _MainTex ("Particle Texture", 2D) = "white" {}
     _InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
     _Contrast("Contrast Factor", Range(0.1,1.0)) = 0.1
}


CGINCLUDE

         #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2
         #pragma target 3.0
         #pragma vertex vert
         #pragma fragmentoption ARB_precision_hint_fastest
         #pragma multi_compile_particles
         #include "UnityCG.cginc"


         struct appdata_t {
            float4 vertex : POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
         };

         struct v2f {
            float4 vertex : POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
            #ifdef SOFTPARTICLES_ON
            float4 projPos : TEXCOORD1;
            #endif
         };

         float4 _MainTex_ST;

         v2f vert (appdata_t v)
         {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            #ifdef SOFTPARTICLES_ON
            o.projPos = ComputeScreenPos (o.vertex);
            COMPUTE_EYEDEPTH(o.projPos.z);
            #endif
            o.color = v.color;
            o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);

            return o;
         }

         sampler2D _CameraDepthTexture;
         float _InvFade;

         sampler2D _MainTex;
         fixed4 _TintColor;
         float _Contrast;


float bias (float val, float b)
{
  return (b>0) ? pow(abs(val),log(b) / log(0.5)) : 0;
}

float gain (float val, float g)
{
  return 0.5 * ((val<0.5) ? bias(2.0*val, 1.0-g) : (2.0 - bias(2.0-2.0*val, 1.0-g)));
}


ENDCG


Category {
     Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
   Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
   BindChannels {
      Bind "Color", color
      Bind "Vertex", vertex
      Bind "TexCoord", texcoord
   }

SubShader {

   Pass {

   Blend Zero SrcColor

   CGPROGRAM

   #pragma fragment frag


   fixed4 frag (v2f i) : COLOR
         {
            #ifdef SOFTPARTICLES_ON
            float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
            float partZ = i.projPos.z;
            float fade = saturate (_InvFade * (sceneZ-partZ));
            i.color.a *= fade;
            #endif

            half4 prev = i.color * tex2D(_MainTex, i.texcoord);

            prev.r =gain( prev.r , _Contrast);
            prev.g =gain( prev.g , _Contrast);
            prev.b= gain( prev.b , _Contrast);

            return lerp(half4(1,1,1,1), prev, prev.a);
         }
   ENDCG

            SetTexture [_MainTex] {
            combine texture * primary
        }
            SetTexture [_MainTex] {
            constantColor (1,1,1,1)
            combine previous lerp (previous) constant
      }


}//pass



Pass {


   Blend SrcAlpha One
   AlphaTest Greater .01
   ColorMask RGB


      CGPROGRAM

      #pragma fragment frag



         fixed4 frag (v2f i) : COLOR
         {
            #ifdef SOFTPARTICLES_ON
            float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
            float partZ = i.projPos.z;
            float fade = saturate (_InvFade * (sceneZ-partZ));
            i.color.a *= fade;
            #endif

              fixed4 prev =  2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);

              prev.r =gain( prev.r , _Contrast);
              prev.g =gain( prev.g , _Contrast);
              prev.b= gain( prev.b , _Contrast);

            return prev;

         }
      ENDCG

              SetTexture [_MainTex] {
    constantColor [_TintColor]
    combine constant * primary
           }
             SetTexture [_MainTex] {
    combine texture * previous DOUBLE
         }

      }
   }

}

}
