//2019/07/29 変更者：佐藤翼
//変更点：particle発光に対応
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Power;
using TextDisplay;

public class UI_PowerUp : MonoBehaviour
{
	public int playerNum;
	public bool isPlayer1;
	public bool isPlayer2;
	public Image current;      //現在選択Image
	public Dictionary<int, UI_PowerUpComponent> displays = new Dictionary<int, UI_PowerUpComponent>();
    public Sprite initSpeed;
    public Sprite speedUp;

    public int addtional;
	public int start;
	public int end;

	[Header("拡大倍数")]
	public float enlargeIndex = 1.2f;

	private void Awake()
	{
		for (var i = start; i < end; ++i)
		{
			if (playerNum == 1 || isPlayer1)
			{
				var power = P1_PowerManager.Instance.GetPower((P1_PowerManager.Power.PowerType)i);
				var number = (int)power.Type;
				transform.GetChild(i - start).gameObject.name = power.Type.ToString();
				displays.Add(number, transform.GetChild(i - start).GetComponent<UI_PowerUpComponent>());
			}
			if(isPlayer2)
			{
				var power = P2_PowerManager.Instance.GetPower((P2_PowerManager.Power.PowerType)i);
				var number = (int)power.Type;
				transform.GetChild(i - start).gameObject.name = power.Type.ToString();
				displays.Add(number, transform.GetChild(i - start).GetComponent<UI_PowerUpComponent>());
			}
		}
		current.name = "Cursor";

		if((int)Game_Master.Number_Of_People != playerNum)
		{
			transform.parent.gameObject.SetActive(false);
		}
	}


	private void Update()
	{
		if (playerNum == 1 || isPlayer1)
		{
			var currentPower = P1_PowerManager.Instance.CurrentPower;
			//現在選択パワー存在
			if (currentPower != null && ((int)currentPower.Type >= start && (int)currentPower.Type < end || (int)currentPower.Type == addtional))
			{

				//現在位置に合わせる
				if (current.gameObject.transform.position != displays[P1_PowerManager.Instance.Position].transform.position && P1_PowerManager.Instance.Position != -1)
				{
					current.gameObject.transform.position = displays[P1_PowerManager.Instance.Position].transform.position;
					displays[P1_PowerManager.Instance.Position].Enlarge(enlargeIndex);
					displays[P1_PowerManager.Instance.Position].GetComponent<Canvas>().sortingOrder = 1;

					for (var i = start; i < end; ++i)
					{
						if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f) && i != P1_PowerManager.Instance.Position)
						{
							displays[i].Resize();
							displays[i].GetComponent<Canvas>().sortingOrder = 0;
						}
					}
				}
			}
			else
			{

				for (var i = start; i < end; ++i)
				{
					if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f))
					{
						displays[i].Resize();
						displays[i].GetComponent<Canvas>().sortingOrder = 0;
					}
				}
			}

			//表示検索ループ
			for (var i = start; i < end; ++i)
			{
				//すべてパワーをチェック
				var power = P1_PowerManager.Instance.GetPower((P1_PowerManager.Power.PowerType)i);
				//表示できる場合、　パワー名を表示させる
				if (power.CanUpgrade)
				{
					if (power.Type == P1_PowerManager.Power.PowerType.INITSPEED)
					{
						if (displays[i].GetComponent<Image>().sprite != initSpeed)
							displays[i].GetComponent<Image>().sprite = initSpeed;
					}

					if (power.Type == P1_PowerManager.Power.PowerType.SPEEDUP)
					{
						if (displays[i].GetComponent<Image>().sprite != speedUp)
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
		if(isPlayer2)
		{

			var currentPower = P2_PowerManager.Instance.CurrentPower;
			//現在選択パワー存在
			if (currentPower != null && ((int)currentPower.Type >= start && (int)currentPower.Type < end || (int)currentPower.Type == addtional))
			{

				//現在位置に合わせる
				if (current.gameObject.transform.position != displays[P2_PowerManager.Instance.Position].transform.position && P2_PowerManager.Instance.Position != -1)
				{
					current.gameObject.transform.position = displays[P2_PowerManager.Instance.Position].transform.position;
					displays[P2_PowerManager.Instance.Position].Enlarge(enlargeIndex);
					displays[P2_PowerManager.Instance.Position].GetComponent<Canvas>().sortingOrder = 1;

					for (var i = start; i < end; ++i)
					{
						if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f) && i != P2_PowerManager.Instance.Position)
						{
							displays[i].Resize();
							displays[i].GetComponent<Canvas>().sortingOrder = 0;
						}
					}
				}
			}
			else
			{
				for (var i = start; i < end; ++i)
				{
					if (displays[i].transform.localScale != new Vector3(1f, 1f, 1f))
					{
						displays[i].Resize();
						displays[i].GetComponent<Canvas>().sortingOrder = 0;
					}
				}
			}

			//表示検索ループ
			for (var i = start; i < end; ++i)
			{
				//すべてパワーをチェック
				var power = P2_PowerManager.Instance.GetPower((P2_PowerManager.Power.PowerType)i);
				//表示できる場合、　パワー名を表示させる
				if (power.CanUpgrade)
				{
					if (power.Type == P2_PowerManager.Power.PowerType.INITSPEED)
					{
						if (displays[i].GetComponent<Image>().sprite != initSpeed)
							displays[i].GetComponent<Image>().sprite = initSpeed;
					}

					if (power.Type == P2_PowerManager.Power.PowerType.SPEEDUP)
					{
						if (displays[i].GetComponent<Image>().sprite != speedUp)
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
}
