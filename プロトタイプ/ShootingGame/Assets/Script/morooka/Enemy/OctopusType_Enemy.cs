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
	[SerializeField,Tooltip ("横のコライダー")] private Parts_Collider sideCollieder;
	[SerializeField,Tooltip ("下のコライダー")] private Parts_Collider downCollieder;
	[SerializeField, Tooltip("ジャンプ力")] private float jumpPower;

	private Rigidbody rigidbody;

    void Start()
    {
		rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
		// 横当たり
		if(sideCollieder.Is_HitRayCast())
		{
			var temp = rigidbody.velocity;
			temp.x *= -1.0f;
			rigidbody.velocity = temp;
		}

		if(downCollieder.Is_HitRayCast())
		{
			//rigidbody transform
		}
    }
}
