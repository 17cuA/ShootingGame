//ガンマ調整ライト確認
//作成者:佐藤翼
Shader "Light_LWF"
{
	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _LightColor0;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.x += _LightColor0.r * 10;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return half4(1, 0, 0, 1);
			}
			ENDCG
		}
	}
}