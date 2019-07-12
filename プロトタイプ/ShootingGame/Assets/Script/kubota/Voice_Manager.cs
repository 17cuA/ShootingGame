using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Manager : MonoBehaviour
{
	public static Voice_Manager VOICE_Obj;
	[Header("音を鳴らすかどうか")]
	public bool Is_Active;
	public AudioSource audiosource;
	private void Awake()
	{
		VOICE_Obj = GetComponent<Voice_Manager>();
	}
	// Start is called before the first frame update
	void Start()
	{
		audiosource = GetComponent<AudioSource>();
	}

	public void Voice_Active(AudioClip voice)
	{
		if (Is_Active) audiosource.PlayOneShot(voice);

	}
}
