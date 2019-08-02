using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * 注意左と右の日本電子、消えるときは。
 * スケールを0.3/sec程度で消失、Y軸が０へ、X軸は右の画面へ最終的には線になる感じで
 * 消える。日電は逆に左へ行く感じ。
 * 昔のブラウン管みたく、電源を消したときの感じです。スケールとアルファ値で簡単に
 * 演出が出来ると思います。最終的にはシェーダーをコールして、適用するんだけど。
 * 基本となる動きは用意しておくといいです。
 */

public class Title_Manager : MonoBehaviour
{
	public enum TITLE_MODE
	{
		eCAUTION,		// 注意書き
		eROGO,				// ロゴ
		eLEGEND,			// 伝説
		eTITLE,				// タイトル
		eSELECT,			// 人数の選択
	}

	[SerializeField, Tooltip("注意書き")]	private Image caution_writing;
	[SerializeField, Tooltip("ロゴ")]			private Image lorgo_writing;
	[SerializeField, Tooltip("伝説")]			private Image legend_writing;

	private RectTransform Caution_Transform { get; set; }		// 注意書きのトランスフォーム
	private RectTransform Lorgo_Transform { get; set; }		// ロゴのトランスフォーム
	private RectTransform Legend_Transform { get; set; }		// 伝説のトランスフォーム

	private float Fade_Speed_Scale { get; set; }		// フェードのスピード Y
	private float Fade_Speed_Pos { get; set; }		// フェードのスピード X

	public TITLE_MODE Mode { get; private set; }

	void Start()
    {
		Mode = TITLE_MODE.eCAUTION;
		Fade_Speed_Scale = 0.3f / 60.0f;
		//Fade_Speed_Pos = 
    }

    void Update()
    {
		switch (Mode)
		{
			case TITLE_MODE.eCAUTION:
				break;
			case TITLE_MODE.eROGO:
				break;
			case TITLE_MODE.eLEGEND:
				break;
			case TITLE_MODE.eTITLE:
				break;
			case TITLE_MODE.eSELECT:
				break;
			default:
				break;
		}
	}
}
