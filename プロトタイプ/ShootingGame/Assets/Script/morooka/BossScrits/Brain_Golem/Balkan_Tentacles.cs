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
	private delegate void HorizontalGoal_Operator();

	[SerializeField, Tooltip("攻撃ディレイ")] private float attackDelay;
	[SerializeField, Tooltip("攻撃マズル")] private GameObject muzzle;
	[SerializeField, Tooltip("弾撃つ間隔")] private int shootingInterval_Max;
	[SerializeField, Tooltip("攻撃時の弾の数")] private int[] numberOfBullets;

	private string Play_AnimationName;
	private GameObject horizontalGoal;
	private POSITIOH_NAME nowPosName;
	private int shootingInterval_Cnt;
	private int numberOfBullets_Cnt;
	private int numberOfBullets_Index;
	HorizontalGoal_Operator[] horizontalGoal_Operators;
	private int horizontalGoal_Operators_Index;
	private bool bunbun;

	new private void Start()
	{
		base.Start();
		Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
		horizontalGoal = new GameObject();
		horizontalGoal.transform.position = BaseBone.transform.position;
		nowPosName = POSITIOH_NAME.eMIDOLLE;
		shootingInterval_Cnt = 0;
		numberOfBullets_Cnt = 0;
		numberOfBullets_Index = 0;
		horizontalGoal_Operators_Index = 0;
		horizontalGoal_Operators = new HorizontalGoal_Operator[2]
		{
			new HorizontalGoal_Operator(HorizontalGoal_KeepHorizontal),
			new HorizontalGoal_Operator(HorizontalGoal_SinMove),
		};
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
				#region ターゲット位置参照、移動先決定
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
				}
				// ターゲットが中央
				else if (Is_TargetPosMiddle())
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
				}
				// ターゲットが一番下
				else if (Is_TargetPosBottom())
				{
					switch (nowPosName)
					{
						case POSITIOH_NAME.eTOP:
							Play_AnimationName = AnimName[(int)Action.eB_TRANSITION];
							AnimationReversePlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eMIDOLLE:
							Play_AnimationName = AnimName[(int)Action.eA_TRANSITION];
							AnimationReversePlay(Play_AnimationName);
							break;
						case POSITIOH_NAME.eBOTTOM:
							ActionStep++;
							break;
						default:
							break;
					}
				}
				#endregion
				if (Play_AnimationName == AnimName[(int)Action.eA_TRANSITION]) nowPosName = A_Animation[Play_AnimationName].speed > 0.0 ? POSITIOH_NAME.eMIDOLLE : POSITIOH_NAME.eBOTTOM;
				else if (Play_AnimationName == AnimName[(int)Action.eA_WAIT]) nowPosName = A_Animation[Play_AnimationName].speed > 0.0 ? POSITIOH_NAME.eTOP : POSITIOH_NAME.eMIDOLLE;
				else if (Play_AnimationName == AnimName[(int)Action.eB_TRANSITION]) nowPosName = A_Animation[Play_AnimationName].speed > 0.0 ? POSITIOH_NAME.eTOP : POSITIOH_NAME.eBOTTOM;
				ActionStep++;
			}
			else if (ActionStep == 1)
			{
				HorizontalGoal_KeepHorizontal();

				// 移動終了時
				if (!A_Animation.IsPlaying(Play_AnimationName))
				{
					ActionStep++;
				}
			}
			else if (ActionStep == 2)
			{
				if (shootingInterval_Cnt > shootingInterval_Max)
				{
					var obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle.transform.position, -muzzle.transform.right);
					var pos = obj.transform.position;
					pos.z = 0.0f;
					obj.transform.position = pos;
					numberOfBullets_Cnt++;
					shootingInterval_Cnt = 0;
				}

				horizontalGoal_Operators[horizontalGoal_Operators_Index]();

				shootingInterval_Cnt++;
				if (numberOfBullets_Cnt > numberOfBullets[numberOfBullets_Index])
				{
					ActionStep++;
				}
			}
			else if(ActionStep == 3)
			{ 
				// ターゲット変更
				if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
				{
					if (NowTarget == Player1 && Player2.activeSelf) NowTarget = Player2;
					if (NowTarget == Player2 && Player1.activeSelf) NowTarget = Player1;
				}

				if (numberOfBullets_Index == numberOfBullets.Length - 1) numberOfBullets_Index = 0;
				else numberOfBullets_Index++;
				if (horizontalGoal_Operators_Index == horizontalGoal_Operators.Length - 1) horizontalGoal_Operators_Index = 0;
				else horizontalGoal_Operators_Index++;

				 numberOfBullets_Cnt = 0;
				Timer = 0.0f;
				ActionStep = 0;
				Is_Attack = false;
			}
		}
		else
		{
			if(bunbun)
			{
					A_Animation.CrossFade(AnimName[(int)Action.eB_WAIT], 5.0f);
			}
			else
			{
				#region マズルの向き調整
				HorizontalGoal_KeepHorizontal();
				#endregion

				Timer += Time.deltaTime;
				if (attackDelay < Timer) Is_Attack = true;
			}
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

	// 水平目標のSin移動
	private void HorizontalGoal_SinMove()
	{
		var tempPos = BaseBone.transform.position;
		tempPos.x = -2.0f;
		tempPos.y += 7.0f * Mathf.Sin(Time.time * 2.0f); 
		horizontalGoal.transform.position = tempPos;
		tempPos = BaseBone.transform.position - horizontalGoal.transform.position;
		BaseBone.transform.right = tempPos;
	}
	// 水平目標の水平維持
	private void HorizontalGoal_KeepHorizontal()
	{
		var tempPos = BaseBone.transform.position;
		tempPos.x = -2.0f;
		horizontalGoal.transform.position = tempPos;
		tempPos = BaseBone.transform.position - horizontalGoal.transform.position;
		BaseBone.transform.right = Vector3.MoveTowards(BaseBone.transform.right, tempPos, Time.deltaTime * 10.0f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(!Is_Attack)
		{
			if(other.tag == "Player") bunbun = true;
		}
	}
}
