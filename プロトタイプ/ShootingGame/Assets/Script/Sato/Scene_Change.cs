﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	private bool isLoaded = false;
	public AudioSource audioSource; //ユニティ側にて設定
	public AudioClip audioClip;			//unity側から設定
	public void Update()
	{
		if (/*Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 0")*/Input.anyKey)
		{
			//isLoaded = !isLoaded;
			//if (isLoaded)
			//{
			//             //Application.LoadLevelAdditive("MENU");
			//             SceneManager.LoadScene("MENU", LoadSceneMode.Additive); // OK
			//             //SceneManager.UnloadSceneAsync("Title");
			//             gameObject.SetActive(false);

			//         }
			//else
			//{
			//             SceneManager.UnloadSceneAsync("Title");
			//             Resources.UnloadUnusedAssets();
			//             gameObject.SetActive(true);
			//         }
			audioSource.PlayOneShot(audioClip);
			Scene_Manager.Manager.Screen_Transition_To_Stage();
		}
	}
}
