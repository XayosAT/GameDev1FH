Shader "Custom/CameraShader"
{
    Properties
    {
        _BorderColor ("Border Color", Color) = (0,0,0,1)
        _BorderSizeLeft ("Border Size Left", Range(0, 0.5)) = 0.1
        _BorderSizeRight ("Border Size Right", Range(0, 0.5)) = 0.1
        _BorderSizeTop ("Border Size Top", Range(0, 0.5)) = 0.1
        _BorderSizeBottom ("Border Size Bottom", Range(0, 0.5)) = 0.1
        _MainTex ("Main Texture", 2D) = "white" {} // Make sure to have a default texture
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            fixed4 _BorderColor;
            float _BorderSizeLeft;
            float _BorderSizeRight;
            float _BorderSizeTop;
            float _BorderSizeBottom;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv); // Sample the main texture
                // Check if the current fragment is within the border size threshold
                if (i.uv.x < _BorderSizeLeft || i.uv.x > 1.0 - _BorderSizeRight || i.uv.y < _BorderSizeBottom || i.uv.y > 1.0 - _BorderSizeTop)
                {
                    return _BorderColor; // Set the color to the border color
                }
                return texColor; // Otherwise, return the texture color
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
