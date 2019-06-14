﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPlayer_SameMotion : MonoBehaviour
{
	public GameObject playerObj;

	public Vector3[] playerPos;
	public Vector3 pos;				//プレイヤーの座標を保存して動いているかを確かめる変数

	public int cnt;
	int array_Num;

	bool defCheck = true;
	public bool check = false;
	public bool isMove = false;
	bool isFreeze = false;

	void Start()
	{
		int cnt = 0;
		array_Num = 8;
		playerPos = new Vector3[array_Num];
	}

	void Update()
	{

		if(Input.GetButtonUp("Bit_Freeze"))
		{
			isFreeze = false;
		}
		else if (Input.GetButton("Bit_Freeze"))
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

		if (!isFreeze)
		{
			if (defCheck)
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
			}
		}
	}
}
