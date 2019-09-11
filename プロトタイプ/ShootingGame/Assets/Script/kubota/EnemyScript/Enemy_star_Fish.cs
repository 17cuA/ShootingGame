using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_star_Fish : character_status
{
    GameObject item;
	public Vector3 playerPos;
	public Vector3 firstPos;
	public Player1 P1;
	public Player2 P2;
	public int num = 0;

    public bool haveItem = false;
    // Start is called before the first frame update
    new void Start()
	{
        if (gameObject.GetComponent<DropItem>())
        {
            DropItem dItem = gameObject.GetComponent<DropItem>();
            haveItem = true;
        }
        item = Resources.Load("Item/Item_Test") as GameObject;

        base.Start();
		//firstPos = transform.position;
	}

	private void OnEnable()
	{
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			//if(P1.Is_Resporn)
			playerPos = P1.transform.position;
			//firstPos = transform.position;
		}
		else
		{
			if (num == 0)
			{
				P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
				playerPos = P1.direction;
			}
			else
			{
				P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
				playerPos = P2.direction;
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
		if(hp < 1)
		{
            if (haveItem)
            {
                //Instantiate(item, this.transform.position, transform.rotation);
                Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);

            }

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
