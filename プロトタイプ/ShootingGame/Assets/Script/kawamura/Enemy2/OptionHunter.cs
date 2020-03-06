using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionHunter : MonoBehaviour
{
	GameObject optionObj;
	Player1 player1_Script;
	Player2 player2_Script;
	Bit_Formation_3 option_Script;

	public int playerNum;				//盗んだプレイヤー
	public int huntOptionNum;		//盗んだオプションの番号
	public int huntNum;				//盗んだオプションの数
	public bool isHunt = false;		//盗んだ判定



    void Start()
    {
		player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		player2_Script = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();

	}


	void Update()
    {
        
    }

	private void OnTriggerEnter(Collider col)
	{
		//オプションに当たった時
		if (col.gameObject.tag == "Option")
		{
			//当たったオプション取得
			optionObj = col.gameObject;
			option_Script = optionObj.GetComponent<Bit_Formation_3>();
			if (option_Script.bState == Bit_Formation_3.BitState.Player1)
			{
				//盗んだプレイヤーのセット
				playerNum = 1;
				huntOptionNum = option_Script.option_OrdinalNum;
				huntNum = (player1_Script.bitIndex - option_Script.option_OrdinalNum) + 1;
			}
			else if (option_Script.bState == Bit_Formation_3.BitState.Player2)
			{
				playerNum = 2;
				huntOptionNum = option_Script.option_OrdinalNum;
				huntNum = (player2_Script.bitIndex - option_Script.option_OrdinalNum) + 1;

			}

			huntNum = option_Script.option_OrdinalNum;
			isHunt = true;

		}
	}
}
