using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wave : MonoBehaviour
{
	public enum State
	{
		WaveUp,
		WaveDown,
		WaveOnlyUp,
		WaveOnlyDown,
	}

	public State eState;
	public float speedX;
	public float speedY;
	public float speedZ;
	public float speedZ_Value;
	public float amplitude;

	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値

	private bool isAddSpeedY = false;   //Yスピードを増加させるかどうか
	private bool isSubSpeedY = false;   //Yスピードを減少させるかどうか

	public float sin;

	float posX;
	float posY;
	float posZ;
	float defPosX;

	float scaleX;
	float scaleY;
	float scaleZ;
	float scale_Value;

	Vector3 velocity;

	bool once = true;
	bool isBig = false;
	bool isWave = false;
	//---------------------------------------------------------

	void Start()
	{
		speedZ = 0;
		speedZ_Value = 8.0f;
		scale_Value	= 0.27f;
		scaleX = 0.25f;
		scaleY = scale_Value;
		scaleZ = scale_Value;
		transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

		posX = transform.position.x;
		posY = transform.position.y;
		posZ = -5.0f;
		defPosX = (13.0f - transform.position.x) / 120.0f;         //13.0fはとりあえず敵が右へ向かう限界の座標
	}

	void Update()
	{
		if(once)
		{
			switch(eState)
			{
				case State.WaveUp:
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					break;

				case State.WaveDown:
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					break;

				case State.WaveOnlyUp:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					transform.localScale = new Vector3(1.5f, 1.0f, 1.0f);

					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					speedY = defaultSpeedY / 2;
					speedX = 5;
					speedZ_Value = 0;
					isWave = true;
					isAddSpeedY = true;
					break;

				case State.WaveOnlyDown:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					transform.localScale = new Vector3(1.5f, 1.0f, 1.0f);

					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					speedY = defaultSpeedY / 2;
					speedX = 5;
					speedZ_Value = 0;
					isWave = true;
					isSubSpeedY = true;
					break;
			}
			once = false;
		}

		if (!isWave)
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
			gameObject.transform.position += velocity * Time.deltaTime;
			if (transform.position.z < 0)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
			}

			if (transform.position.x > 13)
			{
				speedX = 5;
				speedY = defaultSpeedY;

				isWave = true;
			}
			else if (transform.position.x > 8)
			{
				speedZ = speedZ_Value;
				isBig = true;
			}
			if(transform.position.x>10)
			{
				speedX *= 0.95f;
			}
		}
		else if(isWave)
		{
			sin =posY + Mathf.Sin(Time.time*5);

			SpeedY_Check();
			SpeedY_Calculation();

			//this.transform.position = new Vector3(transform.position.x, sin, 0);
			//transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.frameCount*0.1f), transform.position.z);
			velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}

		if (isBig)
		{
			scale_Value += 0.005f;
			scaleX += 0.001f;
			
			if(scaleX>0.3f)
			{
				scaleX = 1.5f;
			}
			if (scale_Value > 1)
			{
				scale_Value = 1;
				isBig = false;

			}
			transform.localScale = new Vector3(scaleX, scale_Value, scale_Value);

		}

	}
	//Yスピードを見てYスピードを増加させるか減少させるかを決める
	void SpeedY_Check()
	{
		if (defaultSpeedY > 0)
		{
			//スピードが初期値以上になった時
			if (speedY >= defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY <= -defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;

			}

		}
		else if (defaultSpeedY < 0)
		{
			//スピードが初期値以上になった時
			if (speedY >= -defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY <= defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;

			}

		}


	}

	//スピードを増減させる
	void SpeedY_Calculation()
	{
		//増加がtrueなら
		if (isAddSpeedY)
		{
			//Yスピードを増加
			speedY += addAndSubValue;
		}
		//減少がtrueなら
		else if (isSubSpeedY)
		{
			//Yスピードを減少
			speedY -= addAndSubValue;
		}

	}

}