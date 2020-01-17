using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharged : MonoBehaviour
{
	public enum MoveType
	{
		LeftCurveUp_90,
		LeftCueveUp_180,
		LeftCurveDown_90,
		LeftCueveDown_180,
		RightCurveUp_90,
		RightCueveUp_180,
		RightCurveDown_90,
		RightCueveDown_180,
	}
	public MoveType moveType;

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

	public GameObject modelObj;

	Vector3 velocity;

	//90度カーブ用
	[Header("入力用　Xスピード")]
	public float speedX;
	[Header("入力用　	最大Xスピード")]
	public float speedXMax;
	public float defaultSpeedX;
	[Header("入力用　Xスピードの増減値")]
	public float changeSpeedX_value;
	[Header("入力用　Yスピード")]
	public float speedY;
	public float defaultSpeedY;
	[Header("入力用　Yスピードの増減値")]
	public float changeSpeedY_value;

	[Header("入力用　横移動の最大時間　秒")]
	public float XMoveTimeMax;
	public float XMoveTimeCnt;
	[Header("入力用　上下移動の最大時間　秒")]
	public float YMoveTimeMax;
	public float YMoveTimeCnt;

	bool isSpeedYCangeEnd = false;
	bool isSpeedXCangeEnd = false;
	//90度カーブ用

	//180度カーブ用
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

	//180度カーブ用

	public bool once = true;



	void Start()
	{
		modelObj = transform.GetChild(0).gameObject;


		if (moveType == Enemy_Discharged.MoveType.LeftCurveUp_90 || moveType == Enemy_Discharged.MoveType.RightCurveUp_90)
		{
			moveState = Enemy_Discharged.MoveState.UpYMove;
		}
		else if(moveType == Enemy_Discharged.MoveType.LeftCurveDown_90 || moveType == Enemy_Discharged.MoveType.RightCurveDown_90)
		{
			moveState = Enemy_Discharged.MoveState.DownYMove;
		}

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
		if (once)
		{
			if (moveType == Enemy_Discharged.MoveType.LeftCurveUp_90 || moveType == Enemy_Discharged.MoveType.LeftCueveUp_180 || moveType == Enemy_Discharged.MoveType.RightCurveUp_90 || moveType == Enemy_Discharged.MoveType.RightCueveUp_180)
			{
				moveState = Enemy_Discharged.MoveState.UpYMove;
			}
			else if (moveType == Enemy_Discharged.MoveType.LeftCurveDown_90 || moveType == Enemy_Discharged.MoveType.LeftCueveDown_180|| moveType == Enemy_Discharged.MoveType.RightCurveDown_90 || moveType == Enemy_Discharged.MoveType.RightCueveDown_180)
			{
				moveState = Enemy_Discharged.MoveState.DownYMove;
			}

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
			speedY180 = defaultSpeedY180;
			XMoveTimeCnt180 = 0;
			speedStateCnt = 0;

			once = false;
		}

		//velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
		//gameObject.transform.position += velocity * Time.deltaTime;

		SpeedCange();

		if (transform.position.x < -20)
		{
			Destroy(gameObject);
		}
			
	}

	void SpeedCange()
	{
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
						speedY180 *= 1.1f;
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
						speedY180 = speedY180 / 11 * 10;
						//speedY180 -= changeSpeedY_value180;
						//speedY180 = 2;
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

				break;

		}


	}

	public void SetState(MoveType mType)
	{
		moveType = mType;
		once = true;
	}
}
