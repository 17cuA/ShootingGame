//作成日2019/06/19
// プレイヤーの使う、ミサイルの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/19 落下と地面衝突時の移動向き変更
 * 2019/06/21 上り坂に衝突時自身の破壊
 * 2019/06/24 EAGLEWINDに対応
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	[SerializeField]
	[Header("等速直線運動のスピード")]
	private float constant_velocity_line_speed;     // 等速直線速度
	[SerializeField]
	[Header("等速直線運動のスピード")]
	private float ray_length = 0.3f;                        // 等速直線運動のスピード
	private RaycastHit hit_mesh;							// 衝突したオブジェクトのメッシュ(コライダーの一部)の情報

	private int Act_Step { get; set; }							// 行動の変更用
	private int Running_Flame { get; set; }				// 起動している間のフレーム
	public float Y_Axis_Facing { get; private set; }		// Y軸の方向

	private new void Start()
	{
		base.Start();
		FacingChange(new Vector3(1.0f, 0.0f, 0.0f));
		hit_mesh = new RaycastHit();
	}

	private new void Update()
	{
		base.Update();

		// 障害物に当たる前の行動
		if (Act_Step == 0)
		{
			HorizontalProjection();
		}
		// 障害物に当たった後の行動
		else if (Act_Step == 2)
		{
			// 自身の向いている方向に移動
			transform.position += transform.right.normalized * shot_speed;
			//	自身の下方向にコライダーがあるとき
			if (Physics.Raycast(transform.position, transform.up * -1.0f, out hit_mesh, ray_length * 3.0f))
			{
				// コライダーの持ち主がWAllのとき、法線が上向きでないとき
				if (hit_mesh.transform.tag=="Wall" && hit_mesh.normal.y != 1.0f)
				{
					Moving_Facing_Confirmation();
				}
			}
		}
		// 先端に触れたメッシュ(コライダーの一部)があるとき
		if (Physics.Raycast(transform.position, transform.right, out hit_mesh, ray_length))
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.transform.gameObject.tag == "Wall")
			{
				Moving_Facing_Confirmation();
				// 移動の種類を変える
				Act_Step = 2;
			}
		}
	}

	void OnEnable()
	{
		Running_Flame = 0;
		Act_Step = 0;
	}

	/// <summary>
	/// 移動向き確認
	/// </summary>
	private void Moving_Facing_Confirmation()
	{
		transform.right = new Vector2(hit_mesh.normal.y, -hit_mesh.normal.x);
		float an = transform.right.x * 0.0f + transform.right.y * 1.0f;

		// 内閣が0以下のとき
		if (an > 0)
		{
			// 自信を消す
			AddExplosionProcess();
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 水平投射
	/// </summary>
	private void HorizontalProjection()
	{
		Vector3 vector = new Vector3(constant_velocity_line_speed, Y_Axis_Facing * (Running_Flame * shot_speed));
		transform.right = vector;
		transform.position += vector.normalized * shot_speed;
		Running_Flame++;
	}

	/// <summary>
	/// Y軸方向の設定
	/// </summary>
	/// <param name="select_number"> 偶数を渡すと上方向に移動、奇数を渡すと下方向に移動 </param>
	public void Y_Axis_Orientation_Preference(int select_number)
	{
		if (select_number % 2 == 0)
		{
			Y_Axis_Facing = 1.0f;
		}
		else if (select_number % 2 == 1)
		{
			Y_Axis_Facing = -1.0f;
		}
	}
}
