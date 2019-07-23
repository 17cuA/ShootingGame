//作成日2019/07/03
// プレイヤーの残機表示
// 作成者:諸岡勇樹
/*
 * 2019/07/03：残機表示
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class Remaining_Display : MonoBehaviour
{
	[SerializeField]
	[Header("フォントのサイズ")]
	private float font_size;
	[SerializeField]
	[Header("表示位置")]
	private Vector3 position;

	private List<GameObject> Remaining_Object { get; set; }         // 残機アイコン表示用オブジェクト
																	//private List<Image> Remaining_Object_Image { get; set; }        // 残機アイコン表示用 Image 
	private Player1 Player_Data { get; set; }                       // プレイヤーの残機確認用
	private Character_Display Object_To_Display { set; get; }       // 文字表示用
	private int Remaining_Num { get; set; }                         // 現在表示してる残機
	private Sprite[] Display_Sprite { get; set; }                   // 表示したいスプライト用
	private string Temp_String{get;set;}

	void Update()
    {
		// プレイヤーの情報がないとき
		if(Player_Data == null)
		{
			// プレイヤーのデータを保存
			Player_Data = Obj_Storage.Storage_Data.Player.Get_Obj()[0].GetComponent<Player1>();
			Remaining_Num = Player_Data.Remaining;

			// リソースからアイコン用の画像取得
			Display_Sprite = Resources.LoadAll<Sprite>("morooka/Remaining");

			// アイコンの初期位置設定用
			Vector2 posTemp = new Vector3(0.0f,-150.0f,0.0f);

			// アイコンの作製と設置
			// プレイヤーの初期残機数からアイコン生成
			Remaining_Object = new List<GameObject>();
			//Remaining_Object_Image = new List<Image>();
			for (int i = 0; i < Remaining_Num; i++)
			{
				Remaining_Object.Add(new GameObject());
				Remaining_Object[i].AddComponent<Image>();
				//Remaining_Object_Image.Add(Remaining_Object[i].GetComponent<Image>());
				Remaining_Object[i].transform.SetParent(transform);

				RectTransform r_transform = Remaining_Object[i].GetComponent<RectTransform>();
				r_transform.localPosition = posTemp;
				r_transform.localScale *= 1.4f;

				posTemp.x += 150.0f;
				//Remaining_Object_Image[i].sprite = Display_Sprite[0];
			}

			// 「1P」の文字作製と設置
			string temp = "1P__LIFE_X_" + Remaining_Num.ToString();
			Object_To_Display = new Character_Display(temp.Length, "morooka/SS", gameObject, position);
			Object_To_Display.Character_Preference(temp);
			Object_To_Display.Size_Change(new Vector3(font_size, font_size, font_size));
		}

		//表示中のアイコンの数とプレイヤーの残機の数が違うとき
		if(Player_Data.Remaining < Remaining_Num)
		{
			// 表示中のアイコンの非表示化
			Remaining_Num = Player_Data.Remaining;

			//Remaining_Object_Image[Remaining_Num].enabled = false;
		}
    }
}
