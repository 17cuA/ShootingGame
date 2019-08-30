//作成者：川村良太
//プレイヤーと同じ動きで追従するオプションの位置用オブジェクト（1つ目のオプション）のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPlayer_SameMotion : MonoBehaviour
{
	public GameObject playerObj;
	Player1 pl1;
	Player2 pl2;
	public GameObject parentObj;
	public string parentName;

	public Vector3[] playerPos;
	public Vector3 pos;				//プレイヤーの座標を保存して動いているかを確かめる変数
    Vector3 savePos;                //フリーズ開始時の座標を入れる
    Vector3 defPos;                 //フリーズ解除時、開始時と今の座標との差を入れる

	public int cnt;
	int array_Num;
	public int childCnt;

	Vector3 freesePos;

	bool defCheck = true;
	public bool check = false;
	public bool isMove = false;
	public bool hasOption = false;
	bool isFreeze = false;
	public bool isFollow1P;
	public bool isFollow2P;

	void Start()
	{
		parentObj = transform.parent.gameObject;
		parentName = parentObj.name;
		if (parentName == "Four_FollowPos_1P")
		{
			isFollow1P = true;
		}
		else if (parentName == "Four_FollowPos_2P")
		{
			isFollow2P = true;
		}

		//int cnt = 0;
		array_Num = 9;
		playerPos = new Vector3[array_Num];
	}

	void Update()
	{
		childCnt = this.transform.childCount;

		//プレイヤー格納がnullなら入れる
		if (playerObj == null)
		{
			if (isFollow1P)
			{
				//プレイヤーがいたら入れる
				if (GameObject.Find("Player"))
				{
					playerObj = GameObject.Find("Player");
					pl1 = playerObj.GetComponent<Player1>();
					//配列にとりあえず追従位置を入れる
					for (int i = 0; i < array_Num; i++)
					{
						playerPos[i] = playerObj.transform.position;
					}
					//isMove = true;
					//playerPos[cnt] = playerObj.transform;
					transform.position = playerObj.transform.position;
					defCheck = true;
					pos = playerObj.transform.position;
				}
			}
			else if (isFollow2P)
			{
				//プレイヤーがいたら入れる
				if (GameObject.Find("Player_2"))
				{
					playerObj = GameObject.Find("Player_2");
					pl2 = playerObj.GetComponent<Player2>();
					//配列にとりあえず追従位置を入れる
					for (int i = 0; i < array_Num; i++)
					{
						playerPos[i] = playerObj.transform.position;
					}
					//isMove = true;
					//playerPos[cnt] = playerObj.transform;
					transform.position = playerObj.transform.position;
					defCheck = true;
					pos = playerObj.transform.position;
				}

			}
		}

		if (isFollow1P)
		{
			if (Input.GetButtonUp("Bit_Freeze") || Input.GetKeyUp(KeyCode.Y))
			{
				isFreeze = false;
				defPos = transform.position - savePos;

				for (int i = 0; i < array_Num; i++)
				{
					playerPos[i] += defPos;
				}
				defPos = new Vector3(0, 0, 0);
				savePos = transform.position;

			}
			else if (Input.GetButton("Bit_Freeze") || Input.GetKey(KeyCode.Y))
			{
				isFreeze = true;
			}
		}
		else if (isFollow2P)
		{
			if (Input.GetButtonUp("P2_Bit_Freeze") || Input.GetKeyUp(KeyCode.Y))
			{
				isFreeze = false;
				defPos = transform.position - savePos;

				for (int i = 0; i < array_Num; i++)
				{
					playerPos[i] += defPos;
				}
				defPos = new Vector3(0, 0, 0);
				savePos = transform.position;

			}
			else if (Input.GetButton("P2_Bit_Freeze") || Input.GetKey(KeyCode.Y))
			{
				isFreeze = true;
			}

		}

		if (!isFreeze)
		{
			if (defCheck)
			{
				if (isFollow1P)
				{
					//プレイヤーの座標が動いていないとき
					//if (pos == playerObj.transform.position)
					if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
					{
						isMove = false;
					}
					//プレイヤーの座標が動いていたとき
					else
					{
						isMove = true;
						//プレイヤーのtransform保存
						pos = playerObj.transform.position;
					}
				}
				else if (isFollow2P)
				{
					//プレイヤーの座標が動いていないとき
					//if (pos == playerObj.transform.position)
					if ((Input.GetAxis("P2_Horizontal") == 0) && (Input.GetAxis("P2_Vertical") == 0))
					{
						isMove = false;
					}
					//プレイヤーの座標が動いていたとき
					else
					{
						isMove = true;
						//プレイヤーのtransform保存
						pos = playerObj.transform.position;
					}
				}
			}

			//プレイヤーが動いていて位置配列すべてに値が入っているとき
			if (isMove)
			{
				//isMove = false;
				//自分の位置を配列に入っている位置に
				//transform.position = playerPos[cnt].position;
				transform.position = playerPos[cnt];

				//自分の位置を移動したのでその位置を今のプレイヤーのいる位置で更新
				//playerPos[cnt] = playerObj.transform;
				playerPos[cnt] = playerObj.transform.position;

				cnt++;
				if (cnt > array_Num - 1)
				{
					cnt = 0;
				}
                savePos = transform.position;
			}
		}
		//else if(isFreeze)
		//{
		//	if(pos==playerObj.transform.position)
		//	{
		//		isMove = false;
		//	}
		//	//プレイヤーの座標が動いていたとき
		//	else
		//	{
		//		isMove = true;
		//		//プレイヤーのtransform保存
		//		pos = playerObj.transform.position;

		//	}

		//	if(isMove)
		//	{
		//		freesePos = playerObj.transform.position - savePos;

		//		this.transform.position += freesePos;
		//		savePos = transform.position;
		//	}
		//}
	}
}
