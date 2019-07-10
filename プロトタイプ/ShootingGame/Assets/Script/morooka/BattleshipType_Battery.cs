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
	private Vector3 Initial_Position { get; set; }

	private void Start()
	{
		hp = 5;
		score = 100;

		gameObject.tag = transform.parent.tag;
		Initial_Position = transform.localPosition;
	}

	private void Update()
	{
		//if(hp<=0)
		//{
		//	//Died_Process();
		//	ParticleCreation(0);
		//	Reset_Status();
		//	//死んだらゲームオブジェクトを遠くに飛ばす処理
		//	transform.position = new Vector3(0, 800.0f, 0);
		//	//稼働しないようにする
		//	//Debug.Log(gameObject.transform.parent.name + "	Destroy");
		//	gameObject.SetActive(false);
		//}
	}

	private void ReBoot()
	{
		hp = 5;
		transform.localPosition = Initial_Position;
	}
}
