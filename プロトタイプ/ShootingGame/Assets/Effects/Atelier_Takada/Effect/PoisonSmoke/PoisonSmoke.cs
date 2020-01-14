using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{
	private GameObject parentObject;	//親(発射台)のオブジェクト
	private float maxDistance = 100f;	//親との最大距離

    void Update()
    {
		//一定以上離れたら非表示
		float distance = Vector3.Distance(transform.position,parentObject.transform.position);
		if(maxDistance < distance)
		{
			gameObject.SetActive(false);
		}
	}

	//親オブジェクトの保存
	public void SetParentData(GameObject parent,float distance)
	{
		parentObject = parent;
		maxDistance = distance;
	}
}
