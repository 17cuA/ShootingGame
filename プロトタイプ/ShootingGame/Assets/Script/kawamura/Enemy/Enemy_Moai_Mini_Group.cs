using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Mini_Group : MonoBehaviour
{
	public GameObject item;
	public GameObject parentObj;
	GameObject[] childObjects;

	public float speedX;

	public int childNum;                    //最初の敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
	public string myName;

	public bool isChildRoll = false;        //子供が回転し始めるときにtrue
	public bool isChildMove = false;		//子供が動き始める(上下の移動)
	public bool isMove = false;				//自分（親）が動くときに使う
	public bool isDead = false;

	private void Awake()
	{
		myName = gameObject.name;
		childNum = transform.childCount;
		remainingEnemiesCnt = childNum;
		childObjects = new GameObject[childNum];
		for (int i = 0; i < childNum; i++)
		{
			childObjects[i] = transform.GetChild(i).gameObject;
		}

	}

	private void OnEnable()
	{
		for (int i = 0; i < childNum; i++)
		{
			childObjects[i].SetActive(enabled);
		}
		
		//defeatedEnemyCnt = 0;
		//notDefeatedEnemyCnt = 0;
	}
	void Start()
	{
		//remainingEnemiesCnt = childNum;
	}

	void Update()
	{
		//倒された敵の数と倒されずに画面外に出た敵の数の合計が最初の子供の数と同じになったら
		if (defeatedEnemyCnt + notDefeatedEnemyCnt == childNum)
		{
			//倒されたのと画面外に出たカウントをリセット
			notDefeatedEnemyCnt = 0;
			defeatedEnemyCnt = 0;
			remainingEnemiesCnt = childNum;
			gameObject.SetActive(false);
			//Destroy(this.gameObject);
			//gameObject.SetActive(false);

			//isDead = true;
			//Died_Process();
		}
	}

	//アイテムを落とす群かを設定する(trueで落とす、falseで落とさない)
	public void WhetherToDropTheItem(bool isDrop)
	{
		//isItemDrop = isDrop;
	}
	private void OnDisable()
	{

	}
}
