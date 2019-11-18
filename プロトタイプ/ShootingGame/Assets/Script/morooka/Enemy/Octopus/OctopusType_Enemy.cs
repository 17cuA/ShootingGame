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
	[Header("コライダー関係")]
	[SerializeField,Tooltip ("前の判定")] private Parts_Collider flomtCollieder;
	[SerializeField,Tooltip ("下のコライダー")] private Parts_Collider downCollieder;

	[Header("移動関係")]
	[SerializeField, Tooltip("ジャンプ力")] private float jumpPower;
	[SerializeField, Tooltip("横移動量")] private float amountMovement;
	[SerializeField, Tooltip("落下速度")] private float fallSpeed;
	[SerializeField, Tooltip("落下向き")] private Vector3 fallingDirection;

	private Rigidbody rigidbody;
	private float horizontalMovementDirection;

	void Start()
    {
		rigidbody = GetComponent<Rigidbody>();
		horizontalMovementDirection = Mathf.Sign(flomtCollieder.transform.eulerAngles.z) * -1.0f;
	}

    void Update()
    {
		// 左右当たり
		if(flomtCollieder.Is_HitRayCast)
		{
			// 移動向き変更
			horizontalMovementDirection *= -1.0f;

			// 当たり判定の向き変更
			Vector3 temp_1 = flomtCollieder.gameObject.transform.eulerAngles;
			temp_1.z += 180.0f;
			flomtCollieder.gameObject.transform.eulerAngles = temp_1;
		}

		// 底面当たり
		if (downCollieder.Is_HitRayCast)
		{
			// 底面の向きに合わせて飛び跳ね
			transform.up = downCollieder.HitObject.normal;
			rigidbody.velocity = downCollieder.HitObject.normal * jumpPower;

			// オブジェクトの向きを合わせる
			fallingDirection = downCollieder.HitObject.normal * -1.0f;
		}

		// 底面方向に落下
		rigidbody.velocity += fallingDirection * 0.1f;

		// 横に移動
		Vector3 temp = transform.position;
		temp.x += horizontalMovementDirection * amountMovement;
		transform.position = temp;
	}
}
