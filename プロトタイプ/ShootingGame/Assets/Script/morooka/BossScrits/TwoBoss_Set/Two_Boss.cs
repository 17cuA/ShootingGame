//作成日2019/08/30
// 一面のボス本番_2匹目
// 作成者:諸岡勇樹
/*
 * 2019/08/30　オプションコア格納
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
	}

	// Update is called once per frame
	private new void Update()
	{
		if(Attack_Type_Instruction == 0)
		{
			Bullet_Attack();
		}
		else
		{
			Attack_Type_Instruction = 0;
		}

		if(Is_Core_Annihilation())
		{

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
			Timeline_Player.Play(Multiple_1_Play);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			if(Is_end_of_timeline)
			{
				Timeline_Player.Pause();
				Next_Step();
			}
		}
		// 攻撃
		else if(Attack_Step == 2)
		{
			Frames_In_Function++;
			Shot_Delay++;

			// 1Pのとき
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{

				//foreach (Two_Boss_Parts TwoP in multiple)
				//{
				//	Quaternion targetRotation = Quaternion.LookRotation(Player1_Script.transform.position - TwoP.transform.position);
				//	TwoP.transform.localRotation = Quaternion.Slerp(TwoP.transform.localRotation, targetRotation, Time.deltaTime);
				//}

				if (Shot_Delay > Shot_DelayMax)
				{
					foreach(Two_Boss_Parts TwoP in multiple)
					{
						Vector3 direction = Player1_Script.transform.position - TwoP.transform.position;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, TwoP.transform.position, direction);
					}
					Shot_Delay = 0;
				}
			}
			// 2Pのとき
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				//for (int i = 0; i < multiple.Length; i++)
				//{
				//	if (i % 2 == 0)
				//	{
				//		Quaternion targetRotation = Quaternion.LookRotation(Player1_Script.transform.position - multiple[i].transform.position);
				//		multiple[i].transform.localRotation = Quaternion.Slerp(multiple[i].transform.localRotation, targetRotation, Time.deltaTime);
				//	}
				//	else
				//	{
				//		Quaternion targetRotation = Quaternion.LookRotation(Player2_Script.transform.position - multiple[i].transform.position);
				//		multiple[i].transform.localRotation = Quaternion.Slerp(multiple[i].transform.localRotation, targetRotation, Time.deltaTime);
				//	}
				//}

				if (Shot_Delay > Shot_DelayMax / 2)
				{
					for(int i = 0; i < multiple.Length; i++)
					{
						if(i % 2 == 0)
						{
							Vector3 direction = Player1_Script.transform.position - multiple[i].transform.position;
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, multiple[i].transform.position, direction);
						}
						else
						{
							Vector3 direction = Player2_Script.transform.position - multiple[i].transform.position;
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, multiple[i].transform.position, direction);
						}
					}
					Shot_Delay = 0;
				}
			}
			
			// 約20秒
			if (Frames_In_Function == 1200)
			{
				Next_Step();
			}
		}
		// 後かたずけ
		else if(Attack_Step == 3)
		{
			bool ok = true;

			//for (int i = 0; i < multiple.Length; i++)
			//{
			//	if (i < multiple.Length / 2)
			//	{
			//		if (multiple[i].transform.rotation != Alignment_Angle[0])
			//		{
			//			ok = false;
			//			multiple[i].transform.rotation = Quaternion.Lerp(multiple[i].transform.rotation, Alignment_Angle[0], 0.1f);
			//		}
			//	}
			//	else if (i > multiple.Length / 2)
			//	{
			//		if (multiple[i].transform.rotation != Alignment_Angle[1])
			//		{
			//			ok = false;
			//			multiple[i].transform.rotation = Quaternion.Lerp(multiple[i].transform.rotation, Alignment_Angle[1], 0.1f);
			//		}
			//	}
			//}

			if(ok)
			{
				Timeline_Player.Play(Multiple_1_Play);
				Timeline_Player.time = 8.0;
				Next_Step();
			}
		}
		else if(Attack_Step == 4)
		{
			if (Is_end_of_timeline)
			{
				Timeline_Player.Stop();
				Attack_End();
			}
		}
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
	/// タイムラインの再生
	/// </summary>
	/// <param name="time_line"> 再生したいタイムライン(PlayableAsset) </param>
	/// <param name="time"> 再生開始時間 </param>
	private void Timeline_Playback(ref PlayableAsset time_line, double time)
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
