using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Power;

public class UI_PowerUp : MonoBehaviour
{
	private Dictionary<int, Text> texts = new Dictionary<int, Text>();  //UI中のTextコンポーネント
	private Image current;																  //現在選択Image
	private void Awake()
	{
		//子オブジェクトのTextコンポーネント取得
		Component[] components = transform.GetComponentsInChildren<Text>();

		//データ構造に入れておく
		for (var i = 0; i < components.Length; ++i)
			texts.Add(i, components[i] as Text);

		//子オブジェクトからImageコンポーネント取得
		current = transform.Find("Image").GetComponent<Image>();
	}


	private void Start()
	{
		for (var i = 0; i < texts.Count; ++i)
		{
			texts[i].text = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i).type.ToString();    //パワーアップ名をUIで表示させる
			if (PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i).type == PowerManager.Power.PowerType.UNKNOWN)
			{
				texts[i].text = "?";
				texts[i].transform.parent.name = texts[i].name;
				continue;
			}
			texts[i].transform.parent.name = texts[i].name;                                                                                  //対応するオブジェクト名を修正
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
			//現在位置に合わせる
			if (current.transform.position != texts[PowerManager.Instance.Position].transform.position)
				current.transform.position = texts[PowerManager.Instance.Position].transform.position;
		}
		//現在選択パワーがない、　有効である場合　->　無効にする
		else
			if (current.gameObject.activeSelf)
			current.gameObject.SetActive(false);

		//表示検索ループ
		for (var i = 0; i < texts.Count; ++i)
		{
			//すべてパワーをチェック
			var power = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);
			//表示できる場合、　パワー名を表示させる
			if (power.CanShow)
			{
				if (power.type == PowerManager.Power.PowerType.UNKNOWN)
				{
					texts[i].text = "?";
					continue;
				}

				if (texts[i].text != power.type.ToString())
					texts[i].text = power.type.ToString();
			}
			//表示できない場合、　パワー名を空にする
			else
			{
				if (texts[i].text != string.Empty)
					texts[i].text = string.Empty;
			}
		}
	}
}
