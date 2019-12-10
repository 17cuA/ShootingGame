//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/11/29
//----------------------------------------------------------------------------------------------
// 開閉するシャッター
//----------------------------------------------------------------------------------------------
// 2019/11/29　シャッターの保存と削除
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutte_Move : character_status
{
	enum MODE
	{
		eOPEN	= 1,			// 開く
		eCLOSE	= -1,			// 閉じる
	}

	[Header("")]
	[SerializeField, Tooltip("箱の大きさ")] private Vector3 boxSize;
	[SerializeField, Tooltip("レイ長さ")] private float rayLength;
	[SerializeField, Tooltip("移動位置")] private Vector3 target;
	[SerializeField, Tooltip("移動するオブジェクト")] private GameObject moveObject;
	[SerializeField, Tooltip("シャッターの初期状態モード")] private MODE mode;

	private bool Is_Move;
	private RaycastHit hit;

	new private void Start()
	{
		switch(mode)
		{
			case MODE.eOPEN:
				moveObject.transform.localPosition = target;
				target = Vector3.zero;
				break;
			case MODE.eCLOSE:
				break;
		}
	}

	new private void Update()
	{
		if (Physics.BoxCast(transform.position, boxSize, (float)mode * transform.right, out hit, Quaternion.identity, rayLength))
		{
			if (hit.transform.tag != "Wall")
			{
				Is_Move = true;
			}
		}

		// 移動可能のとき
		if (Is_Move)
		{
			moveObject.transform.localPosition = Vector3.Lerp(moveObject.transform.localPosition, target, speed);

			// 移動終了判定
			if (Vector3.Distance(moveObject.transform.localPosition, target) <= 0.001f)
			{
				if (mode == MODE.eOPEN)
				{
					enabled = false;
				}
				else if (mode == MODE.eCLOSE)
				{
					hp = 0;
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		//　Cubeのレイを疑似的に視覚化
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position - (float)mode * transform.right * rayLength, boxSize);
	}
}
