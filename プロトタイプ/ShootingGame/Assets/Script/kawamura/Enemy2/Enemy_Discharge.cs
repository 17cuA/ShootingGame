//作成　川村良太
//敵排出する敵

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharge : MonoBehaviour
{
	public enum MyDirection
	{
		Up,
		Down,
		Left,
		Right,
		Free,
	}

	public MyDirection myDirection;

	//public enum SetMoveType
	//{
	//	LeftCurveUp_90,
	//	LeftCueveUp_180,
	//	LeftCurveDown_90,
	//	LeftCueveDown_180,
	//	RightCurveUp_90,
	//	RightCueveUp_180,
	//	RightCurveDown_90,
	//	RightCueveDown_180,
	//}
	//public SetMoveType setMoveType;

	public Enemy_Discharged.MoveType setMoveType;

	public GameObject createObj;
	Enemy_Discharged discharged_Script;

	Quaternion createRotation;

	[Header("入力用　グループ排出数")]
	public int createGroupNum = 0;
	public int createGroupCnt = 0;
	[Header("入力用　グループ内排出数")]
	public int createNum = 0;
	public int createCnt = 0;
	[Header("入力用　グループ排出間隔MAX(フレーム)")]
	public int createGroupDelayMax = 0;
	public int createGroupDelayCnt = 0;
	[Header("入力用　グループ内間隔MAX(フレーム)")]
	public int createDelayMax = 0;
	public int createDelayCnt = 0;


	void Start()
    {
		createRotation = Quaternion.Euler(0, 0, 0);
	}

    void Update()
    {
		if (transform.position.x < 16)
		{
			//ひとまとまりを出すディレイカウントが最大を超えたら
			if (createGroupDelayCnt > createGroupDelayMax)
			{
				createDelayCnt++;
				//ひとまとまりの排出がまだ残っていたら
				if (createGroupNum > createGroupCnt)
				{
					//ひとまとまり内の排出数が残っていたら
					if (createNum > createCnt)
					{
						//一体を出すディレイカウントが最大を超えたら
						if (createDelayCnt > createDelayMax)
						{
							//オブジェクト生成（のちにプーリング）
							GameObject saveObj = Instantiate(createObj, transform.position, createRotation);
							//スクリプト取得
							discharged_Script = saveObj.GetComponent<Enemy_Discharged>();

							if (myDirection==Enemy_Discharge.MyDirection.Free)
							{
								discharged_Script.isRotaReset = false;
								saveObj.transform.rotation = transform.rotation;

							}
							else
							{
								discharged_Script.isRotaReset = true;
							}
							//子供にする
							//saveObj.transform.parent = transform;
							//動きの種類を設定
							discharged_Script.moveType = setMoveType;
							//SetState(setMoveType);

							//一体を出すディレイカウントリセット
							createDelayCnt = 0;
							//出した数カウント加算
							createCnt++;
						}
					}
					//出した数カウントが出す数と同じになったら
					if (createCnt == createNum)
					{
						//出したひとまとまりカウント加算
						createGroupCnt++;
						//ひとまとまりを出すディレイカウントリセット
						createGroupDelayCnt = 0;
						//一体を出すカウントリセット
						createCnt = 0;
					}
				}
			}
			//ひとまとまりを出すディレイカウントが最大以下のとき
			else
			{
				createGroupDelayCnt++;
			}

		}

		//if (createDelayCnt > createDelayMax)
		//{
		//	Instantiate(createObj, transform.position, transform.rotation);
		//	createDelayCnt = 0;
		//}
	}

	public void SetMyDirection(MyDirection direc)
	{
		switch(direc)
		{
			case MyDirection.Up: transform.rotation = Quaternion.Euler(0, 0, 0); break;
			case MyDirection.Down: transform.rotation = Quaternion.Euler(0, 0, 180); break;
			case MyDirection.Left: transform.rotation = Quaternion.Euler(0, 0, 90); break;
			case MyDirection.Right: transform.rotation = Quaternion.Euler(0, 0, 270); break;
			case MyDirection.Free: break;
		}
		myDirection = direc;
	}

	//void SetState(SetMoveType setType)
	//{
	//	switch(setType)
	//	{
	//		case SetMoveType.LeftCurveUp_90:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCurveUp_90);
	//			break;

	//		case SetMoveType.LeftCueveUp_180:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCueveUp_180);
	//			break;

	//		case SetMoveType.LeftCurveDown_90:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCurveDown_90);
	//			break;

	//		case SetMoveType.LeftCueveDown_180:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCueveDown_180);
	//			break;

	//		case SetMoveType.RightCurveUp_90:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.RightCurveUp_90);
	//			break;

	//		case SetMoveType.RightCueveUp_180:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.RightCueveUp_180);
	//			break;

	//		case SetMoveType.RightCurveDown_90:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.RightCurveDown_90);
	//			break;

	//		case SetMoveType.RightCueveDown_180:
	//			discharged_Script.SetState(Enemy_Discharged.MoveType.RightCueveDown_180);
	//			break;
	//	}
	//}
}
