using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioInfo
{
	public C_AudioType audioType;
	public AudioClip audio;
}

public enum C_AudioType
{
	BGM_Ending,
	BGM_Opening_01,
	BGM_Opening_02,
	BGM_Stage_01,


	SE_Intro,
	SE_Credit,
	SE_SelectMove,
	SE_SelectOk,
	SE_Shot,
	SE_ItemGet,
	SE_Destroyed,
	SE_Shot_Hit,
	SE_Explosion,
	SE_BossExplosion,
	SE_SpeedUp,
	SE_Laser,
	SE_Double,
	SE_Liple_Laser,
	SE_Option,
	SE_Force_Field
}


public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;
	public static AudioManager Instance
	{
		get
		{
			return instance;
		}
	}

	[Header("-----------BGMデータ----------")]
	public List<AudioInfo> bgms = new List<AudioInfo>();

	[Header("-----------SEデータ----------")]
	public List<AudioInfo> ses = new List<AudioInfo>();

	private static AudioSource bgmPlayer;
	private static AudioSource sePlayer;

	[Header("デフォルト設定")]
	public float defaultBgmVolume = 1.0f;
	public float defaultSeVolume = 1.0f;
	public float audioFadeInOutDefaultTime = 1f;
	public float audioFadeInDefaultVolume = 1f;

	private float audioFadeInTarget;
	private bool isAudioFadeIn = false;
	private float audioFadeInTime;
	private float audioFadeInCount;

	private bool isAudioFadeOut = false;
	private float audioFadeOutStart;
	private float audioFadeOutTime;
	private float audioFadeOutCount;

	private bool prepartToPause = false;
	private bool prepartToStop = false;

	private void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this.gameObject);

		if(Instance != this)
		{
			Destroy(this.gameObject);
		}

		bgmPlayer = transform.Find("Bgm").GetComponent<AudioSource>();
		sePlayer = transform.Find("Se").GetComponent<AudioSource>();
	}

	
	private void Update()
	{
		if(isAudioFadeIn)
		{
			bgmPlayer.volume = Mathf.Lerp(0, audioFadeInTarget, audioFadeInCount / audioFadeInTime);
			audioFadeInCount += Time.deltaTime;
			if(bgmPlayer.volume >= 1)
			{
				audioFadeInCount = 0;
				isAudioFadeIn = false;
			}
		}

		if(isAudioFadeOut)
		{
			bgmPlayer.volume = Mathf.Lerp(audioFadeOutStart, 0, audioFadeOutCount / audioFadeOutTime);
			audioFadeOutCount += Time.deltaTime;
			if(bgmPlayer.volume <= 0)
			{
				isAudioFadeOut = false;
				audioFadeOutCount = 0;
				if(prepartToPause)
				{
					PauseBGM();
					prepartToPause = false;
				}
				if(prepartToStop)
				{
					StopCurrentAudioPlayer();
					prepartToStop = false;
				}
			}
		}
	}

	private void OnGUI()
	{
		if(GUI.Button (new Rect (60,60,240,120),"Fade In再生,Opening"))
		{
			PlayBGMFadeIn(C_AudioType.BGM_Opening_01);
		}

		if(GUI.Button (new Rect (60,180,240,120),"再生,Opening"))
		{
			PlayBGM(C_AudioType.BGM_Opening_01);
		}

		if(GUI.Button (new Rect (60,300,240,120),"停止,Opening"))
		{
			StopCurrentAudioPlayer();
		}

		if(GUI.Button (new Rect (60,420,240,120),"Fade Out停止,Opening"))
		{
			StopCurrentAudioPlayerFadeOut();
		}

		if(GUI.Button (new Rect (60,540,240,120),"一時停止,Opening"))
		{
			PauseBGM();
		}

		if(GUI.Button (new Rect (60,660,240,120),"Fade Out 一時停止,Opening"))
		{
			PauseBGMFadeOut();
		}

		if(GUI.Button (new Rect (60,780,240,120),"再開,Opening"))
		{
			ResumeBGM();
		}

		if(GUI.Button (new Rect (60,900,240,120),"Fade In再開,Opening"))
		{
			ResumeBGMFadeIn();
		}
	}

	/// <summary>
	/// BGM再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	/// <param name="isLoop"></param>
	public void PlayBGM(C_AudioType type,float volume, bool isLoop)
	{
		if(bgmPlayer.isPlaying)
		{
			Debug.Log("再生中、他の再生処理は実行しない");
			return;
		}

		AudioClip tempBgmChip = null;
		for(var i = 0; i < bgms.Count; ++i)
		{
			if(bgms[i].audioType == type)
			{
				tempBgmChip = bgms[i].audio;
				break;
			}
		}
		if(tempBgmChip == null)
		{
			Debug.LogError("再生するBGMタイプは存在しない");
			return;
		}

		bgmPlayer.clip = tempBgmChip;
		bgmPlayer.volume = volume;
		bgmPlayer.loop = isLoop;
		bgmPlayer.Play();
	}

	/// <summary>
	/// BGM再生2
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	public void PlayBGM(C_AudioType type, float volume)
	{
		PlayBGM(type, volume, true);
	}

	/// <summary>
	/// BGM再生3
	/// </summary>
	/// <param name="type"></param>
	public void PlayBGM(C_AudioType type)
	{
		PlayBGM(type, defaultBgmVolume, true);
	}

	/// <summary>
	/// SE再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
    public void PlaySE(C_AudioType type,float volume)
	{
		AudioClip tempSeChip = null;
		for(var i = 0; i < ses.Count; ++i)
		{
			if(ses[i].audioType == type)
			{
				tempSeChip = ses[i].audio;
				break;
			}
		}
		if(tempSeChip == null)
		{
			Debug.LogError("再生するSEタイプは存在しない");
			return;
		}

		sePlayer.clip = tempSeChip;
		sePlayer.volume = volume;
		sePlayer.loop = false;
		sePlayer.Play();
	}

	/// <summary>
	/// SE再生2
	/// </summary>
	/// <param name="type"></param>
	public void PlaySE(C_AudioType type)
	{
		PlaySE(type, defaultSeVolume);
	}

	/// <summary>
	///  SE　Play One shot1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	public void PlaySEOneShot(C_AudioType type,float volume)
	{
		AudioClip tempSeChip = null;
		for(var i = 0; i < ses.Count; ++i)
		{
			if(ses[i].audioType == type)
			{
				tempSeChip = ses[i].audio;
				break;
			}
		}
		if(tempSeChip == null)
		{
			Debug.LogError("再生するSEタイプは存在しない");
			return;
		}

		sePlayer.PlayOneShot(tempSeChip, volume);
	}


	/// <summary>
	/// SE　Play One shot2
	/// </summary>
	/// <param name="type"></param>
	public void PlaySEOneShot(C_AudioType type)
	{
		PlaySEOneShot(type, defaultSeVolume);
	}

	/// <summary>
	///　BGM　PauseとResume
	/// </summary>
	/// <param name="isPause"></param>
	public void PauseAndResumeBGM(bool isPause)
	{
		if(bgmPlayer.isPlaying　&& !isPause)
		{
			Debug.Log("再生中、再生再開処理は実行しない");
			return;
		}

		if(!bgmPlayer.isPlaying　&& isPause)
		{
			Debug.Log("再生しないため、再生一時停止処理は実行しない");
			return;
		}

		if (isPause) bgmPlayer.Pause();
		else	     bgmPlayer.UnPause();
	}

	public void PauseBGM()
	{
		PauseAndResumeBGM(true);
	}

	public void PauseBGMFadeOut()
	{
		PlayBGMFadeOut();
		prepartToPause = true;
	}

	public void ResumeBGMFadeIn(float targetVolume,float fadeTime)
	{
		if(bgmPlayer.isPlaying)
		{
			Debug.Log("再生中、再生再開処理は実行しない");
			return;
		}

		bgmPlayer.UnPause();
		isAudioFadeIn = true;
		audioFadeInTarget = targetVolume;
		audioFadeInTime = fadeTime;
	}

	public void ResumeBGMFadeIn()
	{
		ResumeBGMFadeIn(audioFadeInDefaultVolume, audioFadeInOutDefaultTime);
	}

	public void ResumeBGM()
	{
		PauseAndResumeBGM(false);
	}

	/// <summary>
	/// SE　PauseとResume
	/// </summary>
	/// <param name="isPause"></param>
	public void PauseAndResumeSE(bool isPause)
	{
		if (isPause) sePlayer.Pause();
		else        sePlayer.UnPause();
	}

	public void PauseSE()
	{
		PauseAndResumeSE(true);
	}

	public void ResumeSE()
	{
		PauseAndResumeSE(false);
	}

	public void StopCurrentAudioPlayer()
	{
		bgmPlayer.Stop();
		sePlayer.Stop();
	}

	public void StopCurrentAudioPlayerFadeOut(float fadeTime)
	{
		PlayBGMFadeOut(fadeTime);
		prepartToStop = true;
	}

	public void StopCurrentAudioPlayerFadeOut()
	{
		StopCurrentAudioPlayerFadeOut(audioFadeInOutDefaultTime);
	}

	public void PlayBGMFadeIn(C_AudioType type, float targetVolume,float fadeTime)
	{
		if(bgmPlayer.isPlaying)
		{
			Debug.Log("再生中、Fade in処理は実行しない");
			return;
		}

		isAudioFadeIn = true;
		audioFadeInTarget = targetVolume;
		audioFadeInTime = fadeTime;
		
		AudioClip tempBgmChip = null;
		for(var i = 0; i < bgms.Count; ++i)
		{
			if(bgms[i].audioType == type)
			{
				tempBgmChip = bgms[i].audio;
				break;
			}
		}
		if(tempBgmChip == null)
		{
			Debug.LogError("再生するBGMタイプは存在しない");
			return;
		}
		bgmPlayer.clip = tempBgmChip;
		bgmPlayer.Play();
	}

	public void PlayBGMFadeIn(C_AudioType type)
	{
		PlayBGMFadeIn(type, audioFadeInDefaultVolume, audioFadeInOutDefaultTime);
	}

	private void PlayBGMFadeOut(float fadeTime)
	{
		if(!bgmPlayer.isPlaying)
		{
			Debug.Log("再生しないため、再生一時停止処理は実行しない");
			return;
		}


		isAudioFadeOut = true;
		audioFadeOutStart = bgmPlayer.volume;
		audioFadeOutTime = fadeTime;

	}

	private void PlayBGMFadeOut()
	{
		PlayBGMFadeOut(audioFadeInOutDefaultTime);
	}

}
