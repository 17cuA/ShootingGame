//作成日2019/08/30
// 一面のボス本番_2匹目
// 作成者:諸岡勇樹
/*
 * 2019/08/30　オプションコア格納
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Boss : character_status
{

	[Header("ボス形成パーツ")]
	[SerializeField, Tooltip("コア")] private Two_Boss_Parts[] core;
	[SerializeField, Tooltip("オプション")] private Two_Boss_Parts[] multiple;

	[Header("アニメーション用")]
	[SerializeField, Tooltip("g")] private int k;

	// Start is called before the first frame update
	private new void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	private new void Update()
	{
	}
}
