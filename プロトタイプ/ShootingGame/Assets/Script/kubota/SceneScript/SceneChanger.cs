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
		//Player = Obj_Storage.Storage_Data.Player;
	}
	void Update()
	{
		//SceneControl();
	}
	public void SceneControl()
	{
		switch (SceneManager.GetActiveScene().name)
		{
			case "Title":
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Stage");
				break;
			case "Stage":
				if (P1.Died_Judgment())
				{
					SceneManager.LoadScene("GameOver");
				}
				if (EMB.Died_Judgment())
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
		}
	}
}
