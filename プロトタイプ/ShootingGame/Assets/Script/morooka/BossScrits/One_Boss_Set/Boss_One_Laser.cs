//作成日2019/08/05
// 一面のボスのレーザー
// 作成者:諸岡勇樹
/*
 * 2019/07/30　レーザーの挙動
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_Laser : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数

	 void Update()
	{
		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
			|| transform.position.y >= 10.5f || transform.position.y <= -10.5f)
		{
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

	public void Manual_Start(Transform parent)
	{
		transform.parent = parent;
		transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);
		//Trail.enabled = true;
	}

	protected void OnTriggerEnter(Collider col)
	{
		if ((gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player"))
		{
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
		}
	}
}
