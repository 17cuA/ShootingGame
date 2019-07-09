//作成日2019/07/08
// 戦艦型エネミーの大砲の挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　必要情報の格納
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleshipType_Battery : character_status
{


	private void Start()
	{
		hp = 5;
		score = 100;

		gameObject.tag = transform.parent.tag;
	}
}
