using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTransparencyChange : MonoBehaviour
{
	//マテリアルリスト
	public Material[] backgroundMaterialList = new Material[9];
	public Material backgroundMaterialBlack;

	private float elapsedTime = 0.0f;   //経過時間
	public float changeTime = 3.0f;     //変化にかかる時間

	public Color activeColor;       //アクティブ状態の色
	public Color inactiveColor;     //インアクティブ状態の色

	private bool changeFlag;         //変化中のフラグ

	private Color startColor;       //変化前の色
	private Color endColor;         //変化後の色

	public float speedDifference;  //スピードの差
	public Vector2 scrollSpead;     //スクロールのスピード

	void Start()
	{
		changeFlag = false;

		for (int i = 0; i < backgroundMaterialList.Length; i++)
		{
			backgroundMaterialList[i].SetColor("_TintColor", inactiveColor);
			backgroundMaterialBlack.SetColor("_TintColor", new Color(0, 0, 0, 0));
		}
	}

	void Update()
	{
		ChangeColor();

		if (Input.GetKeyDown(KeyCode.Q)) { SetActive(); }
		if (Input.GetKeyDown(KeyCode.W)) { SetInactive(); }
	}

	//有効化
	public void SetActive()
	{
		ChangeStart(inactiveColor, activeColor);
	}

	//無効化
	public void SetInactive()
	{
		ChangeStart(activeColor, inactiveColor);
	}

	//変化開始処理
	public void ChangeStart(Color startColor_, Color endColor_)
	{
		startColor = startColor_;
		endColor = endColor_;
		elapsedTime = 0f;
		changeFlag = true;
	}

	//色変化処理
	public void ChangeColor()
	{
		//色変化
		if (changeFlag)
		{
			elapsedTime += Time.deltaTime;

			if (elapsedTime <= changeTime)
			{
				for (int i = 0; i < backgroundMaterialList.Length; i++)
				{
					backgroundMaterialList[i].SetColor
						("_TintColor",
							new Color(
								1,
								1,
								1,
								(endColor.a - startColor.a) * (elapsedTime / changeTime)
							)
						);
					backgroundMaterialBlack.SetColor
						("_TintColor",
							new Color(
								0,
								0,
								0,
								(endColor.a - startColor.a) * (elapsedTime / changeTime)
							)
						);
				}
			}
			else
			{
				changeFlag = false;
			}
		}

		//マテリアルスクロール
		for (int i = 0; i < backgroundMaterialList.Length; i++)
		{
			float yScroll = Mathf.Repeat(Time.time * scrollSpead.y * speedDifference * i * Random.Range(1f, 0.1f), 1);
			float xScroll = Mathf.Repeat(Time.time * scrollSpead.x * speedDifference * i * Random.Range(1f, 0.1f), 1);
			Vector2 offset = new Vector2(xScroll, yScroll);
			backgroundMaterialList[i].SetTextureOffset("_MainTex", offset);
		}
	}
}
