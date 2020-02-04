//作成：川村良太
//タイムラインで関数を呼び出して敵を出す

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate_TimeLine : MonoBehaviour
{
	public GameObject dischargeObj;
	public GameObject followGroundObj;
	public GameObject saveObj;
	public GameObject mapObj;
	public Enemy_Discharge saveDischarge_Script;
	public FollowGround3 saveFollowGrownd_Script;

	public enum CreateEnemyType
	{
		None,
		Discharge_LeftCurveUp90,						 //排出の上向き左90度カープ
		Discharge_RightCurveUp90,					//排出の上向き右90度カープ
		Discharge_LeftCurveDown90,					//排出の下向き左90度カープ
		Discharge_RightCurveDown90,				//排出の下向き右90度カープ
		Discharge_Up_Left180,							//排出左向き180度カーブ上
		Discharge_Down_Left180,						//排出左向き180度カーブ下
		Discharge_Up_Right180,							//排出右向き180度カーブ上
		Discharge_Down_Right180,						//排出右向き180度カーブした
		Discharge_UpAndDown_LeftCurve90,		//排出上下左カーブ
		Discharge_UpAndDown_RightCurve90,	//排出上下右カーブ
		FollowGround_Left,
		FollowGround_Right,
	}

	public enum CreatePos
	{
		None,
		Discharge_Top,
		Discharge_Under,
	}

	public Transform dischargePos_Top;
	public Transform dischargePos_Under;

	[System.Serializable]
	public struct EnemyInformation
	{
		public string enemyName;
		public CreateEnemyType enemyType;
		public CreatePos createPos;
		[Header("出現位置を自分で指定する時にPosをNoneにして入れる")]
		public Vector3 manualVector;					//手打ちで出したい位置を入力できる
	}

	public int createNum;                   //次に出す順番の数
	public string nextGroupName;        //次に出す敵の名前

	[Header("配列の[１]番目から出します")]
	public EnemyInformation[] enemyInformation;


	void Start()
    {
		mapObj = GameObject.Find("Stage_02_Map").gameObject;
		ResouceUpload();
		CreatePosUpload();
		EnemyNameSet();
		createNum = 1;
		
    }

    void Update()
    {
        
    }

	void ResouceUpload()
	{
		dischargeObj = Resources.Load("Enemy2/Enemy_Discharge") as GameObject;
		followGroundObj = Resources.Load("Enemy2/Enemy_FollowGround") as GameObject;
	}

	void CreatePosUpload()
	{
		dischargePos_Top = GameObject.Find("DischargePos_Top").transform;
		dischargePos_Under = GameObject.Find("DischargePos_Under").transform;

	}

	void EnemyNameSet()
	{
		for (int i = 0; i < 5; i++)
		{
			switch(enemyInformation[i].enemyType)
			{
				//なし
				case CreateEnemyType.None:
					enemyInformation[i].enemyName = "なし";
					break;

				//上向き90度左カーブ
				case CreateEnemyType.Discharge_LeftCurveUp90:
					enemyInformation[i].enemyName = "上向き90度左カーブ";

					break;

				//上向き90度右カーブ
				case CreateEnemyType.Discharge_RightCurveUp90:
					enemyInformation[i].enemyName = "上向き90度右カーブ";
					break;

				//下向き90度左カーブ
				case CreateEnemyType.Discharge_LeftCurveDown90:
					enemyInformation[i].enemyName = "下向き90度左カーブ";
					break;

				//下向き90度右カーブ
				case CreateEnemyType.Discharge_RightCurveDown90:
					enemyInformation[i].enemyName = "下向き90度右カーブ";
					break;

				//左向き180度カーブ上
				case CreateEnemyType.Discharge_Up_Left180:
					enemyInformation[i].enemyName = "左向き180度カーブ上";

					break;

				//左向き180度カーブ下
				case CreateEnemyType.Discharge_Down_Left180:
					enemyInformation[i].enemyName = "左向き180度カーブ下";
					break;

				//右向き180度カーブ上
				case CreateEnemyType.Discharge_Up_Right180:
					enemyInformation[i].enemyName = "右向き180度カーブ上";
					break;

				//右向き180度カーブ下
				case CreateEnemyType.Discharge_Down_Right180:
					enemyInformation[i].enemyName = "右向き180度カーブ下";
					break;

				//上下左カーブ
				case CreateEnemyType.Discharge_UpAndDown_LeftCurve90:
					enemyInformation[i].enemyName = "上下左カーブ";
					break;

				//上下右カーブ
				case CreateEnemyType.Discharge_UpAndDown_RightCurve90:
					enemyInformation[i].enemyName = "上下右カーブ";
					break;

				//上下右カーブ
				case CreateEnemyType.FollowGround_Left:
					enemyInformation[i].enemyName = "地面に沿う敵左進み";
					break;

				//上下右カーブ
				case CreateEnemyType.FollowGround_Right:
					enemyInformation[i].enemyName = "地面に沿う敵右進み";
					break;

				default:
					enemyInformation[i].enemyName = "不明";
					break;

			}
		}
	}

	public void EnemyCreate()
	{
		Vector3 pos = Vector3.zero;

		switch(enemyInformation[createNum].createPos)
		{
			case CreatePos.None : pos = enemyInformation[createNum].manualVector; break;
			case CreatePos.Discharge_Top : pos = dischargePos_Top.position; break;
			case CreatePos.Discharge_Under : pos = dischargePos_Under.position; break;

		}

		switch(enemyInformation[createNum].enemyType)
		{
			//なし
			case CreateEnemyType.None:
				createNum++;
				break;

			//上向き90度左カーブ
			case CreateEnemyType.Discharge_LeftCurveUp90:

				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Up);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCurveUp_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//上向き90度右カーブ
			case CreateEnemyType.Discharge_RightCurveUp90:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Up);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCurveUp_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//下向き90度左カーブ
			case CreateEnemyType.Discharge_LeftCurveDown90:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Down);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCurveDown_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//下向き90度右カーブ
			case CreateEnemyType.Discharge_RightCurveDown90:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Down);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCurveDown_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//左向き180度カーブ上
			case CreateEnemyType.Discharge_Up_Left180:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Left);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCueveUp_180;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;

				break;

			//左向き180度カーブ下
			case CreateEnemyType.Discharge_Down_Left180:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Left);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCueveDown_180;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//右向き180度カーブ上
			case CreateEnemyType.Discharge_Up_Right180:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Right);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCueveUp_180;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//右向き180度カーブ下
			case CreateEnemyType.Discharge_Down_Right180:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Right);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCueveDown_180;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//上下左カーブ
			case CreateEnemyType.Discharge_UpAndDown_LeftCurve90:
				saveObj = Instantiate(dischargeObj, dischargePos_Under.position, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Up);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCurveUp_90;

				saveObj = Instantiate(dischargeObj, dischargePos_Top.position, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Down);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCurveDown_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			//上下右カーブ
			case CreateEnemyType.Discharge_UpAndDown_RightCurve90:
				saveObj = Instantiate(dischargeObj, dischargePos_Under.position, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Up);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCurveUp_90;

				saveObj = Instantiate(dischargeObj, dischargePos_Top.position, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Down);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.RightCurveDown_90;

				saveObj = null;
				saveDischarge_Script = null;
				createNum++;
				break;

			case CreateEnemyType.FollowGround_Left:
				saveObj = Instantiate(followGroundObj, pos, transform.rotation);
				//saveObj.transform.parent = mapObj.transform;
				saveFollowGrownd_Script = saveObj.GetComponent<FollowGround3>();
				saveFollowGrownd_Script.SetDirection(FollowGround3.DirectionState.Left);

				saveObj = null;
				saveFollowGrownd_Script = null;
				createNum++;
				break;

			case CreateEnemyType.FollowGround_Right:
				saveObj = Instantiate(followGroundObj, pos, transform.rotation);
				//saveObj.transform.parent = mapObj.transform;
				saveFollowGrownd_Script = saveObj.GetComponent<FollowGround3>();
				saveFollowGrownd_Script.SetDirection(FollowGround3.DirectionState.Right);

				saveObj = null;
				saveFollowGrownd_Script = null;
				createNum++;
				break;

			default:
				break;
		}

		nextGroupName = enemyInformation[createNum].enemyName;
	}
}
