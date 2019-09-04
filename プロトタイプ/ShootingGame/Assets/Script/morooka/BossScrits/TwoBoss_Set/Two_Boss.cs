//作成日2019/08/30
// 一面のボス本番_2匹目
// 作成者:諸岡勇樹
/*
 * 2019/08/30　オプションコア格納
 * 2019/09/02　タイムライン格納
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using StorageReference;

public class Two_Boss : character_status
{
	[Header("ボス形成パーツ")]
	[SerializeField, Tooltip("コア")] private Two_Boss_Parts[] core;
	[SerializeField, Tooltip("オプション")] private Two_Boss_Parts[] multiple;
	[SerializeField, Tooltip("ノーダメージコライダー")] private Collider[] no_damage_collider;
	[SerializeField, Tooltip("イエスダメージコライダー")] private Collider[] yes_damage_collider;

	[Header("アニメーション用")]
	[SerializeField, Tooltip("タイムラインの終了判定")] private bool Is_end_of_timeline;
	[SerializeField, Tooltip("出現タイムライン")] private PlayableAsset Start_Play;
	[SerializeField, Tooltip("死亡タイムライン")] private PlayableAsset Ded_Play;
	[SerializeField, Tooltip("スマッシャータイムライン")] private PlayableAsset Smasher_Play;
	[SerializeField, Tooltip("マルチプルタイムライン")] private PlayableAsset Multiple_1_Play;
	[SerializeField, Tooltip("Animation格納")] private Animator animation_data;

	[Header("攻撃フラグ")]
	[SerializeField, Tooltip("バレット発射")] private bool Is_Bullet_Attack_Multiple;

	//------------------------------------------------------------------------------------------------------
	// Unity側では触れないもの
	//------------------------------------------------------------------------------------------------------
	private PlayableDirector Timeline_Player { get; set; }		// タイムラインの情報
	private int Attack_Step { get; set; } // 攻撃行動段階指示用
	private int Frames_In_Function { get; set; }		// 関数内で使うフレーム数
	private Player1 Player1_Script { get; set; }		// 1P情報
	private Player2 Player2_Script { get; set; }		// 2P情報

	private Quaternion[] Alignment_Angle { get; set; }		// 整列時の角度

	private int Attack_Type_Instruction { get; set; }		// 攻撃種類指示

	private string Playing_Animation { get; set; }
	private string[] Animation_Name { get; set; }

	private new void Start()
	{
		base.Start();
		Timeline_Player = GetComponent<PlayableDirector>();
		Attack_Step = 0;
		Attack_Type_Instruction = 0;
		Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER) Player2_Script = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();

		// 角度保存
		Alignment_Angle = new Quaternion[2] { Quaternion.Euler(0.0f, 90.0f, 0.0f),Quaternion.Euler(0.0f, -90.0f, 0.0f) };

		Animation_Name = new string[6]
		{
			"Bio_Laser",
			"Merry_Go_Round",
			"Straight_Line",
			"Smasher",
			"Before_Rotation",
			"Back_Rotation",
		};
	}

	// Update is called once per frame
	private new void Update()
	{
		if (Attack_Type_Instruction == 0)
		{
			Bullet_Attack();
		}
		else
		{
			Frames_In_Function++;
			if (Frames_In_Function == 2)
			{
				Attack_Type_Instruction = 0;
			}
		}

		if(Is_Core_Annihilation())
		{
			base.Died_Process();
		}
	}

	#region 弾丸攻撃
	/// <summary>
	/// 弾丸攻撃
	/// </summary>
	private void Bullet_Attack()
	{ 
		// 攻撃準備
		if(Attack_Step == 0)
		{
			Animation_Playback(Animation_Name[2]);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			Frames_In_Function++;
			Shot_Delay++;

			if (Shot_Delay > Shot_DelayMax)
			{

				Shot_Delay = 0;
			}

			// 約20秒
			if (Frames_In_Function == 1200)
			{
				Next_Step();
			}
		}
		// 攻撃終了
		else if (Attack_Step == 2)
		{
			if (Animation_End())
			{
				Attack_End();
			}
		}
		//// 後かたずけ
		//else if(Attack_Step == 3)
		//{
		//	bool ok = true;

		//	if(ok)
		//	{
		//		Timeline_Player.Play(Multiple_1_Play);
		//		Timeline_Player.time = 8.0;
		//		Next_Step();
		//	}
		//}
		//else if(Attack_Step == 4)
		//{
		//	if (Is_end_of_timeline)
		//	{
		//		Timeline_Player.Stop();
		//	}
		//}
	}
	#endregion

	/// <summary>
	/// 次の攻撃ステップに移るための処理
	/// </summary>
	private void Next_Step()
	{
		Attack_Step++;
		Frames_In_Function = 0;
	}

	/// <summary>
	/// 攻撃終了時の処理
	/// </summary>
	private void Attack_End()
	{
		Attack_Step = 0;
		Frames_In_Function = 0;
		Attack_Type_Instruction++;
	}

	#region タイムラインの再生
	/// <summary>
	/// タイムラインの再生
	/// </summary>
	/// <param name="time_line"> 再生したいタイムライン(PlayableAsset) </param>
	private void Timeline_Playback(ref PlayableAsset time_line)
	{
		// 再生しているのもがあれば停止
		if(Timeline_Player.state != PlayState.Playing)
		{
			Timeline_Player.Stop();
		}

		Timeline_Player.Play(time_line);
	}

	/// <summary>
	/// アニメーションの再生
	/// </summary>
	/// <param name="time_line"> 再生したいタイムライン(PlayableAsset) </param>
	/// <param name="time"> 再生開始時間 </param>
	private void Animation_Playback(ref PlayableAsset time_line, double time)
	{
		// 再生しているのもがあれば停止
		if (Timeline_Player.state != PlayState.Playing)
		{
			Timeline_Player.Stop();
		}

		Timeline_Player.Play(time_line);
		Timeline_Player.time = time;
	}
	#endregion
	#region アニメーションの再生
	/// <summary>
	/// アニメーションの再生
	/// </summary>
	/// <param name="time_line"> 再生したいタイムライン(PlayableAsset) </param>
	private void Animation_Playback(string s)
	{
		// 再生しているのもがあれば停止
		if (Playing_Animation != null)
		{
			if (!Animation_End())
				animation_data.SetBool(Playing_Animation, false);
		}
		animation_data.SetBool(s　,true);
		Playing_Animation = s;
	}
	#endregion

	#region アニメーターの終了検知
	private bool Animation_End()
	{
		AnimatorStateInfo animInfo = animation_data.GetCurrentAnimatorStateInfo(0);
		return !(animInfo.normalizedTime < 1.0f);
	}
	#endregion

	#region コアの生存確認
	/// <summary>
	/// コアが全滅しているか返す
	/// </summary>
	/// <returns>全滅で true </returns>
	private bool Is_Core_Annihilation()
	{
		bool survival = true;

		foreach(Two_Boss_Parts core in core)
		{
			if(core.gameObject.activeSelf)
			{
				survival = false;
			}
		}
		return survival;
	}
	#endregion
}
