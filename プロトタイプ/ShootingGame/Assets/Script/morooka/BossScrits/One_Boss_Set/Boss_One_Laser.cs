//作成日2019/08/05
// 一面のボスのレーザー
// 作成者:諸岡勇樹
/*
 * 2019/07/30　レーザーの挙動
 * 2019/09/07　フレームのレーザー状態の追加
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_Laser : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	public GameObject Laser_Appearance;		// レーザー時の見た目
	public GameObject Frame_Appearance;     // フレーム時の見た目

	 void Update()
	{
		// 画面外に出たとき
		if (transform.position.x >= 23.5f || transform.position.x <= -23.5f
			|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			// 非アクティブにする
			GameObject obj = gameObject;
			Obj_Storage.Storage_Data.One_Boss_Laser.Set_Parent_Obj(ref obj);
			gameObject.SetActive(false);

		}
	}

	private void LateUpdate()
	{
		Vector3 temp = transform.localPosition;
		temp.x += shot_speed;
		transform.localPosition = temp;
	}

	/// <summary>
	/// 手動初期設定
	/// </summary>
	/// <param name="parent"> レーザーの新しい親 </param>
	public void Manual_Start(Transform parent)
	{
		transform.parent = parent;
		transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);
	}

	protected void OnTriggerEnter(Collider col)
	{
		// プレイヤーに衝突したとき
		if (col.gameObject.tag == "Player")
		{
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
		}
	}
}
