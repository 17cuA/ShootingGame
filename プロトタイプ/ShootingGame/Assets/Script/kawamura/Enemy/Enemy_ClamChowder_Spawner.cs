using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClamChowder_Spawner : MonoBehaviour
{
	public enum EnemyMoveState
	{

	}



	public int createCnt = 0;
	public int createDelay = 0;

	GameObject saveObj;
	Enemy_Wave WaveScript;
	public GameObject[] waveStraightPos;

    void Start()
    {
        
    }

    void Update()
    {
		createDelay++;
		if (createCnt<8 && createDelay >= 13)
		{
			saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
			saveObj.transform.position = waveStraightPos[0].transform.position;
			saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyDown);

			saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
			saveObj.transform.position = waveStraightPos[1].transform.position;
			saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.Straight);

			saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
			saveObj.transform.position = waveStraightPos[2].transform.position;
			saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyUp);

			saveObj = null;
			createDelay = 0;
			createCnt++;
		}
	}
}
