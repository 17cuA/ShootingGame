//作成者：川村良太
//バウンドのスクリプト　バウンド時のスピードを親に渡したりする

//0.9 ~ -1 モデルのxの範囲
//1 ~ -1.2 モデルのyの範囲
//つまり
//xは1.8 ~ -2.0
//yは2.0 ~ -2.4 の範囲なのだ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorBound : character_status
{
	GameObject parentObj;
	Enemy_MeteorBound_Move boundMove;   //親の移動スピード取得用
	Enemy_MeteorBound meteorBound;		//相手のバウンドスクリプト取得用

	Vector3 velocity;
    Vector3 defaultLocalPos;

	public float speedX;
	public float speedY;

	public float defPosX;
	public float defPosY;

	public float defPercentX;
	public float defPercentY;

	new void Start()
	{
		parentObj = transform.parent.gameObject;
		boundMove = parentObj.GetComponent<Enemy_MeteorBound_Move>();
        defaultLocalPos = transform.localPosition;
        speedX = Random.Range(2.0f, 3.5f);
        HP_Setting();
		base.Start();
	}

	new void Update()
	{
		speedX = boundMove.speedX;
		speedY = boundMove.speedY;
        //velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
        //gameObject.transform.position += velocity * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);


        if (hp < 1)
        {
            Died_Process();
        }
    }

    private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Enemy_MeteorBound_Model")
		{
			meteorBound = col.gameObject.GetComponent<Enemy_MeteorBound>();
			defPosX = col.transform.position.x - transform.position.x;
			defPosY = col.transform.position.y - transform.position.y;

			//敵が自分より右側
			if (defPosX > 0)
			{
				defPercentX = defPosX / 1.8f;
				if (defPercentX > 1)
				{
					defPercentX = 1;
				}
				defPercentX *= 0.7f;

				//if (meteorBound.speedX < 0)
				//{
				//	boundMove.speedY += meteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	boundMove.speedY -= meteorBound.speedY * defPercentY;
				//}
			}
			//敵が自分より左側
			else if (defPosX < 0)
			{
				defPercentX = defPosY / -2.0f;
				if (defPercentX > 1)
				{
					defPercentX = 1;
				}
				defPercentX *= 0.7f;
			}
			//x座標が一緒
			else
			{
				defPercentX = 0;
			}


			if (defPosY > 0)
			{
				defPercentY = defPosY / 2.0f;
				if (defPercentY > 1)
				{
					defPercentY = 1;
				}
				defPercentY *= 1f;
				//if (meteorBound.speedY < 0)
				//{
				//	boundMove.speedY += meteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	boundMove.speedY -= meteorBound.speedY * defPercentY;
				//}
			}
			else if (defPosY < 0)
			{
				defPercentY = defPosY / -2.4f;
				if (defPercentY > 1)
				{
					defPercentY = 1;
				}
				defPercentY *= 1f;

				//if (meteorBound.speedY < 0)
				//{
				//	boundMove.speedY -= meteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	boundMove.speedY += meteorBound.speedY * defPercentY;
				//}
			}
			//Y座標が一緒
			else
			{
				defPercentY = 0;
			}

			//当たった相手の位置が自分より上
			if (col.transform.position.y > transform.position.y)
			{
				if (meteorBound.speedY < 0)
				{
					boundMove.speedY += meteorBound.speedY * defPercentY;
				}
				else
				{
					boundMove.speedY -= meteorBound.speedY * defPercentY;
				}
			}
			//当たった相手の位置が自分より下
			else if (col.transform.position.y < transform.position.y)
			{
				if (meteorBound.speedY < 0)
				{
					boundMove.speedY -= meteorBound.speedY * defPercentY;
				}
				else
				{
					boundMove.speedY += meteorBound.speedY * defPercentY;
				}
			}

			//自分より相手が右側
			if (col.transform.position.x > transform.position.x)
			{
				if (meteorBound.speedX < 0)
				{
					if (boundMove.speedX < 0)
					{
						boundMove.speedX += meteorBound.speedX - boundMove.speedX;
					}
					else
					{
						boundMove.speedX += meteorBound.speedX * defPercentX;
					}
				}
				else if (meteorBound.speedX > 0)
				{
					if (boundMove.speedX > 0)
					{
						boundMove.speedX -= boundMove.speedX - meteorBound.speedX;
					}
					boundMove.speedX -= meteorBound.speedX * defPercentX;
				}
			}
			//自分より相手が左側
			else if (col.transform.position.x < transform.position.x)
			{
				if (meteorBound.speedX < 0)
				{
					boundMove.speedX -= meteorBound.speedX * defPercentX;
				}
				else if (meteorBound.speedX > 0)
				{
					boundMove.speedX *= meteorBound.speedX * defPercentX;
				}
			}

		}
		meteorBound = null;
	}
}
