Shader "Custom/GrassWaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPos ("Player Position", Vector) = (0, 0, 0, 0)
        _WaveStrength ("Wave Strength", Float) = 0.1
        _WaveRadius ("Wave Radius", Float) = 1.0
        _Color ("Color Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float4 _PlayerPos;
            float _WaveStrength;
            float _WaveRadius;
            float4 _Color;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Calcular a distância no plano XZY
                float dist = length(worldPos.xzy - _PlayerPos.xzy);

                // Aplicar o efeito da onda baseado na distância
                float waveEffect = _WaveStrength * exp(-dist / _WaveRadius) * sin(_Time.y * 10.0 + dist * 5.0);

                // Modificar a posição Y do vértice localmente, para a ondulação
                v.vertex.y += waveEffect;

                // Transformar o vértice de volta para o espaço de tela
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Passar as coordenadas de textura
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Recuperar a cor da textura
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Aplicar a cor do material
                fixed4 finalColor = texColor * _Color;

                // Retornar a cor final com o alpha preservado
                return finalColor;
            }
            ENDCG
        }
    }
}
