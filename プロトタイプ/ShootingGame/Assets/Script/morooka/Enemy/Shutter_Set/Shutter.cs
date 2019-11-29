using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : character_status
{
	[Header("")]
	[SerializeField, Tooltip("箱の大きさ")] Vector3 boxSize;
	[SerializeField, Tooltip("レイ長さ")] float rayLength;
	[SerializeField, Tooltip("移動位置")] Vector3 target;
	[SerializeField, Tooltip("移動するオブジェクト")] GameObject moveObject;

	private bool Is_Move;
	private RaycastHit hit;

	new private void Update()
	{
		if (Physics.BoxCast(transform.position, boxSize, -transform.right, out hit, Quaternion.identity, rayLength))
		{
			if (hit.transform.tag != "Wall")
			{
				Is_Move = true;
			}
		} 

		// 移動可能のとき
		if(Is_Move)
		{
			moveObject.transform.localPosition = Vector3.Lerp(moveObject.transform.localPosition, target, speed);
			if(Vector3.Distance( moveObject.transform.localPosition, target) <= 0.001f)
			{
				hp = 0;
			}
		}
	}

	void OnDrawGizmos()
	{
		//　Cubeのレイを疑似的に視覚化
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position - transform.right * rayLength, boxSize);
	}
}
