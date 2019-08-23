﻿//作成者：川村良太
//オプションの位置関係の処理のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Formation_3 : MonoBehaviour
{
	enum BitState
	{
		Circular,       //初期位置（円運動）
		Follow,         //追従状態
		Oblique,        //斜め撃ち状態
		Laser,			//レーザー状態
		Stay,           //停止状態
		Return,         //戻ってきている状態
	}

	[SerializeField]
	BitState bState;							//オプションの状態

	//[SerializeField]
	//BitState previous_state;					//オプションの前の状態（レーザーを解除したときに使う）

	public GameObject playerObj;				//プレイヤーのオブジェクト
	//public GameObject parentObj;				//親のオブジェクト
	public GameObject followPosObj;				//プレイヤーを追従するときの位置オブジェクト
	public GameObject followPosFirstObj;		//プレイヤーに一番近い追従位置オブジェクト
	public GameObject followPosSecondObj;		//二番目
	public GameObject followPosThirdObj;		//三番目
	public GameObject followPosFourthObj;		//四番目
	//GameObject obliquePosObj;					//斜めうち状態の座標用オブジェクト
	GameObject laserPos;						//レーザー時の座標用オブジェクト
	public GameObject particleObj;

	public ParticleSystem option_Particle;		//レーザーのパーティクルを取得するための変数

	Bit_Shot b_Shot;							//オプションの攻撃スクリプト情報
	Player1 pl1;								//プレイヤースクリプト情報
	FollowToPlayer_SameMotion FtoPlayer;		//プレイヤーに一番近い追従位置オブジェクトのスクリプト情報
	FollowToPreviousBit FtoPBit_Second;			//二番目の位置のスクリプト情報
	FollowToPreviousBit FtoPBit_Third;			//三番目の位置のスクリプト情報
	FollowToPreviousBit FtoPBit_Fourth;			//四番目の位置のスクリプト情報
	Option_Scale os;							//パーティクルのスケール変更クリプト

	new Renderer renderer;						//レンダラー　3Dオブジェクトの時使う
	Color bit_Color;							//オプションの色　3Dオブジェクトの時使う
	Color particle_Color;							//パーティクルのカラー
	public float scale_value = 0.5f;			//オプションのスケールの値

	float speed;								//オプションの移動スピード（プレイヤー死亡時の処理に使う）
	public float defaultSpeed;					//プレイヤーが死んだときのオプションの初速を入れておく
	float step;									//スピードを計算して入れる
	int collectDelay;							//死亡時すぐ取ってしまわないように当たり判定にディレイを持たせる

	//int state_Num;							//オプションの状態を変えるための数字		
	int option_OrdinalNum;						//オプション自身がどの何番目の追従位置にいるのかの番号

	[SerializeField]
	string myName;								//自分の名前を入れる
	private Quaternion Direction;				//オブジェクトの向きを変更する時に使う

	Vector3 velocity;                           //ベロシティ

	//bool isCircular = false;
	//bool isFollow = false;					//プレイヤーを追従する位置に向かっているかどうか
	//bool once = true;
	//bool isScaleInc = false;
	//bool isScaleDec = false;
	//bool isPlayerDieCheck;					
	public bool isborn = true;					//オプションが出現したときupdateで一回だけ行う処理用
	public bool isDead = false;					//プレイヤーが死んで回収されるまでtrue、回収されたらfalse
	public bool isCollection = false;					//回収されたときに使う
	void Start()
	{
		isborn = true;					//出現時の処理をするように
		//isScaleDec = true;
		defaultSpeed = 20;				//死んだときの初速設定
		speed = defaultSpeed;           //初速を代入
		//値を設定
		//state_Num = 0;				//状態の判別番号

		//bState = BitState.Follow;		//状態の初期設定

		os = particleObj.GetComponent<Option_Scale>();
		renderer = gameObject.GetComponent<Renderer>();			//レンダラー取得

		//4つの追従位置とそれぞれのスクリプト取得
		followPosFirstObj = GameObject.Find("FollowPosFirst_1P");
		FtoPlayer = followPosFirstObj.GetComponent<FollowToPlayer_SameMotion>();

		followPosSecondObj = GameObject.Find("FollowPosSecond_1P");
		FtoPBit_Second = followPosSecondObj.GetComponent<FollowToPreviousBit>();

		followPosThirdObj = GameObject.Find("FollowPosThird_1P");
		FtoPBit_Third=followPosThirdObj.GetComponent<FollowToPreviousBit>();

		followPosFourthObj = GameObject.Find("FollowPosFourth_1P");
		FtoPBit_Fourth=followPosFourthObj.GetComponent<FollowToPreviousBit>();


		//parentObj = transform.parent.gameObject;			//親のオブジェクト取得

		myName = gameObject.name;							//自分の名前取得

		b_Shot = gameObject.GetComponent<Bit_Shot>();		//攻撃の情報取得


	}

	void Update()
	{
		//プレイヤーオブジェクトを取得していなかったら取得してプレイヤーのスクリプトも取得
		if (playerObj == null)
		{
			playerObj = GameObject.Find("Player");

			pl1 = playerObj.GetComponent<Player1>();

		}

		//生成された時の処理
		if (isborn)
		{
			SetFollowPos();				//追従位置設定
			option_Particle.Play();		//オプションの見た目パーティクルを起動
			isborn = false;				//生成時処理をしないようにする
		}

		//追従位置を取得していらその位置にする
		if (followPosObj)
		{
			transform.position = followPosObj.transform.position;
		}

		//回収されたとき
		if (isCollection)
		{
			//オプションの見た目パーティクル起動
			option_Particle.Play();
			//回収判定false
			isCollection = false;

		}

		//プレイヤー死亡時の処理
		if (Input.GetKeyDown(KeyCode.I) || pl1.Dead_Check())
		{
			//死んだ判定true
			isDead = true;
			b_Shot.isShot = false;
			b_Shot.laser_Obj.SetActive(false);

			//追従位置の参照を外す
			followPosObj = null;

			//追従位置番号に合った追従位置オブジェクトのオプションを持っている判定をfalseにする
			switch (option_OrdinalNum)
			{
				case 1:
					FtoPlayer.hasOption = false;
					break;

				case 2:
					FtoPBit_Second.hasOption = false;
					break;

				case 3:
					FtoPBit_Third.hasOption = false;
					break;

				case 4:
					FtoPBit_Fourth.hasOption = false;
					break;
			}
		}

		//死んでいたら
		if (isDead)
		{
			//未回収状態の時の移動
			velocity = gameObject.transform.rotation * new Vector3(speed, 0, -0);
			gameObject.transform.position += velocity * Time.deltaTime;

			//初速からスピードを遅くする
			speed -= 0.5f;
			//スピードは-1.5よりは遅くならない
			if (speed < -1.5f)
			{
				speed = -1.5f;
			}
			//回収の当たり判定のディレイをプラス
			collectDelay++;
		}


		//オプションの移動関数呼び出し
		//Bit_Move();
		//----------------------------------------------
		//未回収状態で画面外に出たら、オフにする
		if (!renderer.isVisible && isDead)
		{
			isDead = false;					//死んでいる判定false
			isborn = true;					//出現時処理できるように
			followPosObj = null;			//追従オブジェクト参照をなくす
			pl1.bitIndex--;					//ゲームに出ているオプション総数カウントを減らす
			gameObject.SetActive(false);	//オブジェクトをオフにする
		}
		//------------------------------------------------
	}

	//------------------ここから関数------------------

	//オプションの状態切り替え関数
	void ChangeState()
	{
		//switch(state_Num)
		//{
			//case 0:
			//	bState = BitState.Circular;
			//	previous_state = bState;
			//	isCircular = true;
			//	break;

			//case 1:
			//	bState = BitState.Oblique;
			//	previous_state = bState;
			//	isOblique = true;
			//	break;

			//case 0:
			//	bState = BitState.Follow;
			//	//previous_state = bState;
			//	isFollow = true;
			//	break;

		//}
	}

	//オプションの移動関数
	void Bit_Move()
	{
		//オプションの移動
		//switch (bState)
		//{
			//case BitState.Circular:
			//	b_Shot.isShot = true;

			//	if (isCircular)
			//	{
			//		//親設定解除
			//		transform.parent = null;
			//		//指定したスピードで親の位置（元の位置）に戻る【parentObjの位置までstepのスピードで戻る】
			//		transform.position = Vector3.MoveTowards(transform.position, parentObj.transform.position, step);
			//		//親と同じ位置に戻ったら
			//		if (transform.position == parentObj.transform.position)
			//		{
			//			isCircular = false;

			//			//親子関係を戻す
			//			transform.parent = parentObj.transform;
			//		}
			//	}
			//	break;

			//case BitState.Oblique:
			//	b_Shot.isShot = true;

			//	if (isOblique)
			//	{
			//		//親設定解除
			//		transform.parent = null;
			//		//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
			//		transform.position = Vector3.MoveTowards(transform.position, obliquePosObj.transform.position, step);
			//		//追従する位置に行ったら
			//		if (transform.position == obliquePosObj.transform.position)
			//		{
			//			isOblique = false;

			//			//追従する位置のオブジェクトを親に設定
			//			transform.parent = obliquePosObj.transform;
			//			transform.rotation = obliquePosObj.transform.rotation;
			//		}
			//	}
			//	break;

			//case BitState.Follow:
			//	//b_Shot.isShot = true;

			//	if (isFollow)
			//	{
			//		//親設定解除
			//		transform.parent = null;
			//		transform.rotation = Quaternion.Euler(0, 0, 0);

			//		//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
			//		transform.position = Vector3.MoveTowards(transform.position, followPosObj.transform.position, step);
			//		//追従する位置に行ったら
			//		if (transform.position == followPosObj.transform.position)
			//		{
			//			isFollow = false;

			//			//追従する位置のオブジェクトを親に設定
			//			transform.parent = followPosObj.transform;
			//			transform.rotation = followPosObj.transform.rotation;
			//		}
			//	}
			//	break;

			//case BitState.Laser:
			//	b_Shot.isShot = false;
			//	break;
		//}
	}

	//生成(画面に表示された時のポジション設定)
	void SetFollowPos()
	{
		//プレイヤーに一番近い追従オブジェクトのオプション所持判定がなかった時
		if (!FtoPlayer.hasOption)
		{
			//オプションを所持判定をtrue,参照する追従位置オブジェクトを入れる,位置を更新
			FtoPlayer.hasOption = true;
			followPosObj = followPosFirstObj;
			transform.position = followPosObj.transform.position;

			//transform.parent = followPosFirstObj.transform;
			//transform.position = followPosFirstObj.transform.position;

			//スピードリセット,オプションの追従位置番号設定,回収の当たり判定ディレイリセット
			speed = defaultSpeed;
			option_OrdinalNum = 1;
			collectDelay = 0;
		}
		//二番目の追従オブジェクトのオプション所持判定がなかった時
		else if (!FtoPBit_Second.hasOption)
		{
			//オプションを所持判定をtrue,参照する追従位置オブジェクトを入れる,位置を更新
			FtoPBit_Second.hasOption = true;
			followPosObj = followPosSecondObj;
			transform.position = followPosObj.transform.position;

			//transform.parent = followPosSecondObj.transform;
			//transform.position = followPosSecondObj.transform.position;

			//スピードリセット,オプションの追従位置番号設定,回収の当たり判定ディレイリセット
			speed = defaultSpeed;
			option_OrdinalNum = 2;
			collectDelay = 0;
		}
		//三番目の追従オブジェクトのオプション所持判定がなかった時
		else if (!FtoPBit_Third.hasOption)
		{
			//オプションを所持判定をtrue,参照する追従位置オブジェクトを入れる,位置を更新
			FtoPBit_Third.hasOption = true;
			followPosObj = followPosThirdObj;
			transform.position = followPosObj.transform.position;

			//transform.parent = followPosThirdObj.transform;
			//transform.position = followPosThirdObj.transform.position;

			//スピードリセット,オプションの追従位置番号設定,回収の当たり判定ディレイリセット
			speed = defaultSpeed;
			option_OrdinalNum = 3;
			collectDelay = 0;
		}
		//四番目の追従オブジェクトのオプション所持判定がなかった時
		else if (!FtoPBit_Fourth.hasOption)
		{
			//オプションを所持判定をtrue,参照する追従位置オブジェクトを入れる,位置を更新
			FtoPBit_Fourth.hasOption = true;
			followPosObj = followPosFourthObj;
			transform.position = followPosObj.transform.position;

			//transform.parent = followPosFourthObj.transform;
			//transform.position = followPosFourthObj.transform.position;

			//スピードリセット,オプションの追従位置番号設定,回収の当たり判定ディレイリセット
			speed = defaultSpeed;
			option_OrdinalNum = 4;
			collectDelay = 0;
		}
	}

	//オプション回収の処理
	private void OnTriggerEnter(Collider col)
	{
		//死んでいる状態で、回収の当たり判定ディレイが10fより大きかったら
		if(isDead && collectDelay>10)
		{
			//プレイヤータグのオブジェクトに当たったら
			if (col.gameObject.tag == "Player")
			{
				//オプションパーティクルストップ
				option_Particle.Stop();
				b_Shot.isShot = true;
				//プレイヤーに一番近い追従位置オブジェクトがオプションを持っていなかったら
				if (!FtoPlayer.hasOption)
				{
					//取得判定true,一番近い位置オブジェクトのオプション所持判定true,参照する追従位置オブジェクト入れる,位置を更新
					isCollection = true;
					FtoPlayer.hasOption = true;
					followPosObj = followPosFirstObj;
					transform.position = followPosObj.transform.position;

					//死んでる状態false,スピードを初速にリセット,オプションの追従位置判別番号設定,当たり判定のディレイリセット
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 1;
					collectDelay = 0;

					//スケール変更スクリプトの回収判定true,回収時のスケール値を０
					os.isCollectInc = true;
					os.scale_Collect = 0;

					//alpha_Value = 0;
					//bit_Color.a = alpha_Value;
					//renderer.material.color = bit_Color;
					//alpha_Value_Par = 0;
					//particle_Color.a = alpha_Value_Par;
					//particleRenderer.material.color = particle_Color;
					//option_Particle.startColor = particle_Color;
				}
				//二番目の追従位置オブジェクトがオプションを持っていなかったら
				else if (!FtoPBit_Second.hasOption)
				{
					//取得判定true,二番目に近い位置オブジェクトのオプション所持判定true,参照する追従位置オブジェクト入れる,位置を更新
					isCollection = true;
					FtoPBit_Second.hasOption = true;
					followPosObj = followPosSecondObj;
					transform.position = followPosObj.transform.position;

					//死んでる状態false,スピードを初速にリセット,オプションの追従位置判別番号設定,当たり判定のディレイリセット
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 2;
					collectDelay = 0;

					//スケール変更スクリプトの回収判定true,回収時のスケール値を０
					os.isCollectInc = true;
					os.scale_Collect = 0;

					//alpha_Value = 0;
					//bit_Color.a = alpha_Value;
					//alpha_Value_Par = 0;
					//renderer.material.color = bit_Color;
					//alpha_Value_Par = 0;
					//particle_Color.a = alpha_Value_Par;
					////particleRenderer.material.color = particle_Color;
					//option_Particle.startColor = particle_Color;
				}
				//三番目の追従位置オブジェクトがオプションを持っていなかったら
				else if (!FtoPBit_Third.hasOption)
				{
					//取得判定true,三番目に近い位置オブジェクトのオプション所持判定true,参照する追従位置オブジェクト入れる,位置を更新
					isCollection = true;
					FtoPBit_Third.hasOption = true;
					followPosObj = followPosThirdObj;
					transform.position = followPosObj.transform.position;

					//死んでる状態false,スピードを初速にリセット,オプションの追従位置判別番号設定,当たり判定のディレイリセット
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 3;
					collectDelay = 0;

					//スケール変更スクリプトの回収判定true,回収時のスケール値を０
					os.isCollectInc = true;
					os.scale_Collect = 0;

					//alpha_Value = 0;
					//bit_Color.a = alpha_Value;
					//alpha_Value_Par = 0;
					//renderer.material.color = bit_Color;
					//alpha_Value_Par = 0;
					//particle_Color.a = alpha_Value_Par;
					////particleRenderer.material.color = particle_Color;
					//option_Particle.startColor = particle_Color;
				}
				//四番目の追従位置オブジェクトがオプションを持っていなかったら
				else if (!FtoPBit_Fourth.hasOption)
				{
					//取得判定true,四番目に近い位置オブジェクトのオプション所持判定true,参照する追従位置オブジェクト入れる,位置を更新
					isCollection = true;
					FtoPBit_Fourth.hasOption = true;
					followPosObj = followPosFourthObj;
					transform.position = followPosObj.transform.position;

					//死んでる状態false,スピードを初速にリセット,オプションの追従位置判別番号設定,当たり判定のディレイリセット
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 4;
					collectDelay = 0;

					//スケール変更スクリプトの回収判定true,回収時のスケール値を０
					os.isCollectInc = true;
					os.scale_Collect = 0;

					//alpha_Value = 0;
					//bit_Color.a = alpha_Value;
					//alpha_Value_Par = 0;
					//renderer.material.color = bit_Color;
					//alpha_Value_Par = 0;
					//particle_Color.a = alpha_Value_Par;
					////particleRenderer.material.color = particle_Color;
					//option_Particle.startColor = particle_Color;
				}
			}
		}
	}
}