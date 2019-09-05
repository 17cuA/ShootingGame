//作成日2019/08/30
// 一面のボス本番_2匹目
// 作成者:諸岡勇樹
/*
 * 2019/08/30　オプションコア格納
 * 2019/09/02　タイムライン格納
 * 2019/09/05　Animationに攻撃タイミングを合わせる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using StorageReference;

public class Two_Boss : character_status
{
	/// <summary>
	/// 攻撃指定用
	/// </summary>
	private enum Attack_Index
	{
			eBio_Laser,
			eMerry_Go_Round,
			eStraight_Line,
			eSmasher,
			eBefore_Rotation,
			eBack_Rotation,
	}
	[Header("ボス形成パーツ")]
	[SerializeField, Tooltip("コア")] private Two_Boss_Parts[] core;
	[SerializeField, Tooltip("オプション")] private Two_Boss_Parts[] multiple;
	[SerializeField, Tooltip("オプションマズル")] private GameObject[] muzzle;
	[SerializeField, Tooltip("ノーダメージコライダー")] private Collider[] no_damage_collider;
	[SerializeField, Tooltip("イエスダメージコライダー")] private Collider[] yes_damage_collider;
	[SerializeField, Tooltip("シャッター")] private Two_Boss_Parts[] shutter;
	[SerializeField, Tooltip("EF用")] private GameObject[] parts_All;

	[Header("アニメーション用")]
	[SerializeField, Tooltip("タイムラインの終了判定")] private bool Is_end_of_timeline;
	[SerializeField, Tooltip("出現タイムライン")] private PlayableAsset Start_Play;
	[SerializeField, Tooltip("死亡タイムライン")] private PlayableAsset Ded_Play;
	[SerializeField, Tooltip("スマッシャータイムライン")] private PlayableAsset Smasher_Play;
	[SerializeField, Tooltip("マルチプルタイムライン")] private PlayableAsset Multiple_1_Play;
	[SerializeField, Tooltip("最終EF")] private GameObject EF_plefab;
	[SerializeField, Tooltip("Animation格納")] private Animation animation_data;

	//------------------------------------------------------------------------------------------------------
	// Unity側では触れないもの
	//------------------------------------------------------------------------------------------------------
	private PlayableDirector Timeline_Player { get; set; }		// タイムラインの情報
	private int Attack_Step { get; set; } // 攻撃行動段階指示用
	private int Frames_In_Function { get; set; }		// 関数内で使うフレーム数
	public float Attack_Seconds { get; private set; } // 攻撃に使う秒数
	private Player1 Player1_Script { get; set; }		// 1P情報
	private Player2 Player2_Script { get; set; }		// 2P情報

	private Quaternion[] Alignment_Angle { get; set; }		// 整列時の角度

	private int Attack_Type_Instruction { get; set; }		// 攻撃種類指示

	private string Playing_Animation { get; set; }		// 再生中のAnimation名
	private string[] Animation_Name { get; set; }		//　Animation名保存
	private List<Two_Boss_Laser> Laser { get; set; }		// レーザー
	private List<Collider> Damage_Collider { get; set; }		// コライダーの段階
	private int Under_Attack { get; set; }
	private float Survival_Time { get; set; }			// せい☆ぞん　時間
	private float Survival_Time_Cnt { get; set; }		// 生存時間カウンター

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

		Damage_Collider = new List<Collider>();
		for(int i = 0; i < shutter.Length; i++)
		{
			Damage_Collider.Add(shutter[i].GetComponent<Collider>());
		}
		Damage_Collider.Add(core[0].GetComponent<Collider>());

		foreach(var col in Damage_Collider)
		{
			col.enabled = false;
		}
		Damage_Collider[0].enabled = true;
		Under_Attack = 0;
		Laser = new List<Two_Boss_Laser>();

		Survival_Time = 180.0f;
	}

	// Update is called once per frame
	private new void Update()
	{
		Survival_Time_Cnt += Time.deltaTime;
		if(Survival_Time_Cnt >= Survival_Time)
		{
			core[0].hp = 0;
		}

		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			Attack_End();
			Attack_Type_Instruction = 3;
		}
		// 攻撃
		if (Attack_Type_Instruction == 0)
		{
			Beam_Attack();
		}
		else if(Attack_Type_Instruction == 1)
		{
			Bacula_And_Smasher();
		}
		else if(Attack_Type_Instruction == 2)
		{
			Rotation_Attack();
		}
		else if(Attack_Type_Instruction == 3)
		{
			Laser_Attack();
		}
		else
		{
			Attack_Type_Instruction = 0;
		}

		// シャッター破壊後コア破壊できる
		// 前のやつが死んだら、次のコライダーを使用できる
		if (!Damage_Collider[Under_Attack].gameObject.activeSelf)
		{
			if (Under_Attack < Damage_Collider.Count -1 )
			{
				Under_Attack++;
				Damage_Collider[Under_Attack].enabled = true;
			}
		}

		// コア破壊で死亡
		if(Is_Core_Annihilation())
		{
			animation_data.Stop();

			//big_core_mk3_EF ef_2 = Instantiate(EF_plefab, transform.position, Quaternion.identity).GetComponent<big_core_mk3_EF>();
			//ef_2.EF_Base.transform.position			= parts_All[0].transform.position;
			//ef_2.EF_Weapon_R.transform.position	= parts_All[1].transform.position;
			//ef_2.EF_Weapon_L.transform.position	= parts_All[2].transform.position;
			//ef_2.Multipl_1.transform.position			= parts_All[3].transform.position;
			//ef_2.Multipl_2.transform.position			= parts_All[4].transform.position;
			//ef_2.Multipl_3.transform.position			= parts_All[5].transform.position;
			//ef_2.Multipl_4.transform.position			= parts_All[6].transform.position;
			//ef_2.Multipl_5.transform.position			= parts_All[7].transform.position;
			//ef_2.Multipl_6.transform.position			= parts_All[8].transform.position;

			//ef_2.EF_Base.transform.rotation			= parts_All[0].transform.rotation;
			//ef_2.EF_Weapon_R.transform.rotation = parts_All[1].transform.rotation;
			//ef_2.EF_Weapon_L.transform.rotation = parts_All[2].transform.rotation;
			//ef_2.Multipl_1.transform.rotation			= parts_All[3].transform.rotation;
			//ef_2.Multipl_2.transform.rotation			= parts_All[4].transform.rotation;
			//ef_2.Multipl_3.transform.rotation			= parts_All[5].transform.rotation;
			//ef_2.Multipl_4.transform.rotation			= parts_All[6].transform.rotation;
			//ef_2.Multipl_5.transform.rotation			= parts_All[7].transform.rotation;
			//ef_2.Multipl_6.transform.rotation			= parts_All[8].transform.rotation;




			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Game_Master.MY.Score_Addition(score, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eTWO_PLAYER);
			}
			base.Died_Process();
		}
	}

	#region ビーム攻撃
	/// <summary>
	/// ビーム攻撃
	/// </summary>
	private void Beam_Attack()
	{ 
		// 攻撃準備
		if(Attack_Step == 0)
		{
			Animation_Playback(Animation_Name[(int)Attack_Index.eStraight_Line]);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			Frames_In_Function++;
			Attack_Seconds += Time.deltaTime;

			if (Attack_Seconds >= 3.5f)
			{
				Shot_Delay++;
				if (Shot_Delay == Shot_DelayMax / 2)
				{
					for(int i = 0;i<multiple.Length;i++)
					{
						if(i % 2 == 0) Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzle[i].transform.position, multiple[i].transform.forward);
					}
				}
				else if (Shot_Delay == Shot_DelayMax )
				{
					for(int i = 0;i<multiple.Length;i++)
					{
						if(i % 2 == 1) Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzle[i].transform.position, multiple[i].transform.forward);
					}

					Shot_Delay = 0;
				}

				if(Attack_Seconds >= 11.1f)
				{
					Next_Step();
				}
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
	}
	#endregion

	#region メリーゴーランド
	private void Rotation_Attack()
	{
		// 攻撃準備
		if (Attack_Step == 0)
		{
			Animation_Playback(Animation_Name[(int)Attack_Index.eMerry_Go_Round]);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			Attack_Seconds += Time.deltaTime;
			if(Attack_Seconds >= 3.5f)
			{
				Shot_Delay++;
				if(Shot_Delay >= Shot_DelayMax /5)
				{
					foreach(var mul in muzzle)
					{
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, mul.transform.position, mul.transform.forward);
					}
					Shot_Delay = 0;
				}

				if(Attack_Seconds >= 15.5f)
				{
					Next_Step();
				}
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
	}
	#endregion

	#region バキュラとスマッシャー
	private void Bacula_And_Smasher()
	{
		// 攻撃準備
		if (Attack_Step == 0)
		{
			Animation_Playback(Animation_Name[(int)Attack_Index.eSmasher]);
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
			//if (Frames_In_Function == 1200)
			//{
			Next_Step();
			//}
		}
		// 攻撃終了
		else if (Attack_Step == 2)
		{
			if (Animation_End())
			{
				Attack_End();
			}
		}
	}
	#endregion

	#region レーザー攻撃
	private void Laser_Attack()
	{
		// 攻撃準備
		if (Attack_Step == 0)
		{
			Laser = new List<Two_Boss_Laser>();
			Animation_Playback(Animation_Name[(int)Attack_Index.eBio_Laser]);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			Attack_Seconds += Time.deltaTime;
			if(Attack_Seconds >= 2.0f)
			{
				Two_Boss_Laser l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[2].transform.position, multiple[2].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[2].transform);
				Laser.Add(l);
				l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[5].transform.position, multiple[5].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[5].transform);
				Laser.Add(l);
			}
			if (Attack_Seconds >= 4.0f)
			{
				Two_Boss_Laser l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[1].transform.position, multiple[1].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[1].transform);
				Laser.Add(l);
				l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[4].transform.position, multiple[4].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[4].transform);
				Laser.Add(l);
			}
			if (Attack_Seconds >= 5.2f)
			{
				Two_Boss_Laser l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[0].transform.position, multiple[0].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[0].transform);
				Laser.Add(l);
				l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[3].transform.position, multiple[3].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[3].transform);
				Laser.Add(l);
			}

			if (Attack_Seconds >= 9.1f)
			{
				foreach(var l in Laser)
				{
					l.Delete_processing();
				}
				Laser.Clear();
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
	}
	#endregion

	/// <summary>
	/// 次の攻撃ステップに移るための処理
	/// </summary>
	private void Next_Step()
	{
		Attack_Step++;
		Attack_Seconds = 0.0f;
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
		if (animation_data.isPlaying)
		{
			animation_data.Stop();
		}
		animation_data.Play(s);
		Playing_Animation = s;
	}
	#endregion

	#region アニメーターの終了検知
	private bool Animation_End()
	{
		return !animation_data.isPlaying;
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
