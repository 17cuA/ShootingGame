using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_First : character_status
{
	public enum State
	{
		TurnUp,			//上に曲がる
		TurnDown,		//下に曲がる
		Generated,		//生成された時
	}

	State eState;

	Vector3 velocity;

    GameObject item;
	GameObject parentObj;
    CreateItem ci;

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

        if (transform.parent)
        {
            parentObj = transform.parent.gameObject;
            ci = parentObj.GetComponent<CreateItem>();
        }

    }

    void Update()
    {
		if (hp < 1)
		{
            isTurn = false;
            frame = 0;
            if (ci.remainingEnemiesCnt == 1)
            {
                ci.itemPos = transform.position;
                ci.itemTransform = this.transform;

                Instantiate(item, this.transform.position, transform.rotation);
            }
            ci.remainingEnemiesCnt -= 1;

            hp = 1;
            isDead = true;

            Died_Process();
		}

        if(!parentObj)
        if (transform.parent)
        {
            if(transform.parent)
            {
                parentObj = transform.parent.gameObject;
                ci = parentObj.GetComponent<CreateItem>();

            }
        }

        switch (eState)
		{
			case State.TurnUp:
				if(!isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
					gameObject.transform.position += velocity * Time.deltaTime;
					if (transform.position.x < 9)
					{
						frame++;
					}
					if (frame>180)
					{
						isTurn = true;
					}
				}
				else if(isTurn)
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

			case State.Generated:
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;
		}
    }
    private void OnDisable()
    {
        if(isDead)
        {
            isDead = false;
        }
    }

    //---------ここから関数--------------
    public void SetState(int num)
	{
		switch(num)
		{
			case 0:
				eState = State.TurnUp;
				break;

			case 1:
				eState = State.TurnDown;
				break;

			case 2:
				eState = State.Generated;
				break;
		}
	}
}
