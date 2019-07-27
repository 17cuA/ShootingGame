using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public GameObject Player;
	public Player1 P1;
	public GameObject[] Boss = new GameObject[2];
	public One_Boss One_Boss_Script;
	public Two_Boss Two_Boss_Script;
	public int frame = 0;
	[Header("呼び出すボスのID設定"),Tooltip("何ステージ目なのかを入れればよい")]
	public int BossID;
	private void Start()
	{
		//プレイヤーの情報取得----------------------------------
		Player = Obj_Storage.Storage_Data.GetPlayer();
		P1 = Player.GetComponent<Player1>();
		//----------------------------------------------------
		//ボスの情報取得---------------------------------------
		Boss[0] = Obj_Storage.Storage_Data.GetBoss(1);
		Boss[1] = Obj_Storage.Storage_Data.GetBoss(2);
		if (Boss[0] != null)	One_Boss_Script = Boss[0].GetComponent<One_Boss>();
		if(Boss[1] != null)	Two_Boss_Script = Boss[1].GetComponent<Two_Boss>();
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
				if (frame > 120) Scene_Manager.Manager.Screen_Transition_To_Clear();
			}
		}
	}

}
