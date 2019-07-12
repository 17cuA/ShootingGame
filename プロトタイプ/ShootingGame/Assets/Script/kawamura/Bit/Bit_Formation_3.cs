﻿//オプションの位置関係の処理のスクリプト
//作成者：川村良太

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
	BitState bState;						//ビットンの状態

	//[SerializeField]
	//BitState previous_state;				//ビットンの前の状態（レーザーを解除したときに使う）

	GameObject playerObj;					//プレイヤーのオブジェクト
	public GameObject parentObj;			//親のオブジェクト
	public GameObject followPosObj;         //プレイヤーを追従するときの位置オブジェクト
	GameObject followPosFirstObj;
	GameObject followPosSecondObj;
	GameObject followPosThirdObj;
	GameObject followPosFourthObj;

	GameObject obliquePosObj;				//斜めうち状態の座標用オブジェクト
	GameObject laserPos;					//レーザー時の座標用オブジェクト

	Bit_Shot b_Shot;                        //ビットンの攻撃スクリプト情報
	Player1 pl1;
	FollowToPlayer_SameMotion FtoPlayer;
	FollowToPreviousBit FtoPBit_Second;
	FollowToPreviousBit FtoPBit_Third;
	FollowToPreviousBit FtoPBit_Fourth;


	Renderer renderer;
	public MeshRenderer meshrender;
	Color bit_Color;
	float alpha_Value = 0;
	public float scale_value = 0.5f;
	public float aaaaaaaaaaaaaaaaa;

	float speed;                        //ビットンの移動スピード
	public float defaultSpeed;
	float step;                             //スピードを計算して入れる
	int collectDelay;                       //死亡時当たり判定にディレイを持たせる
	int scaleDelay;


	int state_Num;                          //ビットンの状態を変えるための数字		
	int option_OrdinalNum;


	[SerializeField]
	string myName;							//自分の名前を入れる
	private Quaternion Direction;			//オブジェクトの向きを変更する時に使う

	Vector3 velocity;		

	bool isCircular = false;
	bool isFollow = false;          //プレイヤーを追従する位置に向かっているかどうか
	bool once = true;
	bool isScaleInc = false;
	bool isScaleDec = false;
	bool isPlayerDieCheck;
	bool isborn=true;
	public bool isDead = false;
	void Start()
	{
		isScaleDec = true;
		defaultSpeed = 20;
		speed = defaultSpeed;
		//値を設定
		state_Num = 0;

		//状態の初期設定
		bState = BitState.Follow;

		renderer = gameObject.GetComponent<Renderer>();
		meshrender = gameObject.GetComponent<MeshRenderer>();

		bit_Color = renderer.material.color;
		bit_Color.a = alpha_Value;
		renderer.material.color = bit_Color;

		//meshrender.material.color = new Color(0, 0, 0, 0);

		//プレイヤーオブジェクト取得
		//playerObj = GameObject.FindGameObjectWithTag("Player");

		//4つの追従位置とそれぞれのスクリプト取得
		followPosFirstObj = GameObject.Find("FollowPosFirst");
		FtoPlayer = followPosFirstObj.GetComponent<FollowToPlayer_SameMotion>();

		followPosSecondObj = GameObject.Find("FollowPosSecond");
		FtoPBit_Second = followPosSecondObj.GetComponent<FollowToPreviousBit>();

		followPosThirdObj = GameObject.Find("FollowPosThird");
		FtoPBit_Third=followPosThirdObj.GetComponent<FollowToPreviousBit>();

		followPosFourthObj = GameObject.Find("FollowPosFourth");
		FtoPBit_Fourth=followPosFourthObj.GetComponent<FollowToPreviousBit>();


		//親のオブジェクト取得
		//parentObj = transform.parent.gameObject;
		//自分の名前取得
		myName = gameObject.name;
		//攻撃の情報取得
		//b_Shot = gameObject.GetComponent<Bit_Shot>();

	}

	void Update()
	{
		if (playerObj == null)
		{
			playerObj = GameObject.Find("Player");

			pl1 = playerObj.GetComponent<Player1>();

		}

		//生成された時の処理
		if (isborn)
		{
			SetParent();
			isborn = false;
		}


		if (parentObj)
		{
			transform.position = parentObj.transform.position;
		}

		alpha_Value += 0.1f;
		if (alpha_Value >= 1.0f)
		{
			alpha_Value = 1.0f;
		}
		bit_Color.a = alpha_Value;
		renderer.material.color = bit_Color;
		//meshrender.material.color = new Color(0, 0, 0, alpha_Value);

		//オプションの縮小試し
		//scaleDelay++;
		//if (scaleDelay > 5)
		//{
		//	scale_value = Mathf.Sin(Time.frameCount) / 12.5f + 0.42f;
		//	transform.localScale = new Vector3(scale_value, scale_value, scale_value);
		//	scaleDelay = 0;
		//}

		//if(isScaleInc)
		//{
		//	scale_value += 0.01f;
		//	if (scale_value > 0.5)
		//	{
		//		scale_value = 0.5f;
		//		isScaleInc = false;
		//		isScaleDec = true;
		//	}
		//}
		//else if(isScaleDec)
		//{
		//	scale_value -= 0.01f;
		//	if (scale_value < 0.35f)
		//	{
		//		scale_value = 0.35f;
		//		isScaleDec = false;
		//		isScaleInc = true;
		//	}
		//}

		if (Input.GetKeyDown(KeyCode.I) || pl1.Died_Judgment())
		{
			isDead = true;
			parentObj = null;

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

		//プレイヤーが死んだかどうか判定
		if (pl1.Died_Judgment())
		{
			isDead = true;
			parentObj = null;

			switch(option_OrdinalNum)
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

		if (isDead)
		{
			
			velocity = gameObject.transform.rotation * new Vector3(speed, 0, -0);
			gameObject.transform.position += velocity * Time.deltaTime;

			speed -= 0.5f;
			if (speed < -1.5f)
			{
				speed = -1.5f;
			}
			collectDelay++;
		}

		//isPlayerDieCheck = pl1.Died_Judgment();

		//スピード計算
		//step = speed * Time.deltaTime;

		//入力の関数呼び出し
		//Bit_Input();

		//ビットンの移動関数呼び出し
		//Bit_Move();
		//----------------------------------------------
		//画面外に出たら、オフにする
		if (!renderer.isVisible && isDead)
		{
			isDead = false;
			isborn = true;
			parentObj = null;
			gameObject.SetActive(false);
		}
		//------------------------------------------------
	}

	//------------------ここから関数------------------
	void Bit_Input()
	{

	}


	//ビットンの状態切り替え関数
	void ChangeState()
	{
		switch(state_Num)
		{
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

			case 0:
				bState = BitState.Follow;
				//previous_state = bState;
				isFollow = true;
				break;

		}
	}

	//ビットンの移動関数
	void Bit_Move()
	{
		//ビットンの移動
		switch (bState)
		{
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

			case BitState.Follow:
				//b_Shot.isShot = true;

				if (isFollow)
				{
					//親設定解除
					transform.parent = null;
					transform.rotation = Quaternion.Euler(0, 0, 0);

					//指定したスピードで追従する位置に行く【followPosObjの位置までstepのスピードで】
					transform.position = Vector3.MoveTowards(transform.position, followPosObj.transform.position, step);
					//追従する位置に行ったら
					if (transform.position == followPosObj.transform.position)
					{
						isFollow = false;

						//追従する位置のオブジェクトを親に設定
						transform.parent = followPosObj.transform;
						transform.rotation = followPosObj.transform.rotation;
					}
				}
				break;

			//case BitState.Laser:
			//	b_Shot.isShot = false;
			//	break;
		}
	}

	//生成(画面に表示された時のポジション設定)
	void SetParent()
	{
		if (!FtoPlayer.hasOption)
		{
			FtoPlayer.hasOption = true;
			parentObj = followPosFirstObj;
			transform.position = parentObj.transform.position;

			//transform.parent = followPosFirstObj.transform;
			//transform.position = followPosFirstObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			option_OrdinalNum = 1;
			collectDelay = 0;
		}
		else if (!FtoPBit_Second.hasOption)
		{
			FtoPBit_Second.hasOption = true;
			parentObj = followPosSecondObj;
			transform.position = parentObj.transform.position;

			//transform.parent = followPosSecondObj.transform;
			//transform.position = followPosSecondObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			option_OrdinalNum = 2;
			collectDelay = 0;
		}
		else if (!FtoPBit_Third.hasOption)
		{
			FtoPBit_Third.hasOption = true;
			parentObj = followPosThirdObj;
			transform.position = parentObj.transform.position;

			//transform.parent = followPosThirdObj.transform;
			//transform.position = followPosThirdObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			option_OrdinalNum = 3;
			collectDelay = 0;
		}
		else if (!FtoPBit_Fourth.hasOption)
		{
			FtoPBit_Fourth.hasOption = true;
			parentObj = followPosFourthObj;
			transform.position = parentObj.transform.position;

			//transform.parent = followPosFourthObj.transform;
			//transform.position = followPosFourthObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			option_OrdinalNum = 4;
			collectDelay = 0;
		}
	}

	//オプション回収の処理
	private void OnTriggerEnter(Collider col)
	{
		if(isDead && collectDelay>10)
		{
			if (col.gameObject.name == "Player")
			{
				if (!FtoPlayer.hasOption)
				{
					FtoPlayer.hasOption = true;
					parentObj = followPosFirstObj;
					transform.position = parentObj.transform.position;

					//transform.parent = followPosFirstObj.transform;
					//transform.position = followPosFirstObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 1;
					alpha_Value = 0;
					bit_Color.a = alpha_Value;
					renderer.material.color = bit_Color;

					collectDelay = 0;
				}
				else if (!FtoPBit_Second.hasOption)
				{
					FtoPBit_Second.hasOption = true;
					parentObj = followPosSecondObj;
					transform.position = parentObj.transform.position;

					//transform.parent = followPosSecondObj.transform;
					//transform.position = followPosSecondObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 2;
					alpha_Value = 0;
					bit_Color.a = alpha_Value;
					renderer.material.color = bit_Color;

					collectDelay = 0;
				}
				else if (!FtoPBit_Third.hasOption)
				{
					FtoPBit_Third.hasOption = true;
					parentObj = followPosThirdObj;
					transform.position = parentObj.transform.position;

					//transform.parent = followPosThirdObj.transform;
					//transform.position = followPosThirdObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 3;
					alpha_Value = 0;
					bit_Color.a = alpha_Value;
					renderer.material.color = bit_Color;

					collectDelay = 0;
				}
				else if (!FtoPBit_Fourth.hasOption)
				{
					FtoPBit_Fourth.hasOption = true;
					parentObj = followPosFourthObj;
					transform.position = parentObj.transform.position;

					//transform.parent = followPosFourthObj.transform;
					//transform.position = followPosFourthObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					option_OrdinalNum = 4;
					alpha_Value = 0;
					bit_Color.a = alpha_Value;
					renderer.material.color = bit_Color;

					collectDelay = 0;
				}
			}
		}
	}
}