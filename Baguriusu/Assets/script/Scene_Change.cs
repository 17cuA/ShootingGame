using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	private bool isLoaded = false;
	public void Update()
	{
		if (Input.anyKeyDown)
		{
			isLoaded = !isLoaded;
			if (isLoaded)
			{
				Application.LoadLevelAdditive("MENU");
				gameObject.SetActive(false);
			}
			else
			{
				Application.UnloadLevel("MENU");
				Resources.UnloadUnusedAssets();
				gameObject.SetActive(true);
			}
		}
	}
}
