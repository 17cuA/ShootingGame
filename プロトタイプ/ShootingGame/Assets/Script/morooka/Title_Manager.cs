using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public TITLE_MODE Mode { get; private set; }

	void Start()
    {
		Mode = TITLE_MODE.eCAUTION;
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
