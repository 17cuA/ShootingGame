using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walk : MonoBehaviour
{
	public enum DirectionState
	{
		Left,
		Right,
		Roll,
		Stop,
	}

	public　DirectionState direcState;
	DirectionState saveDirection;

	Vector3 velocity;

	public float walkSpeed;
	public float rotaY;
	public float rotaSpeed;
	public float stopTime;
	float stopTimeCnt;

	public bool isRoll;

	void Start()
    {
		stopTimeCnt = 0;
		isRoll = false;
	}

    void Update()
    {
		if (transform.position.y < -4.15f)
		{
			transform.position = new Vector3(transform.position.x, -4.15f, 0);
		}
		Walk();
	}

	void Walk()
	{
		switch(direcState)
		{
			case DirectionState.Left:
				velocity = gameObject.transform.rotation * new Vector3(-walkSpeed, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case DirectionState.Right:
				velocity = gameObject.transform.rotation * new Vector3(-walkSpeed, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;

			case DirectionState.Roll:
				if (saveDirection == DirectionState.Left)
				{
					rotaY -= rotaSpeed;
					if (rotaY < -180f)
					{
						rotaY = -180f;
						direcState = DirectionState.Right;
						isRoll = false;
					}
					transform.rotation = Quaternion.Euler(0, rotaY, 0);
				}
				else if (saveDirection == DirectionState.Right)
				{
					rotaY += rotaSpeed;
					if (rotaY > 0)
					{
						rotaY = 0;
						direcState = DirectionState.Left;
						isRoll = false;
					}

					transform.rotation = Quaternion.Euler(0, rotaY, 0);

				}
				break;

			case DirectionState.Stop:
				stopTimeCnt += Time.deltaTime;

				break;
		}


	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (!isRoll)
			{
				saveDirection = direcState;
				direcState = DirectionState.Roll;
				isRoll = true;
			}
		}

	}
}
