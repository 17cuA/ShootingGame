//作成者：川村良太
//ビートルの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beetle : MonoBehaviour
{
	public enum State
	{
		Front,
		Behind,
	}

	State eState;
	Vector3 velocity;

	public float speedX;
	public float speedY;
	public float speedZ;
	public float moveY_Max;

	public bool isUP;
	public bool once;

	void Start()
    {
        
    }

    void Update()
    {
		if (once)
		{
			switch (eState)
			{
				case State.Front:
					break;

				case State.Behind:
					break;
			}
		}

		if (isUP)
		{
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
		else
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
	}
}
