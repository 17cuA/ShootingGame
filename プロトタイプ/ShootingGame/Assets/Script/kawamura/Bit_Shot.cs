//ビットの攻撃スクリプト
//プレイヤーのShot_DelayMaxを参照してプレイヤーの2発おきに攻撃する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Shot : MonoBehaviour
{
	public GameObject Bullet;      //弾のPrefab情報

	GameObject playerObj;
	Player1 pl1;
	//public GameObject shot_Mazle;

	float shot_Delay;
	void Start()
	{
		Bullet = Resources.Load("Player_Bullet") as GameObject;

		playerObj = GameObject.FindGameObjectWithTag("Player");
		pl1 = playerObj.GetComponent<Player1>();
	}

	void Update()
	{
        
		if (shot_Delay > pl1.Shot_DelayMax * 2)
		{
			Bullet_Create();
		}
		shot_Delay++;

	}
	public void Bullet_Create()
	{
		if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space))
		{
			Single_Fire();
			shot_Delay = 0;
		}
	}

	private void Single_Fire()
	{
		Instantiate(Bullet, transform.position, transform.rotation);
	}
}
