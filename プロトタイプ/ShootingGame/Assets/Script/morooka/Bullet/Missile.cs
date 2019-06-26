//作成日2019/06/19
// プレイヤーの使う、ミサイルの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/19 落下と地面衝突時の移動向き変更
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	[SerializeField]
	[Header("二次関数の傾き")]
	private float slope;
	[SerializeField]
	[Header("等速直線運動のスピード")]
	private float ConstantVelocityLineSpeed;     // 等速直線速度

	private Vector3 Vertex { get; set; }
	private int Act_Step { get; set; }
	private int Running_Flame { get; set; }

	RaycastHit hit;
	float ray_length = 0.3f;

	private new void Start()
	{
		base.Start();
		Vertex = transform.position;
		FacingChange(new Vector3(1.0f, 0.0f, 0.0f));
	}

	private new void Update()
	{
		base.Update();

		if (Act_Step == 0)
		{
			Vertex = transform.position;
			Act_Step++;
		}
		if (Act_Step == 1)
		{
			HorizontalProjection();
		}
		else if (Act_Step == 2)
		{
			transform.position += transform.right.normalized * shot_speed;
			if (Physics.Raycast(transform.position, transform.up * -1.0f, out hit, ray_length * 3.0f))
			{
				if (hit.normal.y != 1.0f)
				{
					Moving_Facing_Change();
				}
			}
		}

		if (Physics.Raycast(transform.position, transform.right, out hit, ray_length))
		{
			if (hit.transform.gameObject.tag == "Wall")
			{
				Moving_Facing_Change();
				Act_Step = 2;
				Debug.Log("hei");
			}
		}
	}

	void OnEnable()
	{
		Running_Flame = 0;
		Act_Step = 0;
	}

	/// <summary>
	/// 移動向き変更
	/// </summary>
	private void Moving_Facing_Change()
	{
		transform.right = new Vector2(hit.normal.y, -hit.normal.x);
		float an = transform.right.x * 0.0f + transform.right.y * 1.0f;

		if (an > 0)
		{
			AddExplosionProcess();
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 水平投射
	/// </summary>
	private void HorizontalProjection()
	{
		Vector3 vector = new Vector3(ConstantVelocityLineSpeed, -1.0f * (Running_Flame * shot_speed));
		transform.right = vector;
		transform.position += vector.normalized * shot_speed;
		Running_Flame++;
	}
	//private new void OnTriggerEnter(Collider col)
	//{
	//	if (col.gameObject.tag == "Wall")
	//	{
	//		RaycastHit hit;
	//		Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity);

	//		if((hit.normal.z - transform.right.z) < 90.0f)
	//		{
	//			print("OKK");
	//		}

	//		if (Act_Step == 1)
	//		{
	//			Act_Step++;
	//		}
	//		else if(Act_Step == 2)
	//		{
	//			gameObject.SetActive(false);

	//			//add:0513_takada 爆発エフェクトのテスト
	//			base.AddExplosionProcess();
	//		}
	//	}
	//}
}
