﻿//作成者：川村良太
//画面奥から来たり上下移動をしながら来る敵（闘牛型）のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Wave : character_status
{
	public enum State
	{
		WaveUp,
		WaveDown,
		WaveOnlyUp,
		WaveOnlyDown,
		Rush,
		Straight,
	}
	public State eState;

	GameObject childObj;        //子供入れる
	public GameObject childObj_Shot;
	GameObject childObj_Angle;
	GameObject item;			//アイテム入れる
	GameObject parentObj;		//親入れる（群れの時のため）
	//GameObject blurObj;

	Renderer renderer;			//レンダラー
	//HSVColorController hsvCon;	//シェーダー用
	//Color hsvColor;
	//BlurController blurCon;
	EnemyGroupManage groupManage;			//群れの時の親スクリプト
	Find_Angle fd;
	Find_Angle fd_Rush;
	//public ParticleSystem sonicBoom;			//ジェット噴射の衝撃波のようなパーティクル

	Vector3 velocity;
	Vector3 defaultPos;
	public Quaternion diedAttackRota;

	//----------
	public Vector3 startMarker;
	public Vector3 endMarker;
	float startTime;
	public float slaep_IncValue;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;
	//----------

	public float speedX;			//Xスピード
	public float speedY;			//Yスピード
	public float speedZ;			//Zスピード（移動時）
	public float speedZ_Value;		//Zスピードの値だけ
	float startPosY;                //最初のY座標値
	float rotaY;					//Y角度
	public float amplitude;         //画面奥から出てこない時の上下の振れ幅
	float rushStayCnt;
	public float rushStayCntMax;

	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値

	public float sin;
	[Header("死亡時の弾発射の角度範囲()")]
	public float diedAttack_RotaValue;

	//float posX;
	//float posY;
	//float posZ;
	//float defPosX;
	//float val_Value;					//テクスチャの明るさの増える値
	//float sigma_Value;					//ブラーのぼやけ具合の値（0でぼやけなし）
	//public float h_Value;
	//public float s_Value;

	//public float v_Value;

	public bool isAddSpeedY = false;	//Yスピードを増加させるかどうか
	public bool isSubSpeedY = false;	//Yスピードを減少させるかどうか

	public bool once = true;			//updateで一回だけ呼び出す処理用
	public bool isWave = false;			//奥からくる敵を上下移動に変える用
	public bool isStraight = false;		//直進かどうか
	public bool isOnlyWave;             //上下移動のみか（左へ進みながら）
	public bool haveItem = false;

	public bool isSlerp = false;
	//public bool susumimasu=true;
	public bool isNoSlerp=false;
	bool isSonicPlay = false;
    public bool utsuttemasuyo=false;
    bool isWaveStart = false;
	public bool Died_Attack = false;

	//float present_Location = 0;
	//---------------------------------------------------------

	private void Awake()
	{
		//sonicBoom.Stop();
		isSonicPlay = false;
		defaultPos = transform.localPosition;

		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
		//childCnt = transform.childCount;
	}
	private void OnEnable()
	{
		transform.localPosition = defaultPos;
		startMarker = new Vector3(12.0f, transform.position.y, 40.0f);
		endMarker = new Vector3(12.0f, transform.position.y, 0);

	}

	new void Start()
	{
		//startMarker = new Vector3(12.0f, transform.position.y, 40.0f);
		endMarker = new Vector3(12.0f, transform.position.y, 0);
		distance_two= Vector3.Distance(startMarker, endMarker);
		item = Resources.Load("Item/Item_Test") as GameObject;

		childObj = transform.GetChild(0).gameObject;            //モデルオブジェクトの取得（3Dモデルを子供にしているので）
		childObj_Shot = transform.GetChild(1).gameObject;
		childObj_Angle = transform.GetChild(2).gameObject;
		//childCnt = transform.childCount;
		renderer = childObj.GetComponent<Renderer>();
		fd = childObj_Shot.GetComponent<Find_Angle>();
		fd_Rush = childObj_Angle.GetComponent<Find_Angle>();
		//hsvColor = childObj.GetComponent<Renderer>().material.color;
		//hsvCon = childObj.GetComponent<HSVColorController>();
		//val_Value = 0.025f;

		//blurObj = transform.GetChild(1).gameObject;
		//blurCon = blurObj.GetComponent<BlurController>();
		//sigma_Value = 0.1f;

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
		//posX = transform.position.x;
		startPosY = transform.position.y;
		//posZ = -5.0f;
		//defPosX = (13.0f - transform.position.x) / 120.0f;         //13.0fはとりあえず敵が右へ向かう限界の座標
		startTime = 0.0f;
		HP_Setting();
		base.Start();
	}

	new void Update()
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
					speedX = 18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
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
					speedX = 18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(1, 1, 0.4f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
					break;

				case State.WaveOnlyUp:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
                    //speedY = defaultSpeedY;
                    speedY = 0;
					amplitude = 0.1f;
					speedX = 5;
					speedZ_Value = 0;
					isStraight = false;
					isOnlyWave = true;
					//isWave = true;
					isAddSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

				case State.WaveOnlyDown:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
                    //speedY = defaultSpeedY;
                    speedY = 0;
					amplitude = -0.1f;
					speedX = 5;
					speedZ_Value = 0;
					isOnlyWave = true;
					//isWave = true;
					isStraight = false;
					isSubSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

				case State.Rush:
					isStraight = false;
					isOnlyWave = false;
					speedX = 18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();

					break;

				case State.Straight:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isStraight = true;
					speedX = 5;
					amplitude = 0;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;
			}
			once = false;
		}


		if (eState == State.Straight)
		{
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
		}
		//else if (eState == State.Rush)
		//{

		//}
		else if (eState == State.WaveOnlyUp || eState == State.WaveOnlyDown)
        {
            
            speedX = 5;
            //sin =posY + Mathf.Sin(Time.time*5);


            if (transform.position.x < 20 && transform.position.z == 0)
            {

                if(!isWaveStart)
                {
                    speedY = defaultSpeedY;
                    isWaveStart = true;
                }

            }

            if(isWaveStart)
            {
                SpeedY_Check();
                SpeedY_Calculation();

            }
            else
            {
                speedY = 0;
            }
            //transform.position = new Vector3(transform.position.x, startPosY + Mathf.Sin(Time.frameCount * amplitude), transform.position.z);
            velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);

            //velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
            gameObject.transform.position += velocity * Time.deltaTime;

        }
        else if (!isWave)
        {
            if (isSlerp)
            {
                //if (transform.position.x < 12)
                //{
                //	velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
                //	gameObject.transform.position += velocity * Time.deltaTime;
                //	if (transform.position.x >= 12)
                //	{
                //		transform.position = new Vector3(12.0f, transform.position.y, 40.0f);
                //	}
                //}
                //else if(transform.position.x>=12.0f)
                //{
                //if (isSonicPlay)
                //{
                //    //sonicBoom.Stop();
                //    isSonicPlay = false;
                //}
                present_Location = (Time.time * testSpeed) / distance_two;
                transform.position = Vector3.Slerp(startMarker, endMarker, startTime);
                startTime += slaep_IncValue;
                if (startTime > 1)
                {
                    startTime = 1;
                }
                //startTime++;
                HSV_Change();

                if (transform.position == endMarker)
                {
                    isWave = true;
					transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    speedX = 5;
                    speedY = defaultSpeedY;

                }
                //}
            }
            else if (isNoSlerp)
            {
                velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
                gameObject.transform.position += velocity * Time.deltaTime;
                if (transform.position.z < 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
                }

                if (transform.position.x >= 12)
                {
                    isSlerp = true;
					startMarker = transform.position;
                    //sonicBoom.Play();
                    isSonicPlay = true;
                }

                if (transform.position.x > 7)
                {
                    //speedX -= 0.25f;
                    //speedX *= 0.965f;

                    //speedZ = speedZ_Value;
                    //hsvCon.val += val_Value;

                    //明るさを変える関数
                    HSV_Change();

                    //if (hsvCon.val > 1.0f)
                    //{
                    //	hsvCon.val = 1.0f;
                    //}

                    //blurCon.sigma -= sigma_Value;
                    //if (blurCon.sigma <= 0)
                    //{
                    //	blurCon.sigma = 0.1f;
                    //}
                }
                if (transform.position.z <= 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    speedX = 5;
                    speedY = defaultSpeedY;
                    isWave = true;
                }

                //if (transform.position.x > 13)
                //{
                //	speedX = 5;
                //	speedY = defaultSpeedY;
                //	isWave = true;
                //}
                //else if (transform.position.x > 7)
                //{

                //	//speedZ = speedZ_Value;
                //	//hsvCon.val += val_Value;
                //	v_Value += val_Value;

                //	if (val_Value > 1.0f)
                //	{
                //		v_Value = 1.0f;
                //	}

                //	//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
                //	renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);

                //	//if (hsvCon.val > 1.0f)
                //	//{
                //	//	hsvCon.val = 1.0f;
                //	//}

                //	//blurCon.sigma -= sigma_Value;
                //	//if (blurCon.sigma <= 0)
                //	//{
                //	//	blurCon.sigma = 0.1f;
                //	//}
                //}
                else if (transform.position.x > 1)
                {
                    //blurCon.sigma -= sigma_Value;
                    //speedZ = speedZ_Value;
                }
            }
            //else if(isSlerp)
            //{
            //	if(susumimasu)
            //	{
            //		velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
            //		gameObject.transform.position += velocity * Time.deltaTime;
            //		if (transform.position.x > -26)
            //		{
            //			susumimasu = false;
            //		}
            //	}
            //	else
            //	{
            //		// 現在の位置
            //		float present_Location = (Time.time * testSpeed) / distance_two;

            //		// オブジェクトの移動(ここだけ変わった！)
            //		transform.position = Vector3.Slerp(startMarker, endMarker, present_Location);
            //	}
            //}
        }
        else if (isWave)
		{
			if (eState == State.Rush)
			{
				rushStayCnt++;
				if (rushStayCnt > rushStayCntMax)
				{
					
				}
			}
			else
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
		}

		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);

			}
			if(Died_Attack)
			{
				diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			}

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
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
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
			Enemy_Reset();
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
	//明るさを変える関数
	//void HSV_Change()
	//{
	//	v_Value = 1.0f - transform.position.z * 0.015f;

	//	if (v_Value > 1.0f)
	//	{
	//		v_Value = 1.0f;
	//	}

	//	renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
	//}

	void Enemy_Reset()
	{
		speedZ = 0;
		once = true;
		isSlerp = false;
		isWave = false;
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