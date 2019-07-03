using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Power;
using TextDisplay;

public class UI_PowerUp : MonoBehaviour
{
	private Image current;      //現在選択Image

	public Dictionary<int, Character_Display> displays = new Dictionary<int, Character_Display>();

	public string fontPath;
	public float fontSize;
	private void Awake()
	{
		//子オブジェクトのTextコンポーネント取得
		var count = transform.childCount - 1;

		//データ構造に入れておく
		for (var i = 0; i < count; ++i)
		{
			var power		 = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);
			var stringLength = power.type.ToString().Length;
			if (power.type == PowerManager.Power.PowerType.SPEEDUP)
				stringLength += 2;
			displays.Add(i, new Character_Display(stringLength, fontPath, transform.GetChild(i).gameObject, transform.GetChild(i).localPosition));
			transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(9 * 100f, 100f);
			if (transform.GetChild(i + 1) != null)
				transform.GetChild(i + 1).transform.position = transform.GetChild(i).transform.position + new Vector3(900f * fontSize, 0, 0);
		}
		
		//子オブジェクトからImageコンポーネント取得
		current = transform.Find("Image").GetComponent<Image>();
		current.rectTransform.sizeDelta = new Vector2(8 * 100f, 100f) * fontSize;
	}


	private void Start()
	{
		for (var i = 0; i < displays.Count; ++i)
		{
			var power = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);

			if(power.type == PowerManager.Power.PowerType.SPEEDUP)
				displays[i].Character_Preference(" " + power.type.ToString() + " ");
			else
				displays[i].Character_Preference(power.type.ToString());    //パワーアップ名をUIで表示させる

			displays[i].Size_Change(new Vector3(fontSize, fontSize, fontSize));

			var stringLength = power.type.ToString().Length;

			if (power.type == PowerManager.Power.PowerType.SPEEDUP)
				stringLength += 2;

			var pos = Vector3.left * (stringLength * 100f * 0.5f - 50f);
			for(var j = 0; j < transform.GetChild(i).childCount;++j)
			{
				transform.GetChild(i).GetChild(j).localPosition += pos;
			}

			transform.GetChild(i).name = power.type.ToString();          //対応するオブジェクト名を修正
		}
		//選択オブジェクト名を修正
		current.name = "Cursor";
	}

	private void Update()
	{
		//現在選択パワー存在
		if (PowerManager.Instance.CurrentPower != null)
		{
			//選択画像無効の場合　　->　有効にする
			if (!current.gameObject.activeSelf)
				current.gameObject.SetActive(true);

			var slot = PowerManager.Instance.Position;
			var tempPos = Vector3.zero;

			Transform[] childs = new Transform[transform.GetChild(slot).childCount];
			for(var i = 0; i < transform.GetChild(slot).childCount; ++i)
			{
				childs[i] = transform.GetChild(slot).GetChild(i);
			}
			for(var i = 0; i < childs.Length; ++i)
			{
				tempPos += childs[i].position;
			}
			tempPos = tempPos / childs.Length;

			//現在位置に合わせる
			if (current.gameObject.transform.position != tempPos) 
			{
				current.gameObject.transform.position = tempPos;
			}
		}
		//現在選択パワーがない、　有効である場合　->　無効にする
		else
			if (current.gameObject.activeSelf)
			current.gameObject.SetActive(false);

		//表示検索ループ
		for (var i = 0; i < displays.Count; ++i)
		{
			//すべてパワーをチェック
			var power = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);
			//表示できる場合、　パワー名を表示させる
			if (power.CanShow)
			{
				if (power.type == PowerManager.Power.PowerType.SPEEDUP)
				{
					displays[i].Character_Preference(" " + power.type.ToString() + " ");
					continue;
				}
				
				displays[i].Character_Preference(power.type.ToString());
			}
			//表示できない場合、　パワー名を空にする
			else
			{
				var empty = string.Empty;
				if (power.type == PowerManager.Power.PowerType.SPEEDUP)
				{
					for (var j = 0; j < power.type.ToString().Length + 2; ++j)
					{
						empty += "?";
					}
				}
				else
				{
					for (var j = 0; j < power.type.ToString().Length; ++j)
					{
						empty += "?";
					}
				}
				Debug.Log(empty);
				displays[i].Character_Preference(empty);
			}
		}
	}
}
