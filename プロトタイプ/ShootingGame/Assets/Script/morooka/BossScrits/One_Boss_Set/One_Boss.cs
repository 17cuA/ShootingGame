//作成日2019/07/30
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

	private int Survival_Time { get; set; }
	private int Survival_Time_Cnt { get; set; }
	private int Bullet_Num { get; set; }
	private bool Attack_Now { get; set; }

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

	private new void Start()
	{
		Timeline_Player.playOnAwake = false;

		base.Start();

		A_Num = B_Num = 0;

		Attack_Step = 0;
		Start_Flag = true;

		Warp_EF = new List<ParticleSystem>();

		Survival_Time = (2 * 60 * 60);
		Survival_Time_Cnt = 0;
		Attack_Now = false;

		Bullet_Num = Random.Range(2, 5);

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
			Player2_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player2>();
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
	}

	private new void Update()
	{
		if (Survival_Time_Cnt >= Survival_Time && !Attack_Now && !End_Flag)
		{
			maenoiti = transform.position;
			Timeline_Player.Pause();
			Attack_Step = 0;
			End_Flag = true;
		}

		if (Start_Flag && !End_Flag && Update_Flag)
		{
			Collider_Set(true);
			for(int i =0; i< Damage_Stage_Col.Count;i++)
			{
				Damage_Stage_Col[i][0].enabled = true;
			}
			Timeline_Player.Pause();
			Timeline_Player.time = 60.0;

			Start_Flag = false;
		}
		else if (!End_Flag && !Start_Flag && Update_Flag)
		{
			if (Attack_Type_Instruction < 2)
			{
				Player_Tracking_Bound_Bullets_2();
			}
			else
			{
				if (Number_Of_Lasers < 1)
				{
					//Laser_Clearing_2();
					Laser_Time();
				}
				else
				{
					Rush_2();
				}
			}

			base.Update();
			Survival_Time_Cnt++;

			// 一定HP以下の時コアの色を変える
			for(int i = 0; i< core.Length; i++)
			{
				if (core[i].gameObject.activeSelf)
				{
					if (core[i].hp < Core_Mae_HP[i])
					{
						float RG = (1.0f / 255.0f) * (float)(Core_Mae_HP[i] - core[i].hp);

						Base_Color[i].r += RG;
						Base_Color[i].b -= RG;
						Emissive_Color[i].r += RG;
						Emissive_Color[i].b -= RG;

						if(core[i].hp < Core_Init_HP / 2)
						{
							Base_Color[i].g -= (RG * 2.0f);
							Emissive_Color[i].g -= (RG * 2.0f);
						}
						else
						{
							Base_Color[i].g += (RG*2.0f);
							Emissive_Color[i].g += (RG*2.0f);
						}
					}
						core_renderer[i].material.SetColor("_Color", Base_Color[i]);
						core_renderer[i].material.SetColor("_Emissive_Color", Emissive_Color[i]);

						Core_Mae_HP[i] = core[i].hp;
					


					//if (core[i].hp < Core_Init_HP / 3)
					//{
					//	var color = default(Color);
					//	ColorUtility.TryParseHtmlString("#FF0000", out color);
					//	core_renderer[i].material.SetColor("_Color", color);

					//	ColorUtility.TryParseHtmlString("#BF0000", out color);
					//	core_renderer[i].material.SetColor("_Emissive_Color", color);
					//}
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
		for(int a = 0; a < Damage_Stage_Col.Count; a++)
		{
			for(int b = 0;b < Damage_Stage_Col[a].Count - 1;b++)
			{
				if(!Damage_Stage_Col[a][b].gameObject.activeSelf && !Damage_Stage_Col[a][b+1].enabled)
				{
					Damage_Stage_Col[a][b+1].enabled = true;
					Damage_Stage_Col[a].RemoveAt(b);
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
			Attack_Step++;
		}
		else if(Attack_Step == 1)
		{
			Instantiate(End_Plefab, transform.position, Quaternion.identity);
			gameObject.SetActive(false);

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

	#region レーザーの薙ぎ払い攻撃_2
	/// <summary>
	/// レーザーの薙ぎ払い攻撃_2
	/// </summary>
	private void Laser_Clearing_2()
	{
		if (Attack_Step == 0)
		{
			maenoiti = transform.position;
			Attack_Step++;
			Attack_Now = true;
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
			Flame++;
			Laser_Shooting();

			if (Flame >= 30)
			{
				if (transform.rotation.eulerAngles != For_body_Upward)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Upward), rotational_speed);
				}
				else if (transform.rotation.eulerAngles == For_body_Upward)
				{
					Attack_Step++;
					Flame = 0;
				}
			}
		}
		else if (Attack_Step == 4)
		{
			Flame++;
			Laser_Shooting();

			if (Flame >= 30)
			{
				if (transform.rotation.eulerAngles != For_body_Downward)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Downward), rotational_speed);
				}
				else if (transform.rotation.eulerAngles == For_body_Downward)
				{
					Attack_Step++;
					Flame = 0;
				}
			}
		}
		else if (Attack_Step == 5)
		{
			Flame++;
			Laser_Shooting();

			if (Flame >= 30)
			{
				if (transform.rotation != Quaternion.identity)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotational_speed);
				}
				else if (transform.rotation == Quaternion.identity)
				{
					Attack_Step++;
					Flame = 0;
				}
			}
		}
		else if (Attack_Step == 6)
		{
			Flame++;
			Laser_Shooting();
			if (Flame == 60)
			{
				Attack_Step++;
				Flame = 0;
			}
		}
		else if (Attack_Step == 7)
		{
			Flame++;
			if (Flame == 30)
			{
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				Bullet_Num = Random.Range(2, 6);
				Flame = 0;
				Number_Of_Lasers++;
				Attack_Now = false;
			}
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
			Attack_Now = true;
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
			}
		}
		else if(Attack_Step==5)
		{
			Attack_Step = 0;
			Attack_Type_Instruction = 0;
			Bullet_Num = Random.Range(2, 6);
			Flame = 0;
			Number_Of_Lasers++;
			Attack_Now = false;
		}
	}
		#endregion

	#region プレイヤーを追従しバウンド弾_2
		/// <summary>
		/// プレイヤーを追従しバウンド弾_2
		/// </summary>
		private void Player_Tracking_Bound_Bullets_2()
	{
		if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance)
		{
			if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
		}
		if (Vector_Size(Target, transform.position) < Speed_Change_Distance)
		{
			if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
		}
		transform.position = Moving_To_Target_S(transform.position, Target, Now_Speed);
		Turning();

		if (Attack_Step == 0)
		{
			Attack_Now = true;
			//Flame++;
			//if (Flame == 40)
			//{
			//	for (int i = 0; i < BoundBullet_Rotation.Length; i++)
			//	{
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[1].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//	}
			//	Attack_Step++;
			//	Flame = 0;
			//}

			if (Vector_Size(transform.position, IntermediatePosition) <= Lowest_Speed)
			{
				Bullet_num_Set(Check_Bits());

				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[1].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				}
				Attack_Step++;
			}
		}
		else if (Attack_Step == 1)
		{
			if (transform.position == Target)
			{
				Attack_Type_Instruction++;
				Attack_Step = 0;
				Flame = 0;

				Bullet_num_Set(Check_Bits());

				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[2].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				}
			}

			//Flame++;
			//if (Flame == 40)
			//{
			//	for (int i = 0; i < BoundBullet_Rotation.Length; i++)
			//	{
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[2].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			//	}
			//	Attack_Step++;
			//	Flame = 0;
			//}
		}
		else if (Attack_Step == 2)
		{
			//Flame++;
			//if (Flame == 30)
			//{
			//	Flame = 0;
			//	Attack_Step = 0;
			//	Attack_Type_Instruction++;
			//	Attack_Now = false;
			//}
		}

		if (transform.position == Target)
		{
			int[] rand_I = new int[3] { -1, 0, 1 };
			maenoiti = transform.position;
			int a_temp = rand_I[Random.Range(0, rand_I.Length)];
			if (8 == a_temp + A_Num)
			{
				a_temp = 0;
			}
			else if (a_temp + A_Num == -1)
			{
				a_temp = 7;
			}
			int b_temp = rand_I[Random.Range(0, rand_I.Length)];
			if (b_temp + B_Num == -1 || 5 == b_temp + B_Num)
			{
				b_temp = 0;
			}

			A_Num += a_temp;
			B_Num += b_temp;
			Target = Pos_set[A_Num, B_Num];

			IntermediatePosition = new Vector3((maenoiti.x + Target.x) / 2.0f, (maenoiti.y + Target.y) / 2.0f, 0.0f);
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
			Attack_Now = true;
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
				Bullet_Num = Random.Range(2, 5);
				Number_Of_Lasers = 0;
				Attack_Now = false;
			}
		}
	}
	#endregion

	#region レーザー打ち出し
	/// <summary>
	/// レーザー撃ち出し
	/// </summary>
	private void Laser_Shooting()
	{
		Shot_Delay++;

		if (Shot_Delay > Shot_DelayMax)
		{
			foreach (GameObject obj in laser_muzzle)
			{
				if (obj.activeSelf)
				{
					Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
					laser.Manual_Start(obj.transform);
				}
			}
			Shot_Delay = 0;
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
}