using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterFollow : MonoBehaviour
{
	public GameObject previousObj;			//ひとつ前の追従位置
	public GameObject parentObj;
	public GameObject hunterObj;
	public string parentName;
	FollowPositions followParent_Script;    //4つの追従位置の親スクリプト
	public FollowToPreviousBit followBit_Script;

	public Vector3[] previousBitPos;
	public Vector3 pos;

	public int cnt;
	int array_Num;
	public int childCnt;

	public string myName;
	public int myNumber;

	//bool one = true;
	//bool once = true;
	public bool check = false;      //配列すべてに値が入っているかの判定
	public bool isMove = false;
	public bool hasOption = false;
	public bool isStolen = false;                       //自身がハンターに当たるとtrue
	public bool isStolen_Previous = false;

	void Start()
	{
		myName = gameObject.name;

		parentObj = transform.parent.gameObject;
		parentName = parentObj.name;

		//int cnt = 0;
		array_Num = 9;
		previousBitPos = new Vector3[array_Num];

		pos = previousObj.transform.position;

	}

	void Update()
	{
		childCnt = this.transform.childCount;

		if (check)
		{
			if (pos == previousObj.transform.position)
			{
				isMove = false;
			}
			else
			{
				isMove = true;
				pos = previousObj.transform.position;
			}
		}

		if (!check)
		{
			for (int i = 0; i < array_Num; i++)
			{
				previousBitPos[i] = previousObj.transform.position;

			}
			check = true;
		}
		//前のビットが動いていて位置配列すべてに値が入っているとき
		else if (isMove && check)
		{
			//isMove = false;
			//自分の位置を配列に入っている位置に
			//transform.position = playerPos[cnt].position;
			transform.position = previousBitPos[cnt];

			//自分の位置を移動したのでその位置を今、前のビットのいる位置で更新
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousObj.transform.position;

			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
			}
		}
		

		if (myNumber == 2)
		{

		}
		else if (myNumber == 3 || myNumber == 4)
		{
			if (followBit_Script.isStolen || followBit_Script.isStolen_Previous)
			{
				isStolen_Previous = true;
				transform.parent = null;
			}
		}
	}
}
