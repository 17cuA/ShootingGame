using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Manager : MonoBehaviour
{
	public static Voice_Manager VOICE_Obj;
	[Header("パワーアップ用")]
	public AudioSource audiosource;
	[Header("無線用")]
	public AudioSource Sinario_audio;
	private void Awake()
	{
		VOICE_Obj = GetComponent<Voice_Manager>();
	}
	// Start is called before the first frame update
	void Start()
	{
		//audiosource = GetComponent<AudioSource>();
	}
	//パワーアップ用のAudioSourceを使用
	public void Voice_Active(AudioClip voice)
	{
		audiosource.PlayOneShot(voice);
	}
	//無線用のAudioSourceを使用
	public void Sinario_Active(AudioClip voice)
	{
		Sinario_audio.PlayOneShot(voice);
	}
}
