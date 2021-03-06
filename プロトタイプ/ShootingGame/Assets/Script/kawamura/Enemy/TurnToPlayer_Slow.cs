﻿//作成者：川村良太
//プレイヤーを追従する敵（ハエ型）のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class TurnToPlayer_Slow : character_status
{
    public enum State
    {
        Nomal,
        Back,
    }

    public State eState;

	public GameObject playerObj; // 注視したいオブジェクトをInspectorから入れておく
	GameObject item;
	DropItem dItem;

	List<GameObject> colList = new List<GameObject>();

	Vector3 dif;            //対象と自分の座標の差を入れる変数
	Vector3 velocity;

	//public float speed;
	public float speedX;
	public float rollSpeed;

	public float rotaX;
    public float rotaY;
    public float rotaZ;

	float radian;           //ラジアン
	public float degree;    //角度
	public float degree_plus;
    public int rollDelay;

	public float Zcheck;
	public float rotationZ_Inc;
	public float rotationZ_Dec;

	public float frameCnt;
	public float followStartTime;
    public int followTimeCnt;
	public int followEndTime;

	public float saveDeg;
	public float saveDig_plus;

	bool isFollow = false;
	bool once;
	public bool isInc = false;
	public bool isDec = false;
	//bool isPositive;
	//bool isNegative;
	bool isPlus;
	bool isMinus;
	//bool isCCCCC = false;
    bool isDelay = false;
	bool haveItem = false;
	private void Awake()
	{
		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
	}

	new private void Start()
	{
		rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;

		once = true;
		frameCnt = 0;
		saveDeg = 180;

        //if (eState == State.Nomal)
        //{
        //    rotaZ = 0;

        //    transform.rotation = Quaternion.Euler(rotaX, rotaY, rotaZ);

        //}
        //if (eState == State.Back)
        //{
        //    rotaZ = 180;

        //    transform.rotation = Quaternion.Euler(rotaX, rotaY, -rotaZ);

        //}
        //transform.rotation = Quaternion.Euler(rotaX, rotaY, rotaZ);

		HP_Setting();
		base.Start();

	}
	new void Update()
	{

        frameCnt++;
		if(frameCnt> followStartTime)
		{
			isFollow = true;
			frameCnt = 0;
		}
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}

        velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -0);
        gameObject.transform.position += velocity * Time.deltaTime;

        //プレイヤーの方向へ向く角度の値を更新前の値として保存
        saveDig_plus = degree_plus;
		//プレイヤーの方向角度の値更新
		DegreeCalculation();

		//角度の値が増えたか減ったかを判定
		if ((saveDig_plus-degree_plus>350 || degree_plus > saveDig_plus) && saveDig_plus - degree_plus < 0)
		{
			isPlus = true;
			isMinus = false;

		}
		else if ((degree_plus - saveDig_plus>350 || degree_plus < saveDig_plus))
		{
			isMinus = true;
			isPlus = false;

		}

		//追従を始める
		if (isFollow)
		{
			//一回のみ行う
			if (once)
			{

                if (eState == State.Nomal)
                {
                    if (playerObj.transform.position.y > transform.position.y)
                    {
                        isDec = true;
                        isInc = false;

                    }
                    else
                    {
                        isInc = true;
                        isDec = false;

                    }
                }
                else if (eState == State.Back)
                {
                    if (playerObj.transform.position.y > transform.position.y)
                    {
                        isDec = false;
                        isInc = true;

                    }
                    else
                    {
                        isInc = false;
                        isDec = true;

                    }

                }
                once = false;
			}
            followTimeCnt++;
		}

        if (followTimeCnt > followEndTime)
        {
            isFollow = false;
            isInc = false;
            isDec = false;
        }

		//角度が増えているとき（向く方向が今の角度より大きい）
		if (isInc)
		{
            //rollSpeed += 0.5f;
            //if (rollSpeed > 2.5f)
            //{
            //    rollSpeed = 2.5f;
            //}
			//角度を増やす
			transform.Rotate(0, 0, rollSpeed);

            //if (transform.eulerAngles.z <= 180)
            //{
            //    isPositive = true;
            //    isNegative = false;
            //}
            //else if (transform.eulerAngles.z > 180)
            //{
            //    isNegative = true;
            //    isPositive = false;
            //}
        }
        else if (isDec)
		{
			transform.Rotate(0, 0, -rollSpeed);
			//if (transform.eulerAngles.z <= 180)
			//{
			//	isPositive = true;
			//	isNegative = false;
			//}
			//else if (transform.eulerAngles.z > 180)
			//{
			//	isNegative = true;
			//	isPositive = false;
			//}
        }

		rotationZ_Inc = transform.eulerAngles.z + 3;
		if (rotationZ_Inc > 360)
		{
			//rotationZ_Inc -= 360;
		}

		rotationZ_Dec = transform.eulerAngles.z - 3;
		if (rotationZ_Dec < 0)
		{
			//rotationZ_Dec += 360;
		}

		Zcheck = transform.eulerAngles.z+5;

		if(!isInc && !isDec)
		{
			if(isFollow)
			{
				//if(degree_plus>transform.eulerAngles.z+3)
				//{
				//	isInc = true;
				//}

				//if (degree_plus - transform.eulerAngles.z > 180)
				//{
				//	isDec = true;
				//	isInc = false;
				//}
				if ((degree_plus < rotationZ_Dec && isMinus) || degree_plus < rotationZ_Inc)
				{

                    isDelay = true;
					//isDec = true;
					//isInc = false;


				}
				else if ((degree_plus > rotationZ_Inc && isPlus) || degree_plus > rotationZ_Dec)
				{
                    isDelay = true;
					//isInc = true;
					//isDec = false;
					//isCCCCC = false;
				}

				//else if (degree_plus > transform.eulerAngles.z + 3)
				//{
					
				//	isInc = true;
				//	isDec = false;
				//}
			}
		}

        if(isFollow && isDelay)
        {
            rollDelay++;
            if (rollDelay > 8)
            {
                if ((degree_plus > transform.eulerAngles.z || transform.eulerAngles.z - degree_plus > 180))
                {
                    isInc = true;
                    isDec = false;

                    if (degree_plus > 180 && transform.eulerAngles.z < 60)
                    {
                        isDec = true;
                        isInc = false;
                    }
                }
                else if (degree_plus < transform.eulerAngles.z || degree_plus - transform.eulerAngles.z > 180)
                {
                    isDec = true;
                    isInc = false;
                    if (degree_plus < 60 && transform.eulerAngles.z > 180)
                    {
                        isInc = true;
                        isDec = false;
                    }
                }
                //rollDelay = 0;
                //isDelay = false;
            }
        }
		
		if (saveDig_plus - 3 <= transform.eulerAngles.z && saveDig_plus + 3 >= transform.eulerAngles.z)
		{
			if(isFollow)
			{
                //rollSpeed = 0;
				isInc = false;
				isDec = false;
                isDelay = false;
                rollDelay = 0;
			}
		}

		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
			}
			frameCnt = 0;
			followTimeCnt = 0;
			once = true;
			isDec = false;
			isInc = false;
			Reset_Status();
			Died_Process();
		}

	}
	void DegreeCalculation()
	{
		//座標の差を入れる
		dif = playerObj.transform.position - transform.position;

		//ラジアンを求める
		radian = Mathf.Atan2(dif.y, dif.x);

		//角度を求める
		degree = radian * Mathf.Rad2Deg;

		//if (colList.Count > 0)
		//{
		//	foreach(GameObject checkObj in colList)
		//	{
		//		if(checkObj.tag=="Player")
		//		{
		//			if (isFollow)
		//			{
		//				if (isPlus)
		//				{
		//					isDec = false;
		//					isInc = true;
		//				}
		//				else if (isMinus)
		//				{
		//					isInc = false;
		//					isDec = true;

		//				}
		//			}
		//		}
		//	}
		//}

		if (degree < 0)
		{
			degree_plus = degree + 360.0f;
		}
		else
		{
			degree_plus = degree;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "BattleshipType_Enemy(Clone)")
		{
			hp = 0;
		}
		if(col.gameObject.tag=="Player_Bullet")
		{
			hp = 0;
		}

	}
	//   private void OnTriggerStay(Collider col)
	//   {
	//	colList.Add(col.gameObject);
	//	if (col.gameObject.tag=="Player")
	//       {
	//		isInc = false;
	//		isDec = false;

	//       }  
	//   }

	//private void OnTriggerExit(Collider col)
	//{
	//	colList.Remove(col.gameObject);
	//	if (col.gameObject.tag == "Player")
	//	{
	//		if(isFollow)
	//		{
	//			if (isPlus)
	//			{
	//				isDec = false;
	//				isInc = true;
	//			}
	//			else if (isMinus)
	//			{
	//				isInc = false;
	//				isDec = true;

	//			}
	//		}
	//	}
	//}
}

