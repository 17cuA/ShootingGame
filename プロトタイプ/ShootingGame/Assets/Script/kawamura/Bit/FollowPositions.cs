//作成者：川村良太
//4つのオプションの追従位置オブジェクトの親スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositions : MonoBehaviour
{
	GameObject playerObj;
	public Vector3 pos;
	public Vector3 defPos;
	public Vector3 savePos;

	public string myName;

	bool check = false;
	bool isFreeze = false;
	public bool isMove;
	bool defCheck;
	public bool isFollow1P;
	public bool isFollow2P;
	private void Awake()
	{
		
	}
	void Start()
    {
		myName = gameObject.name;

		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			if (myName == "Four_FollowPos_2P")
			{
				gameObject.SetActive(false);    //オブジェクトをオフにする
			}
		}
		if (myName == "Four_FollowPos_1P")
		{
			isFollow1P = true;
		}
		else if (myName == "Four_FollowPos_2P")
		{
			isFollow2P = true;
		}
    }

	void Update()
    {
		if(playerObj==null)
		{
			if (isFollow1P)
			{
				if (GameObject.Find("Player"))
				{
					playerObj = GameObject.Find("Player");
					check = true;
					defCheck = true;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
				}
			}
			else if (isFollow2P)
			{
				if (GameObject.Find("Player_2"))
				{
					playerObj = GameObject.Find("Player_2");
					check = true;
					defCheck = true;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
				}
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
