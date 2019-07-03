using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
	public static SE_Manager SE_Obj;
	[Header("音を鳴らすかどうか")]
	public bool Is_Active;
	private AudioSource audiosource;
	private void Awake()
	{
		SE_Obj = GetComponent<SE_Manager>();
	}
	// Start is called before the first frame update
	void Start()
    {
		audiosource = GetComponent<AudioSource>();
    }

	public void SE_Active(int se)
	{
		audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[se]);
	}
}
