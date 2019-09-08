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

	public int changecnt;			//交換した回数カウント用

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
		if(Is_fadeout)
		{
			if (fade_num <= 1)
			{
				audiosource.volume = 1f - fade_num;
			}
			fade_num++;
		}
	}

	void fade_in()
	{
		if(Is_fadein)
		{
			if(fade_num <= 1)
			{
				audiosource.volume = fade_num;
			}
			fade_num++;
		}
	}

	void Change_BGM()
	{
		switch(changecnt)
		{
			//ボス戦ように変更
			case 0:
				audiosource.clip = First_Boss_clip;
				break;
				//ボス戦後に変更
			case 1:
				audiosource.clip = Second_clip;
				break;
				//最後のボスように変更
			case 2:
				audiosource.clip = Final_Boss_clip;
				break;
		}
	}
}
