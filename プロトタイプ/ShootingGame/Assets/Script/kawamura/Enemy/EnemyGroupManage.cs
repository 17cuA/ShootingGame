//敵群を管理する(群の中の敵の数)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManage : MonoBehaviour
{
	public GameObject item;
	GameObject[] childObjects;

	public int childNum;                    //最初の敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数


	public Transform itemTransform;
	public Vector3 itemPos;

	public bool isDead = false;
	public bool isItemDrop=true;

	private void Awake()
	{
		childNum = transform.childCount;
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
		item = Resources.Load("Item/Item_Test") as GameObject;
		remainingEnemiesCnt = childNum;
	}

	void Update()
	{
		if (defeatedEnemyCnt + notDefeatedEnemyCnt == childNum)
		{
			if (notDefeatedEnemyCnt == 0)
			{

			}
			notDefeatedEnemyCnt = 0;
			defeatedEnemyCnt = 0;
			itemPos = new Vector3(0, 0, 0);
			itemTransform = null;
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
		isItemDrop = isDrop;
	}
	private void OnDisable()
	{
		if (isDead)
		{
			//Instantiate(item, itemTransform.position, transform.rotation);
			//Instantiate(item, itemPos, this.transform.rotation);

			//itemPos = new Vector3(0, 0, 0);
			//itemTransform = null;
			//remainingEnemiesCnt = childNum;

			//isDead = false;
		}

	}
}
