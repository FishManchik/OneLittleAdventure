Shader"Custom/FogMaterial"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (1, 1, 1, 1)
        _FogDensity ("Fog Density", Float) = 0.1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
Blend SrcAlphaOneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 worldPos : TEXCOORD1;
};

sampler2D _MainTex;
float4 _FogColor;
float _FogDensity;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Compute fog factor based on distance
    float distance = length(i.worldPos - _WorldSpaceCameraPos);
    float fogFactor = exp(-_FogDensity * distance);
    fogFactor = clamp(1.0 - fogFactor, 0.0, 1.0);

    fixed4 texColor = tex2D(_MainTex, i.uv);
    return lerp(texColor, _FogColor, fogFactor);
}
            ENDCG
        }
    }
}