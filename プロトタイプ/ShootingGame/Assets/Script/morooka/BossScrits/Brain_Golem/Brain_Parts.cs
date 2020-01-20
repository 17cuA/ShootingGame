using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Parts : character_status
{
	enum Action
	{
		eA_TRANSITION,				//	A状態に移行アニメ
		eA_WAIT,				//	A状態
		eB_TRANSITION,		//	B状態に移行アニメ
		eB_WAIT,		// B状態
	}

	[SerializeField, Header("今のアニメ")] private Action nowAnimation;
	[SerializeField] private float Timer;

	private Animation _Animation { get; set; }
	private List<string> AnimName { get; set; }


	private void Start()
	{
		AnimName = new List<string>(){ "A_Transition", "A_Wait", "B_Transition", "B_Wait" };
		_Animation = GetComponent<Animation>();
	}

	private void Update()
	{
		Timer += Time.deltaTime;
		if(!_Animation.IsPlaying(AnimName[(int)Action.eA_TRANSITION]) && nowAnimation == Action.eA_TRANSITION)
		{
			_Animation.CrossFade(AnimName[(int)Action.eB_TRANSITION]);
			nowAnimation = Action.eB_TRANSITION;
		}
		else if (!_Animation.IsPlaying(AnimName[(int)Action.eA_WAIT]) && nowAnimation == Action.eA_WAIT)
		{
			_Animation.CrossFade(AnimName[(int)Action.eB_WAIT]);
			nowAnimation = Action.eB_WAIT;
		}

			if(_Animation.IsPlaying(AnimName[(int)Action.eB_TRANSITION]) && Timer > 6.0f)
			{
				_Animation.CrossFade(AnimName[(int)Action.eA_WAIT]);
				nowAnimation = Action.eA_WAIT;
			Timer = 0.0f;
			}
			else if(_Animation.IsPlaying(AnimName[(int)Action.eB_WAIT]) && Timer > 6.0f)
			{
				_Animation.CrossFade(AnimName[(int)Action.eA_TRANSITION]);
				nowAnimation = Action.eA_TRANSITION;
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
