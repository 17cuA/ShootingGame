using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioInfo
{
	public AudioType audioType;
	public AudioClip audio;
}

public enum BGMType
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
	public float audioFadeInOutTime = 1f;
	public float audioFadeInOutVolume = 0.6f;

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

	/// <summary>
	/// BGM再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	/// <param name="isLoop"></param>
	public void PlayBGM(AudioType type,float volume, bool isLoop)
	{
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
	public void PlayBGM(AudioType type, float volume)
	{
		PlayBGM(type, volume, true);
	}

	/// <summary>
	/// BGM再生3
	/// </summary>
	/// <param name="type"></param>
	public void PlayBGM(AudioType type)
	{
		PlayBGM(type, defaultBgmVolume, true);
	}

	/// <summary>
	/// SE再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
    public void PlaySE(AudioType type,float volume)
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
	public void PlaySE(AudioType type)
	{
		PlaySE(type, defaultSeVolume);
	}

	/// <summary>
	///  SE　Play One shot1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	public void PlaySEOneShot(AudioType type,float volume)
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
	public void PlaySEOneShot(AudioType type)
	{
		PlaySEOneShot(type, defaultSeVolume);
	}

	/// <summary>
	///　BGM　PauseとResume
	/// </summary>
	/// <param name="isPause"></param>
	public void PauseAndResumeBGM(bool isPause)
	{
		if (isPause) bgmPlayer.Pause();
		else	     bgmPlayer.UnPause();
	}

	public void PauseBGM()
	{
		PauseAndResumeBGM(true);
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
}
