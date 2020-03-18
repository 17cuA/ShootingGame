﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_BGMTranstion : MonoBehaviour
{

	[System.Serializable]
	public struct BGM
	{
		public string name;		//Inspectorでの名前変更用

		[Header("AudioClip")]
		public AudioClip BGM_Clip;	//BGMを入れる
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

	private int currentWirelessNumber = 0;
	public static int WirelessNumber_temp;
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		isFadeIn = true;
		//audioSource.clip = startBGMClip;
		audioSource.clip = BGMGroups[0].BGM_Clip;
		audioSource.volume = fadeInStartVolume;
	}

	private void Start()
	{
		currentWirelessNumber = 0;
		WirelessNumber_temp = 0;
	}
	// Update is called once per frame
	private void Update()
    {
		//Fade in / Fade out
        if(isFadeIn)
		{
			fadeInTimer += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(fadeInStartVolume, 1.0f, fadeInTimer / fadeInTime);
			if(fadeInTimer >= fadeInTime)
			{
				isFadeIn = false;
				fadeInTimer = 0;
			}
		}

		if(isFadeOut)
		{
			fadeOutTimer += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(1.0f, fadeOutOverVolume, fadeOutTimer / fadeOutTime);
			if(fadeOutTimer >= fadeOutTime)
			{
				isFadeOut = false;
				fadeOutTimer = 0;
				if(currentWirelessNumber == 1)
				{
					audioSource.clip = BGMGroups[currentWirelessNumber].BGM_Clip;
					isFadeIn = true;
					audioSource.Play();
					
				}
				else if(currentWirelessNumber == 2)
				{
					audioSource.clip = BGMGroups[currentWirelessNumber].BGM_Clip;
					isFadeIn = true;
					audioSource.Play();
				}
				else if(currentWirelessNumber == 3)
				{
					audioSource.clip = BGMGroups[currentWirelessNumber].BGM_Clip;
					isFadeIn = true;
					audioSource.Play();
				}
				else if(currentWirelessNumber == 4)
				{
					audioSource.clip = BGMGroups[currentWirelessNumber].BGM_Clip;
					isFadeIn = true;
					audioSource.Play();
				}
				else if(currentWirelessNumber == 5)
				{
					audioSource.clip = BGMGroups[currentWirelessNumber].BGM_Clip;
					isFadeIn = true;

					audioSource.Play();

				}
				else
                {
                    isFadeIn = true;
                }
			}
		}


		if(currentWirelessNumber != WirelessNumber_temp)
		{
			currentWirelessNumber = WirelessNumber_temp;
			Debug.Log("鳴らしているBGMの番号：" + currentWirelessNumber);
			isFadeOut = true;
		}


		//位置
		//if(Wireless_sinario.Is_using_wireless)
		//{
		//	currentWirelessNumber++;
		//	DebugManager.OperationDebug("現在のBGM:" + currentWirelessNumber,"BGM");
		//	isFadeOut = true;
		//}
    }


}
