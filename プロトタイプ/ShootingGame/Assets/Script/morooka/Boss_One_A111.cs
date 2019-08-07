﻿//作成日2019/08/06
// 一面のボスのレーザーためエフェクトのパーティクルアニメーションの管理
// 作成者:諸岡勇樹
/*
 * 2019/08/06　終わり確認
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_A111 : MonoBehaviour
{
	public Boss_One_A111_Individual[] Each_Particle;

	public bool Completion_Confirmation()
	{
		bool ok = true;
		foreach(Boss_One_A111_Individual system in Each_Particle)
		{
			ok = system.Completion;
		}

		return ok;
	}

	public void SetUp()
	{
		foreach(Boss_One_A111_Individual bo in Each_Particle)
		{
			bo.GetComponent<ParticleSystem>().Play();
		}
	}
}
