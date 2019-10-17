//作成日2019/10/17
// 一面のボスのレーザーのコライダー判定の処理
// 作成者:諸岡勇樹
/*
 * 2019/10/17：　一面のボスのレーザーのコライダー判定の処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Collider : MonoBehaviour
{
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
