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
		eBefore_Rotation,
		eBack_Rotation,
		eSmasher_1,
		eSmasher_2,
		eIdol,
		eCrossing,
	}

	[Header("ボス形成パーツ")]
	[SerializeField, Tooltip("コア")] private Two_Boss_Parts[] core;
	[SerializeField, Tooltip("オプション")] private Two_Boss_Parts[] multiple;
	[SerializeField, Tooltip("オプションマズル")] private GameObject[] muzzle;
	[SerializeField, Tooltip("ノーダメージコライダー")] private Collider[] set_collider;
	[SerializeField, Tooltip("シャッター")] private Two_Boss_Parts[] shutter;
	[SerializeField, Tooltip("EF用")] private GameObject[] parts_All;
	[SerializeField, Tooltip("狙撃系マズル")] private GameObject[] Snipes;

	[Header("アニメーション用")]
	[SerializeField, Tooltip("タイムラインの終了判定")] private bool Is_end_of_timeline;
	[SerializeField, Tooltip("出現タイムライン")] private PlayableAsset Start_Play;
	[SerializeField, Tooltip("スマッシャータイムライン")] private PlayableAsset Smasher_Play;
	[SerializeField, Tooltip("マルチプルタイムライン")] private PlayableAsset Merry_1_Play;
	[SerializeField, Tooltip("ボスバキュラ")]private GameObject Boss_Bacula;
	[SerializeField, Tooltip("最終EF")] private GameObject EF_plefab;
	[SerializeField, Tooltip("Animation格納")] private Animation animation_data;
	[SerializeField, Tooltip("メッシュ")] private Renderer[] Mesh_Renderer;
	[SerializeField, Tooltip("スマッシャージェット")] private ParticleSystem[] smasher_jet;
	[SerializeField, Tooltip("スマッシャーノズル")] private ParticleSystem[] smasher_nozzle;

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
	private float Survival_Time_Cnt { get; set; }       // 生存時間カウンター

	private bool Update_Ok { get; set; }        // アップデート

	private Vector3[] shutter_Init
	{
		get;set;
	}		// なぜか移動するから
	private Vector3 core_Init
	{
		get;set;
	}		// なぜか移動するから

	private new void Start()
	{
		base.Start();
		Update_Ok = false;
 		Timeline_Player = GetComponent<PlayableDirector>();
		Attack_Step = 0;
		Attack_Type_Instruction = 0;
		Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER) Player2_Script = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();

		// 角度保存
		Alignment_Angle = new Quaternion[2] { Quaternion.Euler(0.0f, 90.0f, 0.0f),Quaternion.Euler(0.0f, -90.0f, 0.0f) };

		Animation_Name = new string[9]
		{
			"Bio_Laser",
			"Merry_Go_Round",
			"Straight_Line",
			"Before_Rotation",
			"Back_Rotation",
			"Smasher_1",
			"Smasher_2",
			"Idol",
			"Crossing",
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
		Under_Attack = 0;
		Laser = new List<Two_Boss_Laser>();

		Survival_Time = 60.0f * 5.0f;

		Timeline_Player.Play(Start_Play);

		shutter_Init = new Vector3[shutter.Length];
		for (int i = 0; i < shutter_Init.Length; i++)
		{
			shutter_Init[i] = shutter[i].transform.localPosition;
		}
		core_Init = core[0].transform.localPosition;

		// コライダーの非使用
		foreach (var col in set_collider)
		{
			col.enabled = false;
		}

		//武器の使用不可
		foreach(var sp in Snipes)
		{
			sp.SetActive(false);
		}

	}

	// Update is called once per frame
	private new void Update()
	{
		if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.O)) core[0].hp = 0;

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

		if(!Update_Ok)
		{
			if (Is_end_of_timeline)
			{
				Update_Ok = true;
				Destroy(transform.GetChild(1).gameObject);
				Destroy(transform.GetChild(2).gameObject);
				Damage_Collider[0].enabled = true;

				// コライダーの使用
				foreach (var col in set_collider)
				{
					col.enabled = true;
				}

				// 武器の使用
				for(int i = 0; i<Snipes.Length/2;i++)
				{
					Snipes[i].SetActive(true);
					Snipes[i].GetComponent<Sniper_Muzzle>().Shot_Delay -= i * 60;
					Snipes[i+3].SetActive(true);
					Snipes[i+3].GetComponent<Sniper_Muzzle>().Shot_Delay -= i * 60;
				}
				foreach (var r in Mesh_Renderer)
				{
					r.enabled = true;
				}
			}
		}
		else if(Update_Ok)
		{
			var v3 = transform.position;
			v3.z = 0.0f;
			transform.position = v3;

			// 攻撃
			if (Attack_Type_Instruction == 0)
			{
				Beam_Attack();
			}
			else if (Attack_Type_Instruction == 1)
			{
				Bacula_And_Smasher();
			}
			else if (Attack_Type_Instruction == 2)
			{
				Laser_Attack();

			}
			else if (Attack_Type_Instruction == 3)
			{
				Rotation_Attack();

			}
			else if (Attack_Type_Instruction == 4)
			{
				Crossing_Attack();
			}
			else
			{
				//Attack_Seconds += Time.deltaTime;
				//if (Attack_Seconds >= 3.15f)
				//{
					Attack_Seconds = 0.0f;
					Attack_Type_Instruction = 0;
				//}
			}

			// シャッター破壊後コア破壊できる
			// 前のやつが死んだら、次のコライダーを使用できる
			if (!Damage_Collider[Under_Attack].gameObject.activeSelf)
			{
				if (Under_Attack < Damage_Collider.Count - 1)
				{
					Under_Attack++;
					Damage_Collider[Under_Attack].enabled = true;
				}
			}

		}

		// コア破壊で死亡
		if (Is_Core_Annihilation())
		{
			animation_data.Stop();

			big_core_mk3_EF ef_2 = Instantiate(EF_plefab, transform.position, Quaternion.identity).GetComponent<big_core_mk3_EF>();
			SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);
			ef_2.EF_Base.transform.position = parts_All[0].transform.position;
			ef_2.EF_Weapon_R.transform.position = parts_All[1].transform.position;
			ef_2.EF_Weapon_L.transform.position = parts_All[2].transform.position;
			ef_2.Multipl_1.transform.position = parts_All[3].transform.position;
			ef_2.Multipl_2.transform.position = parts_All[4].transform.position;
			ef_2.Multipl_3.transform.position = parts_All[5].transform.position;
			ef_2.Multipl_4.transform.position = parts_All[6].transform.position;
			ef_2.Multipl_5.transform.position = parts_All[7].transform.position;
			ef_2.Multipl_6.transform.position = parts_All[8].transform.position;

			ef_2.EF_Base.transform.rotation = parts_All[0].transform.rotation;
			ef_2.EF_Weapon_R.transform.rotation = parts_All[1].transform.rotation;
			ef_2.EF_Weapon_L.transform.rotation = parts_All[2].transform.rotation;
			ef_2.Multipl_1.transform.rotation = parts_All[3].transform.rotation;
			ef_2.Multipl_2.transform.rotation = parts_All[4].transform.rotation;
			ef_2.Multipl_3.transform.rotation = parts_All[5].transform.rotation;
			ef_2.Multipl_4.transform.rotation = parts_All[6].transform.rotation;
			ef_2.Multipl_5.transform.rotation = parts_All[7].transform.rotation;
			ef_2.Multipl_6.transform.rotation = parts_All[8].transform.rotation;
			ef_2.Set_Init();

			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Game_Master.MY.Score_Addition(score, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eTWO_PLAYER);
			}
			base.Died_Judgment();
			base.Died_Process();
		}

		for (int i = 0; i < shutter_Init.Length; i++)
		{
			if (shutter[i].gameObject.activeSelf)
			{
				shutter[i].transform.localPosition = shutter_Init[i];
			}
		}
		core[0].transform.localPosition = core_Init;
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
			Timeline_Player.Play(Merry_1_Play);
			Timeline_Player.time = 0.0;
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
					for (var i = 0; i < muzzle.Length; i++)
					{
						multiple[i].Bullet_Shot(muzzle[i].transform);
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
			Instantiate(Boss_Bacula, new Vector3(7.0f, 12.0f, 0.0f), Quaternion.identity);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			var target = new Vector3(13.0f, -1.0f, 0.0f);
			if (Vector3.Distance(transform.position, target) > 0.1f)
			{
				transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 1.5f);
			}
			else if (Vector3.Distance(transform.position, target) < 0.1f)
			{
				Attack_Seconds += Time.deltaTime;
				Animation_Playback(Animation_Name[(int)Attack_Index.eSmasher_1]);
				if (Attack_Seconds >= 0.2f)
				{
					smasher_jet[1].Play();
					smasher_nozzle[1].Play();
				}
				Next_Step();
			}
		}
		else if (Attack_Step == 2)
		{
			if (!animation_data.isPlaying)
			{
				var target = new Vector3(13.0f, 1.0f, 0.0f);
				if (Vector3.Distance(transform.position, target) > 0.01f)
				{
					transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 1.5f);
				}
				else if (Vector3.Distance(transform.position, target) < 0.01f)
				{
					Animation_Playback(Animation_Name[(int)Attack_Index.eSmasher_2]);
					smasher_jet[0].Play();
					smasher_nozzle[0].Play();
					Next_Step();
				}
			}
		}
		else if (Attack_Step == 3)
		{
			if (!animation_data.isPlaying)
			{
				var target = new Vector3(13.0f, 0.0f, 0.0f);
				transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2.0f);
				if (Vector3.Distance(transform.position, target) < 0.01f) Next_Step();
			}
		}
		else if (Attack_Step == 4)
		{
			if (!animation_data.isPlaying)
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
			if(Attack_Seconds >= 2.14f)
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
			if (Attack_Seconds >= 5.0f)
			{
				Two_Boss_Laser l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[0].transform.position, multiple[0].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[0].transform);
				Laser.Add(l);
				l = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, muzzle[3].transform.position, multiple[3].transform.up).GetComponent<Two_Boss_Laser>();
				l.Manual_Start(multiple[3].transform);
				Laser.Add(l);
			}

			if (Attack_Seconds >= 14.06f)
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

	#region　交差攻撃
	private void Crossing_Attack()
	{
		// 攻撃準備
		if (Attack_Step == 0)
		{
			Laser = new List<Two_Boss_Laser>();
			Animation_Playback(Animation_Name[(int)Attack_Index.eCrossing]);
			Next_Step();
		}
		else if (Attack_Step == 1)
		{
			Attack_Seconds += Time.deltaTime;
			if (Attack_Seconds >= 3.0f)
			{
				//foreach(var mul in multiple)
				//{
				//	// 子供のマズル情報格納
				//	Transform objT = mul.transform.GetChild(0);
				//	Two_Boss_Laser tl = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, objT.position, mul.transform.up).GetComponent<Two_Boss_Laser>();
				//	// レーザーの初期設定
				//	tl.Manual_Start(mul.transform);
				//	// レーザー情報の格納
				//	Laser.Add(tl);
				//}

				//1mul
				// 子供のマズル情報格納
				Transform objT = multiple[0].transform.GetChild(0);
				Two_Boss_Laser tl = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, objT.position, multiple[0].transform.up).GetComponent<Two_Boss_Laser>();
				// レーザーの初期設定
				tl.Manual_Start(multiple[0].transform);
				// レーザー情報の格納
				Laser.Add(tl);
				//2mul
				tl = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, objT.position, multiple[1].transform.up).GetComponent<Two_Boss_Laser>();
				tl.Manual_Start(multiple[1].transform);
				Laser.Add(tl);
				//4mul
				tl = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, objT.position, multiple[3].transform.up).GetComponent<Two_Boss_Laser>();
				tl.Manual_Start(multiple[3].transform);
				Laser.Add(tl);
				//6mul
				tl = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eTWO_BOSS_LASER, objT.position, multiple[5].transform.up).GetComponent<Two_Boss_Laser>();
				tl.Manual_Start(multiple[5].transform);
				Laser.Add(tl);

				// レーザーの削除
				if (Attack_Seconds >= 13.2f)
				{
					foreach (var l in Laser)
					{
						l.Delete_processing();
					}
					Laser.Clear();
					// 次のステップ
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
		Animation_Playback(Animation_Name[(int)Attack_Index.eIdol]);
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
