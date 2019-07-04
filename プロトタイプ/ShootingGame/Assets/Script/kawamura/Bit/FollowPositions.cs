using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositions : MonoBehaviour
{
	GameObject playerObj;
	public Vector3 pos;
	public Vector3 defPos;
	public Vector3 savePos;

	bool check = false;
	bool isFreeze = false;
	public bool isMove;
	bool defCheck;
    void Start()
    {
        
    }

	void Update()
    {
		if(playerObj==null)
		{
			if(GameObject.Find("Player"))
			{
				playerObj = GameObject.Find("Player");
				check = true;
				defCheck = true;
				pos = playerObj.transform.position;
				savePos = playerObj.transform.position;
			}
		}


		if (pos == playerObj.transform.position)
		{
			isMove = false;
		}
		else
		{
			isMove = true;
			pos = playerObj.transform.position;
		}

		if (check)
		{
			if(Input.GetButtonUp("Bit_Freeze")||Input.GetKeyUp(KeyCode.Y))
			{
				isFreeze = false;
				//transform.parent = null;
			}
			else if (Input.GetButton("Bit_Freeze") || Input.GetKey(KeyCode.Y))
			{
				isFreeze = true;
				//transform.parent = playerObj.transform;

			}
			//savePos = playerObj.transform.position;
		}

		if (!isFreeze)
		{
			if (defCheck)
			{
			}

			//プレイヤーが動いていて位置配列すべてに値が入っているとき
			if (isMove)
			{
				savePos = playerObj.transform.position;
			}
		}

		else if (isFreeze && isMove)
		{
			defPos = pos - savePos;
			//defPos = playerObj.transform.position - transform.position;
			transform.position = transform.position + defPos;
			savePos = playerObj.transform.position;
		}
	}
}
