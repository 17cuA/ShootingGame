using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Mark : MonoBehaviour
{
	[SerializeField]
	private GameObject PlayerObj;   //プレイヤーのゲームオブジェクト
	[SerializeField]
	private GameObject[] EnemyObj;  //敵のゲームオブジェクト
	[SerializeField]
	private GameObject LockOnObj;   //ロックオンのゲームオブジェクト
	[SerializeField]
	private Canvas mainCanvas;      //キャンバス

	private GameObject TargetObj;   //ロックオン対象のゲームオブジェクト

	void Start()
	{
		//ロックオンマーカーの表示ON
		LockOnObj.SetActive(true);
	}

	void Update()
	{
		//右にいる敵にロックオンを切り替える
		if (Input.GetKeyDown(KeyCode.D))
		{
			F("right");
		}
		//左にいる敵にロックオンを切り替える
		else if (Input.GetKeyDown(KeyCode.A))
		{
			F("left");
		}
		//ロックオンマーカーの座標更新
		LockOnObj.transform.GetComponent<RectTransform>().localPosition = ToScreenPositionCaseScreenSpaceCamera(TargetObj.transform.position, mainCanvas);
	}

	//ワールド座標から、スクリーン座標に変換
	private Vector2 ToScreenPositionCaseScreenSpaceCamera(Vector3 position, Canvas canvas)
	{
		var p = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
		var retPosition = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			canvas.GetComponent<RectTransform>(),
			p,
			Camera.main,
			out retPosition
		);
		return retPosition;
	}

	//ロックオンの切り替え処理
	private void F(string str)
	{
		Vector3 TargetScreenPos = Camera.main.WorldToScreenPoint(TargetObj.transform.position);
		float TempPosX = TargetObj.transform.position.x;
		GameObject TempObj = null;

		for (byte i = 0; i < EnemyObj.Length; i++)
		{
			//プレイヤーの後ろにあるオブジェクトは対象にしない
			Vector3 Vec = EnemyObj[i].transform.position - PlayerObj.transform.position;
			float dot = Vector3.Dot(Camera.main.transform.forward.normalized, Vec.normalized);
			if (dot < -0.8f)
			{
				continue;
			}

			Vector3 ScreenPos = Camera.main.WorldToScreenPoint(EnemyObj[i].transform.position);

			//右にロックオンを切り替える
			if (str == "right" && TargetScreenPos.x < ScreenPos.x)
			{
				if (TempObj == null)
				{
					TempObj = EnemyObj[i];
				}
				else if (Camera.main.WorldToScreenPoint(TempObj.transform.position).x > ScreenPos.x)
				{
					TempObj = EnemyObj[i];
				}
			}
			//左にロックオンを切り替える
			else if (str == "left" && TargetScreenPos.x > ScreenPos.x)
			{
				if (TempObj == null)
				{
					TempObj = EnemyObj[i];
				}
				else if (Camera.main.WorldToScreenPoint(TempObj.transform.position).x < ScreenPos.x)
				{
					TempObj = EnemyObj[i];
				}
			}
		}

		if (TempObj != null)
		{
			TargetObj = TempObj;
		}
	}
}