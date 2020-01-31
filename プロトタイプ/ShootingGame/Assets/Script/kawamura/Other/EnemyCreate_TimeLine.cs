//作成：川村良太
//タイムラインで関数を呼び出して敵を出す

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate_TimeLine : MonoBehaviour
{
	public GameObject dischargeObj;
	public GameObject saveObj;
	public GameObject saveObj2;
	public GameObject mapObj;
	public Enemy_Discharge saveDischarge_Script;

	public enum CreateEnemyType
	{
		None,
		Discharge_LeftCurveUp90,                    //排出の上向き左90度カープ
		Discharge_RightCurveUp90,               //排出の上向き右90度カープ
		Discharge_LeftCurveDown90,          //排出の上向き左90度カープ
		Discharge_RightCurveDown90,			//排出の上向き右90度カープ
		Discharge_Up_Left180,				//排出左向き
		Discharge_Up_Right180,				//
		Discharge_Down_Left180,			//
		Discharge_Down_Right180,			//
		Discharge_UpAndDown,				//
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
		public Vector3 manualVector;					//手打ちで出したい位置を入力できる
	}

	[Header("配列の[１]番目から出します")]
	public EnemyInformation[] enemyInformation;

	public int createNum;					//次に出す順番の数
	public string nextGroupName;		//次に出す敵の名前

	void Start()
    {
		mapObj = GameObject.Find("Stage_02_Map").gameObject;
		ResouceUpload();
		CreatePosUpload();

		createNum = 1;
		
    }

    void Update()
    {
        
    }

	void ResouceUpload()
	{
		dischargeObj = Resources.Load("Enemy2/Enemy_Discharge") as GameObject;
	}

	void CreatePosUpload()
	{
		dischargePos_Top = GameObject.Find("DischargePos_Top").transform;
		dischargePos_Under = GameObject.Find("DischargePos_Under").transform;

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
			case CreateEnemyType.None:
				createNum++;
				break;

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

		}
	}
}
