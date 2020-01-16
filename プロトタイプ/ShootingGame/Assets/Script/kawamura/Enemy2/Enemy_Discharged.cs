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

	Vector3 velocity;

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

	[Header("入力用　横移動の最大時間")]
	public float XMoveTimeMax;
	public float XMoveTimeCnt;
	[Header("入力用　上下移動の最大時間")]
	public float YMoveTimeMax;
	public float YMoveTimeCnt;

	public bool once = true;

	public bool isSpeedYCangeEnd = false;
	public bool isSpeedXCangeEnd = false;


	void Start()
	{
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
			if (moveType == Enemy_Discharged.MoveType.LeftCurveUp_90 || moveType == Enemy_Discharged.MoveType.RightCurveUp_90)
			{
				moveState = Enemy_Discharged.MoveState.UpYMove;
			}
			else if (moveType == Enemy_Discharged.MoveType.LeftCurveDown_90 || moveType == Enemy_Discharged.MoveType.RightCurveDown_90)
			{
				moveState = Enemy_Discharged.MoveState.DownYMove;
			}

			if (moveState == MoveState.UpYMove && speedY < 0)
			{
				speedY *= -1;
			}
			else if (moveState == MoveState.DownYMove && speedY > 0)
			{
				speedY *= -1;
			}
			once = false;
		}

		velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

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

				break;

			case MoveType.LeftCueveUp_180:

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
						speedX += changeSpeedX_value;
						if (speedX > speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = speedXMax;
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
						speedX += changeSpeedX_value;
						if (speedX > speedXMax)
						{
							isSpeedXCangeEnd = true;
							speedX = speedXMax;
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
