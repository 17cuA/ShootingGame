using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_First : MonoBehaviour
{
	public enum State
	{
		Turn,			//曲がる
		Generated,		//生成された時
	}

	State eState = State.Turn;

	float speed;
	Vector3 velocity;


	public float timeCnt = 0;                   //回転の度合い（0～59）で周期
	public float circleSpeed = 10.0f;             //移動速度
	public float radius = 1.0f;             //回転する円の大きさ
	float _y;
	float _z;

	int frame = 0;

	bool isTurn;
    void Start()
    {
		speed = 5.0f;
	}

	void Update()
    {
        switch(eState)
		{
			case State.Turn:
				if(!isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
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
					_y = radius * Mathf.Cos(timeCnt * circleSpeed);
					_z = radius * Mathf.Sin(timeCnt * circleSpeed);

					//}
					//else
					//{
					//	_y = radius * Mathf.Cos(timeCnt * speed);
					//	_z = radius * Mathf.Sin(timeCnt * speed);
					//	isStart = false;
					//}
					//_y = radius * Mathf.Cos(timeCnt * speed) + transform.position.y;
					//_z = radius * Mathf.Sin(timeCnt * speed) + transform.position.z;

					transform.position = new Vector3(transform.position.x + _y, transform.position.y - _z, transform.position.z );
					timeCnt += 0.01f;
					if (timeCnt > 3.0f)
					{
						timeCnt = 0;
					}

				}
				break;

			case State.Generated:

				break;
		}
    }

	public void SetState(State s)
	{
		eState = s;
	}
}
