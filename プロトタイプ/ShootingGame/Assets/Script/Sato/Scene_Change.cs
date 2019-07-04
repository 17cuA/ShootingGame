using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	private bool isLoaded = false;
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.K)|| Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 0"))
		{
			isLoaded = !isLoaded;
			if (isLoaded)
			{
				//Application.LoadLevelAdditive("MENU");
				SceneManager.LoadScene("MENU", LoadSceneMode.Additive); // OK
				gameObject.SetActive(false);
			}
			else
			{
				//Application.UnloadLevel("MENU");
				SceneManager.LoadScene("MENU", LoadSceneMode.Additive);
				Resources.UnloadUnusedAssets();
				gameObject.SetActive(true);
			}
		}
	}
}
