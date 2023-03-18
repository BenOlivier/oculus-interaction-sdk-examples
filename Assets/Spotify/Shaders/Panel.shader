Shader "Custom/Panel"
{
    Properties {
        _Color("Color", Color) = (1, 1, 1, 1)

        _BorderColor("BorderColor", Color) = (0, 0, 0, 1)

        // width, height, border radius, unused
        _Dimensions("Dimensions", Vector) = (0, 0, 0, 0)

        // radius corners
        _Radii("Radii", Vector) = (0, 0, 0, 0)

        // defaults to LEqual
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4

        // position for hover effect
        _HoverPosition("HoverPosition", Vector) = (0, 0, 0, 0)

        _HoverColor("HoverColor", Color) = (1, 1, 1, 1)

        // scale, power, unused, unused
        _HoverPower("HoverPower", Vector) = (0.1, 1, 0, 0)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest [_ZTest]
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"
            #include "Box2DSignedDistance.cginc"

            struct appdata
            {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                UNITY_VERTEX_OUTPUT_STEREO
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                fixed4 borderColor : TEXCOORD1;
                fixed4 dimensions : TEXCOORD2;
                fixed4 radii : TEXCOORD3;
                fixed4 hoverColor : TEXCOORD4;
                fixed2 hoverPower : TEXCOORD5;
                fixed4 hoverPosition : TEXCOORD6;
                fixed4 worldPos : TEXCOORD7;
            };

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _BorderColor)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Dimensions)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Radii)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _HoverColor)
                UNITY_DEFINE_INSTANCED_PROP(fixed2, _HoverPower)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _HoverPosition)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
                o.borderColor = UNITY_ACCESS_INSTANCED_PROP(Props, _BorderColor);
                o.dimensions = UNITY_ACCESS_INSTANCED_PROP(Props, _Dimensions);
                o.radii = UNITY_ACCESS_INSTANCED_PROP(Props, _Radii);
                o.hoverColor = UNITY_ACCESS_INSTANCED_PROP(Props, _HoverColor);
                o.hoverPower = UNITY_ACCESS_INSTANCED_PROP(Props, _HoverPower);
                o.hoverPosition = UNITY_ACCESS_INSTANCED_PROP(Props, _HoverPosition);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = (v.uv-float2(.5f,.5f))*2.0f*o.dimensions.xy;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed hoverDistance = distance(i.worldPos.xyz, i.hoverPosition.xyz);
                
                fixed highlightStrength = (1 - saturate(pow(hoverDistance / i.hoverPower.x, i.hoverPower.y)));

                fixed alpha = lerp(i.color.a, i.hoverColor.a, highlightStrength);
                
                return fixed4(alpha.xxx, 1);




                float sdResult = sdRoundBox(i.uv, i.dimensions.xy - i.dimensions.ww * 2.0f, i.radii);

                clip(i.dimensions.w * 2.0f - sdResult);

                if (-i.dimensions.z * 2.0f - sdResult < 0.0f)
                {
                    return i.borderColor;
                }
                return i.color;
            }
            ENDCG
        }
    }
}
