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

	float speed;                        //ビットンの移動スピード
	public float defaultSpeed;
	float step;                             //スピードを計算して入れる
	int collectDelay;

	int state_Num;                          //ビットンの状態を変えるための数字		

	[SerializeField]
	string myName;							//自分の名前を入れる
	private Quaternion Direction;			//オブジェクトの向きを変更する時に使う

	float smoothTime;                       //レーザー時の座標へ移動するのにかかる時間
	Vector3 velocity;		
	public int laserCnt = 0;				//レーザーのボタンを押してからの時間カウント
	int returnNum;							//レーザーを解除できる時間

	bool isCircular = false;
	bool isFollow = false;          //プレイヤーを追従する位置に向かっているかどうか
	bool isOblique = false;         //斜め撃ちの位置に向かっているかどうか
	bool once = true;
	bool isPlayerDieCheck;
	bool isborn=true;
	public bool isDead = false;
	void Start()
	{
		speed = defaultSpeed ;
		//値を設定
		state_Num = 0;
		smoothTime = 0.35f;
		returnNum = 30;

		//状態の初期設定
		bState = BitState.Follow;

		renderer = gameObject.GetComponent<Renderer>();

		bit_Color = renderer.material.color;
		//meshrender = gameObject.GetComponent<MeshRenderer>();
		//meshrender.material.color = new Color(0, 0, 0, 0);

		//プレイヤーオブジェクト取得
		playerObj = GameObject.FindGameObjectWithTag("Player");

		//4つの追従位置とそれぞれのスクリプト取得
		followPosFirstObj = GameObject.Find("FollowPosFirst");
		FtoPlayer = followPosFirstObj.GetComponent<FollowToPlayer_SameMotion>();

		followPosSecondObj = GameObject.Find("FollowPosSecond");
		FtoPBit_Second = followPosSecondObj.GetComponent<FollowToPreviousBit>();

		followPosThirdObj = GameObject.Find("FollowPosThird");
		FtoPBit_Third=followPosThirdObj.GetComponent<FollowToPreviousBit>();

		followPosFourthObj = GameObject.Find("FollowPosFourth");
		FtoPBit_Fourth=followPosFourthObj.GetComponent<FollowToPreviousBit>();


		pl1 = playerObj.GetComponent<Player1>();
		//親のオブジェクト取得
		//parentObj = transform.parent.gameObject;
		//自分の名前取得
		myName = gameObject.name;
		//攻撃の情報取得
		//b_Shot = gameObject.GetComponent<Bit_Shot>();

		//laserPos = GameObject.Find("LaserPos");

		//自分の名前によって取得するオブジェクトを変える
		if (myName == "Bit_First(Clone)")
		{
			//プレイヤーを追従する座標オブジェクト取得
			//followPosObj = GameObject.Find("FollowPosFirst");
			//斜め撃ちの上の座標オブジェクト取得
			//obliquePosObj = GameObject.Find("ObliquePosTop");
		}
		else if (myName == "Bit_Second(Clone)")
		{
			//プレイヤーを追従する座標オブジェクト取得
			//followPosObj = GameObject.Find("FollowPosSecond");
			//斜め撃ちの下の座標オブジェクト取得
			//obliquePosObj = GameObject.Find("ObliquePosUnder");
		}
		else if (myName == "Bit_Third(Clone)")
		{
			//プレイヤーを追従する座標オブジェクト取得
			//followPosObj = GameObject.Find("FollowPosThird");
			//斜め撃ちの下の座標オブジェクト取得
			//obliquePosObj = GameObject.Find("ObliquePosUnder");
		}
		else if (myName == "Bit_Fourth(Clone)")
		{
			//プレイヤーを追従する座標オブジェクト取得
			//followPosObj = GameObject.Find("FollowPosFourth");
			//斜め撃ちの下の座標オブジェクト取得
			//obliquePosObj = GameObject.Find("ObliquePosUnder");
		}
	}

	void Update()
	{
		//生成された時の処理
		if(isborn)
		{
			SetParent();
			isborn = false;
		}
		
		alpha_Value += 0.1f;
		if (alpha_Value >= 1.0f)
		{
			alpha_Value = 1.0f;
		}
		bit_Color.a = alpha_Value;
		renderer.material.color = bit_Color;
		//meshrender.material.color = new Color(0, 0, 0, alpha_Value);

		if (Input.GetKeyDown(KeyCode.I))
		{
			isDead = true;
		}


		if (pl1.Died_Judgment())
		{
			isDead = true;
			transform.parent = null;
		}

		if (isDead)
		{
			transform.parent = null;
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
		if (!renderer.isVisible)
		{
			gameObject.SetActive(false);
		}
		//------------------------------------------------
	}

	//------------------ここから関数------------------
	void Bit_Input()
	{
		//Lキーが離された時
		//if (Input.GetKeyUp(KeyCode.L))
		//{
		//	//レーザー状態になってからの経過時間が解除可能時間より小さかったら解除できる
		//	if (laserCnt < returnNum)
		//	{
		//		//ビットンの状態をレーザーになる前の状態に戻す
		//		bState = previous_state;
		//		//レーザー経過時間リセット
		//		laserCnt = 0;

		//		//状態が変わったのでそれぞれの位置に戻すためのbool変数をtrueに
		//		switch (bState)
		//		{
		//			case BitState.Circular:
		//				isCircular = true;
		//				break;

		//			case BitState.Oblique:
		//				isOblique = true;
		//				break;

		//			case BitState.Follow:
		//				isFollow = true;
		//				break;
		//		}
		//	}
		//	laserCnt = 0;
		//}
		//Lキーを押している間レーザー状態になる
		//else if (Input.GetKey(KeyCode.L))
		//{
		//	//レーザー時の関数呼び出し
		//	Bit_Laser();
		//}
		//Tキーか学校のコントローラーだとXボタンを押したときビットンの状態切り替え
		//else if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Fire3"))
		//{
		//	state_Num++;
		//	if (state_Num > 0)
		//	{
		//		state_Num = 0;
		//	}
		//	//状態切り替えの関数呼び出し
		//	ChangeState();
		//}
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

	void SetParent()
	{
		if (FtoPlayer.childCnt == 0)
		{
			transform.parent = followPosFirstObj.transform;
			transform.position = followPosFirstObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			collectDelay = 0;
		}
		else if (FtoPBit_Second.childCnt == 0)
		{
			transform.parent = followPosSecondObj.transform;
			transform.position = followPosSecondObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			collectDelay = 0;
		}
		else if (FtoPBit_Third.childCnt == 0)
		{
			transform.parent = followPosThirdObj.transform;
			transform.position = followPosThirdObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			collectDelay = 0;
		}
		else if (FtoPBit_Fourth.childCnt == 0)
		{
			transform.parent = followPosFourthObj.transform;
			transform.position = followPosFourthObj.transform.position;
			isDead = false;
			speed = defaultSpeed;
			collectDelay = 0;
		}
	}

	//オプション回収の処理
	private void OnTriggerEnter(Collider col)
	{
		if(isDead && collectDelay>10)
		{
			if (col.gameObject.tag == "Player")
			{
				if (FtoPlayer.childCnt == 0)
				{
					alpha_Value = 0;
					transform.parent = followPosFirstObj.transform;
					transform.position = followPosFirstObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					collectDelay = 0;
				}
				else if (FtoPBit_Second.childCnt == 0)
				{
					alpha_Value = 0;
					transform.parent = followPosSecondObj.transform;
					transform.position = followPosSecondObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					collectDelay = 0;
				}
				else if (FtoPBit_Third.childCnt == 0)
				{
					alpha_Value = 0;
					transform.parent = followPosThirdObj.transform;
					transform.position = followPosThirdObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					collectDelay = 0;
				}
				else if (FtoPBit_Fourth.childCnt == 0)
				{
					alpha_Value = 0;
					transform.parent = followPosFourthObj.transform;
					transform.position = followPosFourthObj.transform.position;
					isDead = false;
					speed = defaultSpeed;
					collectDelay = 0;
				}
			}
		}
	}
}