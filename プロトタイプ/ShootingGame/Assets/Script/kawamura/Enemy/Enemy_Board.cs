using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Board : character_status
{
	GameObject parentObj;
	Enemy_Board_Parent ebp;

	private void Awake()
	{
		parentObj = transform.parent.gameObject;
		ebp = parentObj.GetComponent<Enemy_Board_Parent>();
	}
	private void OnEnable()
	{
		Reset_Status();
	}
	new void Start()
    {
		HP_Setting();
		base.Start();
    }

    new void Update()
    {
		if (hp < 1)
		{
			ebp.isDead = true;
			Died_Process();
		}
    }

	void Enemy_Reset()
	{

	}
}
