//作成：川村良太
//タイムラインで関数を呼び出して敵を出す

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate_TimeLine : MonoBehaviour
{
	GameObject dischargeObj;
	GameObject saveObj;
	GameObject mapObj;
	Enemy_Discharge saveDischarge_Script;

	public enum CreateEnemyType
	{
		Discharge_Up_Left90,				//排出の上向き左90度カープ
		Discharge_Up_Right90,           //排出の上向き右90度カープ
		Discharge_Down_Left90,				//排出の上向き左90度カープ
		Discharge_Down_Right90,         //排出の上向き右90度カープ
		Discharge_Left_180,
		Discharge_Right_180,

	}

	public enum CreatePos
	{
		None,
		Discharge_Top,
		Discharge_Under,
	}

	Transform dischargePos_Top;
	Transform dischargePos_Under;


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
		dischargeObj = Resources.Load("Enemy_Discharge") as GameObject;
	}

	void CreatePosUpload()
	{
		dischargePos_Top = GameObject.Find("DischargePos_Top").transform;
		dischargePos_Under = GameObject.Find("DischargePos_Under").transform;

	}

	void EnemyCreate()
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
			case CreateEnemyType.Discharge_Up_Left90:
				saveObj = Instantiate(dischargeObj, pos, transform.rotation);
				saveObj.transform.parent = mapObj.transform;
				saveDischarge_Script = saveObj.GetComponent<Enemy_Discharge>();
				saveDischarge_Script.SetMyDirection(Enemy_Discharge.MyDirection.Up);
				saveDischarge_Script.setMoveType = Enemy_Discharged.MoveType.LeftCurveUp_90;

				saveObj = null;
				saveDischarge_Script = null;

				break;
		}
	}
}
