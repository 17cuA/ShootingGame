//ビットの攻撃スクリプト
//プレイヤーのShot_DelayMaxを参照してプレイヤーの2発おきに攻撃する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Bit_Shot : MonoBehaviour
{
	GameObject playerObj;
	Player1 pl1;
	Bit_Formation_3 bf;
	//public GameObject shot_Mazle;

	float shot_Delay;

	public bool isShot = true;
	void Start()
	{
		bf = gameObject.GetComponent<Bit_Formation_3>();

		playerObj = GameObject.FindGameObjectWithTag("Player");
		pl1 = playerObj.GetComponent<Player1>();
	}

	void Update()
	{
		if(!bf.isDead)
		{
			if (isShot)
			{
				shot_Delay++;
				if (shot_Delay > pl1.Shot_DelayMax * 2)
				{
					Bullet_Create();
				}
			}
		}

	}
	public void Bullet_Create()
	{
		if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			Single_Fire();
			shot_Delay = 0;
		}
	}

	private void Single_Fire()
	{
		GameObject Bullet = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
		pl1.transform.rotation = pl1.Direction;
		Bullet.transform.position =gameObject.transform.position;
	}
}
