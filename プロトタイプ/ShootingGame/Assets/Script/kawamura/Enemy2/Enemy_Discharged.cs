﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharged : MonoBehaviour
{
	//動きタイプ
	public enum MoveType
	{
		LeftCurveUp_90,				//上に上がって90度左に曲がる
		LeftCueveUp_180,				//右に出て左回りに180度曲がる
		LeftCurveDown_90,			//下に下がって90度左に曲がる
		LeftCueveDown_180,			//右に出て右回りに180度曲がる
		RightCurveUp_90,				//上に上がって90度右に曲がる
		RightCueveUp_180,			//左に出て右曲がりに180度曲がる
		RightCurveDown_90,			//下に下がって90度左に曲がる
		RightCueveDown_180,		//左に出て左回りに180度に曲がる
	}
	public MoveType moveType;		//動きタイプ変数


	public enum MoveState
	{
		LeftXMove,			//左移動
		RightXMove,			//右移動
		UpYMove,				//上移動
		DownYMove,			//下移動
		MiddleMove,			//上下移動から横移動に移るまでの間の移動
	}

	public MoveState moveState;
	public MoveState saveMoveState;

	public GameObject modelObj;			//モデルオブジェクト（主に左右の動きで向きを変えるのに使う）

	Vector3 velocity;

	//ここから90度カーブ用
	[Header("入力用　Xスピード")]
	public float speedX;
	[Header("入力用　	最大Xスピード")]
	public float speedXMax;
	public float defaultSpeedX;					//X初期スピード
	[Header("入力用　Xスピードの増減値")]
	public float changeSpeedX_value;
	[Header("入力用　Yスピード")]
	public float speedY;
	public float defaultSpeedY;					//Y初期スピード
	[Header("入力用　Yスピードの増減値")]
	public float changeSpeedY_value;

	[Header("入力用　横移動の最大時間　秒")]
	public float XMoveTimeMax;
	public float XMoveTimeCnt;					//横に動いている時間カウント
	[Header("入力用　上下移動の最大時間　秒")]
	public float YMoveTimeMax;
	public float YMoveTimeCnt;                  //上下に動いている時間カウント

	bool isSpeedXCangeEnd = false;          //横移動のスピードが変化し終わったか用
	bool isSpeedYCangeEnd = false;			//上下移動のスピードが変化し終わったかどうか用
	//ここまで90度カーブ用

	//ここから180度カーブ用
	[Header("入力用　180Xスピード")]
	public float speedX180;
	[Header("入力用　	180最大Xスピード")]
	public float speedXMax180;
	public float defaultSpeedX180;
	[Header("入力用　180Xスピードの増減値")]
	public float changeSpeedX_value180;

	public float speedY180;
	[Header("入力用　180初期Yスピード")]
	public float defaultSpeedY180;
	public float speedYMax180;
	[Header("入力用　180Yスピードの増減値")]
	public float changeSpeedY_value180;

	[Header("入力用　180横移動の最大時間　秒")]
	public float XMoveTimeMax180 = 0;
	public float XMoveTimeCnt180 = 0;

	public int speedStateCnt = 0;

	//ここまで180度カーブ用

	public bool once = true;		//一回だけ行う処理用



	void Start()
	{
		//モデル取得
		modelObj = transform.GetChild(0).gameObject;		

		defaultSpeedX = speedX;
		defaultSpeedY = speedY;
		XMoveTimeCnt = 0;
		YMoveTimeCnt = 0;

		once = true;
		isSpeedYCangeEnd = false;
		isSpeedXCangeEnd = false;
	}

	void Update()
	{
		//一回だけ行う
		if (once)
		{
			//動きのタイプで最初上に動くか下に動くか決める
			if (moveType == Enemy_Discharged.MoveType.LeftCurveUp_90 || moveType == Enemy_Discharged.MoveType.LeftCueveUp_180 || moveType == Enemy_Discharged.MoveType.RightCurveUp_90 || moveType == Enemy_Discharged.MoveType.RightCueveUp_180)
			{
				moveState = Enemy_Discharged.MoveState.UpYMove;
			}
			else if (moveType == Enemy_Discharged.MoveType.LeftCurveDown_90 || moveType == Enemy_Discharged.MoveType.LeftCueveDown_180|| moveType == Enemy_Discharged.MoveType.RightCurveDown_90 || moveType == Enemy_Discharged.MoveType.RightCueveDown_180)
			{
				moveState = Enemy_Discharged.MoveState.DownYMove;
			}

			//動きの種類でモデルの向きを変える
			if (moveType == Enemy_Discharged.MoveType.LeftCurveUp_90 || moveType == Enemy_Discharged.MoveType.LeftCueveUp_180 || moveType == Enemy_Discharged.MoveType.LeftCurveDown_90 || moveType == Enemy_Discharged.MoveType.LeftCueveDown_180)
			{
				modelObj.transform.rotation = Quaternion.Euler(0, -90, 0);
				transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else if (moveType == Enemy_Discharged.MoveType.RightCurveUp_90 || moveType == Enemy_Discharged.MoveType.RightCueveUp_180 || moveType == Enemy_Discharged.MoveType.RightCurveDown_90 || moveType == Enemy_Discharged.MoveType.RightCueveDown_180)
			{
				modelObj.transform.rotation = Quaternion.Euler(0, -270, 0);
				transform.rotation = Quaternion.Euler(0, 180, 0);
			}

			//上下の移動のスピードを決める（プラスマイナスがあっていなかったら変える）
			if (moveState == MoveState.UpYMove && speedY < 0)
			{
				speedY *= -1;
			}
			else if (moveState == MoveState.DownYMove && speedY > 0)
			{
				speedY *= -1;
			}

			defaultSpeedX = speedX;
			defaultSpeedY = speedY;
			XMoveTimeCnt = 0;
			YMoveTimeCnt = 0;

			speedX180 = defaultSpeedX180;
			speedY180 = 0;
			XMoveTimeCnt180 = 0;
			speedStateCnt = 0;

			once = false;
		}

		//velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
		//gameObject.transform.position += velocity * Time.deltaTime;

		SpeedCange();

		//画面外左で消す
		if (transform.position.x < -20)
		{
			Destroy(gameObject);
		}
			
	}

	//スピード変化関数
	void SpeedCange()
	{
		//動きのタイプを見る
		switch(moveType)
		{
			case MoveType.LeftCurveUp_90:
				if (moveState == MoveState.UpYMove)
				{
					saveMoveState = moveState;
					YMoveTimeCnt += Time.deltaTime;
					if (YMoveTimeCnt > YMoveTimeMax)
					{
						moveState = MoveState.MiddleMove;
					}
				}
				else if (moveState == MoveState.MiddleMove)
				{
					if (!isSpeedXCangeEnd)
					{
						speedX -= changeSpeedX_value;
						if (speedX < -speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = -speedXMax;
						}
					}

					if (!isSpeedYCangeEnd)
					{
						speedY -= changeSpeedX_value;
						if (speedY < 0)
						{
							isSpeedYCangeEnd = true;
							speedY = 0;
						}	
					}

					if (isSpeedXCangeEnd && isSpeedYCangeEnd)
					{
						moveState = MoveState.LeftXMove;
					}
				}
				velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case MoveType.LeftCueveUp_180:
				switch(speedStateCnt)
				{
					//最初に横移動している状態
					case 0:
						if (XMoveTimeCnt180 > XMoveTimeMax180)
						{
							speedStateCnt++;
						}
						XMoveTimeCnt180 += Time.deltaTime;

						break;

						//横移動速度が減少して上移動速度が上昇している状態
					case 1:
						speedX180 -= changeSpeedX_value180;
						//speedY180 += changeSpeedY_value180;
						//speedY180 *= 1.1f;
						speedY180 = defaultSpeedY180;
						//if (speedY180 > speedYMax180)
						//{
						//	speedY180 = speedYMax180;
						//}
						if (speedX180 <= 0)
						{
							speedStateCnt++;
						}
						break;

						//横移動が0になったあと最初と逆方向にスピードが上がる状態
					case 2:
						speedX180 -= changeSpeedX_value180;
						//speedY180 = speedY180 / 11 * 10;
						//speedY180 -= changeSpeedY_value180;
						speedY180 = defaultSpeedY180;
						if (speedX180 < -defaultSpeedX180)
						{
							//speedX180 = speedXMax180;
							speedY180 = 0;

							speedStateCnt++;
						}

						//if (speedY180 <= 0)
						//{
						//	speedY180 = 0;
						//	speedStateCnt++;
						//}
						break;

					case 3:
						speedX180 -= changeSpeedX_value180;
						if(speedX180 < speedXMax180)
						{
							speedX180 = speedXMax180;
							speedStateCnt++;
						}
						break;

					case 4:
						break;

				}
				velocity = gameObject.transform.rotation * new Vector3(speedX180, speedY180, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case MoveType.LeftCurveDown_90:
				if (moveState == MoveState.DownYMove)
				{
					saveMoveState = moveState;
					YMoveTimeCnt += Time.deltaTime;
					if (YMoveTimeCnt > YMoveTimeMax)
					{
						moveState = MoveState.MiddleMove;
					}
				}
				else if (moveState == MoveState.MiddleMove)
				{
					if (!isSpeedXCangeEnd)
					{
						speedX -= changeSpeedX_value;
						if (speedX < -speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = -speedXMax;
						}
					}

					if (!isSpeedYCangeEnd)
					{
						speedY += changeSpeedX_value;
						if (speedY > 0)
						{
							isSpeedYCangeEnd = true;
							speedY = 0;
						}		
					}

					if (isSpeedXCangeEnd && isSpeedYCangeEnd)
					{
						moveState = MoveState.LeftXMove;
					}
				}
				velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case MoveType.LeftCueveDown_180:
				switch (speedStateCnt)
				{
					//最初に横移動している状態
					case 0:
						if (XMoveTimeCnt180 > XMoveTimeMax180)
						{
							speedStateCnt++;
						}
						XMoveTimeCnt180 += Time.deltaTime;

						break;

					//横移動速度が減少して上移動速度が上昇している状態
					case 1:
						speedX180 -= changeSpeedX_value180;
						//speedY180 += changeSpeedY_value180;
						//speedY180 *= 1.1f;
						speedY180 = -defaultSpeedY180;
						//if (speedY180 > speedYMax180)
						//{
						//	speedY180 = speedYMax180;
						//}
						if (speedX180 <= 0)
						{
							speedStateCnt++;
						}
						break;

					//横移動が0になったあと最初と逆方向にスピードが上がる状態
					case 2:
						speedX180 -= changeSpeedX_value180;
						//speedY180 = speedY180 / 11 * 10;
						//speedY180 -= changeSpeedY_value180;
						speedY180 = -defaultSpeedY180;
						if (speedX180 < -defaultSpeedX180)
						{
							//speedX180 = speedXMax180;
							speedY180 = 0;

							speedStateCnt++;
						}

						//if (speedY180 <= 0)
						//{
						//	speedY180 = 0;
						//	speedStateCnt++;
						//}
						break;

					case 3:
						speedX180 -= changeSpeedX_value180;
						if (speedX180 < speedXMax180)
						{
							speedX180 = speedXMax180;
							speedStateCnt++;
						}
						break;

					case 4:
						break;

				}
				velocity = gameObject.transform.rotation * new Vector3(speedX180, speedY180, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case MoveType.RightCurveUp_90:
				if (moveState == MoveState.UpYMove)
				{
					saveMoveState = moveState;
					YMoveTimeCnt += Time.deltaTime;
					if (YMoveTimeCnt > YMoveTimeMax)
					{
						moveState = MoveState.MiddleMove;
					}
				}
				else if (moveState == MoveState.MiddleMove)
				{
					if (!isSpeedXCangeEnd)
					{
						speedX -= changeSpeedX_value;
						if (speedX < -speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = -speedXMax;
						}
					}

					if (!isSpeedYCangeEnd)
					{
						speedY -= changeSpeedX_value;
						if (speedY < 0)
						{
							isSpeedYCangeEnd = true;
							speedY = 0;
						}
					}

					if (isSpeedXCangeEnd && isSpeedYCangeEnd)
					{
						moveState = MoveState.LeftXMove;
					}
				}
				velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				//if (moveState == MoveState.UpYMove)
				//{
				//	saveMoveState = moveState;
				//	YMoveTimeCnt += Time.deltaTime;
				//	if (YMoveTimeCnt > YMoveTimeMax)
				//	{
				//		moveState = MoveState.MiddleMove;
				//	}
				//}
				//else if (moveState == MoveState.MiddleMove)
				//{
				//	if (!isSpeedXCangeEnd)
				//	{
				//		speedX += changeSpeedX_value;
				//		if (speedX > speedXMax)
				//		{
				//			isSpeedXCangeEnd = true;
				//			speedX = speedXMax;
				//		}
				//	}

				//	if (!isSpeedYCangeEnd)
				//	{
				//		speedY -= changeSpeedX_value;
				//		if (speedY < 0)
				//		{
				//			isSpeedYCangeEnd = true;
				//			speedY = 0;
				//		}
				//	}

				//	if (isSpeedXCangeEnd && isSpeedYCangeEnd)
				//	{
				//		moveState = MoveState.LeftXMove;
				//	}
				//}

				break;

			case MoveType.RightCueveUp_180:
				switch (speedStateCnt)
				{
					//最初に横移動している状態
					case 0:
						if (XMoveTimeCnt180 > XMoveTimeMax180)
						{
							speedStateCnt++;
						}
						XMoveTimeCnt180 += Time.deltaTime;

						break;

					//横移動速度が減少して上移動速度が上昇している状態
					case 1:
						speedX180 -= changeSpeedX_value180;
						//speedY180 += changeSpeedY_value180;
						//speedY180 *= 1.1f;
						speedY180 = defaultSpeedY180;
						//if (speedY180 > speedYMax180)
						//{
						//	speedY180 = speedYMax180;
						//}
						if (speedX180 <= 0)
						{
							speedStateCnt++;
						}
						break;

					//横移動が0になったあと最初と逆方向にスピードが上がる状態
					case 2:
						speedX180 -= changeSpeedX_value180;
						//speedY180 = speedY180 / 11 * 10;
						//speedY180 -= changeSpeedY_value180;
						speedY180 = defaultSpeedY180;
						if (speedX180 < -defaultSpeedX180)
						{
							//speedX180 = speedXMax180;
							speedY180 = 0;

							speedStateCnt++;
						}

						//if (speedY180 <= 0)
						//{
						//	speedY180 = 0;
						//	speedStateCnt++;
						//}
						break;

					case 3:
						speedX180 -= changeSpeedX_value180;
						if (speedX180 < speedXMax180)
						{
							speedX180 = speedXMax180;
							speedStateCnt++;
						}
						break;

					case 4:
						break;

				}
				velocity = gameObject.transform.rotation * new Vector3(speedX180, speedY180, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case MoveType.RightCurveDown_90:
				if (moveState == MoveState.DownYMove)
				{
					saveMoveState = moveState;
					YMoveTimeCnt += Time.deltaTime;
					if (YMoveTimeCnt > YMoveTimeMax)
					{
						moveState = MoveState.MiddleMove;
					}
				}
				else if (moveState == MoveState.MiddleMove)
				{
					if (!isSpeedXCangeEnd)
					{
						speedX -= changeSpeedX_value;
						if (speedX < -speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = -speedXMax;
						}
					}

					if (!isSpeedYCangeEnd)
					{
						speedY += changeSpeedX_value;
						if (speedY > 0)
						{
							isSpeedYCangeEnd = true;
							speedY = 0;
						}
					}

					if (isSpeedXCangeEnd && isSpeedYCangeEnd)
					{
						moveState = MoveState.LeftXMove;
					}
				}
				velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				//if (moveState == MoveState.DownYMove)
				//{
				//	saveMoveState = moveState;
				//	YMoveTimeCnt += Time.deltaTime;
				//	if (YMoveTimeCnt > YMoveTimeMax)
				//	{
				//		moveState = MoveState.MiddleMove;
				//	}
				//}
				//else if (moveState == MoveState.MiddleMove)
				//{
				//	if (!isSpeedXCangeEnd)
				//	{
				//		speedX += changeSpeedX_value;
				//		if (speedX > speedXMax)
				//		{
				//			isSpeedXCangeEnd = true;
				//			speedX = speedXMax;
				//		}
				//	}

				//	if (!isSpeedYCangeEnd)
				//	{
				//		speedY += changeSpeedX_value;
				//		if (speedY > 0)
				//		{
				//			isSpeedYCangeEnd = true;
				//			speedY = 0;
				//		}
				//	}

				//	if (isSpeedXCangeEnd && isSpeedYCangeEnd)
				//	{
				//		moveState = MoveState.LeftXMove;
				//	}
				//}

				break;

			case MoveType.RightCueveDown_180:
				switch (speedStateCnt)
				{
					//最初に横移動している状態
					case 0:
						if (XMoveTimeCnt180 > XMoveTimeMax180)
						{
							speedStateCnt++;
						}
						XMoveTimeCnt180 += Time.deltaTime;

						break;

					//横移動速度が減少して上移動速度が上昇している状態
					case 1:
						speedX180 -= changeSpeedX_value180;
						//speedY180 += changeSpeedY_value180;
						//speedY180 *= 1.1f;
						speedY180 = -defaultSpeedY180;
						//if (speedY180 > speedYMax180)
						//{
						//	speedY180 = speedYMax180;
						//}
						if (speedX180 <= 0)
						{
							speedStateCnt++;
						}
						break;

					//横移動が0になったあと最初と逆方向にスピードが上がる状態
					case 2:
						speedX180 -= changeSpeedX_value180;
						//speedY180 = speedY180 / 11 * 10;
						//speedY180 -= changeSpeedY_value180;
						speedY180 = -defaultSpeedY180;
						if (speedX180 < -defaultSpeedX180)
						{
							//speedX180 = speedXMax180;
							speedY180 = 0;

							speedStateCnt++;
						}

						//if (speedY180 <= 0)
						//{
						//	speedY180 = 0;
						//	speedStateCnt++;
						//}
						break;

					case 3:
						speedX180 -= changeSpeedX_value180;
						if (speedX180 < speedXMax180)
						{
							speedX180 = speedXMax180;
							speedStateCnt++;
						}
						break;

					case 4:
						break;

				}
				velocity = gameObject.transform.rotation * new Vector3(speedX180, speedY180, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

		}


	}

	public void SetState(MoveType mType)
	{
		moveType = mType;
		once = true;
	}
}
