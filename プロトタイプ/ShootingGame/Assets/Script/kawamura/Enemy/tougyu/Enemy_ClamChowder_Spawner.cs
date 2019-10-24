using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClamChowder_Spawner : MonoBehaviour
{
	public enum EnemyMoveState
	{
		BackWave,
		WaveOnlyUp,
		WaveOnlyDown,
		WaveAndStraight,
		Straight,
		Rush,
	}

	public EnemyMoveState moveState;

	GameObject saveObj;
	Enemy_Wave WaveScript;
	public GameObject[] waveStraightPos;

	public int createNum;                   //作り出す処理の回数（敵の数ではない）
	public int createCnt = 0;				//敵を作り出す処理の回数カウント（敵の数ではない）
	public int createDelay = 0;				//敵を作るときの間隔
	public int childNum;                    //敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
	public string myName;

	bool isCreate = true;
	bool isFirstAppearance = false;     //一番はじめに出す敵が出たかどうか
	bool isAppearanceEnd = false;       //敵を出し終わったかどうか
	public bool isItemDrop = true;


	void Start()
	{

	}

	void Update()
	{
		//敵の出現が終わっていなかったら
		if (!isAppearanceEnd)
		{
			//出現関数呼び出し
			EnemyAppearance(moveState, createNum);
			createDelay++;

		}

		//倒された敵の数と倒されずに画面外に出た敵の数の合計が最初の子供の数と同じになったら
		if ((defeatedEnemyCnt + notDefeatedEnemyCnt == childNum) && isAppearanceEnd)
		{
			//remainingEnemiesCnt = childNum;
			ResetState();
			gameObject.SetActive(false);
			//Destroy(this.gameObject);
			//gameObject.SetActive(false);

			//isDead = true;
			//Died_Process();
		}
	}

	//出現させる敵の動きと数の設定
	void AppearanceSetting(EnemyMoveState state, int num)
	{
		moveState = state;
		createNum = num;
	}

	//敵を出現させる
	void EnemyAppearance(EnemyMoveState state, int num)
	{
		switch (state)
		{
			case EnemyMoveState.BackWave:
				break;

			case EnemyMoveState.WaveOnlyUp:
				break;

			case EnemyMoveState.WaveOnlyDown:
				break;

			case EnemyMoveState.WaveAndStraight:
				if ((createCnt < num && createDelay >= 13) || !isFirstAppearance)
				{
					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[0].transform.position;
					saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyDown);

					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[1].transform.position;
					saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.Straight);

					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[2].transform.position;
					saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyUp);

					saveObj = null;
					isFirstAppearance = true;
					createDelay = 0;
					createCnt++;
					childNum += 3;
					remainingEnemiesCnt += 3;
				}
				if (createCnt == num)
				{
					isAppearanceEnd = true;
				}

				break;

			case EnemyMoveState.Straight:
				break;

			case EnemyMoveState.Rush:
				break;
		}
	}

	//アイテムを落とすかどうか
	public void WhetherToDropTheItem(bool isDrop)
	{
		isItemDrop = isDrop;
	}

	//いろいろリセット
	void ResetState()
	{
		//倒されたのと画面外に出たカウントをリセット
		notDefeatedEnemyCnt = 0;
		defeatedEnemyCnt = 0;
		remainingEnemiesCnt = 0;
		createNum = 0;
		createCnt = 0;
		childNum = 0;
		isFirstAppearance = false;
		isAppearanceEnd = false;
	}
}
