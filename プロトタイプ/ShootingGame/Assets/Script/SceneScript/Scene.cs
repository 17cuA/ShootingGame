using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
	private GameObject Player;
	private Player1 P1;
    void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage1")
		{
			Player = GameObject.Find("Player_Demo 1(Clone)");           //プレイヤーを名前で検索
			P1 = Player.GetComponent<Player1>();
		}
    }

    void Update()
    {
		SceneControl();

    }
	public void SceneControl()
	{
		switch(SceneManager.GetActiveScene().name)
		{
			case "Titel":
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Stage1");
				break;
			case "Stage1":
				if(P1.Died_Judgment())
				{
					SceneManager.LoadScene("GameOver");
					//if (Input.GetButtonDown("Fire1")|| Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("GameOver");
				}
				break;
			case "GameOver":
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Title");
				break;
		}
	}
}