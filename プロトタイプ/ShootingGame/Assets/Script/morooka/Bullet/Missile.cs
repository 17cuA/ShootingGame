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
	Vector3 pos;
	float a = -1.9f;
	int num = 0;
	int flame = 0;

    private void Start()
    {
		base.Start();
		pos = transform.position;
		FacingChange(new Vector3(1.0f,0.0f,0.0f));
    }

	private void Update()
    {
		base.Update();
		Vector3 vector = transform.position;
		vector.y = a * ((flame * shot_speed) - pos.x) * (transform.position.x - pos.x) + pos.y;

		//transform.right = vector;
		transform.position = vector;

		Moving_To_Travelling_Direction();
    }

	void OnEnable()
	{
		pos = transform.position;
		flame = 0;
	}
}
