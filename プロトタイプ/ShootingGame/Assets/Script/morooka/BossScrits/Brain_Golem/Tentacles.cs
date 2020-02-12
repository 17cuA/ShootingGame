//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2020/01/22
//----------------------------------------------------------------------------------------------
// 触手の基底クラス
//----------------------------------------------------------------------------------------------
// 2020/01/22　アニメーションの再生、再生時間、再生順序の設定
// 2020/02/06　本体への追従
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
	/// <summary>
	/// アニメーションの行動
	/// </summary>
	public enum Action
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
	[SerializeField, Tooltip("追従したいオブジェクト")] protected GameObject parent_Obj;

	protected Animation A_Animation { get; set; }				// アニメーションアセット
	protected List<string> AnimName { get; set; }				// アニメーションの名前
	protected int ActionStep { get; set; }								// 攻撃手順指示番号
	protected float Timer { get; set; }									// タイマー
	protected GameObject Player1 { get; set; }					// プレイヤー1の情報格納
	protected GameObject Player2 { get; set; }					// プレイヤー2の情報格納
	protected GameObject NowTarget { get; set; }				// 今のターゲット
	protected GameObject BaseBone { get; private set; }		// 先端を動かすボーン

	public bool Is_Attack { get; protected set; }                        // 攻撃しているかどうか

	private float MyTimer { get; set; }                           // 自分のタイマー

	protected void Start()
	{
		AnimName = new List<string>() { "A_Transition", "A_Wait", "B_Transition", "B_Wait" };
		A_Animation = GetComponent<Animation>();

		for(GameObject tempObj = bone; tempObj.transform.childCount != 0; )
		{ 
			if(tempObj.name == "Bone011")
			{
				BaseBone = tempObj;
				break;
			}
			tempObj = tempObj.transform.GetChild(0).gameObject;
		}

		Player1 = Obj_Storage.Storage_Data.GetPlayer();
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player2 = Obj_Storage.Storage_Data.GetPlayer2();
		}
		NowTarget = Player1;
		Is_Attack = false;

		if (parent_Obj != null)
		{
			transform.parent.parent = parent_Obj.transform;
		}
	}

	protected void Update()
	{
		if (nowAnimation == Action.eA_WAIT || nowAnimation == Action.eB_WAIT)
		{
			MyTimer += Time.deltaTime;
			if (MyTimer >= aWaitTime && nowAnimation == Action.eA_WAIT)
			{
				ChangeAnimation(Action.eB_TRANSITION);
				MyTimer = 0.0f;
			}
			else if (MyTimer >= bWaitTime && nowAnimation == Action.eB_WAIT)
			{
				ChangeAnimation(Action.eA_TRANSITION);
				MyTimer = 0.0f;
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
	private void ChangeAnimation(Action nextAction)
	{
		A_Animation.CrossFade(AnimName[(int)nextAction]);
		nowAnimation = nextAction;
	}

	/// <summary>
	/// 3次元ベクトル→2次元ベクトル
	/// </summary>
	/// <param name="temp"></param>
	/// <returns></returns>
	protected Vector2 VectorChange_3To2(Vector3 temp)
	{
		return new Vector2(temp.x, temp.y);
	}
}
