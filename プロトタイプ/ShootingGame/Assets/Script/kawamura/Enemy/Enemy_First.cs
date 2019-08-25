//作成者：川村良太
//円盤形の敵のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_First : character_status
{
	public enum State
	{
		TurnUp,         //上に曲がる
		TurnDown,       //下に曲がる
		defaultStraight,
		Straight,       //生成された時(曲がらない)
	}

	public State eState;
	GameObject item;
	public GameObject parentObj;
	GameObject childObj;
	private GameObject Bullet;  //弾のプレハブ、リソースフォルダに入っている物を名前から取得。

	Vector3 velocity;
	public Vector3 defaultPos_Local;
    public Vector3 defaultPos_PlusZ;
	public Quaternion diedAttackRota;
	public Transform diedAttack_Transform;

	EnemyGroupManage groupManage;
	Find_Angle fd;
	Enemy_BurstShot ebs;
	//Renderer renderer;

	public float timeCnt = 0;                   //回転の度合い（0～59）で周期
	public float circleSpeed = 10.0f;             //移動速度
	public float radius = 1.0f;             //回転する円の大きさ
	[Header("Zポジションの移動開始位置")]
	public float ZMovePos;
	float _y;
	float _z;

	public float defaultPosY_World;
	public float defaultPosY_Local;
	public float endPosY_Local;
	public float localPosY;
	//float frame = 0;
	float straightFrame;
	float straightFrame_Default;
	public float speedX;
	public float speedX_Straight;
	public float speedY;
    public float speedZ;
    public float speedZ_Value;
	public float diedAttack_RotaZ;
	[Header("死亡時の弾発射の角度範囲()")]
	public float diedAttack_RotaValue;
	bool once = true;
	bool isTurn;
	//bool isAddition = false;
	//bool isDead = false;
	public bool haveItem = false;
	public bool Died_Attack = false;

	private void Awake()
	{
		//renderer = childObj.GetComponent<Renderer>();
		Bullet = Resources.Load("Bullet/Enemy_Bullet") as GameObject;

		straightFrame_Default = 300;
		straightFrame = straightFrame_Default;
		defaultPos_Local = transform.localPosition;
        defaultPos_PlusZ = defaultPos_Local + new Vector3(0, 0, 40);
		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
	}
	//追加したよ
	private void OnEnable()
	{
		
		//if (parentObj)
		//{
		//	if (parentObj.name != "enemy_UFO_Group")
		//	{
		//		eState = State.Straight;
		//		speedX = speedX_Straight;
		//	}
		//	else
		//	{
		//		transform.localPosition = defaultPos_Local;
		//		if (transform.position.y > 0)
		//		{
		//			//transform.localPosition = defaultPos_Local;
		//			speedX = 5;
		//			eState = State.TurnDown;
		//		}
		//		else
		//		{
		//			//transform.localPosition = defaultPos_Local;
		//			speedX = 5;
		//			eState = State.TurnUp;
		//		}
		//	}
		//}
	}


	new void Start()
	{
		item = Resources.Load("Item/Item_Test") as GameObject;
		childObj = transform.GetChild(0).gameObject;
		fd = childObj.GetComponent<Find_Angle>();
		ebs = childObj.GetComponent<Enemy_BurstShot>();
		//renderer = gameObject.GetComponent<Renderer>();

		//speedX = 5.0f;
		speedY = 5.0f;

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			if (parentObj.name == "enemy_UFO_Group")
			{
				groupManage = parentObj.GetComponent<EnemyGroupManage>();
				//transform.position = defaultPos_PlusZ;
				speedX = 5;
				//if (transform.position.y > 0)
				//{

				//	speedX = 5;
				//	eState = State.TurnDown;
				//}
				//else
				//{
				//	speedX = 5;
				//	eState = State.TurnUp;
				//}
			}
			else
			{
				eState = State.Straight;
			}
		}
		else
		{
			parentObj = GameObject.Find("TemporaryParent");
			transform.parent = parentObj.transform;
		}

		HP_Setting();
		base.Start();

	}

	new void Update()
	{
		if(once)
		{
			if (parentObj)
			{
				if (parentObj.name != "enemy_UFO_Group")
				{
					eState = State.Straight;
					speedX = speedX_Straight;
				}
				else
				{
					transform.localPosition = new Vector3(defaultPos_Local.x, defaultPos_Local.y,40.0f);

					defaultPosY_World = transform.position.y;
					defaultPosY_Local = transform.localPosition.y;

					endPosY_Local = defaultPosY_World * -0.29f;

					//transform.localPosition = defaultPos_Local;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					//transform.localPosition = new Vector3(defaultPos_Local.x, defaultPos_Local.y, 20.0f);

					//transform.localPosition = new Vector3(0, 0, 20.0f);
					//transform.localPosition = defaultPos_PlusZ;
					//transform.localPosition = defaultPos_Local;

					speedX = 5;
     //               if (transform.position.y > 0)
					//{
					//	//transform.localPosition = defaultPos_Local;
					//	speedX = 5;
     //                   speedY = 5;
					//	eState = State.TurnDown;
					//}
					//else
					//{
					//	//transform.localPosition = defaultPos_Local;
					//	speedX = 5;
     //                   speedY = 5;
					//	eState = State.TurnUp;
					//}
				}
			}
			once = false;
		}

		//移動関数呼び出し
		Move();
		HSV_Change();
		//倒されたとき
		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
			}

			if(Died_Attack)
			{
				//diedAttack_Transform = childObj.transform;
				//diedAttack_RotaZ = Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue);
				//diedAttack_Transform.rotation = Quaternion.Euler(0, 0, diedAttack_RotaZ);
				diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			}

			if (parentObj == null)
			{

			}
			else if (parentObj.name == "enemy_UFO_Group")
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
			//Reset_Status();
			//isDead = true;
			//isTurn = false;
			//straightFrame = straightFrame_Default;
			//frame = 0;
			Enemy_Reset();
			Died_Process();
		}
	}

	//---------ここから関数--------------

	//移動の関数
	void Move()
	{
		switch (eState)
		{
			case State.defaultStraight:
				if (!isTurn)
				{
					//straightFrame--;
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, -speedZ);
					gameObject.transform.position += velocity * Time.deltaTime;
					localPosY = transform.localPosition.z * (-endPosY_Local / 20.0f) + endPosY_Local;
					gameObject.transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY_Local + localPosY, transform.localPosition.z);
					//HSV_Change();

					if (transform.position.z < 0)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
						speedZ = 0;
						speedZ_Value = 0;
					}

					if (transform.localPosition.x <= ZMovePos)
					{
						speedZ = speedZ_Value;
					}
                    //if (transform.localPosition.x <= -32)
                    if (transform.localPosition.x <= -40.5)
                    {
                        //frame += Time.deltaTime;
                        //if (frame > 3)
                        //{
                        //	isTurn = true;
                        //}
                        speedX = 5;
						//isTurn = true;
					}

                    //else if (transform.localPosition.x < -21)
                    else if (transform.localPosition.x < -29.5)
                    {
                        speedX -= 0.36f;
						if (speedX < 5)
						{
							speedX = 5;
						}

					}
                    //else if (transform.localPosition.x < -9)
                    else if (transform.localPosition.x < -18.5)
                    {
						speedX += 0.12f;
					}
					//if (transform.position.x < 9)
					//{
					//	frame += Time.deltaTime;
					//	if (frame > 3)
					//	{
					//		isTurn = true;
					//	}
					//}
				}
				else if (isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
					gameObject.transform.position += velocity * Time.deltaTime;

					speedX -= 0.12f;

					if (transform.position.y > 0.5)
					{
						speedX -= 0.36f;
						speedY += 0.36f;
					}
					if (speedX < -12.0f)
					{
						speedX = -12.0f;
					}
				}
				break;

			//case State.TurnUp:
			//	if (!isTurn)
			//	{
			//		//straightFrame--;
			//		velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, -speedZ);
			//		gameObject.transform.position += velocity * Time.deltaTime;
			//		localPosY = transform.localPosition.z * (-endPosY_Local / 20.0f) + endPosY_Local;
			//		gameObject.transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY_Local + localPosY, transform.localPosition.z);
			//		//HSV_Change();

			//		if (transform.position.z < 0)
			//		{
			//			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
			//			speedZ = 0;
			//			speedZ_Value = 0;
			//		}

			//		if (transform.localPosition.x <= ZMovePos)
			//		{
			//			speedZ = speedZ_Value;
			//		}
			//		if (transform.localPosition.x <= -32)
			//		{
			//			//frame += Time.deltaTime;
			//			//if (frame > 3)
			//			//{
			//			//	isTurn = true;
			//			//}
			//			speedX = 5;
			//			//isTurn = true;
			//		}

			//		else if (transform.localPosition.x < -21)
			//		{
			//			speedX -= 0.36f;
			//			if (speedX < 5)
			//			{
			//				speedX = 5;
			//			}

			//		}
			//		else if (transform.localPosition.x < -9)
			//		{
			//			speedX += 0.12f;
			//		}
			//		//if (transform.position.x < 9)
			//		//{
			//		//	frame += Time.deltaTime;
			//		//	if (frame > 3)
			//		//	{
			//		//		isTurn = true;
			//		//	}
			//		//}
			//	}
			//	else if (isTurn)
			//	{
			//		velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
			//		gameObject.transform.position += velocity * Time.deltaTime;

			//		speedX -= 0.12f;

			//                 if (transform.position.y > 0.5)
			//                 {
			//                     speedX -= 0.36f;
			//                     speedY += 0.36f;
			//                 }
			//                 if (speedX < -12.0f)
			//                 {
			//                     speedX = -12.0f;
			//                 }
			//             }
			//             break;

			//case State.TurnDown:
			//	if (!isTurn)
			//	{
			//		velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, -speedZ);
			//		gameObject.transform.position += velocity * Time.deltaTime;
			//		localPosY = transform.localPosition.z * (-endPosY_Local / 20.0f) + endPosY_Local;
			//		gameObject.transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY_Local + localPosY, transform.localPosition.z);

			//		//HSV_Change();
			//		if (transform.position.z < 0)
			//		{
			//			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
			//			speedZ = 0;
			//			speedZ_Value = 0;
			//		}

			//		if (transform.localPosition.x <= ZMovePos)
			//		{
			//			speedZ = speedZ_Value;
			//		}

			//		if (transform.localPosition.x <= -32)
			//		{
			//			//frame += Time.deltaTime;
			//			//if (frame > 3)
			//			//{
			//			//	isTurn = true;
			//			//}
			//			speedX = 5;
			//			//isTurn = true;
			//		}
			//		else if (transform.localPosition.x < -21)
			//		{
			//			speedX -= 0.36f;
			//			if (speedX < 5)
			//			{
			//				speedX = 5;
			//			}
			//		}

			//		else if (transform.localPosition.x < -9)
			//		{
			//			speedX += 0.12f;
			//		}

			//		//if (transform.position.x < 9)
			//		//{
			//		//	frame += Time.deltaTime;
			//		//}
			//		//if (frame > 3)
			//		//{
			//		//	isTurn = true;
			//		//}
			//	}
			//	else if (isTurn)
			//	{
			//		velocity = gameObject.transform.rotation * new Vector3(speedX, -speedY, 0);
			//		gameObject.transform.position += velocity * Time.deltaTime;

			//		speedX -= 0.12f;

   //                 if (transform.position.y < -0.5)
   //                 {
   //                     speedX -= 0.36f;
   //                     speedY += 0.36f;
   //                 }
			//		if (speedX < -12.0f)
			//		{
			//			speedX = -12.0f;
			//		}
			//	}
			//	break;

			case State.Straight:
				speedX = speedX_Straight;
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;
		}
	}

	//状態を変える(主に出現時に曲がらせたくないときに使うと思われます)
	public void SetState(int num)
	{
		switch (num)
		{
			//上に曲がる
			case 0:
				eState = State.TurnUp;
				break;

			//下に曲がる
			case 1:
				eState = State.TurnDown;
				break;

			//直進
			case 2:
				eState = State.Straight;
				break;
		}
	}
	void Enemy_Reset()
	{
		//frame = 0;
		straightFrame = straightFrame_Default;
		speedZ_Value = 50;
		once = true;
		isTurn = false;
		Is_Dead = false;
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallUnder" || col.gameObject.name == "WallTop")
		{
			if (parentObj)
			{
				if (parentObj.name == "enemy_UFO_Group")
				{
					groupManage.notDefeatedEnemyCnt++;
					groupManage.remainingEnemiesCnt -= 1;
				}
			}
			//frame = 0;
			Enemy_Reset();
			gameObject.SetActive(false);

		}
		else if(eState==State.defaultStraight&& col.gameObject.name == "WallLeft")
		{
			if (parentObj)
			{
				if (parentObj.name == "enemy_UFO_Group")
				{
					groupManage.notDefeatedEnemyCnt++;
					groupManage.remainingEnemiesCnt -= 1;
				}
			}

			Enemy_Reset();
			gameObject.SetActive(false);

		}
		else if (eState == State.Straight && (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallRight"))
		{
			//frame = 0;
			Enemy_Reset();
			gameObject.SetActive(false);
		}
	}
}
