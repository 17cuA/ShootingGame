//作成者：川村良太
//ビートルの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beetle : character_status
{
	public enum State
	{
		Front,
		Behind,
	}

	public State eState;

	GameObject smallBeamObj;
	Vector3 velocity;

	//[Header("入力用　Xスピード")]
	public float speedX;
	[Header("入力用　Xスピード")]
	public float defaultSpeedX_Value;
	[Header("入力用　Yスピード")]
	public float speedY;
	public float defaultSpeedY_Value;
	[Header("入力用　Y移動速度を減速し始める大きさ")]
	public float decelerationY_Start;        //回転の減速開始をする角度
	public float speedZ;
	[Header("入力用　Zスピード")]
	public float speedZ_Value;      //Zスピードの値
	[Header("入力用　Yの移動する距離")]
	public float moveY_Max;			//Yの最大移動値
	public float savePosY;			//前のY座標を入れる（移動量を求めるため）

	public bool isUP;				//上に上がるとき
	public bool once;				//一回だけ行う処理

	new void Start()
    {
		defaultSpeedY_Value = speedY;
		//defaultSpeedX_Value = speedX;
		isUP = true;
		once = true;
		base.Start();
    }

    new void Update()
    {
		if (once)
		{
			switch (eState)
			{
				case State.Front:
					//transform.rotation = Quaternion.Euler(0, -90, 90);
					if (defaultSpeedX_Value > 0)
					{
						defaultSpeedX_Value *= -1;
					}
					break;

				case State.Behind:
					transform.rotation = Quaternion.Euler(0, 180, 0);
					if (defaultSpeedX_Value < 0)
					{
						defaultSpeedX_Value *= -1;
					}

					break;
			}
		}

		if (isUP)
		{
			savePosY = transform.position.y;

			velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, -speedZ);
			gameObject.transform.position += velocity * Time.deltaTime;

			moveY_Max -= transform.position.y - savePosY;

			if (moveY_Max < decelerationY_Start)
			{
				speedY = defaultSpeedY_Value * moveY_Max / decelerationY_Start;
			}
			else if (moveY_Max < 3)
			{
				speedZ = speedZ_Value;
				speedX = defaultSpeedX_Value;
			}
			if (transform.position.z < 0)
			{
				speedZ = 0;
				speedX = 0;
				transform.position = new Vector3(transform.position.x, transform.position.y, 0);
			}
		}
		else
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
		HSV_Change();
		base.Update();
	}
}
