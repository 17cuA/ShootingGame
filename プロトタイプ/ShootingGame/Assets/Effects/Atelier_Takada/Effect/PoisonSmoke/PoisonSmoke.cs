using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : character_status
{
	private GameObject parentObject;	//親(発射台)のオブジェクト
	private float maxDistance = 100f;	//親との最大距離
	private Vector3 velocity;			//移動量
	private bool hitFlag;				//接触判定

	void Start()
	{
		hitFlag = false;
	}

	void Update()
	{
		//一定以上離れたら非表示
		float distance = Vector3.Distance(transform.position, parentObject.transform.position);
		if (maxDistance < distance)
		{
			gameObject.SetActive(false);
		}

		/*
		if (!hitFlag)
		{
			// Rigidbodyに力を加えて発射
			GetComponent<Rigidbody>().velocity = velocity;
		}
		*/
	}

	//何かのオブジェクトと当たったら
	void OnCollisionEnter(Collision collision)
	{
		if (!hitFlag)
		{
			hitFlag = true;
		}
	}

	//親オブジェクトのデータの保存
	public void SetParentData(GameObject _parent, float _distance, Vector3 _velocity)
	{
		parentObject = _parent;
		maxDistance = _distance;
		velocity = _velocity;

		// Rigidbodyに力を加えて発射
		GetComponent<Rigidbody>().velocity = velocity;
	}
}
