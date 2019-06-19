using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUp : MonoBehaviour
{
	private Dictionary<int, Text> texts = new Dictionary<int, Text>();  //UI中のTextコンポーネント
	private Image current;											　　//現在選択Image
	private int slot;												　  //現在選択位置
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
		var index = 0;
		foreach(var value in texts.Values)
		{
			value.text 　　　　　　　　　 = PowerUpManager.Instance.GetPowerUp((PowerUpType)index).Name;	//パワーアップ名をUIで表示させる
			value.transform.parent.name = PowerUpManager.Instance.GetPowerUp((PowerUpType)index).Name;	//対応するオブジェクト名を修正
			index++;
		}
		//選択オブジェクト名を修正
		current.name = "Cursor";	
    }

    private void Update()
    {	
		//パワーアップ選択開始（パワーアップ決定入力が確認されるまで）
		if (PowerUpManager.Instance.IsSelect)
		{
			//選択が無効であれば有効にする
			if (!current.gameObject.activeSelf)
				current.gameObject.SetActive(true);

			//パワーアップマネージャーの中の選択位置を取得
			slot = PowerUpManager.Instance.CurrentSlot;

			//選択オブジェクト名は現在位置ではなければ、移動させる
			if(current.transform.position != texts[slot].transform.position)
				current.transform.position = texts[slot].transform.position;		
		}
		//パワーアップ決定
		else
		{
			//選択が有効であれば無効にする
			if(current.gameObject.activeSelf)
				current.gameObject.SetActive(false);
		}


		for(var i = 0; i < texts.Count; ++i)
		{
			var power = PowerUpManager.Instance.GetPowerUp((PowerUpType)i);
			if (power != null)
			{
				if ((!power.IsMainWeaponUpgrade && !power.CannotUpgrade) || (power.IsMainWeaponUpgrade && !power.IsWeaponUsing))
					texts[i].text = power.Name;
				if ((!power.IsMainWeaponUpgrade && power.CannotUpgrade) || (power.IsMainWeaponUpgrade && power.IsWeaponUsing))
					texts[i].text = string.Empty;
			}
		}
		
	}
}
