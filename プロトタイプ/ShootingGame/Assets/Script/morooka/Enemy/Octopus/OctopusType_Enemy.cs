//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/11/15
//----------------------------------------------------------------------------------------------
// タコ型エネミーの挙動
//----------------------------------------------------------------------------------------------
// 2019/11/15　移動の挙動
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusType_Enemy : MonoBehaviour
{
	// 移動の方向
	public enum DIRECTION
	{
		eUP　= 1,				// 上はね
		eDOWN = -1,			// 下はね
	}

	[Header("コライダー関係")]
	[SerializeField,Tooltip ("前の判定")] private Parts_Collider flomtCollieder;
	[SerializeField,Tooltip ("下のコライダー")] private Parts_Collider downCollieder;

	[Header("移動関係")]
	[SerializeField, Tooltip("ジャンプ力")] private float jumpPower;
	[SerializeField, Tooltip("横移動量")] private float amountMovement;
	[SerializeField, Tooltip("落下速度")] private float fallSpeed;
	[SerializeField, Tooltip("落下向き")] private DIRECTION bottomDirection;

	private Rigidbody rigidbody;
	private float horizontalMovementDirection;
	private Vector3 FallingDirection;
	void Start()
    {
		rigidbody = GetComponent<Rigidbody>();

		switch (bottomDirection)
		{
			case DIRECTION.eUP:
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
				break;
			case DIRECTION.eDOWN:
				transform.rotation = Quaternion.identity;
				break;
			default:
				break;
		}

		horizontalMovementDirection = Mathf.Sign(transform.right.x);

		FallingDirection.y = (float)bottomDirection;

		Vector3 temp = rigidbody.velocity;
		temp.x += horizontalMovementDirection * amountMovement;
		rigidbody.velocity = temp;
	}

    void Update()
    {
		// 左右当たり
		if(flomtCollieder.Is_HitRayCast)
		{
			// 移動向き変更
			horizontalMovementDirection *= -1.0f;

			Vector3 temp = rigidbody.velocity;
			temp.x += (horizontalMovementDirection * amountMovement) * 2.0f;
			rigidbody.velocity = temp;

			// 当たり判定の向き変更
			Vector3 temp_2 = flomtCollieder.gameObject.transform.eulerAngles;
			temp_2.z += 180.0f;
			flomtCollieder.gameObject.transform.eulerAngles = temp_2;
		}

		// 底面当たり
		if (downCollieder.Is_HitRayCast)
		{
			// 底面の向きに合わせて飛び跳ね
			transform.up = downCollieder.HitObject.normal;
			rigidbody.velocity = downCollieder.HitObject.normal * jumpPower;

			Vector3 temp = rigidbody.velocity;
			temp.x += horizontalMovementDirection * amountMovement;
			rigidbody.velocity = temp;

			// オブジェクトの向きを合わせる
			FallingDirection = downCollieder.HitObject.normal * -1.0f;
		}

		// 底面方向に落下
		rigidbody.velocity += FallingDirection * fallSpeed;
	}
}
