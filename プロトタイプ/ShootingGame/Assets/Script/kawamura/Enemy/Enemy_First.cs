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
		Straight,       //生成された時(曲がらない)
	}

	public State eState;

	Vector3 velocity;
	public Vector3 defaultPos;

	GameObject item;
	public GameObject parentObj;
	GameObject childObj;

	EnemyGroupManage groupManage;
	VisibleCheck vc;

	//Renderer renderer;

	public float timeCnt = 0;                   //回転の度合い（0～59）で周期
	public float circleSpeed = 10.0f;             //移動速度
	public float radius = 1.0f;             //回転する円の大きさ
	float _y;
	float _z;

	float frame = 0;
	float straightFrame;
	float straightFrame_Default;
	public float speedX;
	public float speedX_Straight;
	public float speedY;

	bool isTurn;
	bool isAddition = false;
	bool isDead = false;
	public bool haveItem = false;

	private void Awake()
	{
		straightFrame_Default = 300;
		straightFrame = straightFrame_Default;
		defaultPos = transform.localPosition;
		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
	}

	private void OnEnable()
	{
		
		if (parentObj)
		{
			if (parentObj.name != "enemy_UFO_Group")
			{
				eState = State.Straight;
				speedX = speedX_Straight;
			}
			else if (parentObj.transform.position.y > 0)
			{
				transform.localPosition = defaultPos;
				speedX = 5;
				eState = State.TurnDown;
			}
			else
			{
				transform.localPosition = defaultPos;
				speedX = 5;
				eState = State.TurnUp;
			}
		}
	}


	new void Start()
	{
		item = Resources.Load("Item/Item_Test") as GameObject;
		childObj = transform.GetChild(0).gameObject;
		vc = childObj.GetComponent<VisibleCheck>();
		//renderer = gameObject.GetComponent<Renderer>();

		//speedX = 5.0f;
		speedY = 5.0f;

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			if (parentObj.name == "enemy_UFO_Group")
			{
				groupManage = parentObj.GetComponent<EnemyGroupManage>();

				if (parentObj.transform.position.y > 0)
				{
					speedX = 5;
					eState = State.TurnDown;
				}
				else
				{
					speedX = 5;
					eState = State.TurnUp;
				}
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


	void Update()
	{
		//倒されたとき
		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
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
		//移動関数呼び出し
		Move();
	}

	//---------ここから関数--------------

	//移動の関数
	void Move()
	{
		switch (eState)
		{
			case State.TurnUp:
				if (!isTurn)
				{
					straightFrame--;
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.localPosition.x <= -29)
					{
						//frame += Time.deltaTime;
						//if (frame > 3)
						//{
						//	isTurn = true;
						//}
						isTurn = true;
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

					if (speedX < -5.0f)
					{
						speedX = -5.0f;
					}
				}
				break;

			case State.TurnDown:
				if (!isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.localPosition.x <= -29)
					{
						//frame += Time.deltaTime;
						//if (frame > 3)
						//{
						//	isTurn = true;
						//}
						isTurn = true;
					}

					//if (transform.position.x < 9)
					//{
					//	frame += Time.deltaTime;
					//}
					//if (frame > 3)
					//{
					//	isTurn = true;
					//}
				}
				else if (isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(speedX, -speedY, 0);
					gameObject.transform.position += velocity * Time.deltaTime;

					speedX -= 0.12f;

					if (speedX < -5.0f)
					{
						speedX = -5.0f;
					}
				}
				break;

			case State.Straight:
				speedX = speedX_Straight;
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;
		}
	}

	//状態を変える(主に生成時に曲がらせたくないときに使うと思われます)
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
		frame = 0;
		straightFrame = straightFrame_Default;
		isTurn = false;
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
		else if (eState == State.Straight && (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallRight"))
		{
			//frame = 0;
			Enemy_Reset();
			gameObject.SetActive(false);
		}
	}
}
