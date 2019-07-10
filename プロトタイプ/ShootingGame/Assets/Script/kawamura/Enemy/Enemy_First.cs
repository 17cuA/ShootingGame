using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_First : character_status
{
	public enum State
	{
		TurnUp,         //上に曲がる
		TurnDown,       //下に曲がる
		Straight,       //生成された時(曲がらない)
	}

	State eState;

	Vector3 velocity;

	GameObject item;
	GameObject parentObj;
	GameObject childObj;

	EnemyGroupManage groupManage;
	VisibleCheck vc;

	//Renderer renderer;

	public float timeCnt = 0;                   //回転の度合い（0～59）で周期
	public float circleSpeed = 10.0f;             //移動速度
	public float radius = 1.0f;             //回転する円の大きさ
	float _y;
	float _z;

	int frame = 0;
	public float speedX;
	public float speedY;

	bool isTurn;
	bool isAddition = false;
	bool isDead = false;
	void Start()
	{
		item = Resources.Load("Item/Item_Test") as GameObject;
		childObj = transform.GetChild(0).gameObject;
		vc = childObj.GetComponent<VisibleCheck>();
		//renderer = gameObject.GetComponent<Renderer>();

		if (transform.position.y > 0)
		{
			eState = State.TurnDown;
		}
		else
		{
			eState = State.TurnUp;
		}
		speedX = 5.0f;
		speedY = 5.0f;

		if (transform.parent != null)
		{
			parentObj = transform.parent.gameObject;
			groupManage = parentObj.GetComponent<EnemyGroupManage>();
		}
		HP_Setting();
	}

	void Update()
	{
		//倒されたとき
		if (hp < 1)
		{
			if (parentObj.name == "Enemy_UFO_Group")
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
						Instantiate(item, this.transform.position, transform.rotation);
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
			Reset_Status();
			isDead = true;
			frame = 0;

			Died_Process();
		}

		if (!parentObj)
		{
			if (transform.parent)
			{
				if (transform.parent)
				{
					parentObj = transform.parent.gameObject;
					groupManage = parentObj.GetComponent<EnemyGroupManage>();

				}
			}
		}

		Move();

		//画面外に出たら、オフにする
		//if (!renderer.isVisible)
		//{
		//	isTurn = false;
		//	isDead = false;
		//	gameObject.SetActive(false);
		//}
	}

	//オフになったとき実行される
	private void OnDisable()
	{
		if (isDead)
		{
			isDead = false;
		}
	}

	//---------ここから関数--------------
	void Move()
	{
		switch (eState)
		{
			case State.TurnUp:
				if (!isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.position.x < 9)
					{
						frame++;
						if (frame > 180)
						{
							isTurn = true;
						}
					}
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

					//_y = radius * Mathf.Cos(timeCnt * circleSpeed);
					//_z = radius * Mathf.Sin(timeCnt * circleSpeed);

					////}
					////else
					////{
					////	_y = radius * Mathf.Cos(timeCnt * speed);
					////	_z = radius * Mathf.Sin(timeCnt * speed);
					////	isStart = false;
					////}
					////_y = radius * Mathf.Cos(timeCnt * speed) + transform.position.y;
					////_z = radius * Mathf.Sin(timeCnt * speed) + transform.position.z;

					//transform.position = new Vector3(transform.position.x + _y, transform.position.y - _z, transform.position.z );
					//timeCnt += 0.01f;
					//if (timeCnt > 3.0f)
					//{
					//	timeCnt = 0;
					//}

				}
				break;

			case State.TurnDown:
				if (!isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.position.x < 9)
					{
						frame++;
					}
					if (frame > 180)
					{
						isTurn = true;
					}
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
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;
		}
	}

	//状態を変える(主に生成時に曲がらせたくないときに使うと思われます)
	public void SetState(int num)
	{
		switch(num)
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

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallUnder" || col.gameObject.name == "WallTop")
		{
			if (parentObj.name == "Enemy_UFO_Group")
			{
				groupManage.notDefeatedEnemyCnt++;
				groupManage.remainingEnemiesCnt -= 1;
			}
			frame = 0;
			gameObject.SetActive(false);
		}
	}
}
