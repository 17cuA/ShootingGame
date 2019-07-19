//作成日2019/07/18
// エンドロール表示
// 作成者:諸岡勇樹
/*
 * 2019/07/18	名前列挙
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;

public class End_Roll_Text : MonoBehaviour
{
	[SerializeField, Header("役職")]	private string role;
	[SerializeField, Header("名前")]	private string[] name;

	private Character_Display Role_Text { get; set; }
	private GameObject Role_Text_Parent { get; set; }
	public Vector3 role_text_pos;
	public float role_text_size;

	private Character_Display[] Name_Texts { get; set; }
	private GameObject[] Name_Text_Parents { get; set; }
	public Vector3 name_text_pos;
	public float name_text_size;

	public float line_interval;

	void Start()
    {
		Role_Text_Parent = new GameObject();
		Role_Text_Parent.transform.parent = transform;
		Role_Text = new Character_Display(role.Length, "morooka/SS", Role_Text_Parent, role_text_pos);
		Role_Text.Character_Preference(role);
		Role_Text.Size_Change(new Vector3(role_text_size, role_text_size, role_text_size));

		Name_Texts = new Character_Display[name.Length];
		Name_Text_Parents = new GameObject[name.Length];
		//Vector3 temp = name_text_pos;
		Vector3 temp = role_text_pos;
		temp.y -= 100.0f * (role_text_size + name_text_size) / 2.0f + line_interval;

		for (int i = 0; i< name.Length;i++)
		{
			Name_Text_Parents[i] = new GameObject();
			Name_Text_Parents[i].transform.parent = transform;
			Name_Texts[i] = new Character_Display(name[i].Length, "morooka/SS", Name_Text_Parents[i], temp);
			Name_Texts[i].Character_Preference(name[i]);
			Name_Texts[i].Size_Change(new Vector3(name_text_size, name_text_size, name_text_size));
			temp.y -= 100.0f * name_text_size + line_interval;
		}
	}
}
