﻿//作成者：川村良太
//闘牛型の向きを変えるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wave_Direction : MonoBehaviour
{
	public GameObject parentObj;
	Enemy_Wave ew;
	public float zzzzz;
	public float rotaY;

	private void Awake()
	{
		parentObj = transform.parent.gameObject;
		ew = parentObj.GetComponent<Enemy_Wave>();
	}

	private void OnEnable()
	{
		//parentObj = transform.parent.gameObject;

		if (!ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * 4.5f) + 270.0f;

		}
		else if(ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * -4.5f) + 90.0f;

		}
	}
	void Start()
    {
		//parentObj = transform.parent.gameObject;
		if (!ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * 4.5f) + 270.0f;

		}
		else if (ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * -4.5f) + 90.0f;

		}
	}

	// Update is called once per frame
	void Update()
    {
		zzzzz = parentObj.transform.position.z;

		if (!ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * -4.5f) + 270.0f;

		}
		else if (ew.isBehind)
		{
			rotaY = (parentObj.transform.position.z * 4.5f) + 90.0f;

		}
		//transform.rotation = new Quaternion(0, rotaY, 0, 0);
		transform.rotation = Quaternion.Euler(0, rotaY, 0);
	}
}
