using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public GameObject Player;
	public Player1 P1;
	public GameObject Boss;
	public One_Boss One_Boss_Script;
	public Two_Boss Two_Boss_Script;
	public int frame = 0;
	private void Start()
	{
		//プレイヤーの情報取得----------------------------------
		Player = Obj_Storage.Storage_Data.GetPlayer();
		P1 = Player.GetComponent<Player1>();
		//----------------------------------------------------
		//ボスの情報取得---------------------------------------
		Boss = Obj_Storage.Storage_Data.GetBoss();
		if (Boss.GetComponent<One_Boss>() != null)			One_Boss_Script = Boss.GetComponent<One_Boss>();
		else if(Boss.GetComponent<Two_Boss>() != null)	Two_Boss_Script = Boss.GetComponent<Two_Boss>();
		//----------------------------------------------------

	}
	void Update()
	{
		SceneControl();
	}
	private void SceneControl()
	{
		if (P1.Is_Dead)
		{
			frame++;
			//if(frame > 180) SceneManager.LoadScene("GameOver");
			if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Over();
		}
		if (One_Boss_Script != null)
		{
			if (One_Boss_Script.Is_Dead)
			{
				frame++;
				//if(frame > 180) SceneManager.LoadScene("GameClear");
				if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Stage_02();
			}
		}
		if(Two_Boss_Script != null)
		{
			if(Two_Boss_Script.Is_Dead)
			{
				frame++;
				//if(frame > 180) SceneManager.LoadScene("GameClear");
				if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Stage_02();
			}
		}
	}

}
