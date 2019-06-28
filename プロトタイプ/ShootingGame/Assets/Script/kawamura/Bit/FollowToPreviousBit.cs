using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPreviousBit : MonoBehaviour
{
	public GameObject playerObj;
	GameObject previousBitObj;

	public Vector3[] previousBitPos;
	public Vector3 pos;
    Vector3 savePos;                //フリーズ開始時の座標を入れる
    Vector3 defPos;                 //フリーズ解除時、開始時と今の座標との差を入れる

    public int cnt;
	int array_Num;
	public int childCnt;

	public string myName;

	bool one = true;
	bool once = true;
	public bool check = false;      //配列すべてに値が入っているかの判定
	public bool isMove = false;
	bool defCheck = false;
	bool isFreeze = false;
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

		defCheck = true;
		pos = previousBitObj.transform.position;

	}

	void Update()
	{
		childCnt = this.transform.childCount;

		if (Input.GetButtonUp("Bit_Freeze") || Input.GetKeyUp(KeyCode.Y))
		{
			isFreeze = false;
            defPos = transform.position - savePos;

            for (int i = 0; i < array_Num; i++)
            {
                previousBitPos[i] += defPos;
            }
		}
		else if (Input.GetButton("Bit_Freeze") || Input.GetKey(KeyCode.Y))
		{
			isFreeze = true;
		}

		//プレイヤー格納がnullなら入れる
		if (playerObj == null)
		{
			//プレイヤーがいたら入れる
			if (GameObject.FindGameObjectWithTag("Player"))
			{
				playerObj = GameObject.FindGameObjectWithTag("Player");
				//isMove = true;
				//playerPos[cnt] = playerObj.transform;
				transform.position = playerObj.transform.position;
				defCheck = true;
				//pos = playerObj.transform.position;
			}
		}
		//前のビットの座標と今のビットの座標が違うとき　かつ　位置配列すべてに値が入っていないとき
		if (pos != previousBitObj.transform.position && !check)
		{
			//位置配列にビットの位置を入れる
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;
			//if(one)
			//{
			//	transform.position=previousBitPos[0];
			//	one=false;
			//}
			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
				check = true;
			}
		}
		//配列の最後まで値が入っていたら
		//if (previousBitPos[array_Num - 1] != null)
		//{
		//	check = true;
		//}

		if (!isFreeze)
		{
			if (defCheck)
			{
				//前のビットの座標が動いていないとき
				if (pos == previousBitObj.transform.position)
				{
					isMove = false;
				}
				//前のビットの座標が動いていたとき
				else
				{
					isMove = true;
					//前のビットのtransform保存
					pos = previousBitObj.transform.position;

				}
			}

			//前のビットが動いていて位置配列すべてに値が入っているとき
			if (isMove && check)
			{
				//isMove = false;
				//自分の位置を配列に入っている位置に
				//transform.position = playerPos[cnt].position;
				transform.position = previousBitPos[cnt];

				//自分の位置を移動したのでその位置を今、前のビットのいる位置で更新
				//playerPos[cnt] = playerObj.transform;
				previousBitPos[cnt] = previousBitObj.transform.position;


				cnt++;
				if (cnt > array_Num - 1)
				{
					cnt = 0;
				}
                savePos = transform.position;
            }
		}
	}
}
