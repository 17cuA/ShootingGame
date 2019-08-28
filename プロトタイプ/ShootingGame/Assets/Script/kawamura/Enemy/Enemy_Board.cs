//作成者：川村良太
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
			ebp.speedX -= 0.4f;
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
    }

    void Enemy_Reset()
	{

	}
}
