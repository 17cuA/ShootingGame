//作成者：川村良太
//バウンドする隕石の挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorBound : character_status
{

	new void Start()
	{
		base.Start();
	}

	new void Update()
	{

	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Enemy_MeteorBound_Model")
		{

		}
	}
}
