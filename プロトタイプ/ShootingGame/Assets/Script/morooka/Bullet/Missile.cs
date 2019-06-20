//作成日2019/06/19
// プレイヤーの使う、ミサイルの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/19 落下と地面衝突時の移動向き変更
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	[SerializeField]
	[Header("二次関数の傾き")]
	private float slope;

	private Vector3 Vertex { get; set; }
	private int Act_Step { get; set; }
	private int Running_Flame { get; set; }

    private new void Start()
    {
		base.Start();
		Vertex = transform.position;
		FacingChange(new Vector3(1.0f,0.0f,0.0f));
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
			Vector3 vector = Vector3.zero;
			vector.x = Running_Flame;
			vector.y = (slope * Mathf.Pow((int)(vector.x - Vertex.x), 2)) + Vertex.y;
			transform.right = vector;
			transform.position += vector.normalized * shot_speed;
			Running_Flame++;

			RaycastHit hit;
			Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
			if (Physics.Raycast(transform.position, transform.position + transform.right, out hit, 3.0f))
			{
				if(hit.transform.gameObject.tag == "Wall")
				{
					transform.right = new Vector3(1.0f, 0.0f, 0.0f);
					Act_Step++;
				}
			}


		}
		else if(Act_Step == 2)
		{
			Vector3 vector = transform.position;
			vector.x += shot_speed;
			transform.position = vector;
		}
	}

	void OnEnable()
	{
		Running_Flame = 0;
		Act_Step = 0;
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
