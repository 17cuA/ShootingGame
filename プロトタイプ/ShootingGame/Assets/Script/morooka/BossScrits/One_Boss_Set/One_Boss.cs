﻿//作成日2019/07/30
// 一面のボス本番
// 作成者:諸岡勇樹
/*
 * 2019/07/30　グリッド移動の適応
 * 2019/08/02　ボスにレーザー追加
 * 2019/08/14　シェーダーでAnimation
 * 2019/08/27　タイムラインで一部制御
 * 2019/08/28　シャッター制御
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;
using UnityEngine.Playables;

public class One_Boss : character_status
{
	/*
	 * 2019/07/30　グリッド移動
	 */
	//-----------------------------------------------------------------------------------------------------------------------------//
	Vector3 MOVEX = new Vector3(0.175f, 0, 0); // x軸方向に１マス移動するときの距離
	Vector3 MOVEY = new Vector3(0, 0.175f, 0); // y軸方向に１マス移動するときの距離
	Vector3 Target { get; set; }     // 入力受付時、移動後の位置を算出して保存 
	Vector3 Prev_Pos { get; set; }     // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存
	//-----------------------------------------------------------------------------------------------------------------------------//

	[Header("ボスの個別で動かしたい形成パーツ")]
	[SerializeField, Tooltip("回転速度")] private float rotational_speed;
	[SerializeField, Tooltip("コア")] private One_Boss_Parts[] core;
	[SerializeField, Tooltip("コアのレンダー")]private Renderer[] core_renderer;
	[SerializeField, Tooltip("アームのパーツ")] private GameObject[] arm_parts;
	[SerializeField, Tooltip("ボディのパーツ")] private GameObject Body_Parts;
	[SerializeField, Tooltip("アームの見た目")] private GameObject[] arm_mesh;
	[SerializeField, Tooltip("ビームまずる")] private GameObject[] muzzles;
	[SerializeField, Tooltip("レーザーのまずる")] private GameObject[] laser_muzzle;
	[SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;
	[SerializeField, Tooltip("バウンドする弾の発射数(最低二個は発射)")] private int number_of_fires;
	[SerializeField, Tooltip("ポジションセットプレハブ")] private GameObject pos_set_prefab;
	[SerializeField, Tooltip("ボスのコアシャッター")] private One_Boss_Parts[] core_shutter;
	[SerializeField, Tooltip("弾閉じ込めるよう")] private GameObject framework;

	[Header("ボスのアニメーション用")]
	[SerializeField, Tooltip("atame開始時間")] private double[] anime_start_time;
	[SerializeField, Tooltip("ワープエフェクト")] private GameObject warp_ef;
	[SerializeField, Tooltip("スタートアニメーション")] private bool Start_Flag;
	[SerializeField, Tooltip("アップデートアニメーション")] private bool Update_Flag;
	[SerializeField, Tooltip("タイムライン")] private PlayableDirector Timeline_Player;
	[SerializeField, Tooltip("死ぬとき用")] private GameObject End_Plefab;

	[Header("アニメーションタイムライン")]
	[SerializeField, Tooltip("今までの")] private PlayableAsset sonota_Timeline;
	[SerializeField, Tooltip("タイムラインの保管")] private PlayableAsset layser_timeline;
	[SerializeField, Tooltip("タイムラインの保管")] private PlayableAsset Bullet_timeline;
	[SerializeField, Tooltip("タイムラインの終了判定")] private bool Is_end_of_timeline;

	[Header("レーザー用")]
	[SerializeField, Tooltip("撃つ？撃たない？")] public bool Is_Laser_Attack;

	[Header("突進攻撃用")]
	[SerializeField, Tooltip("突進中フラグ")] public bool now_rush;

	// アニメーション用
	private List<ParticleSystem> Warp_EF { get; set; }
	//-------------------------------------------------

	// 全体で使用
	private Vector3 maenoiti { get; set; }						// 前回の位置
	private Vector3[,] Pos_set { get; set; }					// ポジションのtargetセット
	public float Max_Speed { get; set; }						 // 最大速度
	public float Now_Speed { get; set; }						 // 今の速度
	public float Lowest_Speed { get; set; }					 // 最小速度
	public float Speed​_Change_Distance { get; set; }		// 速度変更距離
	private uint Flame { get; set; }								// ボス内でのフレーム数
	private int Attack_Step { get; set; }							// 関数内 攻撃ステップ
	//-------------------------------------------------

	private int A_Num { get; set; }
	private int B_Num { get; set; }
	private Vector3 IntermediatePosition { get; set; }

	private Vector3[] BoundBullet_Rotation { get; set; }    // バウンドバレットの角度

	private Vector3 For_body_Upward { get; set; }       // 本体の上向き角度
	private Vector3 For_body_Downward { get; set; }     // 本体の下向き角度

	public GameObject[] Player_Data { get; private set; }       // プレイヤーのデータ
	public GameObject Now_player_Traget { get; set; }           // ターゲット情報の保管用
	private int Attack_Type_Instruction { get; set; }           // 攻撃タイプ支持

	private bool End_Flag { get; set; }         // 終わりのフラグ

	private int Survival_Time { get; set; }				// ボスの生存する時間
	private int Survival_Time_Cnt { get; set; }		// 生存している時間カウント
	private bool Is_Attack_Now { get; set; }			// 現在攻撃しているか

	private  List<List<Collider>> Damage_Stage_Col { get; set; }		// ダメージの段階

	// 旋回用
	private float PreviousPosition { get; set; }        // 前の位置
	private Vector3[] SwingAngle { get; set; }          // 旋回角度
	private int Number_Of_Lasers { get; set; }		// レーザー撃った回数
	private int[] Core_Mae_HP { get; set; }
	private int Core_Init_HP { get; set; }// コアの初期HP

	private Color[] Base_Color { get; set; }
	private Color[] Emissive_Color { get; set; }

	private Player1 Player1_Script { get; set; }
	private Player2 Player2_Script { get; set; }

	// 背景遷移トリガー
	SetTimeTrigger setTimeTrigger = null;

	private bool fafaa;

	//---------------------------------------------------------
	//レーザー音追加
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	private AudioSource audioSource;
	//---------------------------------------------------------

	private new void Start()
	{
		//-------------追加--------------
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = laserEnd;
		audioSource.playOnAwake = false;
		//-------------------------------

		Timeline_Player.playOnAwake = false;

		base.Start();

		A_Num = B_Num = 0;

		Attack_Step = 0;
		Start_Flag = true;

		Warp_EF = new List<ParticleSystem>();

		Survival_Time = (2 * 60 * 60);
		Survival_Time_Cnt = 0;
		Is_Attack_Now = false;

		Pos_set = new Vector3[pos_set_prefab.transform.childCount, pos_set_prefab.transform.GetChild(0).childCount];
		for (int i = 0; i < pos_set_prefab.transform.childCount; i++)
		{
			for (int j = 0; j < pos_set_prefab.transform.GetChild(i).childCount; j++)
			{
				Pos_set[i, j] = pos_set_prefab.transform.GetChild(i).GetChild(j).position;
			}
		}

		Target = Pos_set[0, 0];

		Player_Data = new GameObject[(int)Game_Master.Number_Of_People];
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player_Data[0] = Obj_Storage.Storage_Data.GetPlayer();
		}
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player_Data[0] = Obj_Storage.Storage_Data.GetPlayer();
			Player_Data[1] = Obj_Storage.Storage_Data.GetPlayer2();
		}
		Now_player_Traget = Player_Data[0];

		Max_Speed = speed;
		Now_Speed = Lowest_Speed = Max_Speed / 20.0f;
		for (int i = 0; i < 30; i++)
		{
			Speed​_Change_Distance += Now_Speed;
			Now_Speed += Lowest_Speed;
		}
		Now_Speed = Lowest_Speed;

		For_body_Upward = new Vector3(0.0f, 0.0f, 45.0f);
		For_body_Downward = new Vector3(0.0f, 0.0f, 360.0f - 45.0f);

		Bullet_num_Set(number_of_fires);

		End_Flag = false;

		Collider_Set(false);
		//旋回初期化
		PreviousPosition = transform.position.y;
		SwingAngle = new Vector3[2]
		{
			new Vector3(5.0f,0.0f,0.0f),
			new Vector3(-5.0f,0.0f,0.0f),
		};

		Core_Mae_HP = new int[core.Length];
		for(int i = 0;i<Core_Mae_HP.Length;i++)
		{
			Core_Mae_HP[i] = core[i].hp;
		}
		Core_Init_HP = 255;
		Damage_Stage_Col = new List<List<Collider>>();
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[0].GetComponent<Collider>(), core_shutter[1].GetComponent<Collider>(), core_shutter[2].GetComponent<Collider>(), core[0].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[3].GetComponent<Collider>(), core_shutter[4].GetComponent<Collider>(), core_shutter[5].GetComponent<Collider>(), core[1].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[6].GetComponent<Collider>(), core_shutter[7].GetComponent<Collider>(), core_shutter[8].GetComponent<Collider>(), core[2].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[9].GetComponent<Collider>(), core_shutter[10].GetComponent<Collider>(), core_shutter[11].GetComponent<Collider>(), core[3].GetComponent<Collider>() });

		for(int i = 0;i<Damage_Stage_Col.Count; i++)
		{
			for(int j = 0;j<Damage_Stage_Col[i].Count;j++)
			{
					Damage_Stage_Col[i][j].enabled = false;
			}
		}

		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		}
		else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			Player2_Script = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
		}

		Base_Color = new Color[4]
		{
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
		};

		Emissive_Color = new Color[4]
		{
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
		};

		setTimeTrigger = FindObjectOfType<SetTimeTrigger>();
	}

	private new void Update()
	{
		if (!PauseManager.IsPause)
		{
			if (Survival_Time_Cnt >= Survival_Time && !Is_Attack_Now && !End_Flag)
			{
				maenoiti = transform.position;
				Timeline_Player.Pause();
				Attack_Step = 0;
				End_Flag = true;
			}

			if (Start_Flag && !End_Flag && Update_Flag)
			{
				Collider_Set(true);
				for (int i = 0; i < Damage_Stage_Col.Count; i++)
				{
					Damage_Stage_Col[i][0].enabled = true;
				}
				Timeline_Player.Pause();
				Timeline_Player.time = 60.0;

				Start_Flag = false;
			}
			else if (!End_Flag && !Start_Flag && Update_Flag)
			{
				// デバッグ　HP　0か
				if(Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.H))
				{
					foreach(One_Boss_Parts core in core)
					{
						core.hp = 0;
					}
				}


				//if (Attack_Type_Instruction < 2)
				//{
				//	Player_Tracking_Bound_Bullets_3();
				//	//Player_Tracking_Bound_Bullets_2();
				//}
				//else
				//{
				if (Number_Of_Lasers == 0)
				{
					//Player_Tracking_Bound_Bullets_3();
					Laser_And_Bouncing_Bullets();
				}
				//else if (Number_Of_Lasers == 1)
				//{
				//	//Laser_Clearing_2();
				//	Laser_Time();
				//}
				else
				{
					Rush_2();
				}
				//}

				base.Update();
				Survival_Time_Cnt++;

				// 一定HP以下の時コアの色を変える
				for (int i = 0; i < core.Length; i++)
				{
					if (core[i].gameObject.activeSelf)
					{
						// HPが前フレームより少ないとき
						if (core[i].hp < Core_Mae_HP[i])
						{
							// RGBの変化数
							float RGB = (1.0f / 255.0f) * (float)(Core_Mae_HP[i] - core[i].hp);
							
							//// 青抜き、赤入れ
							// Gの値ははじめ増やして、後半減らす
							if (Base_Color[i].r >= 1.0f)
							{
								Base_Color[i].b -= (RGB * 2.0f);
								Emissive_Color[i].b -= RGB;

								Base_Color[i].g -= (RGB * 2.0f);
								Emissive_Color[i].g -= (RGB * 2.0f);
							}
							else if(Base_Color[i].r <= 1.0f)
							{
								Base_Color[i].r += (RGB * 2.0f);
								Emissive_Color[i].r += RGB;

								Base_Color[i].g += (RGB * 2.0f);
								Emissive_Color[i].g += (RGB * 2.0f);
							}
						}
						core_renderer[i].material.SetColor("_Color", Base_Color[i]);
						core_renderer[i].material.SetColor("_Emissive_Color", Emissive_Color[i]);

						Core_Mae_HP[i] = core[i].hp;
					}
				}

				// パーツのコアが壊れたら死亡
				if (!core[0].gameObject.activeSelf && !core[1].gameObject.activeSelf && !core[2].gameObject.activeSelf && !core[3].gameObject.activeSelf)
				{
					Timeline_Player.Stop();
					Attack_Step = 0;
					Timeline_Player.time = 60.0;
					End_Flag = true;
				}
			}
			else if (End_Flag && !Start_Flag && Update_Flag)
			{
				End_Anime();
			}

			// コライダー管理
			// シャッターが壊れると次のコライダーが起動
			for (int a = 0; a < Damage_Stage_Col.Count; a++)
			{
				for (int b = 0; b < Damage_Stage_Col[a].Count - 1; b++)
				{
					if (!Damage_Stage_Col[a][b].gameObject.activeSelf && !Damage_Stage_Col[a][b + 1].enabled)
					{
						Damage_Stage_Col[a][b + 1].enabled = true;
						Damage_Stage_Col[a].RemoveAt(b);
					}
				}
			}
		}
	}

	private void OnEnable()
	{
		Update_Flag = false;
		Timeline_Player.time = 0.0;
		Timeline_Player.Play(sonota_Timeline);
	}

	#region 終わりアニメーション
	private void End_Anime()
	{
		if(Attack_Step == 0)
		{
			Is_Dead = true;
			setTimeTrigger.Trigger = true;
			Attack_Step++;
		}
		else if(Attack_Step == 1)
		{
			Instantiate(End_Plefab, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
			if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Game_Master.MY.Score_Addition(score, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
			}
			else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eTWO_PLAYER);
			}
			//Timeline_Player.Play(sonota_Timeline);
			//Timeline_Player.time = 60.0;
			//Attack_Step++;
		}
		else if(Attack_Step == 2)
		{
			//Instantiate(End_Plefab, transform.position, Quaternion.identity);
			//gameObject.SetActive(false);
			//if (Is_end_of_timeline)
			//{
			//	gameObject.SetActive(false);
			//}
		}
	}
	#endregion

	#region タイムラインレーザー
	private void Laser_Time()
	{
		if (Attack_Step == 0)
		{
			maenoiti = transform.position;
			Attack_Step++;
			Is_Attack_Now = true;
		}
		else if (Attack_Step == 1)
		{
			if (transform.position != Pos_set[0, 0] || transform.rotation != Quaternion.identity)
			{
				if (Vector_Size(Target, transform.position) < Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime);
				transform.position = Moving_To_Target_S(transform.position, Pos_set[0, 0], Now_Speed * 2.0f);
			}
			else if (transform.position == Pos_set[0, 0] && transform.rotation == Quaternion.identity)
			{
				Attack_Step++;
			}
		}
		else if (Attack_Step == 2)
		{
			if (audioSource.clip == laserEnd)
			{
				audioSource.clip = laserBegin;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}

			if (!supply[0].gameObject.activeSelf && !supply[1].gameObject.activeSelf)
			{
				supply[0].gameObject.SetActive(true);
				supply[1].gameObject.SetActive(true);

				supply[0].SetUp();
				supply[1].SetUp();
			}

			if (supply[0].Completion_Confirmation() && supply[1].Completion_Confirmation())
			{
				supply[0].gameObject.SetActive(false);
				supply[1].gameObject.SetActive(false);

				Attack_Step++;
			}
		}
		else if (Attack_Step == 3)
		{
			Timeline_Player.Play(layser_timeline);
			Timeline_Player.time = 0.0;
			Attack_Step++;
			//--------------追加-----------------
			if (audioSource.clip == laserBegin)
			{
				audioSource.clip = laserContinuing;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}
			//-----------------------------------
		}
		// ここから打ち出し！！！！
		else if(Attack_Step == 4)
		{
			if(Is_Laser_Attack)
			{	
				Laser_Shooting();
			}

			if (Is_end_of_timeline)
			{
				Timeline_Player.Stop();
				Attack_Step++;

				//---------------追加-----------------
				audioSource.clip = laserEnd;
				audioSource.Stop();
				audioSource.loop = false;
				audioSource.Play();
				//------------------------------------
			}
		}
		else if(Attack_Step==5)
		{
			Attack_Step = 0;
			Attack_Type_Instruction = 0;
			Flame = 0;
			Number_Of_Lasers++;
			Is_Attack_Now = false;
		}
	}
	#endregion

	# region プレイヤーを追従しバウンド弾_3
	private void Player_Tracking_Bound_Bullets_3()
	{
		if (Attack_Step == 0)
		{
			Timeline_Player.Play(Bullet_timeline);
			Timeline_Player.time = 0.0f;
			Attack_Step++;
			Is_Attack_Now = true;
			Shot_Delay = -60;
		}
		else if (Attack_Step == 1)
		{
			Flame++;

			foreach (GameObject obj in laser_muzzle)
			{
				if (obj.activeSelf)
				{
					Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
					laser.Manual_Start(obj.transform);
				}
			}

			if (Flame % 20 == 0)
			{
				Bullet_num_Set(Check_Bits());
				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				}
			}

			Shot_Delay++;
			if(Shot_Delay == (Shot_DelayMax / 3))
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if(Shot_Delay == (Shot_DelayMax / 3) * 2)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if(Shot_Delay == Shot_DelayMax)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);

				Shot_Delay -= Shot_DelayMax * 2;
			}

			if(Is_end_of_timeline)
			{
				Is_Attack_Now = false;
				Attack_Step = 0;
				Is_end_of_timeline = false;
				Number_Of_Lasers++;
				Flame = 0;
				Shot_Delay = 0;
			}
		}
	}
	#endregion

	#region ターゲット移動
	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target(Vector3 origin, Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;       // 移動する前のターゲットとの向き
		Vector3 return_pos = Vector3.zero;              // 返すポジション

		//direction = target - origin;
		//return_pos = origin + (direction.normalized * speed);

		return_pos = Vector3.Lerp(origin, target, speed);

		if (Vector_Size(return_pos, target) < Lowest_Speed)
		{
			return_pos = target;
			Now_Speed = 0;
		}

		return return_pos;
	}
	#endregion

	#region ターゲット移動
	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target_S(Vector3 origin, Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;       // 移動する前のターゲットとの向き
		Vector3 return_pos = Vector3.zero;              // 返すポジション

		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		//return_pos = Vector3.Slerp(origin, target, speed);

		if (Vector_Size(return_pos, target) < speed)
		{
			return_pos = target;
			Now_Speed = Lowest_Speed;
		}

		return return_pos;
	}
	#endregion

	#region ベクトルの長さ出す
	/// <summary>
	/// ベクトルの長さを出す
	/// </summary>
	/// <param name="a"> 開始座標 </param>
	/// <param name="b"> 目標座標 </param>
	/// <returns></returns>
	private float Vector_Size(Vector3 a, Vector3 b)
	{
		float xx = a.x - b.x;
		float yy = a.y - b.y;
		float zz = a.z - b.z;

		return Mathf.Sqrt(xx * xx + yy * yy + zz * zz);
	}
	#endregion

	#region 突進攻撃2
	/// <summary>
	/// 突進攻撃
	/// </summary>
	private void Rush_2()
	{
		if (Attack_Step == 0)
		{
			maenoiti = transform.position;
			Timeline_Player.time = 30.0;
			Attack_Step++;
			Is_Attack_Now = true;
		}
		else if (Attack_Step == 1)
		{
			if (transform.position != Pos_set[0, 0] || transform.rotation != Quaternion.identity)
			{
				if (Vector_Size(Target, transform.position) < Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime);
				transform.position = Moving_To_Target_S(transform.position, Pos_set[0, 0], Now_Speed * 2.0f);
			}
			else if (transform.position == Pos_set[0, 0] && transform.rotation == Quaternion.identity)
			{
				Attack_Step++;
			}
		}
		else if (Attack_Step == 2)
		{
			now_rush = true;
			Timeline_Player.Play(sonota_Timeline);
			Timeline_Player.time = 30.0;
			Attack_Step++;
		}
		else if (Attack_Step == 3)
		{

			if(!now_rush)
			{
				Timeline_Player.Pause();
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				Number_Of_Lasers = 0;
				Is_Attack_Now = false;
			}
		}
	}
	#endregion

	/// <summary>
	/// レーザーとバウンド弾
	/// </summary>
	private void Laser_And_Bouncing_Bullets()
	{
		// レーザーチャージ
		if (Attack_Step == 0)
		{	
			// 攻撃開始
			Is_Attack_Now = true;

			// チャージ音
			if (audioSource.clip == laserEnd)
			{
				audioSource.clip = laserBegin;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}

			// チャージエフェクト再生
			if (!supply[0].gameObject.activeSelf && !supply[1].gameObject.activeSelf)
			{
				supply[0].gameObject.SetActive(true);
				supply[1].gameObject.SetActive(true);

				supply[0].SetUp();
				supply[1].SetUp();
			}
			// チャージエフェクト終了
			if (supply[0].Completion_Confirmation() && supply[1].Completion_Confirmation())
			{
				supply[0].gameObject.SetActive(false);
				supply[1].gameObject.SetActive(false);

				Attack_Step++;
			}
		}
		// タイムライン再生
		else if(Attack_Step == 1)
		{
			// デフォルト位置での揺れ
			Timeline_Player.Play(Bullet_timeline);
			Timeline_Player.time = 0.0f;
			// ディレイ調節
			Shot_Delay = -60;
			// 枠組み使用開始
			framework.SetActive(true);
			// 次の動きへ
			Attack_Step++;
			//--------------追加-----------------
			// レーザー音
			if (audioSource.clip == laserBegin)
			{
				audioSource.clip = laserContinuing;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}
			//-----------------------------------
		}
		// 攻撃
		else if(Attack_Step == 2)
		{
			Flame++;
			Shot_Delay++;

			//　レーザー撃ち出し
			Laser_Shooting();

			// バウンド弾撃ちだし
			if (Flame % 80 == 0)
			{
				Bullet_num_Set(Check_Bits());
				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				}
			}

			// ビーム撃ちだし
			if (Shot_Delay == (Shot_DelayMax / 3))
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if (Shot_Delay == (Shot_DelayMax / 3) * 2)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if (Shot_Delay == Shot_DelayMax)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);

				Shot_Delay -= Shot_DelayMax * 2;
			}

			// タイムライン終了時
			if (Is_end_of_timeline)
			{
				// 薙ぎ払い攻撃
				Timeline_Player.Play(layser_timeline);
				Timeline_Player.time = 0.0;


				// 次のステップへ
				Attack_Step++;
				Is_end_of_timeline = false;
				Flame = 0;
				Shot_Delay = 0;
			}
		}
		else if (Attack_Step == 3)
		{
			Flame++;
			Shot_Delay++;

			//　レーザー撃ち出し
			Laser_Shooting();

			//// バウンド弾撃ちだし
			//if (Flame % 80 == 0)
			//{
			//	Bullet_num_Set(Check_Bits());
			//	for (int i = 0; i < BoundBullet_Rotation.Length; i++)
			//	{
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//	}
			//}

			//if (Shot_Delay == (Shot_DelayMax / 3))
			//{
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[1].transform.position, muzzles[1].transform.right);
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[2].transform.position, muzzles[2].transform.right);
			//}
			//else if (Shot_Delay == (Shot_DelayMax / 3) * 2)
			//{
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[1].transform.position, muzzles[1].transform.right);
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[2].transform.position, muzzles[2].transform.right);
			//}
			//else if (Shot_Delay == Shot_DelayMax)
			//{
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[1].transform.position, muzzles[1].transform.right);
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzles[2].transform.position, muzzles[2].transform.right);

			//	Shot_Delay -= Shot_DelayMax * 2;
			//}

			fafaa = true;

			if (Is_end_of_timeline)
			{
				// 枠組み使用終了
				framework.SetActive(false);

				fafaa = false;

				Flame = 0;
				Timeline_Player.Stop();
				Attack_Step = 0;
				Is_Attack_Now = false;
				Number_Of_Lasers++;
			}
		}
	}

	#region レーザー打ち出し
	/// <summary>
	/// レーザー撃ち出し
	/// </summary>
	private void Laser_Shooting()
	{
			foreach (GameObject obj in laser_muzzle)
			{
				if (obj.activeSelf)
				{
					Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
					laser.Manual_Start(obj.transform);
				}
			}
	}
	#endregion

	/// <summary>
	/// 旋回
	/// </summary>
	private void Turning()
	{
		float Difference = transform.position.y - PreviousPosition;
		if (Difference > 0)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(SwingAngle[0]), Time.deltaTime);
		}
		else if (Difference < 0)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(SwingAngle[1]), Time.deltaTime);
		}
		else if (Difference == 0)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);
		}
		PreviousPosition = transform.position.y;
	}

	/// <summary>
	/// コライダーの使用未使用の切り替え
	/// </summary>
	/// <param name="State"></param>
	private void Collider_Set(bool State)
	{
		arm_parts[0].SetActive(State);
		arm_parts[1].SetActive(State);
		Body_Parts.SetActive(State);
	}

	/// <summary>
	/// バウンド弾の数設定
	/// </summary>
	/// <param name="set_num"></param>
	private void Bullet_num_Set(int set_num )
	{
		if(set_num % 2 == 1)
		{
			set_num--;
		}

		set_num += number_of_fires;

		BoundBullet_Rotation = new Vector3[set_num + 2];
		float z_rotation = 120.0f / ((float)BoundBullet_Rotation.Length - 1.0f);
		for (int i = 0; i < BoundBullet_Rotation.Length; i++)
		{
			BoundBullet_Rotation[i] = new Vector3(0.0f, 0.0f, (z_rotation * i) + -60.0f);
		}
	}

	/// <summary>
	/// ビットの数確認
	/// </summary>
	/// <returns></returns>
	private int Check_Bits()
	{
		int num = 0;

		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			num = Player1_Script.bitIndex;
		}
		else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			num = Player1_Script.bitIndex + Player2_Script.bitIndex;
		}

		return num;
	}

	private new void OnTriggerEnter(Collider col)
	{
		if (now_rush || fafaa)
		{
			if (col.GetComponent<One_Boss_BoundBullet>() != null)
			{
				col.gameObject.SetActive(false);
			}
		}
	}
}