using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Discharged : MonoBehaviour
{
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

	bool once = true;

	bool isSpeedYCangeEnd = false;
	bool isSpeedXCangeEnd = false;


	void Start()
	{
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
			if (moveState == MoveState.UpYMove && speedY < 0)
			{
				speedY *= -1;
			}
			else if (moveState == MoveState.DownYMove && speedY > 0)
			{
				speedY *= -1;
			}
		}

		velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

		SpeedCange();

		if (transform.position.x < -20)
		{
			Destroy(gameObject);
		}
			
	}

	void SpeedCange()
	{
		if (moveState == MoveState.UpYMove || moveState == MoveState.DownYMove)
		{
			saveMoveState = moveState;
			YMoveTimeCnt += Time.deltaTime;
			if (YMoveTimeCnt > YMoveTimeMax)
			{
				moveState = MoveState.MiddleMove;
			}
		}
		else if(moveState==MoveState.MiddleMove)
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
				if (saveMoveState == MoveState.UpYMove)
				{
					speedY -= changeSpeedX_value;
					if (speedY < 0)
					{
						isSpeedYCangeEnd = true;
						speedY = 0;
					}
				}
				else if (saveMoveState == MoveState.DownYMove)
				{
					speedY += changeSpeedX_value;
					if (speedY > 0)
					{
						isSpeedYCangeEnd = true;
						speedY = 0;
					}
				}
			}

			if (isSpeedXCangeEnd && isSpeedYCangeEnd)
			{
				moveState = MoveState.LeftXMove;
			}
		}

		//switch(moveState)
		//{
		//	case MoveState.LeftXMove:
		//		XMoveTimeCnt += Time.deltaTime;
		//		break;

		//	case MoveState.UpYMove:
		//		YMoveTimeCnt += Time.deltaTime;
		//		break;

		//	case MoveState.MiddleMove:

		//		break;
		//}
	}
}
