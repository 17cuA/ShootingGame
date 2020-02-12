//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2020/01/22
//----------------------------------------------------------------------------------------------
// バルカン・レーザー射出触手
//----------------------------------------------------------------------------------------------
// 2020/01/22　バルカン・レーザー射出触手
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Balkan_Tentacles : Tentacles
{
	[SerializeField, Tooltip("攻撃ディレイ")] private float attackDelay;
	[SerializeField, Tooltip("攻撃マズル")] private GameObject muzzle;
	[SerializeField, Tooltip("攻撃時の弾の数")] private int[] numberOfBullets;

	private string Play_AnimationName;

	new private void Start()
	{
		base.Start();
		Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
	}
	new private void Update()
	{
		if (parent_Obj != null)
		{
			var temp = VectorChange_3To2(transform.parent.position - Vector3.zero);
			transform.parent.right = temp;
		}

		if (Is_Attack)
		{
			if (ActionStep == 0)
			{
				// ターゲットが一番上
				if (Is_TargetPosTop())
				{
					// 自身が一番下
					if(Is_BaseBonePosBottom())
					{
						Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
						A_Animation.Play(Play_AnimationName);
					}
					// 自身が中央
					else if(Is_BaseBonePosMiddle())
					{
						Play_AnimationName = AnimName[(int)Action.eA_WAIT];
						A_Animation.Play(Play_AnimationName);
					}
					// つまり同ライン
					else
					{
						ActionStep++;
					}
				}
				// ターゲットが中央
				else if(Is_TargetPosMiddle())
				{
					// 自身が一番下
					if (Is_BaseBonePosBottom())
					{
						Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
						A_Animation.Play(Play_AnimationName);
					}
					// 自身が一番上
					else if (Is_BaseBoonPosTop())
					{
						Play_AnimationName = AnimName[(int)Action.eA_WAIT];
						A_Animation.Rewind(Play_AnimationName);
					}
					// つまり同ライン
					else
					{
						ActionStep++;
					}
				}
				// ターゲットが一番下
				else if(Is_TargetPosMiddle())
				{
					// 自身が中央
					if (Is_BaseBonePosBottom())
					{
						Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
						A_Animation.Rewind(Play_AnimationName);
					}
					// 自身が一番上
					else if (Is_BaseBoonPosTop())
					{
						Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
						A_Animation.Rewind(Play_AnimationName);
					}
					// つまり同ライン
					else
					{
						ActionStep++;
					}
				}
				ActionStep++;
			}
			else if (ActionStep == 1)
			{
				// 移動終了時
				if(!A_Animation.IsPlaying(Play_AnimationName))
				{
					ActionStep++;
				}
			}
			else if (ActionStep == 2)
			{
				var obj =  Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eCONTAINER, muzzle.transform.position, muzzle.transform.right);
				var pos = obj.transform.position;
				pos.z = 0.0f;
				obj.transform.position = pos;

				Timer = 0.0f;
				ActionStep = 0;
				Is_Attack = false;
			}
		}
		else
		{
			Timer += Time.deltaTime;
			if (attackDelay < Timer) Is_Attack = true;
		}
	}

	private bool Is_TargetPosTop()
	{
		return NowTarget.transform.position.y > 2.0f;
	}
	private bool Is_TargetPosMiddle()
	{
		return NowTarget.transform.position.y <= 2.0f && NowTarget.transform.position.y >= -2.0f;
	}
	private bool Is_TargetPosBottom()
	{
		return NowTarget.transform.position.y < -2.0f;
	}
	private bool Is_BaseBoonPosTop()
	{
		return BaseBone.transform.position.y > 2.0f;
	}
	private bool Is_BaseBonePosMiddle()
	{
		return BaseBone.transform.position.y <= 2.0f && NowTarget.transform.position.y >= -2.0f;
	}
	private bool Is_BaseBonePosBottom()
	{
		return BaseBone.transform.position.y < -2.0f;
	}

}
