using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_star_Fish : character_status
{
	public Vector3 playerPos;
	public Vector3 firstPos;
	public Player1 P1;
	public Player2 P2;
	public int num = 0;
	
	// Start is called before the first frame update
	new void Start()
	{
		base.Start();
		//firstPos = transform.position;
	}

	private void OnEnable()
	{
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			playerPos = P1.transform.position;
			//firstPos = transform.position;
		}
		else
		{
			if (num == 0)
			{
				P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
				playerPos = P1.transform.position;
			}
			else
			{
				P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
				playerPos = P2.transform.position;
			}
		}
	}

	//SetActiveがfalseになった時に呼ばれる
	private void OnDisable()
	{
		if (P1 != null) P1 = null;
		if (P2 != null) P2 = null;
	}

	// Update is called once per frame
	new void Update()
	{
		transform.position -= calcPos() * speed;
		Debug.Log(calcPos() * speed + transform.position);
		if(hp < 1)
		{
			base.Died_Process();
		}
		base.Update();
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallTop" || col.gameObject.name == "WallUnder" || col.gameObject.name == "WallRight")
		{
			gameObject.SetActive(false);
		}
	}
	//単位ベクトル計算用
	Vector3 calcPos()
	{
		Vector3 pos = playerPos - firstPos;
		//pos.z = 0;
		return pos.normalized;
	}

	public void Attack_Target_Decision(int number)
	{
		num = number;
	}

}
