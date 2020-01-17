using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Parts : character_status
{
	enum Action
	{
		eANIM1,				//	1状態に移行アニメ
		eANIM2,				//	2状態に移行アニメ
		eANIM1_STAY,		//	1状態待機アニメ
		eANIM2_STAY,		// 2状態待機アニメ
	}

	private Animation _Animation { get; set; }
	private List<string> AnimName { get; set; }
	private float Timer { get; set; }

	private void Start()
	{
		AnimName = new List<string>(){"Anim1","Anim2","Anim1_Stay","Anim2_Stay"};
		_Animation = GetComponent<Animation>();
	}

	private void Update()
	{
		Timer += Time.deltaTime;
		if(_Animation.IsPlaying(AnimName[(int)Action.eANIM1]))
		{
			_Animation.CrossFade(AnimName[(int)Action.eANIM1_STAY]);
		}
		else if (_Animation.IsPlaying(AnimName[(int)Action.eANIM2]))
		{
			_Animation.CrossFade(AnimName[(int)Action.eANIM2_STAY]);
		}

		if(Timer > 6.0f)
		{
			if(_Animation.IsPlaying(AnimName[(int)Action.eANIM1_STAY]))
			{
				_Animation.CrossFade(AnimName[(int)Action.eANIM2]);
			}
			else if(_Animation.IsPlaying(AnimName[(int)Action.eANIM2_STAY]))
			{
				_Animation.CrossFade(AnimName[(int)Action.eANIM1]);
			}
			Timer = 0.0f;
		}

		// HPが0のとき
		if (hp < 1)
		{
			Died_Judgment();
			enabled = false;
		}
	}
}
