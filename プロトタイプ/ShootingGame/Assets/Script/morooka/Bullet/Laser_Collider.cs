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
	/// <summary>
	///  ボスの種類
	/// </summary>
	public enum BOSS_TYPE
	{
		eBOSS_ONE,			// 一ボス
		eBOSS_TWO,			// 二ボス
	}

	public BOSS_TYPE bType;

	protected void OnTriggerEnter(Collider col)
	{
		switch (bType)
		{
			case BOSS_TYPE.eBOSS_ONE:
				// プレイヤーに衝突したとき
				if (col.gameObject.tag == "Player")
				{
					GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
					ParticleSystem particle = effect.GetComponent<ParticleSystem>();
					effect.transform.position = gameObject.transform.position;
					particle.Play();
				}
				break;
			case BOSS_TYPE.eBOSS_TWO:
				if (col.gameObject.tag == "Player")
				{
					GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
					ParticleSystem particle = effect.GetComponent<ParticleSystem>();
					effect.transform.position = gameObject.transform.position;
					particle.Play();
				}
				else if (col.gameObject != transform.parent && col.gameObject.layer != 14 && col.gameObject.tag != "Option")
				{
					var temp = transform.parent;
					temp.GetComponent<Two_Boss_Laser>().Scraps.Add(gameObject);
				}
				break;
			default:
				break;
		}
	}
}
