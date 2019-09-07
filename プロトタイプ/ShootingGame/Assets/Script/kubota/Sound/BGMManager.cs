using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
	AudioSource audiosource;//ＢＧＭを流しているAudioSource

	public AudioClip First_clip;				//開戦時のＢＧＭ
	public AudioClip First_Boss_clip;			//最初のボスのＢＧＭ
	public AudioClip Second_clip;			//ボス語のＢＧＭ
	public AudioClip Final_Boss_clip;           //最後のボスのＢＧＭ

	[Range(0, 1)]
	float fade_num;             //フェードを行う際に使用
	private bool Is_fadeout;		//フェードアウトを行うかどうか
	private bool Is_fadein;			//フェードインを行うかどうか
    // Start is called before the first frame update
    void Start()
    {
		audiosource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			audiosource.clip = First_clip;
			audiosource.Play();
		}
	}

	void fade_out()
	{
		if(fade_num <= 1)
		{
			audiosource.volume = 1f - fade_num;
		}
		fade_num++;
	}


}
