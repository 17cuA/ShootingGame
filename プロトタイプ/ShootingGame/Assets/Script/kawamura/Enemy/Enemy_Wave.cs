﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wave : character_status
{
	public enum State
	{
		WaveUp,
		WaveDown,
		WaveOnlyUp,
		WaveOnlyDown,
		Straight,
	}
	public State eState;

	GameObject childObj;
	GameObject item;
	GameObject parentObj;
	//GameObject blurObj;

	HSVColorController hsvCon;
	Color hsvColor;
	//BlurController blurCon;
	EnemyGroupManage groupManage;
	VisibleCheck vc;

	Vector3 velocity;

	//----------
	public Vector3 startMarker;
	public Vector3 endMarker;

	public float testSpeed = 1.0f;

	private float distance_two;
	//----------

	public float speedX;
	public float speedY;
	public float speedZ;
	public float speedZ_Value;
	float startPosY;
	public float amplitude;

	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値


	public float sin;

	float posX;
	float posY;
	float posZ;
	float defPosX;
	float val_Value;
	float sigma_Value;


	public bool isAddSpeedY = false;   //Yスピードを増加させるかどうか
	public bool isSubSpeedY = false;   //Yスピードを減少させるかどうか

	public bool once = true;
	public bool isWave = false;
	public bool isStraight = false;
	public bool isOnlyWave;

	public bool isSlerp = false;
	public bool susumimasu=true;
	public bool isNoSlerp=false;
	float present_Location = 0;
	//---------------------------------------------------------

	void Start()
	{
		startMarker = new Vector3(-26.0f, transform.position.y, 38.0f);
		endMarker = new Vector3(13.0f, transform.position.y, 0);
		distance_two= Vector3.Distance(startMarker, endMarker);

		childObj = transform.GetChild(0).gameObject;
		hsvColor = childObj.GetComponent<Renderer>().material.color;
		hsvCon = childObj.GetComponent<HSVColorController>();
		val_Value = 0.025f;

		//blurObj = transform.GetChild(1).gameObject;
		//blurCon = blurObj.GetComponent<BlurController>();
		sigma_Value = 0.1f;

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			groupManage = parentObj.GetComponent<EnemyGroupManage>();
		}
        else
        {
            parentObj = GameObject.Find("TemporaryParent");
            transform.parent = parentObj.transform;
        }

        speedZ = 0;

		posX = transform.position.x;
		startPosY = transform.position.y;
		posZ = -5.0f;
		defPosX = (13.0f - transform.position.x) / 120.0f;         //13.0fはとりあえず敵が右へ向かう限界の座標

		HP_Setting();
	}

	void Update()
	{
		//if (transform.childCount == 0)
		//{
		//	Destroy(this.gameObject);
		//}
		if(once)
		{
			//状態によって値を変える
			switch(eState)
			{
				case State.WaveUp:
					isStraight = false;
					isOnlyWave = false;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					isSubSpeedY = true;
					isAddSpeedY = false;
					speedX = 15;
					speedZ_Value = 38;
					transform.position = new Vector3(transform.position.x, transform.position.y, 38.0f);
					//hsvCon.val = 0.4f;
					hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);

					break;

				case State.WaveDown:
					isStraight = false;
					isOnlyWave = false;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					isAddSpeedY = true;
					isSubSpeedY = false;
					speedX = 16;
					speedZ_Value = 38;
					transform.position = new Vector3(transform.position.x, transform.position.y, 38.0f);
					//hsvCon.val = 0.4f;
					hsvColor = UnityEngine.Color.HSVToRGB(1, 1, 0.4f);

					break;

				case State.WaveOnlyUp:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					speedY = defaultSpeedY;
					amplitude = 0.1f;
					speedX = 5;
					speedZ_Value = 0;
					isStraight = false;
					isOnlyWave = true;
					//isWave = true;
					isAddSpeedY = true;
					hsvCon.val = 1.0f;

					break;

				case State.WaveOnlyDown:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					speedY = defaultSpeedY;
					amplitude = -0.1f;
					speedX = 5;
					speedZ_Value = 0;
					isOnlyWave = true;
					//isWave = true;
					isStraight = false;
					isSubSpeedY = true;
					hsvCon.val = 1.0f;

					break;

				case State.Straight:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isStraight = true;
					speedX = 5;
					amplitude = 0;
					hsvCon.val = 1.0f;


					break;
			}
			once = false;
		}

		if(isStraight)
		{
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
		else if (isOnlyWave)
		{
			transform.position = new Vector3(transform.position.x, startPosY + Mathf.Sin(Time.frameCount * amplitude), transform.position.z);
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
		else if (!isWave)
		{
			if(!isSlerp&&!isNoSlerp)
			{
				velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
				gameObject.transform.position += velocity * Time.deltaTime;
			}
			if (isNoSlerp)
			{
				velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
				gameObject.transform.position += velocity * Time.deltaTime;
				if (transform.position.z < 0)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
				}

				if (transform.position.x > 7)
				{
					//speedX -= 0.25f;
					speedX *= 0.965f;
				}

				if (transform.position.x > 13)
				{
					speedX = 5;
					speedY = defaultSpeedY;

					isWave = true;
				}
				else if (transform.position.x > 7)
				{
					
					//speedZ = speedZ_Value;
					hsvCon.val += val_Value;
					if (hsvCon.val > 1.0f)
					{
						hsvCon.val = 1.0f;
					}

					//blurCon.sigma -= sigma_Value;
					//if (blurCon.sigma <= 0)
					//{
					//	blurCon.sigma = 0.1f;
					//}
				}
				else if (transform.position.x > 1)
				{
					//blurCon.sigma -= sigma_Value;

					speedZ = speedZ_Value;
				}
			}
			else if(isSlerp)
			{
				if(susumimasu)
				{
					velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.position.x > -26)
					{
						susumimasu = false;
					}
				}
				else
				{
					// 現在の位置
					float present_Location = (Time.time * testSpeed) / distance_two;

					// オブジェクトの移動(ここだけ変わった！)
					transform.position = Vector3.Slerp(startMarker, endMarker, present_Location);
				}
			}
		}
		else if(isWave)
		{
			speedX = 5;
			//sin =posY + Mathf.Sin(Time.time*5);

			SpeedY_Check();
			SpeedY_Calculation();

			//this.transform.position = new Vector3(transform.position.x, sin, 0);
			//transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.frameCount * 0.1f), transform.position.z);
			velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}

		if (hp < 1)
		{
			if (parentObj)
			{
                if(parentObj.name!= "TemporaryParent")
                {
				    //群を管理している親の残っている敵カウントマイナス
				    groupManage.remainingEnemiesCnt--;
				    //倒された敵のカウントプラス
				    groupManage.defeatedEnemyCnt++;
				    //群に残っている敵がいなくなったとき
				    if (groupManage.remainingEnemiesCnt == 0)
				    {
					    //倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
					    if (groupManage.notDefeatedEnemyCnt == 0 && groupManage.isItemDrop)
					    {
						    //アイテム生成
						    //Instantiate(item, this.transform.position, transform.rotation);
					    }
					    //一体でも倒されていなかったら
					    else
					    {
						    //なにもしない
					    }
					    groupManage.itemPos = transform.position;
					    groupManage.itemTransform = this.transform;
				    }
                }
            }
			Died_Process();

			speedZ = 0;
			hsvCon.val = 0.4f;
			once = true;
			isWave = false;


			//Reset_Status();
			Died_Process();
		}
	}
	//-------------ここから関数------------------

	//Yスピードを見てYスピードを増加させるか減少させるかを決める
	void SpeedY_Check()
	{
		if (defaultSpeedY >= 0)
		{
			//スピードが初期値以上になった時
			if (speedY > defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < -defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;

			}

		}
		else if (defaultSpeedY < 0)
		{
			//スピードが初期値以上になった時
			if (speedY > -defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < defaultSpeedY)
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

	public void SetState(int n)
	{
		switch(n)
		{
			case 1:
				eState = State.WaveUp;
				break;

			case 2:
				eState = State.WaveDown;
				break;

			case 3:
				eState = State.WaveOnlyUp;
				break;

			case 4:
				eState = State.WaveOnlyDown;
				break;

			case 5:
				eState = State.Straight;
				break;

		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallLeft")
		{
			groupManage.notDefeatedEnemyCnt++;
			groupManage.remainingEnemiesCnt -= 1;
			gameObject.SetActive(false);
		}
	}

}