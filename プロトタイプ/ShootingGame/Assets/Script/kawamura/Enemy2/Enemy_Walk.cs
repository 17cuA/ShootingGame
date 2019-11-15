//作成者：川村良太
//歩く敵のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Walk : MonoBehaviour
{
	//自分の状態
	public enum DirectionState
	{
		Left,			//左向き
		Right,		//右向き
		Roll,			//回転中
		Stop,			//停止
	}

	public　DirectionState direcState;		//状態変数
	DirectionState saveDirection;           //状態を一時保存する変数

	GameObject childObj;
	Vector3 velocity;

	[Header("入力用　歩くスピード")]
	public float walkSpeed;
	public float rotaY;			//角度
	[Header("入力用　回転スピード")]
	public float rotaSpeed;
	[Header("入力用　歩く最大時間（秒）")]
	public float walkTimeMax;
	public float walkTimeCnt;
	[Header("入力用　止まっている最大時間（秒）")]
	public float stopTimeMax;
	public float stopTimeCnt;          //止まっている時間カウント
	[Header("入力用　攻撃間隔")]
	public float attackTimeMax;
	public float attackTimeCnt;
	float rollDelayCnt;			//回転した後のカウント（回転直後に当たり判定をしないようにするため）

	public bool isRoll;			//回転中かどうか
	bool isRollEnd = false;     //回転が終わったかどうか
	bool isAttack = true;

	void Start()
    {
		childObj = transform.GetChild(0).gameObject;
		walkTimeCnt = 0;
		stopTimeCnt = 0;
		rollDelayCnt = 0;
		isRoll = false;
		isAttack = true;
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

		//if (direcState == DirectionState.Left || direcState == DirectionState.Right)
		//{
		//	walkTimeCnt += Time.deltaTime;
		//	if (walkTimeCnt > walkTimeMax)
		//	{
		//		walkTimeCnt = 0;
		//		direcState = DirectionState.Stop;
		//	}
		//}
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

				walkTimeCnt += Time.deltaTime;
				if (walkTimeCnt > walkTimeMax)
				{
					walkTimeCnt = 0;
					saveDirection = direcState;
					direcState = DirectionState.Stop;
				}
				break;

			//右向きの時移動する
			case DirectionState.Right:
				velocity = gameObject.transform.rotation * new Vector3(-walkSpeed, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				walkTimeCnt += Time.deltaTime;
				if (walkTimeCnt > walkTimeMax)
				{
					walkTimeCnt = 0;
					saveDirection = direcState;
					direcState = DirectionState.Stop;
				}
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
				attackTimeCnt += Time.deltaTime;

				if (stopTimeCnt > stopTimeMax)
				{
					direcState = saveDirection;
					stopTimeCnt = 0;
					attackTimeCnt = 0;
					isAttack = true;
				}
				if (isAttack && attackTimeCnt > attackTimeMax)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET,childObj.transform.position,childObj.transform.rotation);
					isAttack = false;
				}
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
