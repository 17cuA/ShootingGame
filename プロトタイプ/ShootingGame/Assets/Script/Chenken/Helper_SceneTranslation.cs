using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helper_SceneTranslation : MonoBehaviour
{
	[SerializeField, Header("入力受け付け開始")] private bool input_reception;

	public Scene_Manager.SCENE_NAME sceneName;
    private bool isLoaded;
	public AudioSource audioSource; //ユニティ側にて設定
	public AudioClip audioClip;         //unity側から設定

	public int Set_Step { get; private set; }

	private void Start()
	{
		isLoaded = false;
		Set_Step = 0;
	}
	public void Update()
	{
		if (Set_Step == 0 && input_reception)
		{
			if (Input.anyKeyDown && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl))
			{
				audioSource?.PlayOneShot(audioClip);
				Set_Step++;
			}
		}
		else if(Set_Step==1)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				Set_Step++;
			}
			else if(Input.GetButtonDown("Fire2"))
			{
				Set_Step--;
			}
		}
		else if(Set_Step == 2)
		{
			Scene_Manager.Manager.Scene_Transition(sceneName);
		}
	}
}
