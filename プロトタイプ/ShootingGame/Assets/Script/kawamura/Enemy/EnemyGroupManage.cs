﻿//作成者：川村良太
//敵群を管理する(群の中の敵の数)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManage : MonoBehaviour
{
	public GameObject item;
    public GameObject parentObj;
	GameObject[] childObjects;

	public int childNum;                    //最初の敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
    public string myName;

	public Transform itemTransform;
	public Vector3 itemPos;

    BaculasManager bacuManager;

	public bool isDead = false;
	public bool isItemDrop=true;

	private void Awake()
	{
        myName = gameObject.name;
        if(myName== "Enemy_Bacula_Four")
        {
            parentObj = transform.parent.gameObject;
            bacuManager = parentObj.GetComponent<BaculasManager>();
            childNum = transform.childCount * 16;
            remainingEnemiesCnt = childNum;

        }
        else
        {
            childNum = transform.childCount;
            remainingEnemiesCnt = childNum;
            childObjects = new GameObject[childNum];
            for (int i = 0; i < childNum; i++)
            {
                childObjects[i] = transform.GetChild(i).gameObject;
            }

        }
        item = Resources.Load("Item/Item_Test") as GameObject;

    }

    private void OnEnable()
	{
        if (myName != "Enemy_Bacula_Four")
        {
            for (int i = 0; i < childNum; i++)
            {
                childObjects[i].SetActive(enabled);
            }
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
		if (defeatedEnemyCnt + notDefeatedEnemyCnt == childNum)
		{
			notDefeatedEnemyCnt = 0;
			defeatedEnemyCnt = 0;
			itemPos = new Vector3(0, 0, 0);
			itemTransform = null;
			remainingEnemiesCnt = childNum;
            if(bacuManager)
            {
                //群を管理している親の残っている敵カウントマイナス
                bacuManager.remainingEnemiesCnt--;
                //倒された敵のカウントプラス
                bacuManager.defeatedEnemyCnt++;
            }
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

	}
}
