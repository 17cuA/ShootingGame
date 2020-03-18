using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

	[System.Serializable]
	public struct BGM
	{
		public string name;     //Inspectorでの名前変更用

		[Header("AudioClip")]
		public AudioClip BGM_Clip;  //BGMを入れる
		/// <summary>
		/// インスペクターの名前を変更
		/// </summary>
		/// <param name="Name">インスペクターにて変更</param>
		public BGM(string Name) : this()
		{
			this.name = Name;
		}

	}

	[SerializeField]
	private List<BGM> BGMGroups = new List<BGM>();

	[SerializeField] private float fadeInStartVolume;
	[SerializeField] private float fadeInTime;
	private float fadeInTimer;
	[SerializeField] private bool isFadeIn = false;

	[SerializeField] private float fadeOutOverVolume;
	[SerializeField] private float fadeOutTime;
	private float fadeOutTimer;
	[SerializeField] private bool isFadeOut = false;

	private AudioSource audioSource;
	public float StartFadeoutTime;      //フェードアウト開始するまでの時間【単位：秒】
	private float TimeCnt;				//ふぇーどアウトするまでの時間をカウントするよう
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		isFadeIn = true;
		audioSource.clip = BGMGroups[0].BGM_Clip;
		audioSource.volume = fadeInStartVolume;
	}

	// Update is called once per frame
	private void Update()
	{
		TimeCnt += Time.deltaTime;

		if(TimeCnt > StartFadeoutTime)
		{
			isFadeOut = true;
		}
		//Fade in / Fade out
		if (isFadeIn)
		{
			fadeInTimer += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(fadeInStartVolume, 1.0f, fadeInTimer / fadeInTime);
			if (fadeInTimer >= fadeInTime)
			{
				isFadeIn = false;
				fadeInTimer = 0;
			}
		}

		if (isFadeOut)
		{
			fadeOutTimer += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(1.0f, fadeOutOverVolume, fadeOutTimer / fadeOutTime);
			if (fadeOutTimer >= fadeOutTime)
			{
				isFadeOut = false;
				fadeOutTimer = 0;
				audioSource.Stop();
			}
		}


	}


}
