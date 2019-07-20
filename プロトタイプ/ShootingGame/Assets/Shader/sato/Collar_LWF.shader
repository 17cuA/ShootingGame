//ガンマ調整色確認
//作成者:佐藤翼
Shader "Collar_LWF"
{
	Properties
	{
		_Color("Color", Color) = (1,0,0,1)
	}
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.x += _Color.r * 10;
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