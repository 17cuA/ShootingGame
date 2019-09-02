using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[Header("Debug用")]
	public bool isUseKey = true;

	[Header("-----キーボード設定-----")]
	[Header("Pause制御キー")]
	public KeyCode controlKey;

	[Header("Pause音量")]
	[Range(0.1f,0.8f)]public float pauseVolume = 0.8f;

	//[Header("-----コントローラ設定-----")]
	//[Header("Pause制御ボタン")]
	//public string controlName;


	private static bool isPause;
	public static bool IsPause
	{
		get
		{
			return isPause;
		}
	}

	private GameObject pauseMask;
	private GameObject pauseText;
	private AudioSource[] allAudios;

	private void Start()
	{
		pauseMask = transform.GetChild(0).gameObject;
		pauseText = transform.GetChild(1).gameObject;
		allAudios = GameObject.FindObjectsOfType<AudioSource>();
	}

	void Update()
    {
		if (!isPause)
		{
			if ((Input.GetKeyDown(controlKey) && isUseKey) /*|| (Input.GetButtonDown(controlName) && !isUseKey)*/)
			{
				Time.timeScale = 0f;
				isPause = !isPause;
				PauseComponent.Pause();
				pauseMask.SetActive(true);
				pauseText.SetActive(true);
				for(var i = 0; i < allAudios.Length; ++i)
				{
					allAudios[i].volume = pauseVolume;
				}
			}
			
		}
		else
		{
			if((Input.GetKeyDown(controlKey) && isUseKey) /*|| (Input.GetButtonDown(controlName) && !isUseKey)*/)
			{
				Time.timeScale = 1f;
				isPause = !isPause;
				PauseComponent.Resume();
				pauseMask.SetActive(false);
				pauseText.SetActive(false);
				for (var i = 0; i < allAudios.Length; ++i)
				{
					allAudios[i].volume = 1.0f;
				}
			}
		}
		
    }
}
