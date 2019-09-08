﻿//作成日2019/07/08
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
	private GameObject mae { get; set; }
	private GameObject boss { get; set; }
	private Collider _Collider { get; set; }

	private new void Start()
	{
		base.Start();
		Ray_Direction = transform.right;
		boss = Obj_Storage.Storage_Data.GetBoss(1);
		_Collider = GetComponent<Collider>();
	}

	// Update is called once per frame
	private new void Update()
	{
		base.Update();
		Ray_Direction = transform.right;
		transform.position -= Ray_Direction.normalized * shot_speed;

		Debug.DrawRay(transform.position, -Ray_Direction.normalized * length_on_landing, Color.red);
		if (Physics.Raycast(transform.position, -Ray_Direction.normalized, out hit_mesh, length_on_landing)
			&& boss.activeSelf)
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.collider.gameObject.tag != "Player_Bullet" && hit_mesh.collider.gameObject.tag != "Enemy_Bullet"/*&& hit_mesh.collider.gameObject != mae*/)
			{
				mae = hit_mesh.transform.gameObject;
				Ray_Direction = ReflectionCalculation(Ray_Direction, hit_mesh.normal);
				transform.right = Ray_Direction;
				_Collider.isTrigger = false;
;			}
		}
	}

	private void OnEnable()
	{
		mae = null;
		if (_Collider != null)_Collider.isTrigger = true;
	}

	/// <summary>
	/// 反射の計算
	/// </summary>
	/// <param name="progressVector_F"> 進行方向のベクトル </param>
	/// <param name="normalVector_N"> 法線ベクトル </param>
	/// <returns></returns>
	private Vector2 ReflectionCalculation(Vector3 progressVector_F, Vector3 normalVector_N)
	{
		Vector2 vecocity = Vector2.zero;

		//　公式の利用
		vecocity = progressVector_F + (2 * Vector2.Dot(-progressVector_F, normalVector_N) * normalVector_N);

		return vecocity;
	}

	private new void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player_Bullet")
		{
			gameObject.SetActive(false);
			col.GetComponent<bullet_status>().Player_Bullet_Des();
			col.gameObject.SetActive(false);
		}
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player_Bullet")
		{
			Debug.Log("Player_Bullet");
			gameObject.SetActive(false);
			col.gameObject.GetComponent<bullet_status>().Player_Bullet_Des();
			col.gameObject.SetActive(false);
		}
	}
}