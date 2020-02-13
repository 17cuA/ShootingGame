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
	private enum POSITIOH_NAME
	{
		eTOP,
		eMIDOLLE,
		eBOTTOM,
	}
	[SerializeField, Tooltip("攻撃ディレイ")] private float attackDelay;
	[SerializeField, Tooltip("攻撃マズル")] private GameObject muzzle;
	[SerializeField, Tooltip("攻撃時の弾の数")] private int[] numberOfBullets;

	private string Play_AnimationName;
	private GameObject horizontalGoal;
	private POSITIOH_NAME nowPosName;
	new private void Start()
	{
		base.Start();
		Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
		horizontalGoal = new GameObject();
		horizontalGoal.transform.position = BaseBone.transform.position;

		nowPosName = POSITIOH_NAME.eMIDOLLE;
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
					switch (nowPosName)
					{
						case POSITIOH_NAME.eTOP:
							ActionStep++;
							break;
						case POSITIOH_NAME.eMIDOLLE:
							Play_AnimationName = AnimName[(int)Action.eA_WAIT];
							AnimationPlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eBOTTOM:
							Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
							AnimationPlay(Play_AnimationName);
							break;
						default:
							break;
					}

					nowPosName = POSITIOH_NAME.eTOP;
					//// つまり同ライン
					//if (Is_BaseBoonPosTop())
					//{
					//	ActionStep++;
					//}
					//// 自身が中央
					//else if(Is_BaseBonePosMiddle())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eA_WAIT];
					//	AnimationPlay(Play_AnimationName);
					//}
					//// 自身が一番下
					//else if (Is_BaseBonePosBottom())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
					//	AnimationPlay(Play_AnimationName);
					//}
				}
				// ターゲットが中央
				else if(Is_TargetPosMiddle())
				{
					switch (nowPosName)
					{
						case POSITIOH_NAME.eTOP:
							Play_AnimationName = AnimName[(int)Action.eA_WAIT];
							AnimationReversePlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eMIDOLLE:
							ActionStep++;
							break;
						case POSITIOH_NAME.eBOTTOM:
							Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
							AnimationPlay(Play_AnimationName);
							break;
						default:
							break;
					}

					nowPosName = POSITIOH_NAME.eMIDOLLE;
					//// 自身が一番上
					//if (Is_BaseBoonPosTop())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eA_WAIT];
					//	AnimationReversePlay(Play_AnimationName);
					//}
					//// つまり同ライン
					//else if (Is_BaseBonePosMiddle())
					//{
					//	ActionStep++;
					//}
					//// 自身が一番下
					//else if (Is_BaseBonePosBottom())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
					//	AnimationPlay(Play_AnimationName);
					//}
				}
				// ターゲットが一番下
				else if(Is_TargetPosBottom())
				{
					switch (nowPosName)
					{
						case POSITIOH_NAME.eTOP:
							Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
							AnimationReversePlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eMIDOLLE:
							Play_AnimationName = AnimName[(int)Action.eA_WAIT];
							AnimationReversePlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eBOTTOM:
							ActionStep++;
							break;
						default:
							break;
					}
					nowPosName = POSITIOH_NAME.eBOTTOM;
					//nowPosName = POSITIOH_NAME.eMIDOLLE;
					//// 自身が一番上
					//if (Is_BaseBoonPosTop())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
					//	AnimationReversePlay(Play_AnimationName);
					//}
					//// 自分が中心
					//else if (Is_BaseBonePosMiddle())
					//{
					//	Play_AnimationName = AnimName[(int)Action.eA_WAIT];
					//	AnimationReversePlay(Play_AnimationName);
					//}
					//// つまり同ライン
					//else if (Is_BaseBonePosBottom())
					//{
					//	ActionStep++;
					//}
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
				var obj =  Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle.transform.position, -muzzle.transform.right);
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

		#region
		var tempPos = BaseBone.transform.position;
		tempPos.x = -2.0f;
		horizontalGoal.transform.position = tempPos;
		tempPos = BaseBone.transform.position - horizontalGoal.transform.position;
		BaseBone.transform.right = tempPos;
		#endregion
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
		return BaseBone.transform.position.y <= 2.0f && BaseBone.transform.position.y >= -2.0f;
	}
	private bool Is_BaseBonePosBottom()
	{
		return BaseBone.transform.position.y < -2.0f;
	}


}
