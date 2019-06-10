using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPlayer_SameMotion : MonoBehaviour
{
	public GameObject playerObj;

	public Vector3[] playerPos;
	public Vector3 pos;
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

	public int cnt;
	int array_Num;

	bool once = true;
	public bool check = false;
	public bool isMove = false;
	void Start()
	{
		int cnt = 0;
		array_Num = 8;
		playerPos = new Vector3[array_Num];
	}

	void Update()
	{
		Direction = transform.rotation;

		//プレイヤー格納がnullなら入れる
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
			//配列にとりあえず追従位置を入れる
			//for(int i = 0;i<60;i++)
			//{
			//	playerPos[i] = playerObj.transform;
			//}
			//isMove = true;
			//playerPos[cnt] = playerObj.transform;
			once = true;
			pos = playerObj.transform.position;
		}

		//前のプレイヤーの座標と今のプレイヤーの座標が違うとき　かつ　位置配列すべてに値が入っていないとき
		if (pos != playerObj.transform.position && !check)
		{
			//位置配列にプレイヤーの位置を入れる
			//playerPos[cnt] = playerObj.transform;
			playerPos[cnt] = playerObj.transform.position;

			cnt++;
			if (cnt > array_Num-1)
			{
				cnt = 0;
			}
		}
		if (playerPos[array_Num-1] != null)
		{
			check = true;
		}

		if (once)
		{
			//プレイヤーの座標が動いていないとき
			if (pos == playerObj.transform.position)
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

		//プレイヤーが動いていて位置配列すべてに値が入っているとき
		if (isMove && check)
		{
			//isMove = false;
			//自分の位置を配列に入っている位置に
			//transform.position = playerPos[cnt].position;
			transform.position = playerPos[cnt];

			//自分の位置を移動したのでその位置を今のプレイヤーのいる位置で更新
			//playerPos[cnt] = playerObj.transform;
			playerPos[cnt] = playerObj.transform.position;


			cnt++;
			if(cnt>array_Num-1)
			{
				cnt = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire1"))
		{
			//方向転換させる関数の呼び出し
			Change_In_Direction();

		}

	}
	//ビットンの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1, 0, 0);
		transform.rotation = Direction;
	}

}
