using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleWall : MonoBehaviour
{
	Vector3 velocity;
	Vector3 defaultPos;

	public float upSpeed;
	public float upSpeed_Max;
	public float downSpeed;
	public float downSpeed_Max;

	float speed;
	
	float defaultPosY;

	float delayCnt;
	public float delayMax;

	bool isHit = false;
    void Start()
    {
		defaultPos = transform.position;
		defaultPosY = transform.position.y;

		delayCnt = 0;
    }

    void Update()
    {
		if (isHit)
		{
			//velocity = gameObject.transform.rotation * new Vector3(0, downSpeed, 0);
			//gameObject.transform.position += velocity * Time.deltaTime;
			velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

			delayCnt += Time.deltaTime;
		}
		else
		{
			speed += 0.2f;
			upSpeed += 0.2f;
			if (upSpeed > upSpeed_Max)
			{
				upSpeed = upSpeed_Max;
			}
			if (speed > upSpeed_Max)
			{
				speed = upSpeed_Max;
			}

			//velocity = gameObject.transform.rotation * new Vector3(0, upSpeed, 0);
			//gameObject.transform.position += velocity * Time.deltaTime;
			velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

			if (transform.position.y > defaultPosY)
			{
				transform.position = defaultPos;
			}
		}

		if (delayCnt > delayMax)
		{
			isHit = false;
		}

	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player_Bullet")
		{
			isHit = true;
			speed = downSpeed_Max;
			upSpeed = 0;
			delayCnt = 0;
		}
	}
}
