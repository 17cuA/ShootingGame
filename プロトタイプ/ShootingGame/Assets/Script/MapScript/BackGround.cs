﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	private Vector3 Reset_Position = new Vector3(38, 1, 1.63f);
	[Tooltip("画面スクロールするときの速度（基本0.1くらいが基準かも）")]
	public float speed;     //速度を入れる

	void Update()
	{
		transform.Translate(-speed, 0, 0);
		if (transform.position.x < -38)
		{
			transform.position = Reset_Position;
		}
	}
}
