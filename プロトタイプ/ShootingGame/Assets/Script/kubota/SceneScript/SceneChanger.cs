using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public GameObject Player;
	public Player1 P1;
	public GameObject Boss;
	public Enemy_MiddleBoss EMB;
	private void Start()
	{
		//プレイヤーの情報取得----------------------------------
		Player = Obj_Storage.Storage_Data.GetPlayer();
		P1 = Player.GetComponent<Player1>();
		//----------------------------------------------------
		//ボスの情報取得---------------------------------------
		Boss = Obj_Storage.Storage_Data.GetBoss();
		EMB = Boss.GetComponent<Enemy_MiddleBoss>();
		//----------------------------------------------------
	}
	void Update()
	{
		SceneControl();
	}
	private void SceneControl()
	{
		if (P1.Died_Judgment())
		{
			SceneManager.LoadScene("GameOver");
		}
		if (EMB.Died_Judgment())
		{
			SceneManager.LoadScene("GameClear");
		}
	}

}
