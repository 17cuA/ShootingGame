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

public class Shutte_Move : MonoBehaviour
{
	enum MODE
	{
		eOPEN	= 1,			// 開いている
		eCLOSE	= -1,			// 閉じている
	}

	[Header("")]
	[SerializeField, Tooltip("箱の大きさ")] private Vector3 boxSize;
	[SerializeField, Tooltip("レイ長さ")] private float rayLength;
	[SerializeField, Tooltip("シャッターの現在の状態")] private MODE mode;

	[SerializeField]private bool Is_Move;
	private RaycastHit hit;

	private Vector3 target;
	private float speed;

	private Vector3 Open_TargetPos { get; set; }		// 開いているときのポジション
	private Vector3 Close_TargetPos { get; set; }		// 閉じているときのポジション

	new private void Start()
	{
		//----------------------------------------------------------------
		speed = 0.01f;
		target = new Vector3(0.0f, 0.0f, 7.1f);
		//----------------------------------------------------------------

		Open_TargetPos = target;
		Close_TargetPos = transform.localPosition;
	}

	new private void Update()
	{
		if (Physics.BoxCast(transform.position, boxSize, -(float)mode * transform.right, out hit, Quaternion.identity, rayLength))
		{
			if (hit.transform.tag != "Wall" && hit.transform.tag != "Enemy")
			{
				Is_Move = true;
			}
		}

		// 移動可能のとき
		if (Is_Move)
		{
			if(mode == MODE.eOPEN)
			{
				Vector3 temp = transform.localPosition;
				temp.z = Mathf.Lerp(transform.localPosition.z, Close_TargetPos.z, speed);
				transform.localPosition = temp;

				// 移動終了判定
				if (Mathf.Abs(transform.localPosition.z - Close_TargetPos.z) <= 0.001f)
				{
					enabled = false;
				}
			}
			else if (mode == MODE.eCLOSE)
			{
				Vector3 temp = transform.localPosition;
				temp.z = Mathf.Lerp(transform.localPosition.z, Open_TargetPos.z, speed);
				transform.localPosition = temp;

				// 移動終了判定
				if (Mathf.Abs(transform.localPosition.z - Open_TargetPos.z) <= 0.001f)
				{
					enabled = false;
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
