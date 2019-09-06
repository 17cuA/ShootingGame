using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Helper_TitleSceneBGMAdjuster : MonoBehaviour
{
	[Header("------フェードアウト設定------")]
	//[SerializeField] [Range(0.5f, 1)] private float fadeOutFromVolume = 1;
	//[SerializeField] [Range(0, 0.5f)] private float fadeOutToVolue = 0;
	[SerializeField] private float fadeOutSpeed;
	[Header("------タイトルBGM------")]
	[SerializeField] private AudioClip titleAudioClip;

	private AudioSource audioSource;
	private bool isFadeOutOn = false;
	private int enterCount = 0;

	private static Helper_TitleSceneBGMAdjuster instance;
	private void Awake()
	{
		instance = this;

		DontDestroyOnLoad(gameObject);

		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
    {
        if(instance != this)
		{
			Destroy(gameObject);
		}

		audioSource.Play();
    }

	private void Update()
	{
		if(Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "Title")
		{
			enterCount++;
		}

		if(enterCount == 2)
		{
			isFadeOutOn = true;
		}

		if(isFadeOutOn)
		{
			audioSource.volume -= fadeOutSpeed * 0.0166667f;
		}

		if(SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Caution"&& SceneManager.GetActiveScene().name != "RogoScenes")
		{
			audioSource.volume = 1f;
			audioSource.Stop();
			isFadeOutOn = false;
			enterCount = 0;
		}
	}

}
