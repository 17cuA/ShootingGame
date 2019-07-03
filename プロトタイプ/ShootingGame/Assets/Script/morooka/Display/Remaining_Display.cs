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

	private string Font_Path { get; set; }
	private string Image_Path { get; set; }
	private List<GameObject> Object_For_Display { get; set; }
	private List<Image> image_image { get; set; }
	private Player1 Player_Data { get; set; }
	public Character_Display Object_To_Display { private set; get; }

	private int Remaining_Num { get; set; }

	public Sprite[] Sprite_Ok { get; set; }
	public int One_Play;

	// Update is called once per frame
	void Update()
    {
		if(One_Play == 0)
		{
			Player_Data = Obj_Storage.Storage_Data.Player.Get_Obj()[0].GetComponent<Player1>();
			Remaining_Num = Player_Data.Remaining;

			Sprite_Ok = Resources.LoadAll<Sprite>("morooka/Remaining");

			Vector2 posTemp = transform.position;
			posTemp.y = -100;

			Object_For_Display = new List<GameObject>();
			image_image = new List<Image>();
			for (int i = 0; i < Remaining_Num; i++)
			{
				Object_For_Display.Add(new GameObject());
				Object_For_Display[i].AddComponent<Image>();
				image_image.Add(Object_For_Display[i].GetComponent<Image>());
				Object_For_Display[i].transform.position = posTemp;
				posTemp.x += 200.0f;
				Object_For_Display[i].transform.SetParent(transform);

				image_image[i].sprite = Sprite_Ok[0];
			}


			//for (int i = 0; i < transform.childCount; i++)
			//{
			//	image_image.Add(transform.GetChild(i).GetComponent<Image>());
			//	image_image[i].sprite = Sprite_Ok[0];
			//}

			Object_To_Display = new Character_Display(2, "morooka/SS", gameObject, position);
			Object_To_Display.Character_Preference("1P");
			Object_To_Display.Size_Change(new Vector3(font_size, font_size, font_size));

			One_Play++;
		}

		if(Player_Data.Remaining < Remaining_Num)
		{
			Remaining_Num--;
			image_image[Remaining_Num].enabled = false;
		}
    }
}
