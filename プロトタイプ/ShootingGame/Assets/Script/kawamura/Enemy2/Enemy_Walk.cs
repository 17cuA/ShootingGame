//作成者：川村良太
//歩く敵のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walk : MonoBehaviour
{
	//自分の状態
	public enum DirectionState
	{
		Left,		//左向き
		Right,		//右向き
		Roll,		//回転中
		Stop,		//停止
	}

	public　DirectionState direcState;		//状態変数
	DirectionState saveDirection;			//状態を一時保存する変数

	Vector3 velocity;

	[Header("入力用　歩くスピード")]
	public float walkSpeed;
	public float rotaY;			//角度
	[Header("入力用　回転スピード")]
	public float rotaSpeed;
	public float stopTime;
	float stopTimeCnt;			//止まっている時間カウント
	float rollDelayCnt;			//回転した後のカウント（回転直後に当たり判定をしないようにするため）

	public bool isRoll;			//回転中かどうか
	bool isRollEnd = false;		//回転が終わったかどうか

	void Start()
    {
		stopTimeCnt = 0;
		rollDelayCnt = 0;
		isRoll = false;
	}

    void Update()
    {
		//とりあえずすり抜けをなくす処理
		if (transform.position.y < -4.15f)
		{
			transform.position = new Vector3(transform.position.x, -4.15f, 0);
		}
		//回転が終わった後当たり判定に間を空けるためカウント
		if (isRollEnd)
		{
			rollDelayCnt++;
			if (rollDelayCnt > 5)
			{
				isRollEnd = false;
				rollDelayCnt = 0;
			}
		}
		//動く関数
		Move();
	}

	//----------------ここから関数----------------

	//動く関数
	void Move()
	{
		switch(direcState)
		{
			//左向きの時移動する
			case DirectionState.Left:
				velocity = gameObject.transform.rotation * new Vector3(-walkSpeed, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			//右向きの時移動する
			case DirectionState.Right:
				velocity = gameObject.transform.rotation * new Vector3(-walkSpeed, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			//回転する
			case DirectionState.Roll:
				//直前の状態が左向きだったら
				if (saveDirection == DirectionState.Left)
				{
					//向きをマイナス
					rotaY -= rotaSpeed;
					if (rotaY < -180f)
					{
						rotaY = -180f;
						direcState = DirectionState.Right;
						isRoll = false;
						isRollEnd = true;
					}
					transform.rotation = Quaternion.Euler(0, rotaY, 0);
				}
				//直前の状態が右向きだったら
				else if (saveDirection == DirectionState.Right)
				{
					//向きをプラス
					rotaY += rotaSpeed;
					if (rotaY > 0)
					{
						rotaY = 0;
						direcState = DirectionState.Left;
						isRoll = false;
						isRollEnd = true;
					}

					transform.rotation = Quaternion.Euler(0, rotaY, 0);
				}
				break;

			case DirectionState.Stop:
				stopTimeCnt += Time.deltaTime;

				break;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		//当たったら
		if (col.gameObject.tag == "Player")
		{
			if (!isRollEnd && !isRoll)
			{
				saveDirection = direcState;
				direcState = DirectionState.Roll;
				isRoll = true;
			}
		}

	}
}
