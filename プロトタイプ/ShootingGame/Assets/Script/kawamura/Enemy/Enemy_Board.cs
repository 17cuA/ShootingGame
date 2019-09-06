﻿//作成者：川村良太
//バキュラの3Dオブジェクトにつける
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Board : character_status
{
	public int saveHp;
	GameObject parentObj;
	public Enemy_Board_Parent ebp;

	private void Awake()
	{
		parentObj = transform.parent.parent.gameObject;
		ebp = parentObj.GetComponent<Enemy_Board_Parent>();
	}
	//private void OnEnable()
	//{
	//	hp = 10;
	//}
	new void Start()
    {
		saveHp = hp;
		HP_Setting();
		base.Start();
    }

    new void Update()
    {
		if (hp < saveHp)
		{
            saveHp = hp;
            ebp.damageDelay = 0;
            ebp.isDamage = true;
			ebp.speedX -= 0.4f;
            if (ebp.speedX < ebp.speedX_Min)
            {
                ebp.speedX = ebp.speedX_Min;
            }
		}

		if (hp < 1)
		{
			ebp.isDead = true;
			Died_Process();
		}
		base.Update();
    }

    private void OnTriggerExit(Collider col)
    {
		if (col.gameObject.name == "WallLeft")
		{
			ebp.isDisappearance = true;
		}
		else if (col.gameObject.name == "smasher_left" || col.gameObject.name == "smasher_right")
		{
			hp = 0;
		}
    }

    void Enemy_Reset()
	{

	}
}
