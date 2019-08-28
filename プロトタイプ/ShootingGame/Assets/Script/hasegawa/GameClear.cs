/*
 * 20190828 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

namespace Costom
{
	/// <summary>
	/// ゲームクリアシーンを管理する
	/// </summary>
	public class GameClear : MonoBehaviour
	{
		enum EDisplayInformation
		{
			eResult,
			eRanking,
			eNone
		}
		[SerializeField] Image displayPlane;							// 画面を覆うImage

		ResultDisplay resultDisplay;									// リザルト画面

		Character_Display pressButtonDisplay;							// PressButtonを表示するためのもの
		Vector2 pressButtonDisplayPosition = new Vector2(25f, -360f);	// PressButtonの位置
		float pressButtonSize = 0.24f;									// PressButtonの大きさ

		void Start()
		{

		}

		void Update()
		{

		}
	}
}