﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
	/// <summary>
	/// アニメーションの行動
	/// </summary>
	enum Action
	{
		eA_TRANSITION,		//	A状態に移行アニメ
		eA_WAIT,					//	A状態
		eB_TRANSITION,		//	B状態に移行アニメ
		eB_WAIT,					// B状態
	}

	[SerializeField, Header("今のアニメ")] private Action nowAnimation;		
	[SerializeField, Tooltip("Aの待機状態の維持時間")] private float aWaitTime;
	[SerializeField, Tooltip("Bの待機状態の維持時間")] private float bWaitTime;
	[SerializeField, Tooltip("ボーンの先頭")] private GameObject bone;

	private Animation A_Animation { get; set; }			// アニメーションアセット
	private List<string> AnimName { get; set; }			// アニメーションの名前
	private float Timer { get; set; }								// タイマー

	public List<GameObject> Bones { get; private set; }		// ボーンの格納
	public GameObject BaseBone { get; private set; }			// 先端を動かすボーン

	private void Start()
	{
		AnimName = new List<string>() { "A_Transition", "A_Wait", "B_Transition", "B_Wait" };
		A_Animation = GetComponent<Animation>();

		Bones = new List<GameObject>();
		foreach(GameObject temp in bone.transform)
		{
			Bones.Add(temp);
			if(temp.name == "Bone011")
			{
				BaseBone = temp;
			}
		}
	}

	private void Update()
	{
		if (nowAnimation == Action.eA_WAIT || nowAnimation == Action.eB_WAIT)
		{
			Timer += Time.deltaTime;
			if (Timer >= aWaitTime && nowAnimation == Action.eA_WAIT)
			{
				ChangeAnimation(Action.eB_TRANSITION);
			}
			else if (Timer >= bWaitTime && nowAnimation == Action.eB_WAIT)
			{
				ChangeAnimation(Action.eA_TRANSITION);
			}
		}
		else if(nowAnimation == Action.eA_TRANSITION || nowAnimation == Action.eB_TRANSITION)
		{
			if(!A_Animation.IsPlaying(AnimName[(int)Action.eA_TRANSITION]) && nowAnimation == Action.eA_TRANSITION)
			{
				ChangeAnimation(Action.eA_WAIT);
			}
			else if(!A_Animation.IsPlaying(AnimName[(int)Action.eB_TRANSITION]) && nowAnimation == Action.eB_TRANSITION)
			{
				ChangeAnimation(Action.eB_WAIT);
			}
		}
	}

	/// <summary>
	/// 次のアニメーション
	/// </summary>
	/// <param name="nextAction"> 次に再生するアニメーション </param>
	void ChangeAnimation(Action nextAction)
	{
		A_Animation.CrossFade(AnimName[(int)nextAction]);
		nowAnimation = nextAction;
	}
}
