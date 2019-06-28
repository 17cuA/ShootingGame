﻿/*
 * 直線に進んでくる敵キャラ
 * 久保田達己		
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_straight : character_status
{
    // Update is called once per frame
    void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}
		Enemy_Move();
	}

	private void Enemy_Move()
	{
		transform.position += new Vector3(-0.1f, 0, 0) * Time.deltaTime * speed;
	}
}
