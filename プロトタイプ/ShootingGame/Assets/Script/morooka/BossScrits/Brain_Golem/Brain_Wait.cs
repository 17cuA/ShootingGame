﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Wait : character_status
{
	[SerializeField, Tooltip("ダメージ受けるパーツ")] private List<Brain_Parts> damagedParts;
	[SerializeField, Tooltip("ダメージ受けないパーツ")] private List<Brain_Parts> notTakeDamageParts;
	WaitLoopTrigger waitLoopTrigger = null;

	void Start()
    {
		waitLoopTrigger = FindObjectOfType<WaitLoopTrigger>();
    }

    void Update()
    {
        if(Is_PartsNotAlive())
		{
			waitLoopTrigger.Trigger = true;
		}
    }

	private bool Is_PartsNotAlive()
	{
		// パーツがすべて死んでいたとき
		if(damagedParts.Count == 0)
		{
			// 死亡判定
			return true;
		}

		// 生存パーツリストの確認
		foreach(var parts in damagedParts)
		{
			if(parts.hp < 0)
			{
				damagedParts.Remove(parts);
			}
		}

		return false;
	}
}
