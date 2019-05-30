using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public GameObject Player;
	public Player1 P1;
	public MapCreate Map;
	public GameObject Boss;
	public BossAll BA;
	void Update()
	{
		if(Player != null) SceneControl();


	}
	public void SceneControl()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "Title":
				Debug.Log("hollo");
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Stage");
				break;
			case "Stage":
				if (P1.Died_Judgment())
				{
					SceneManager.LoadScene("GameOver");
					//if (Input.GetButtonDown("Fire1")|| Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("GameOver");
				}
				if (BA.Is_PartsAlive())
				{
					SceneManager.LoadScene("GameClear");
				}
				break;
			case "GameOver":
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Title");
				break;
			case "GameClear":
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Title");
				break;
		}
	}
	public void Chara_Get()
	{
		if (SceneManager.GetActiveScene().name == "Stage")
		{
			Map = gameObject.GetComponent<MapCreate>();
			Player = Map.GetPlayer();           //プレイヤーを名前で検索
			P1 = Player.GetComponent<Player1>();
			Boss = Map.GetBoss();
			BA = Boss.GetComponent<BossAll>();
		}
	}
}
