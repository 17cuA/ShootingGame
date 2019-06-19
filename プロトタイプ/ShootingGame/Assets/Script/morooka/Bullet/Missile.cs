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
	private Vector3 pos;
	float a = -0.01f;
	int num = 0;
	int flame = 0;

    private new void Start()
    {
		base.Start();
		pos = transform.position;
		FacingChange(new Vector3(1.0f,0.0f,0.0f));
    }

	private new void Update()
    {
		base.Update();

		if(num == 0)
		{
			pos = transform.position;
			num++;
		}

		Vector3 vector = Vector3.zero;
		vector.x = flame;
		vector.y = (a * Mathf.Pow((int)(vector.x - pos.x), 2)) + pos.y;
		transform.position += vector.normalized * shot_speed;
		flame++;
    }

	void OnEnable()
	{
		flame = 0;
		num = 0;
	}
}
