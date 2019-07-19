using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Power;
using TextDisplay;

public class UI_PowerUp : MonoBehaviour
{
	public Image current;      //現在選択Image
	public Dictionary<int, GameObject> displays = new Dictionary<int, GameObject>();
    public Sprite initSpeed;
    public Sprite speedUp;

    public int addtional;
	public int start;
	public int end;

	private void Awake()
	{
		for (var i = start; i < end; ++i)
		{
			var power		 = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);
			var number       = (int)power.Type;
			transform.GetChild(i - start).gameObject.name = power.Type.ToString();
			displays.Add(number, transform.GetChild(i - start).gameObject);
		}
		
		current.name = "Cursor";
	}

	private void Update()
	{
		var currentPower = PowerManager.Instance.CurrentPower;
		//現在選択パワー存在
		if (currentPower != null && ((int)currentPower.Type >= start && (int)currentPower.Type < end || (int)currentPower.Type == addtional))
		{
			//選択画像無効の場合　　->　有効にする
			if (!current.gameObject.activeSelf)
				current.gameObject.SetActive(true);

			//現在位置に合わせる
			if (current.gameObject.transform.position != displays[PowerManager.Instance.Position].transform.position && PowerManager.Instance.Position != -1) 
			{
				current.gameObject.transform.position = displays[PowerManager.Instance.Position].transform.position;
				current.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
				displays[PowerManager.Instance.Position].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

				for (var i = start; i < end; ++i)
				{
					if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f) && i != PowerManager.Instance.Position)
						displays[i].transform.localScale = new Vector3(1f, 1f, 1f);
				}
			}			
		}
		else
		{
			if (current.gameObject.activeSelf)
				current.gameObject.SetActive(false);

			for (var i = start; i < end; ++i)
			{
				if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f))
					displays[i].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}

		//表示検索ループ
		for (var i = start; i < end; ++i)
		{
			//すべてパワーをチェック
			var power = PowerManager.Instance.GetPower((PowerManager.Power.PowerType)i);
			//表示できる場合、　パワー名を表示させる
			if (power.CanUpgrade)
			{
                if(power.Type == PowerManager.Power.PowerType.INITSPEED)
                {
                    if(displays[i].GetComponent<Image>().sprite != initSpeed)
                        displays[i].GetComponent<Image>().sprite = initSpeed;                
                }

                if(power.Type == PowerManager.Power.PowerType.SPEEDUP)
                {
                    if(displays[i].GetComponent<Image>().sprite != speedUp)
                        displays[i].GetComponent<Image>().sprite = speedUp;                
                }

				if (transform.GetChild(i - start).GetChild(0).gameObject.activeSelf)
					transform.GetChild(i - start).GetChild(0).gameObject.SetActive(false);
			}
			//表示できない場合、　パワー名を空にする
			else
			{

				if (!transform.GetChild(i - start).GetChild(0).gameObject.activeSelf)
					transform.GetChild(i - start).GetChild(0).gameObject.SetActive(true);
			}
		}
	}
}
