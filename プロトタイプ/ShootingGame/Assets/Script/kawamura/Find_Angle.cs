//作成者：川村良太
//対象の方を向くスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Find_Angle : MonoBehaviour
{
	public GameObject playerObj;   //プレイヤー（向く対象）情報を入れる変数
	Vector3 playerPos;      //プレイヤー（向く対象）の座標を入れる変数
	Vector3 midBossPos;     //自分の座標を入れる変数
	Vector3 dif;            //対象と自分の座標の差を入れる変数

	public bool imasuyo = false;
	public bool isPlayerActive=true;
	float radian;           //ラジアン
	public float degree;    //角度

	//float midBossDig;	

	void Start()
	{

	}

	void Update()
	{
		//プレイヤー（向く対象）情報が入っていないなら入れる
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
		
		if(isPlayerActive)
		{
			if (playerObj.activeInHierarchy)
			{
				//imasuyo = true;
			}
			else
			{
				isPlayerActive = false;
			}

		}
		//プレイヤー（向く対象）の座標を入れる
		if (playerObj.activeInHierarchy)
		{
			playerPos = playerObj.transform.position;
		}
		//自分の座標を入れる
		midBossPos = this.transform.position;

		//角度計算の関数呼び出し
		DegreeCalculation();

		//if(degree>0)
		//{
		//	midBossDig = degree;
		//}
		//else if(degree<=0)
		//{
		//	midBossDig = degree;
		//}

		//自分を対象の方向へ向かせる
		this.transform.rotation = Quaternion.Euler(0, 0, degree);

	}

	//角度を求める関数
	void DegreeCalculation()
	{
		//座標の差を入れる
		dif = playerPos - midBossPos;

		//ラジアンを求める
		radian = Mathf.Atan2(dif.y, dif.x);

		//角度を求める
		degree = radian * Mathf.Rad2Deg;
	}
}
