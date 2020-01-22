using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate2 : MonoBehaviour
{
	[Header("生成する敵オブジェクト　上から生成")]
	public GameObject[] createObjects;
	[Header("生成する位置")]
	public Vector3[] createPositions;
	[Header("生成時のRotation")]
	public Quaternion[] createRotation;

	GameObject saveObj;		//一時保存用

	[Header("敵排出を出す場合の排出タイプ設定")]
	public Enemy_Discharged.MoveType[] movetype;
	Enemy_Discharge discharge_Script;

	int createNum;			//出すオブジェクトの番号
	int moveTypeNum;		//敵排出の敵の動き番号

    void Start()
    {
		createNum = 0;
    }

    void Update()
    {
		//生成関数
		Create();
    }

	void Create()
	{
		saveObj = Instantiate(createObjects[createNum], createPositions[createNum], createRotation[createNum]);
		saveObj.transform.parent = transform;
		saveObj.transform.localPosition = createPositions[createNum];
		if (saveObj.name == "Enemy_Discharge(Clone)")
		{
			discharge_Script = saveObj.GetComponent<Enemy_Discharge>();
			discharge_Script.setMoveType = movetype[moveTypeNum];
			moveTypeNum++;
		}

		saveObj = null;

		createNum++;

	}
}
