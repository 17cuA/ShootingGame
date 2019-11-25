using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FollowGround : MonoBehaviour
{


	public enum MoveState
	{
		Up,
		Dowx,
		Left,
		Right,
		DefaultLeft,
		DefaultRight,
	}

	public MoveState defaultState;
	public MoveState moveState;
	public MoveState saveState;

	Vector3 velocity;

	Collider coll;

	public ColCheck TopCheck;
	public ColCheck UnderCheck;
	public ColCheck LeftCheck;
	public ColCheck RightCheck;

	public float speedX;
	public float speedY;

	public float changeDelayCnt;
	public float chamgeDelayMax;

	public bool isTop;
	public bool isUnder;
	public bool isLeft;
	public bool isRight;


	void Start()
    {
		if (defaultState == MoveState.DefaultRight)
		{
			moveState = MoveState.Right;
			saveState = moveState;
		}
		else if (defaultState == MoveState.DefaultLeft)
		{
			moveState = MoveState.Left;
			saveState = moveState;
		}

	}


	void Update()
    {
		isTop = TopCheck.isCheck;
		isUnder = UnderCheck.isCheck;
		isLeft = LeftCheck.isCheck;
		isRight = RightCheck.isCheck;

		if (changeDelayCnt > chamgeDelayMax)
		{
			ChangeDirection();
		}
		else
		{
			changeDelayCnt += Time.deltaTime;
		}

		Move();
    }

	//動く関数
	void Move()
	{
		switch(moveState)
		{
			case MoveState.Up:
				velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
				break;

			case MoveState.Dowx:
				velocity = gameObject.transform.rotation * new Vector3(0, -speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
				break;

			case MoveState.Left:
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
				break;

			case MoveState.Right:
				velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
				break;
		}
	}

	//移動方向を変える関数
	void ChangeDirection()
	{
		if (defaultState == MoveState.DefaultRight)
		{
			if (isUnder && isRight)
			{
				moveState = MoveState.Up;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isRight && isTop)
			{
				moveState = MoveState.Left;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isTop && isLeft)
			{
				moveState = MoveState.Dowx;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isLeft && isUnder)
			{
				moveState = MoveState.Right;
				saveState = moveState;
				changeDelayCnt = 0;

			}
		}
		else if (defaultState == MoveState.DefaultLeft)
		{
			if (isUnder && isRight)
			{
				moveState = MoveState.Left;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isRight && isTop)
			{
				moveState = MoveState.Dowx;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isTop && isLeft)
			{
				moveState = MoveState.Right;
				saveState = moveState;
				changeDelayCnt = 0;
			}
			else if (isLeft && isUnder)
			{
				moveState = MoveState.Up;
				saveState = moveState;
				changeDelayCnt = 0;
			}
		}

		//地面の角に来た時
		if (!isTop && !isUnder && !isLeft && !isRight)
		{
			if (defaultState == MoveState.DefaultRight)
			{
				switch (saveState)
				{
					case MoveState.Up:
						moveState = MoveState.Right;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Dowx:
						moveState = MoveState.Left;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Left:
						moveState = MoveState.Up;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Right:
						moveState = MoveState.Dowx;
						saveState = moveState;
						changeDelayCnt = 0;
						break;
				}
			}
			else if (defaultState == MoveState.DefaultLeft)
			{
				switch (saveState)
				{
					case MoveState.Up:
						moveState = MoveState.Left;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Dowx:
						moveState = MoveState.Right;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Left:
						moveState = MoveState.Dowx;
						saveState = moveState;
						changeDelayCnt = 0;
						break;

					case MoveState.Right:
						moveState = MoveState.Up;
						saveState = moveState;
						changeDelayCnt = 0;
						break;
				}
			}
		}

	}
}
