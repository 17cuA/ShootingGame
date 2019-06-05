using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPlayer : MonoBehaviour
{
	GameObject playerObj;


	public float speed;             //スピード
	float step;                     //スピードを計算して入れる

	public float offsetX;
	public float offserY;
    //public float offsetZ;


	bool isMove = true;
	void Start()
    {

	}

	void Update()
    {

		if(playerObj==null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");

		}


		//スピード計算
		step = speed * Time.deltaTime;

		//Vector3 pos = playerObj.transform.position;

		if ((transform.position.x < playerObj.transform.position.x + offsetX && transform.position.x > playerObj.transform.position.x - offsetX)
			&& (transform.position.y < playerObj.transform.position.y + offserY && transform.position.y > playerObj.transform.position.y - offserY))
		{
			isMove = false;
		}
		else
		{
			isMove = true;
		}

		//if ((transform.position.x > playerObj.transform.position.x + 2.5 || transform.position.x < playerObj.transform.position.x - 2.5)
		//	&&(transform.position.y > playerObj.transform.position.y +1.5 || transform.position.y < playerObj.transform.position.y - 1.5))
		//{
		//	isMove = true;	
		//}
		//else
		//{
		//	isMove = false;
		//}
		
		if(isMove)
		{
			transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, step);
		}
		//transform.position = new Vector3(pos.x + offsetX, pos.y + offserY, pos.z);
	}
}
