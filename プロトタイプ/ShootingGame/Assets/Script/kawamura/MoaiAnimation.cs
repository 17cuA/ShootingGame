//作成者：川村良太
//モアイの口を開け閉めするスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaiAnimation : MonoBehaviour
{
	public float speedY;
	Vector3 velocity;
	Vector3 defaultPos;
	Vector3 openPos;

	public Vector3 startMarker;
	public Vector3 endMarker;
	float startTime;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;

	Enemy_Moai moai_Script;

	public float moveSpeed;
	public Animation anim;
	public bool isOpen = true;

	private void Awake()
	{
		defaultPos = transform.localPosition;
		moai_Script = transform.parent.gameObject.GetComponent<Enemy_Moai>();

		startMarker = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		startMarker = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.0388f, transform.localPosition.z);

	}
	void Start()
	{
		distance_two = Vector3.Distance(startMarker, endMarker);

		anim = this.gameObject.GetComponent<Animation>();
	}

	void Update()
	{
		if (isOpen)
		{
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			if (transform.localPosition.y < defaultPos.y - 0.0388f)
			{
				transform.localPosition = new Vector3(defaultPos.x, defaultPos.y - 0.0388f, defaultPos.z);
				isOpen = false;
				moai_Script.isMouthOpen = true;
			}
		}
		else
		{
			velocity = gameObject.transform.rotation * new Vector3(0, -speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			if (transform.localPosition.y > defaultPos.y)
			{
				transform.localPosition = defaultPos;
				isOpen = true;
			}
		}
		//present_Location = (Time.time * testSpeed) / distance_two;
		//transform.position = Vector3.Lerp(startMarker, endMarker, present_Location);
		////startTime += moveSpeed;

		if (Input.GetMouseButtonDown(0))
		{
			//isOpen
			//bo = true;
			//anim.Play();
		}
	}
}
