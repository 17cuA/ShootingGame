using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	private bool isLoaded = false;
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 0"))
		{
			isLoaded = !isLoaded;
			if (isLoaded)
			{
                //Application.LoadLevelAdditive("MENU");
                SceneManager.LoadScene("MENU", LoadSceneMode.Additive); // OK
                //SceneManager.UnloadSceneAsync("Title");
                gameObject.SetActive(false);

            }
			else
			{
                SceneManager.UnloadSceneAsync("Title");
                Resources.UnloadUnusedAssets();
                gameObject.SetActive(true);
            }
		}
	}
}
