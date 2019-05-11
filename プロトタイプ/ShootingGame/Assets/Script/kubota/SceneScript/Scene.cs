using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
	public GameObject Player;
	public Player1 P1;
	public MapCreate Map;
    void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage")
		{
			Map =gameObject.GetComponent<MapCreate>();
			Player = Map.GetPlayer();           //プレイヤーを名前で検索
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
			case "Title":
				Debug.Log("hollo");
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Stage");
				break;
			case "Stage":
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