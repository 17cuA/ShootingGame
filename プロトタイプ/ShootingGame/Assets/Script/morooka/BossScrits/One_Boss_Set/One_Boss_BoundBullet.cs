//作成日2019/07/08
// バウンドする１面のボスの弾
// 作成者:諸岡勇樹
/*
 * 2019/07/17 バウンド処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss_BoundBullet : bullet_status
{
	[SerializeField, Tooltip("バウンド回数")] private int bound_count;
	[SerializeField, Tooltip("レイの長さ")] private float length_on_landing;

	private RaycastHit hit_mesh;
	private Vector3 Ray_Direction { get; set; }

	private new void Start()
    {
		base.Start();
		Ray_Direction = transform.right;
    }

    // Update is called once per frame
    private new void Update()
    {
		base.Update();
		transform.position -= transform.right * shot_speed;

		if (Physics.Raycast(transform.position, Ray_Direction, out hit_mesh, length_on_landing))
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.transform.gameObject.tag == "Wall_Length")
			{
				transform.right = transform.right + hit_mesh.normal;
			}
		}
	}
}
