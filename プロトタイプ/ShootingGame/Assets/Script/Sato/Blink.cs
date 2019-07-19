using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class Blink : MonoBehaviour
{
	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;

	//public
	public float speed = 1.0f;

	//private
	private Text text;
	private Image image;
	private float time;

	private enum ObjType
	{
		TEXT,
		IMAGE
	};
	private ObjType thisObjType = ObjType.TEXT;

	void Start()
	{
		////アタッチしてるオブジェクトを判別
		//if (this.gameObject.GetComponent<Image>())
		//{
		//	thisObjType = ObjType.IMAGE;
		//	image = this.gameObject.GetComponent<Image>();
		//}
		//else if (this.gameObject.GetComponent<Text>())
		//{
		//	thisObjType = ObjType.TEXT;
		//	text = this.gameObject.GetComponent<Text>();
		//}

		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(16, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference("PRESS_ANY_BUTTON");
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));
		please_push_button.Centering();
	}

	void Update()
	{
		////オブジェクトのAlpha値を更新
		//if (thisObjType == ObjType.IMAGE)
		//{
		//	image.color = GetAlphaColor(image.color);
		//}
		//else if (thisObjType == ObjType.TEXT)
		//{
		//	text.color = GetAlphaColor(text.color);
		//}

		please_push_button.Color_Change(GetAlphaColor(please_push_button.Font_Color));
	}

	//Alpha値を更新してColorを返す
	Color GetAlphaColor(Color color)
	{
		time += Time.deltaTime * 5.0f * speed;
		color.a = Mathf.Sin(time) * 10.5f + 10.5f;

		return color;
	}
}