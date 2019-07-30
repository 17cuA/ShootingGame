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
	[SerializeField,Header("表示位置2")]
	private Vector3 lifePosition;
	[SerializeField,Header("表示位置3")]
	private Vector3 xRPosition;

	//private List<GameObject> Remaining_Object { get; set; }         // 残機アイコン表示用オブジェクト
	//private List<Image> Remaining_Object_Image { get; set; }        // 残機アイコン表示用 Image 
	private GameObject[] oya;
	private Player1 Player_Data { get; set; }								// プレイヤーの残機確認用
	private Character_Display Object_To_Display { set; get; }       // 文字表示用
	private Character_Display[] Life;							// ライフ表示
	private int Remaining_Num { get; set; }                         // 現在表示してる残機
	private Sprite[] Display_Sprite { get; set; }                   // 表示したいスプライト用
	private string Temp_String{get;set;}

	void Update()
    {
		// プレイヤーの情報がないとき
		if(Player_Data == null)
		{
			oya = new GameObject[3] { new GameObject(),new GameObject(),new GameObject()};
			foreach(GameObject obj in oya)
			{
				obj.transform.parent = transform;
			}
			// プレイヤーのデータを保存
			Player_Data = Obj_Storage.Storage_Data.Player.Get_Obj()[0].GetComponent<Player1>();
			Remaining_Num = Player_Data.Remaining;

			// リソースからアイコン用の画像取得
			Display_Sprite = Resources.LoadAll<Sprite>("morooka/Remaining");

			// アイコンの初期位置設定用
			Vector2 posTemp = new Vector3(0.0f,-150.0f,0.0f);

			// アイコンの作製と設置
			// プレイヤーの初期残機数からアイコン生成
			//Remaining_Object = new List<GameObject>();
			//Remaining_Object_Image = new List<Image>();
			for (int i = 0; i < Remaining_Num; i++)
			{
				//Remaining_Object.Add(new GameObject());
				//Remaining_Object[i].AddComponent<Image>();
				//Remaining_Object_Image.Add(Remaining_Object[i].GetComponent<Image>());
				//Remaining_Object[i].transform.SetParent(transform);

				//RectTransform r_transform = Remaining_Object[i].GetComponent<RectTransform>();
				//r_transform.localPosition = posTemp;
				//r_transform.localScale *= 1.4f;

				posTemp.x += 150.0f;
				//Remaining_Object_Image[i].sprite = Display_Sprite[0];
			}

			// 「1P」の文字作製と設置
			// アンダーバーはスペース部分、1P　と　ＬＩＦＥの間にスコア表示
			Temp_String = "1P______________LIFE";
			Object_To_Display = new Character_Display(Temp_String.Length, "morooka/SS", oya[0], position);
			Object_To_Display.Character_Preference(Temp_String);
			Object_To_Display.Size_Change(new Vector3(font_size, font_size, font_size));

			// Ｘは小さく
			Life = new Character_Display[2];
			Temp_String = "_X_";
			Life[0] = new Character_Display(Temp_String.Length, "morooka/SS", oya[1], lifePosition);
			Life[0].Character_Preference(Temp_String);
			Life[0].Size_Change(new Vector3(font_size / 3.0f * 2.0f, font_size / 3.0f * 2.0f, font_size / 3.0f * 2.0f));

			// プレイヤーの残機数表示
			Life[1] = new Character_Display(1, "morooka/SS", oya[2],xRPosition);
			Life[1].Character_Preference(Remaining_Num.ToString());
			Life[1].Size_Change(new Vector3(font_size, font_size, font_size));
		}

		//表示中のアイコンの数とプレイヤーの残機の数が違うとき
		if(Player_Data.Remaining < Remaining_Num)
		{
			// 表示中のアイコンの非表示化
			Remaining_Num = Player_Data.Remaining;
			Life[1].Character_Preference(Remaining_Num.ToString());
			//Remaining_Object_Image[Remaining_Num].enabled = false;
		}
    }
}
