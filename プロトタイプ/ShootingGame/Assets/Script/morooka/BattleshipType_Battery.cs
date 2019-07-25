//作成日2019/07/08
// 戦艦型エネミーの大砲(パーツ)の挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　必要情報の格納
 * 2019/07/10　HPが0以下のときの挙動
 * 2019/07/25　加減速の挙動
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleshipType_Battery : character_status
{
	private Vector3 Initial_Position { get; set; }		// 自身の初期位置
	private GameObject Muzzle { get; set; }				// 弾が発射される位置

	private new void Start()
	{
		hp = 5;
		score = 100;

		gameObject.tag = transform.parent.tag;
		Initial_Position = transform.localPosition;

		//if(Muzzle == null)
		//{
		//	Muzzle = transform.GetChild(0).gameObject;
		//}
		base.Start();
	}

	private new void Update()
	{
		// HPが0以下のとき
		if (hp <= 0)
		{
			Died_Process();

			//// character_status の Died_Process()を一部を変更しての処理
			//// 爆発のパーティクルをマズルのポジションに発生させる
			//ParticleCreation(0).transform.position = Muzzle.transform.position;
			//Reset_Status();
			////死んだらゲームオブジェクトを遠くに飛ばす処理
			//transform.position = new Vector3(0, 800.0f, 0);
			//gameObject.SetActive(false);

			//// character_status の Died_Process()を一部を変更しての処理
			//// 爆発のパーティクルをマズルのポジションに発生させる
			//ParticleCreation(0).transform.position = transform.position;
			//Reset_Status();
			////死んだらゲームオブジェクトを遠くに飛ばす処理
			//transform.position = new Vector3(0, 800.0f, 0);
			//gameObject.SetActive(false);
		}
		base.Update();
	}

	/// <summary>
	/// 再起動時の処理
	/// </summary>
	public void ReBoot()
	{
		hp = 5;
		transform.localPosition = Initial_Position;
	}
}
