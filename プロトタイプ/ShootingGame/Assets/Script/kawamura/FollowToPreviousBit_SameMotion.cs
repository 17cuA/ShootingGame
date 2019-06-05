using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPreviousBit_SameMotion : MonoBehaviour
{
	public GameObject playerObj;
	GameObject previousBitObj;

	public Vector3[] previousBitPos;
	public Vector3 pos;

	public int cnt;
	int array_Num;

	public string myName;

	bool once = true;
	public bool check = false;
	public bool isMove = false;
	void Start()
	{
		myName = gameObject.name;
		if (myName == "FollowPosSecond")
		{
			previousBitObj = GameObject.Find("FollowPosFirst");
		}
		else if (myName == "FollowPosThird")
		{
			previousBitObj = GameObject.Find("FollowPosSecond");

		}
		else if (myName == "FollowPosFourth")
		{
			previousBitObj = GameObject.Find("FollowPosThird");

		}

		int cnt = 0;
		array_Num = 8;
		previousBitPos = new Vector3[array_Num];

		once = true;
		pos = previousBitObj.transform.position;

	}

	void Update()
	{
		//前のプレイヤーの座標と今のプレイヤーの座標が違うとき　かつ　位置配列すべてに値が入っていないとき
		if (pos != previousBitObj.transform.position && !check)
		{
			//位置配列にプレイヤーの位置を入れる
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;

			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
			}
		}
		if (previousBitPos[array_Num - 1] != null)
		{
			check = true;
		}

		if (once)
		{
			//プレイヤーの座標が動いていないとき
			if (pos == previousBitObj.transform.position)
			{
				isMove = false;
			}
			//プレイヤーの座標が動いていたとき
			else
			{
				isMove = true;
				//プレイヤーのtransform保存
				pos = previousBitObj.transform.position;

			}
		}

		//プレイヤーが動いていて位置配列すべてに値が入っているとき
		if (isMove && check)
		{
			//isMove = false;
			//自分の位置を配列に入っている位置に
			//transform.position = playerPos[cnt].position;
			transform.position = previousBitPos[cnt];

			//自分の位置を移動したのでその位置を今のプレイヤーのいる位置で更新
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;


			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
			}
		}
	}
}
