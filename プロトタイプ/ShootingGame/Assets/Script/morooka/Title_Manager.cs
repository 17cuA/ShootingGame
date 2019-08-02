using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Manager : MonoBehaviour
{
	enum TITLE_MODE
	{
		eCAUTION,		// 注意書き
		eROGO,				// ロゴ
		eLEGEND,			// 伝説
		eTITLE,				// タイトル
		eSELECT,			// 人数の選択
	}

	private TITLE_MODE Mode { get; set; }


	void Start()
    {
		Mode = TITLE_MODE.eCAUTION;
    }

    void Update()
    {
        s
    }
}
