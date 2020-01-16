using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharge : MonoBehaviour
{
	public enum SetMoveType
	{
		LeftCurveUp_90,
		LeftCueveUp_180,
		LeftCurveDown_90,
		LeftCueveDown_180,
		RightCurveUp_90,
		RightCueveUp_180,
		RightCurveDown_90,
		RightCueveDown_180,
	}
	public SetMoveType setMoveType;

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
		if (createGroupDelayCnt > createGroupDelayMax)
		{
			createDelayCnt++;
			//ひとまとまりの排出がまだ残っていたら
			if (createGroupNum > createGroupCnt)
			{
				//ひとまとまり内の排出数が残っていたら
				if (createNum > createCnt)
				{
					if (createDelayCnt > createDelayMax)
					{
						GameObject saveObj =	Instantiate(createObj, transform.position, createRotation);
						discharged_Script = saveObj.GetComponent<Enemy_Discharged>();
						SetState(setMoveType);

						createDelayCnt = 0;
						createCnt++;
					}
				}
				if (createCnt == createNum)
				{
					createGroupCnt++;
					createGroupDelayCnt = 0;
					createCnt = 0;
				}
			}
		}
		else
		{
			createGroupDelayCnt++;
		}

		//if (createDelayCnt > createDelayMax)
		//{
		//	Instantiate(createObj, transform.position, transform.rotation);
		//	createDelayCnt = 0;
		//}
    }
	void SetState(SetMoveType setType)
	{
		switch(setType)
		{
			case SetMoveType.LeftCurveUp_90:
				discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCurveUp_90);
				break;

			case SetMoveType.LeftCueveUp_180:
				discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCueveUp_180);
				break;

			case SetMoveType.LeftCurveDown_90:
				discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCurveDown_90);
				break;

			case SetMoveType.LeftCueveDown_180:
				discharged_Script.SetState(Enemy_Discharged.MoveType.LeftCueveDown_180);
				break;

			case SetMoveType.RightCurveUp_90:
				discharged_Script.SetState(Enemy_Discharged.MoveType.RightCurveUp_90);
				break;

			case SetMoveType.RightCueveUp_180:
				discharged_Script.SetState(Enemy_Discharged.MoveType.RightCueveUp_180);
				break;

			case SetMoveType.RightCurveDown_90:
				discharged_Script.SetState(Enemy_Discharged.MoveType.RightCurveDown_90);
				break;

			case SetMoveType.RightCueveDown_180:
				discharged_Script.SetState(Enemy_Discharged.MoveType.RightCueveDown_180);
				break;
		}
}
}
