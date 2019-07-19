﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wave_Direction : MonoBehaviour
{
	public GameObject parentObj;
	public float zzzzz;
	public float rotaY;

	private void Awake()
	{
		parentObj = transform.parent.gameObject;
		rotaY = (parentObj.transform.position.z * 4.5f) - 90.0f;
	}

	private void OnEnable()
	{
		rotaY = (parentObj.transform.position.z * 4.5f) - 90.0f;

	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		zzzzz = parentObj.transform.position.z;
		rotaY = (parentObj.transform.position.z * -4.5f) - 90.0f;
		//transform.rotation = new Quaternion(0, rotaY, 0, 0);
		transform.rotation = Quaternion.Euler(0, rotaY, 0);
	}
}
