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
		/// <summary>
		/// 画面に表示するステータス
		/// </summary>
		enum EDisplayInformation
		{
			eResult,
			eRanking,
			eNone
		}
		EDisplayInformation displayInfo = EDisplayInformation.eNone;	// 画面の情報

		[SerializeField] ResultDisplay resultDisplay;					// リザルト画面用クラス

		GameObject pressButtonDisplayParent;							// PressButtonをまとめるオブジェクト
		Character_Display pressButtonDisplay;							// PressButtonを表示するためのもの
		Vector2 pressButtonDisplayPosition = new Vector2(25f, -360f);	// PressButtonの位置
		float pressButtonSize = 0.24f;									// PressButtonの大きさ

		[SerializeField] Image displayPlane;							// 画面を覆うImage

		int blinkFrame = 0;												// 点滅するためのフレーム数

		void Start()
		{
			// リザルト表示用クラスが設定されていなかったら取得する
			if (!resultDisplay) { resultDisplay = FindObjectOfType<ResultDisplay>(); }
			// PressButtonの生成
			pressButtonDisplayParent = new GameObject();
			pressButtonDisplayParent.transform.parent = FindObjectOfType<RankingDisplay>().transform;
			pressButtonDisplay = new Character_Display("PRESS__BUTTON".Length, "morooka/SS", pressButtonDisplayParent, pressButtonDisplayPosition);
			pressButtonDisplay.Character_Preference("PRESS__BUTTON");
			pressButtonDisplay.Size_Change(Vector2.one * pressButtonSize);
			pressButtonDisplayParent.SetActive(false);
			displayInfo = EDisplayInformation.eResult;
		}

		void Update()
		{
			resultDisplay.gameObject.SetActive(displayInfo == EDisplayInformation.eResult);
			RankingDisplay.instance?.gameObject.SetActive(displayInfo == EDisplayInformation.eRanking);
			// 入力を受けたらScene移行
			if ((Input.anyKeyDown /*&& RankingDisplay.instance.IsDecision*/) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl))
			{
				//Ranking_Strage.Strage_Data.Ranking_Save();
				Scene_Manager.Manager.Screen_Transition_To_Caution();
			}
		}
	}
}