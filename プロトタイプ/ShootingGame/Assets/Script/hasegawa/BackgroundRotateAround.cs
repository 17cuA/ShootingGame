/*
 20190818 作成
 author hasegawa yuuta
*/
/* 背景を回転させる */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotateAround : MonoBehaviour
{
	void Start()
	{
		//transform.parent = transform.parent.parent;
	}
	void Update()
	{
		transform.RotateAround(Vector3.forward + Vector3.up * 0.2f, 0.025f * Time.deltaTime);
	}
}
