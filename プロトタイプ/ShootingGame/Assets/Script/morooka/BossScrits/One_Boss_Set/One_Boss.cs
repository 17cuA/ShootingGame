//作成日2019/07/30
// 一面のボス本番
// 作成者:諸岡勇樹
/*
 * 2019/07/30　グリッド移動の適応
 * 2019/08/02　ボスにレーザー追加
 * 2019/08/14　シェーダーでAnimation
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
	[SerializeField, Tooltip("ボスのコア")] private GameObject core;
	[SerializeField, Tooltip("パーツコア")] private One_Boss_Parts[] parts_core;
	[SerializeField, Tooltip("アームのパーツ")] private GameObject[] arm_parts;
	[SerializeField, Tooltip("ボディのパーツ")] private GameObject Body_Parts;
	[SerializeField, Tooltip("アームの見た目")] private GameObject[] arm_mesh;
	[SerializeField, Tooltip("ビームまずる")] private GameObject[] muzzles;
	[SerializeField, Tooltip("レーザーのまずる")] private GameObject[] laser_muzzle;
	[SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;
	[SerializeField, Tooltip("バウンドする弾の発射数(最低二個は発射)")] private int number_of_fires;
	[SerializeField, Tooltip("ポジションセットプレハブ")] private GameObject pos_set_prefab;

	[Header("ボスのアニメーション用")]
	[SerializeField, Tooltip("ディゾルブエフェクト用")] private MeshRenderer[] Dissolve_Effect_Material;
	[SerializeField, Tooltip("出現スピード")] private float appearance_speed;
	[SerializeField, Tooltip("ワープエフェクト")] private GameObject warp_ef;
	[SerializeField, Tooltip("アーム見た目")] private GameObject[] weapons;
	[SerializeField, Tooltip("アニメ移動速度")] private float move_speed_A;
	[SerializeField, Tooltip("アニメ回転速度")] private float Rotate_speed_A;
	[SerializeField, Tooltip("スタートアニメーション")] private bool Start_Flag;
	[SerializeField, Tooltip("アップデートアニメーション")] private bool Update_Flag;
	[SerializeField, Tooltip("タイムライン")] private PlayableDirector start_timecline;

	private Vector3[] Init_Weapons_pos { get; set; }
	private Vector3[] Standby_Pos { get; set; }
	private float Speed​_Change_Distance_A { get; set; }
	private float Mini_Speed_A { get; set; }
	private float Max_Speed_A { get; set; }
	private float Now_Speed_A { get; set; }
	private Vector3 Target_2 { get; set; }
	private Vector3 IntermediatePosition { get; set; }

	Vector3 velocity = Vector3.zero;

	//const int y_num = 3;
	//const int x_num = 4;
	private int a_num { get; set; }
	private int b_num { get; set; }
	//private Vector2[,] poinnto { get; set; }
	//private int y_p { get; set; }
	//private int x_p { get; set; }
	//private Vector3 Target_2 { get; set; }			// カーブ用
	//private Vector3 Move_Poinnto1 { get; set; }		// カーブ用動くポイント
	//private Vector3 Move_Pionnto2 { get; set; }		// カーブ用動くポイント	
	//private bool is_naname { get; set; }

	private Vector3 maenoiti { get; set; }
	private Vector3[,] Pos_set { get; set; }

	private One_Boss_Parts Core { get; set; }               // コアのパーツ情報
	public float Max_Speed { get; set; }                    // 最大速度
	public float Now_Speed { get; set; }                    // 今の速度
	public float Lowest_Speed { get; set; }                 // 最小速度
	public float Speed​_Change_Distance { get; set; }       // 速度変更距離

	private Vector3[] Arm_Closed_Position { get; set; }     // アーム閉じいている位置
	private Vector3[] Arm_Open_Position { get; set; }       // アーム開いてる位置
	private Vector3[] Arm_Laser_Pos { get; set; }           // アームのレーザーを撃つ位置
	private Vector3[] Arm_Ini_Rotation { get; set; }        // アームの初期角度
	private Vector3[] Arm_45_Rotation { get; set; }         // アームの45度の角度

	private Vector3[] BoundBullet_Rotation { get; set; }    // バウンドバレットの角度

	private Vector3 For_body_Upward { get; set; }       // 本体の上向き角度
	private Vector3 For_body_Downward { get; set; }     // 本体の下向き角度

	private uint Flame { get; set; }                    // ボス内でのフレーム数

	private int Attack_Step { get; set; }       // 関数内 攻撃ステップ

	public GameObject[] Player_Data { get; private set; }       // プレイヤーのデータ
	public GameObject Now_player_Traget { get; set; }           // ターゲット情報の保管用
	private int Attack_Type_Instruction { get; set; }           // 攻撃タイプ支持

	private bool End_Flag { get; set; }         // 終わりのフラグ

	private int Survival_Time { get; set; }
	private int Survival_Time_Cnt { get; set; }
	private int Bullet_Num;

	private bool Attack_Now { get; set; }

	private float Full_View { get; set; }                   // 完全表示時の値
	private float Hidden_View { get; set; }             // 完全非表示時の値
	private float Display_Amount { get; set; }      // 1FPSの表示量
	private float Now_View { get; set; } // 現状の表示量

	private List<ParticleSystem> Warp_EF { get; set; }
	private float speed_cc;

	// 旋回用
	private float PreviousPosition { get; set; }        // 前の位置
	private Vector3[] SwingAngle { get; set; }          // 旋回角度

	private new void Start()
	{
		start_timecline.playOnAwake = false;

		base.Start();

		a_num = b_num = 0;

		Full_View = 0.0f;
		Attack_Step = 0;
		Start_Flag = true;
		//warp_ef = Instantiate(warp_ef, transform.position, Quaternion.identity);
		//for(int i = 0; i < warp_ef.transform.childCount;i++)
		//{
		//	Warp_EF.AddRange(warp_ef.transform.GetChild(i).GetComponentsInChildren<ParticleSystem>());
		//}
		//warp_ef.SetActive(false);
		//Warp_EF.Pause();
		//warp_ef.transform.SetParent(null);
		Warp_EF = new List<ParticleSystem>();
		saiki_shoki(warp_ef.transform);

		Survival_Time = (1 * 60 * 60);
		Survival_Time_Cnt = 0;
		Attack_Now = false;

		Bullet_Num = Random.Range(2, 5);

		Pos_set = new Vector3[pos_set_prefab.transform.childCount, pos_set_prefab.transform.GetChild(0).childCount];
		for (int i = 0; i < pos_set_prefab.transform.childCount; i++)
		{
			for (int j = 0; j < pos_set_prefab.transform.GetChild(i).childCount; j++)
			{
				Pos_set[i, j] = pos_set_prefab.transform.GetChild(i).GetChild(j).position;
				//Pos_set[i, j] += new Vector3(10.0f, 0.0f, 0.0f);
				Debug.Log(Pos_set[i, j]);
			}
		}

		Target = Pos_set[0, 0];

		Core = core.GetComponent<One_Boss_Parts>();
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

		Max_Speed_A = move_speed_A;
		Now_Speed_A = Mini_Speed_A = Max_Speed_A / 30.0f;
		for (int i = 0; i < 20.0f; i++)
		{
			Speed​_Change_Distance_A += Now_Speed_A;
			Now_Speed_A += Mini_Speed_A;
		}
		Now_Speed_A = Mini_Speed_A;

		Arm_Closed_Position = new Vector3[arm_parts.Length];
		Arm_Open_Position = new Vector3[arm_parts.Length];
		Arm_Ini_Rotation = new Vector3[arm_parts.Length];
		Arm_45_Rotation = new Vector3[arm_parts.Length];
		Arm_Laser_Pos = new Vector3[arm_parts.Length];

		Arm_Closed_Position[0] = new Vector3(0.12f, 2.75f, 0.0f);
		Arm_Closed_Position[1] = new Vector3(0.12f, -2.75f, 0.0f);
		Arm_Open_Position[0] = new Vector3(0.12f, 3.5f, 0.0f);
		Arm_Open_Position[1] = new Vector3(0.12f, -3.5f, 0.0f);
		Arm_Laser_Pos[0] = new Vector3(-8.1f, 5.0f, 0.0f);
		Arm_Laser_Pos[1] = new Vector3(-8.1f, -5.0f, 0.0f);

		Arm_Ini_Rotation[0] = arm_parts[0].transform.localEulerAngles;
		Arm_Ini_Rotation[1] = arm_parts[1].transform.localEulerAngles;
		Arm_45_Rotation[0] = new Vector3(45.0f, arm_parts[0].transform.localEulerAngles.y, arm_parts[0].transform.localEulerAngles.z);
		Arm_45_Rotation[1] = new Vector3(360.0f - 45.0f, arm_parts[1].transform.localEulerAngles.y, arm_parts[1].transform.localEulerAngles.z);

		For_body_Upward = new Vector3(0.0f, 0.0f, 45.0f);
		For_body_Downward = new Vector3(0.0f, 0.0f, 360.0f - 45.0f);

		BoundBullet_Rotation = new Vector3[number_of_fires + 2];
		float z_rotation = 120.0f / ((float)BoundBullet_Rotation.Length - 1.0f);
		for (int i = 0; i < BoundBullet_Rotation.Length; i++)
		{
			BoundBullet_Rotation[i] = new Vector3(0.0f, 0.0f, (z_rotation * i) + -60.0f);
		}

		End_Flag = false;

		arm_parts[0].SetActive(false);
		arm_parts[1].SetActive(false);
		Body_Parts.SetActive(false);
		core.SetActive(false);

		//旋回初期化
		PreviousPosition = transform.position.y;
		SwingAngle = new Vector3[2]
		{
			new Vector3(10.0f,0.0f,0.0f),
			new Vector3(-10.0f,0.0f,0.0f),
		};

		//shokika();
	}

	private new void Update()
	{
		if (Survival_Time_Cnt >= Survival_Time && !Attack_Now)
		{
			maenoiti = transform.position;
			End_Flag = true;
		}

		if (Start_Flag && !End_Flag && !Update_Flag)
		{
			//Start_Anime_2();
		}
		else if (Start_Flag && !End_Flag && Update_Flag)
		{
			start_timecline.Pause();
			arm_parts[0].SetActive(true);
			arm_parts[1].SetActive(true);
			Body_Parts.SetActive(true);
			core.SetActive(true);
			//warp_ef.SetActive(false);

			Start_Flag = false;
		}
		else if (!End_Flag && !Start_Flag && Update_Flag)
		{
			base.Update();
			Survival_Time_Cnt++;

			if (Core.hp < 1)
			{
				foreach (One_Boss_Parts parts in parts_core)
				{
					if (parts.gameObject.activeSelf && parts.hp > 4)
					{
						parts.hp = 1;
					}
				}
			}

			if (!parts_core[0].gameObject.activeSelf && !parts_core[1].gameObject.activeSelf)
			{
				Attack_Step = 0;
				maenoiti = transform.position;
				End_Flag = true;
			}

			if (Attack_Type_Instruction < Bullet_Num)
			{
				//Player_Tracking_Movement_Attack();
				//Player_Tracking_Movement_Attack_2();
				//Player_Tracking_Bound_Bullets();
				Player_Tracking_Bound_Bullets_2();
			}
			else
			{
				Rush();
				//Laser_Clearing_2();
			}
		}
		else if (End_Flag && !Start_Flag && Update_Flag)
		{
			//End_Anime();
			//Warp_EF.Play();
			start_timecline.Resume();

			if (transform.position != Pos_set[0, 0])
			{
				if (Vector_Size(Pos_set[0, 0], transform.position) < Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}

				transform.position = Moving_To_Target_S(transform.position, Pos_set[0, 0], Now_Speed * 2.0f);
			}
			else if (transform.position == Pos_set[0, 0])
			{
				//warp_ef.SetActive(true);
				ParticleSystem p = warp_ef.GetComponent<ParticleSystem>();
				p.Play();
				start_timecline.Resume();
				Update_Flag = false;
				//foreach (ParticleSystem system in Warp_EF)
				//{
				//	system.Play();
				//}
			}
		}
		else if (End_Flag && !Start_Flag && !Update_Flag)
		{
		}
		if (transform.position.x >= 30.0f)
		{
			Is_Dead = true;
		}
	}


	private void OnEnable()
	{
		//shokika();
		Now_View = Hidden_View = 1.0f;
		Display_Amount = appearance_speed / 60.0f;
		for (int i = 0; i < Dissolve_Effect_Material.Length; i++)
		{
			Dissolve_Effect_Material[i].material.SetFloat("_Dissolve_Alfa", Hidden_View);
		}
		Update_Flag = false;
		//warp_ef.SetActive(true);
		//Warp_EF.Play();
		start_timecline.Play();
	}

	#region スタートアニメ_1
	private void Start_Anime()
	{
		if (Attack_Step == 0)
		{
			Target_2 = transform.position;
			Attack_Step++;
		}
		else if (Attack_Step == 1)
		{
			Vector3 temp = Target_2;
			temp.x = Pos_set[0, 0].x;
			Target_2 = temp;
			if (transform.position != Target_2)
			{
				if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance_A)
				{
					if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
				}
				if (Vector_Size(Target_2, transform.position) < Speed_Change_Distance_A)
				{
					if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
				}
				transform.position = Moving_To_Target_A(transform.position, Target_2, Now_Speed_A);
			}
			else if (transform.position == Target_2)
			{
				maenoiti = transform.position;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 2)
		{
			bool[] b = new bool[2] { false, false };

			if (Vector_Size(Standby_Pos[0], weapons[0].transform.localPosition) < Speed_Change_Distance_A
				&& Vector_Size(Standby_Pos[1], weapons[1].transform.localPosition) < Speed_Change_Distance_A)
			{
				if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
			}
			if (Vector_Size(Init_Weapons_pos[0], weapons[0].transform.localPosition) < Speed_Change_Distance_A
				&& Vector_Size(Init_Weapons_pos[1], weapons[1].transform.localPosition) < Speed_Change_Distance_A)
			{

				if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
			}

			for (int i = 0; i < weapons.Length; i++)
			{
				if (weapons[i].transform.localPosition != Init_Weapons_pos[i])
				{
					weapons[i].transform.localPosition = Moving_To_Target_A(weapons[i].transform.localPosition, Init_Weapons_pos[i], Now_Speed_A * 1000.0f);
					b[i] = false;
				}
				else if (weapons[i].transform.localPosition == Init_Weapons_pos[i])
				{
					b[i] = true;
				}
			}

			if (b[0] && b[1])
			{
				Attack_Step++;
			}
		}
		else if (Attack_Step == 3)
		{
			if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance_A)
			{
				if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
			}
			if (Vector_Size(Pos_set[0, 0], transform.position) < Speed_Change_Distance_A)
			{
				if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
			}

			if (transform.position != Pos_set[0, 0])
			{
				transform.position = Moving_To_Target_A(transform.position, Pos_set[0, 0], Now_Speed_A);
			}
			else if (transform.position == Pos_set[0, 0])
			{
				Attack_Step++;
			}
		}
		else if (Attack_Step == 4)
		{
			Start_Flag = false;
			Attack_Step = 0;
		}
	}
	#endregion

	#region スタートアニメ_2
	private void Start_Anime_2()
	{
		if (Attack_Step == 0)
		{
			warp_ef.SetActive(true);
			warp_ef.transform.localPosition = Vector3.zero;
			foreach (ParticleSystem system in Warp_EF)
			{
				system.Play();
			}
			Flame = 0;

			//GetComponent<Collider>().enabled = false;
			maenoiti = transform.position = new Vector3(5.0f, 0.0f, 0.0f);
			Target_2 = new Vector3(-5.0f, 0.0f, 0.0f);

			transform.rotation = Quaternion.identity;
			speed_cc = 0.0f;
			Attack_Step++;
		}
		else if (Attack_Step == 1)
		{
			Flame++;
			if (Flame >= 350)
			{
				speed_cc += Time.deltaTime * move_speed_A;
				Now_View -= Display_Amount;

				if (Vector_Size(transform.position, Target_2) > 0.01f)
				{
					if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance_A)
					{
						if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
					}
					if (Vector_Size(Target_2, transform.position) < Speed_Change_Distance_A)
					{
						if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
					}
					transform.position = Moving_To_Target_A(transform.position, Target_2, Now_Speed_A);

					transform.Rotate(new Vector3(Rotate_speed_A, 0.0f, 0.0f));
					if (transform.rotation.eulerAngles.x >= -0.1f && transform.rotation.eulerAngles.x <= 0.1f)
					{
						transform.rotation = Quaternion.identity;
					}

					//transform.position = Vector3.Lerp(transform.position, Target, speed_cc);
					//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(-359.0f,0.0f,0.0f)), move_speed);
					//float f = Mathf.Lerp(transform.rotation.eulerAngles.x, -360.0f, speed_cc);
					//transform.rotation = Quaternion.Euler(f, 0.0f, 0.0f);
					//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-360.0f, 0.0f, 0.0f), speed_cc);
				}
				if (Vector_Size(transform.position, Target_2) <= 0.01f)
				{
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-360.0f, 0.0f, 0.0f), 5.0f);
				}

				for (int i = 0; i < Dissolve_Effect_Material.Length; i++)
				{
					Dissolve_Effect_Material[i].material.SetFloat("_Dissolve_Alfa", Now_View);
				}

				if (Now_View <= Full_View && transform.rotation.x <= 0.01f && transform.rotation.x >= -0.01f)
				{
					transform.rotation = Quaternion.Euler(-360.0f, 0.0f, 0.0f);
					maenoiti = transform.position;
					Now_View = Full_View;
					Attack_Step++;
					Flame = 0;
				}
			}
		}
		else if (Attack_Step == 2)
		{
			Flame++;
			if (Flame == 1)
			{
				Attack_Step++;
				Flame = 0;
			}
		}
		else if (Attack_Step == 3)
		{
			if (transform.position != Pos_set[0, 0])
			{
				if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance_A)
				{
					if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
				}
				if (Vector_Size(Pos_set[0, 0], transform.position) < Speed_Change_Distance_A)
				{
					if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
				}
				transform.position = Moving_To_Target_A(transform.position, Pos_set[0, 0], Now_Speed_A * 1.5f);
			}
			else if (transform.position == Pos_set[0, 0])
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 4)
		{
			Flame++;
			if (Flame == 30)
			{
				Start_Flag = false;
				Flame = 0;
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				maenoiti = transform.position;
			}
		}
	}
	#endregion

	#region 終わりアニメーション
	private void End_Anime()
	{
		if (Attack_Step == 0)
		{
			if (supply[0].gameObject.activeSelf &&
			supply[1].gameObject.activeSelf)
			{

				if (supply[0].Completion_Confirmation() && supply[1].Completion_Confirmation())
				{
					supply[0].gameObject.SetActive(false);
					supply[1].gameObject.SetActive(false);

					Vector3 temp = transform.position;
					temp.z += 20.0f;
					Target_2 = temp;
					maenoiti = transform.position;

					Attack_Step++;
				}
			}
			else
			{
				Vector3 temp = transform.position;
				temp.z += 20.0f;
				Target_2 = temp;
				maenoiti = transform.position;

				Attack_Step++;
			}
		}
		else if (Attack_Step == 1)
		{
			bool[] b = new bool[2] { false, false };

			if (Vector_Size(maenoiti, transform.position) < Speed_Change_Distance_A)
			{
				if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;
			}
			if (Vector_Size(Target_2, transform.position) < Speed_Change_Distance_A)
			{
				if (Now_Speed_A > Mini_Speed_A) Now_Speed_A -= Mini_Speed_A;
			}

			if (transform.position != Target_2)
			{
				b[0] = false;
				transform.position = Moving_To_Target_A(transform.position, Target_2, Now_Speed_A);
			}
			else if (transform.position == Target_2)
			{
				b[0] = true;
			}

			if (transform.rotation != Quaternion.identity)
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotational_speed);
				b[1] = false;
			}
			else if (transform.rotation == Quaternion.identity)
			{
				b[1] = true;
			}

			if (b[0] && b[1])
			{
				Attack_Step++;
			}
		}

		else if (Attack_Step == 2)
		{
			if (Now_Speed_A < Max_Speed_A) Now_Speed_A += Mini_Speed_A;

			transform.position += transform.right * Now_Speed_A;
			if (transform.position.x >= 30.0f)
			{
				Is_Dead = true;
			}
		}
	}
	#endregion

	#region レーザーの薙ぎ払い攻撃_1
	/// <summary>
	/// レーザーの薙ぎ払い攻撃_1
	/// </summary>
	//private void Laser_Clearing()
	//{
	//	// アーム分離
	//	if (Attack_Step == 0)
	//	{
	//		bool[] b = new bool[arm_parts.Length + 1];
	//		for (int i = 0; i < arm_parts.Length; i++)
	//		{
	//			b[i] = true;
	//			if (arm_parts[i].transform.localPosition != Arm_Laser_Pos[i])
	//			{
	//				b[i] = false;
	//				arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Laser_Pos[i], speed * 1.5f);
	//			}
	//		}
	//		b[2] = true;
	//		if (transform.position.y != 0.0f)
	//		{
	//			b[2] = false;
	//			Vector3 temp = new Vector3(transform.position.x, 0.0f, 0.0f);
	//			transform.position = Moving_To_Target(transform.position, temp, speed);
	//		}

	//		if (b[0] && b[1] && b[2])
	//		{
	//			Attack_Step++;
	//		}
	//	}
	//	else if (Attack_Step == 1)
	//	{
	//		Flame++;

	//		Boss_One_Laser laser = Instantiate(Laser_Prefab, laser_muzzle[0].transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
	//		laser.Manual_Start(laser_muzzle[0].transform);

	//		if (Flame >= 30)
	//		{
	//			if (arm_parts[0].transform.localEulerAngles != Arm_45_Rotation[0])
	//			{
	//				arm_parts[0].transform.localRotation
	//					= Quaternion.RotateTowards(arm_parts[0].transform.localRotation, Quaternion.Euler(Arm_45_Rotation[0]), speed * 10.0f);
	//			}
	//			else if (arm_parts[0].transform.localEulerAngles == Arm_45_Rotation[0])
	//			{
	//				Flame = 0;
	//				Attack_Step++;
	//			}
	//		}
	//	}
	//	else if (Attack_Step == 2)
	//	{
	//		Flame++;
	//		if (Flame == 40)
	//		{
	//			Flame = 0;
	//			Attack_Step++;
	//		}
	//	}
	//	else if (Attack_Step == 3)
	//	{
	//		Flame++;

	//		Boss_One_Laser laser = Instantiate(Laser_Prefab, laser_muzzle[1].transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
	//		laser.Manual_Start(laser_muzzle[1].transform);

	//		if (Flame >= 30)
	//		{
	//			if (arm_parts[1].transform.localEulerAngles != Arm_45_Rotation[1])
	//			{
	//				arm_parts[1].transform.localRotation
	//					= Quaternion.RotateTowards(arm_parts[1].transform.localRotation, Quaternion.Euler(Arm_45_Rotation[1]), speed * 10.0f);
	//			}
	//			else if (arm_parts[1].transform.localEulerAngles == Arm_45_Rotation[1])
	//			{
	//				Flame = 0;
	//				Attack_Step++;
	//			}
	//		}
	//	}
	//	else if (Attack_Step == 4)
	//	{
	//		Flame++;
	//		if (Flame == 40)
	//		{
	//			Flame = 0;
	//			Attack_Step++;
	//		}
	//	}
	//	else if (Attack_Step == 5)
	//	{
	//		Flame++;

	//		foreach (GameObject obj in laser_muzzle)
	//		{
	//			Boss_One_Laser laser = Instantiate(Laser_Prefab, obj.transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
	//			laser.Manual_Start(obj.transform);
	//		}

	//		if (Flame >= 30)
	//		{
	//			bool[] b = new bool[arm_parts.Length];

	//			for (int i = 0; i < arm_parts.Length; i++)
	//			{
	//				b[i] = true;
	//				if (arm_parts[i].transform.localEulerAngles != Arm_Ini_Rotation[i])
	//				{
	//					b[i] = false;
	//					arm_parts[i].transform.localRotation
	//						= Quaternion.RotateTowards(arm_parts[i].transform.localRotation, Quaternion.Euler(Arm_Ini_Rotation[i]), speed * 10.0f);
	//				}
	//			}

	//			if (b[0] && b[1])
	//			{
	//				Flame = 0;
	//				Attack_Step++;
	//			}
	//		}
	//	}
	//	else if (Attack_Step == 6)
	//	{
	//		Flame++;
	//		foreach (GameObject obj in laser_muzzle)
	//		{
	//			Boss_One_Laser laser = Instantiate(Laser_Prefab, obj.transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
	//			laser.Manual_Start(obj.transform);
	//		}
	//		if (Flame == 60)
	//		{
	//			Flame = 0;
	//			Attack_Step++;
	//		}
	//	}
	//	else if (Attack_Step == 7)
	//	{
	//		Flame++;
	//		if (Flame == 40)
	//		{
	//			Flame = 0;
	//			Attack_Step++;
	//		}
	//	}
	//	else if (Attack_Step == 8)
	//	{
	//		bool[] b = new bool[arm_parts.Length];
	//		for (int i = 0; i < arm_parts.Length; i++)
	//		{
	//			b[i] = true;
	//			if (arm_parts[i].transform.localPosition != Arm_Closed_Position[i])
	//			{
	//				b[i] = false;
	//				arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Closed_Position[i], speed * 1.5f);
	//			}
	//		}
	//		if (b[0] && b[1])
	//		{
	//			Attack_Step = 0;
	//			Attack_Type_Instruction = 0;
	//			Flame = 0;
	//		}
	//	}
	//}
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
				//Vector3 temp = new Vector3(transform.position.x, 0.0f, 0.0f);

				if (Vector_Size(Target, transform.position) < Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}

				//if (Now_Speed < Max_Speed)
				//{
				//	Now_Speed += Lowest_Speed;
				//}
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
				Attack_Now = false;
			}
		}
	}
	#endregion

	#region レーザーの薙ぎ払い攻撃_3
	/// <summary>
	/// レーザーの薙ぎ払い攻撃_3
	/// </summary>
	private void Laser_Clearing_3()
	{
		// 下回り
		if (transform.position.y < 0)
		{
			if (Attack_Step == 0)
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
			else if (Attack_Step == 1)
			{
				Flame++;
				Laser_Shooting();

				if (Flame >= 30)
				{
					if (transform.rotation != Quaternion.Euler(For_body_Downward))
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Downward), speed * 3.0f);
					}
					else if (transform.rotation == Quaternion.Euler(For_body_Downward))
					{
						Attack_Step++;
						Flame = 0;
					}
				}
			}
			else if (Attack_Step == 2)
			{
				Flame++;
				Laser_Shooting();

				if (Flame == 40)
				{
					Attack_Step++;
					Flame = 0;
				}
			}
			else if (Attack_Step == 3)
			{
				if (transform.rotation != Quaternion.identity)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, speed * 15.0f);
				}
				else if (transform.rotation == Quaternion.identity)
				{
					Attack_Step = 0;
					Flame = 0;
					Attack_Type_Instruction = 0;
				}
			}
		}
		else if (transform.position.y > 0)
		{
			if (Attack_Step == 0)
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
			else if (Attack_Step == 1)
			{
				Flame++;
				Laser_Shooting();

				if (Flame >= 30)
				{
					if (transform.rotation != Quaternion.Euler(For_body_Upward))
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Upward), speed * 3.0f);
					}
					else if (transform.rotation == Quaternion.Euler(For_body_Upward))
					{
						Attack_Step++;
						Flame = 0;
					}
				}
			}
			else if (Attack_Step == 2)
			{
				Flame++;
				Laser_Shooting();
				if (Flame == 40)
				{
					Attack_Step++;
					Flame = 0;
				}
			}
			else if (Attack_Step == 3)
			{
				if (transform.rotation != Quaternion.identity)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, speed * 15.0f);
				}
				else if (transform.rotation == Quaternion.identity)
				{
					Attack_Step = 0;
					Flame = 0;
					Attack_Type_Instruction = 0;
				}
			}
		}
	}
	#endregion

	#region プレイヤーを追従しビーム攻撃
	/// <summary>
	/// プレイヤーを追従しビーム攻撃
	/// </summary>
	private void Player_Tracking_Movement_Attack()
	{
		if (Attack_Step == 0)
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Now_player_Traget = Player_Data[0];
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Now_player_Traget = Player_Data[Random.Range(0, 1)];
			}
			Attack_Step++;
		}
		// プレイヤー追従移動
		if (Attack_Step == 1)
		{
			Vector3 temp = transform.position;
			if (Now_player_Traget.transform.position.y >= 1.0f)
			{
				temp.y = 1.0f;
			}
			else if (Now_player_Traget.transform.position.y <= -1.0f)
			{
				temp.y = -1.0f;
			}
			else
			{
				temp.y = Now_player_Traget.transform.position.y;
			}

			//if (Vector_Size(temp, transform.position) > 0.5f)
			//{
			//	if (Target == transform.position)
			//	{
			//		Prev_Pos = Target;
			//		if (transform.position.y > Now_player_Traget.transform.position.y)
			//		{
			//			Target = transform.position - MOVEY;
			//		}
			//		else if (transform.position.y < Now_player_Traget.transform.position.y)
			//		{
			//			Target = transform.position + MOVEY;
			//		}

			//		if (Target.y > 1.0f || Target.y < -1.0f)
			//		{
			//			Target = Prev_Pos;
			//		}
			//	}

			if (Vector_Size(temp, transform.position) <= Speed_Change_Distance)
			{
				if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
			}
			else if (Vector_Size(temp, transform.position) > Speed_Change_Distance)
			{
				if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
			}

			transform.position = Moving_To_Target(transform.position, temp, Now_Speed);

			//}
			if (Vector_Size(temp, transform.position) < Lowest_Speed)
			{
				Attack_Step++;
			}
		}
		//// 開く
		//else if (Attack_Step == 1)
		//{
		//	bool[] b = new bool[arm_parts.Length];
		//	for (int i = 0; i < arm_parts.Length; i++)
		//	{
		//		if (arm_parts[i].transform.localPosition != Arm_Open_Position[i] && !b[i])
		//		{
		//			arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Open_Position[i], speed * 2.0f);
		//			b[i] = false;
		//		}
		//		else if (arm_parts[i].transform.localPosition == Arm_Open_Position[i])
		//		{
		//			b[i] = true;
		//		}
		//	}
		//	if (b[0] && b[1])
		//	{
		//		Attack_Step++;
		//	}
		//}
		// 攻撃
		else if (Attack_Step == 2)
		{
			Flame++;
			if (Flame == 1)
			{
				foreach (GameObject Muzzle in muzzles)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Muzzle.transform.position, Muzzle.transform.right);
				}
			}
			else if (Flame == 30)
			{
				Attack_Step = 0;
				Attack_Type_Instruction++;
				Flame = 0;
			}
		}
		//// 閉じる
		//else if (Attack_Step == 3)
		//{
		//	bool[] b = new bool[arm_parts.Length];
		//	for (int i = 0; i < arm_parts.Length; i++)
		//	{
		//		if (arm_parts[i].transform.localPosition != Arm_Closed_Position[i] && !b[i])
		//		{
		//			arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Closed_Position[i], speed * 2.0f);
		//			b[i] = false;
		//		}
		//		else if (arm_parts[i].transform.localPosition == Arm_Closed_Position[i])
		//		{
		//			b[i] = true;
		//		}
		//	}
		//	if (b[0] && b[1])
		//	{
		//		Attack_Step=0;
		//		IIIIII++;
		//	}
		//}
	}
	#endregion

	#region プレイヤーを追従しビーム攻撃_2
	/// <summary>
	/// プレイヤーを追従しビーム攻撃_2
	/// </summary>
	private void Player_Tracking_Movement_Attack_2()
	{
		if (Attack_Step == 0)
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Now_player_Traget = Player_Data[0];
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Now_player_Traget = Player_Data[Random.Range(0, 1)];
			}
			Attack_Step++;
		}
		if (Attack_Step == 1)
		{
			#region 追従
			Vector3 temp = transform.position;
			temp.y = Now_player_Traget.transform.position.y;
			if (Target == transform.position)
			{
				Prev_Pos = Target;
				if (transform.position.y > Now_player_Traget.transform.position.y)
				{
					Target = transform.position - MOVEY;
				}
				else if (transform.position.y < Now_player_Traget.transform.position.y)
				{
					Target = transform.position + MOVEY;
				}

				if (Target.y >= 1.0f || Target.y <= -1.0f)
				{
					Target = Prev_Pos;
				}
			}

			if (Vector_Size(temp, transform.position) < Speed_Change_Distance)
			{
				if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
			}
			else if (Vector_Size(temp, transform.position) >= Speed_Change_Distance)
			{
				if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
			}

			transform.position = Moving_To_Target(transform.position, Target, Now_Speed);
			#endregion

			Flame++;
			if (Flame == 60)
			{
				foreach (GameObject Muzzle in muzzles)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Muzzle.transform.position, Muzzle.transform.right);
				}
				Attack_Step = 0;
				Attack_Type_Instruction++;

				Flame = 0;
			}
		}
		// 攻撃
		else if (Attack_Step == 2)
		{
			Flame++;
			if (Flame == 1)
			{
			}
			else if (Flame == 30)
			{
				Flame = 0;
			}
		}
	}
	#endregion

	#region プレイヤーを追従しバウンド弾
	/// <summary>
	/// プレイヤーを追従しバウンド弾
	/// </summary>
	private void Player_Tracking_Bound_Bullets()
	{
		if (Attack_Step == 0)
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Now_player_Traget = Player_Data[0];
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Now_player_Traget = Player_Data[Random.Range(0, 1)];
			}
			Attack_Step++;
		}
		// プレイヤー追従移動
		if (Attack_Step == 1)
		{
			Vector3 temp = transform.position;
			if (Now_player_Traget.transform.position.y >= 1.0f)
			{
				temp.y = 1.0f;
			}
			else if (Now_player_Traget.transform.position.y <= -1.0f)
			{
				temp.y = -1.0f;
			}
			else
			{
				temp.y = Now_player_Traget.transform.position.y;
			}

			//if (Vector_Size(temp, transform.position) < Speed_Change_Distance)
			//{
			//	if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
			//}
			//else if (Vector_Size(temp, transform.position) > Speed_Change_Distance)
			//{
			//	if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
			//}

			//transform.position = Moving_To_Target(transform.position, temp, Now_Speed);
			if (Now_Speed < Max_Speed)
			{
				Now_Speed += Lowest_Speed;
			}
			transform.position = Vector3.Lerp(transform.position, temp, Now_Speed);

			//}
			if (Vector_Size(temp, transform.position) < 0.1f)
			{
				Attack_Step++;
				Now_Speed = 0;
			}
		}
		// 攻撃
		else if (Attack_Step == 2)
		{
			Flame++;
			if (Flame == 1)
			{
				//foreach (GameObject Muzzle in muzzles)
				//{
				//	//for(int i = 0; i < BoundBullet_Rotation.Length; i++)
				//	//{
				//	//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, Muzzle.transform.position,new Vector3(0.0f,0.0f, BoundBullet_Rotation[i]));
				//	//}

				//	foreach (Vector3 Dir in BoundBullet_Rotation)
				//	{
				//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, Muzzle.transform.position, Quaternion.Euler(Dir));
				//	}
				//}

				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				}
			}
			else if (Flame == 30)
			{
				Attack_Step = 0;
				Attack_Type_Instruction++;
				Flame = 0;
			}
		}
	}
	#endregion

	#region
	/// <summary>
	/// プレイヤーを追従しバウンド弾_2
	/// </summary>
	//private void Player_Tracking_Bound_Bullets_2()
	//{
	//	if (transform.position == Target)
	//	{
	//		Move_Poinnto1 = transform.position;
	//		is_naname = false;

	//		int x_temp = Random.Range(-1, 2);
	//		if (x_temp + x_p < 0 || x_num <= x_temp + x_p)
	//		{
	//			x_temp = 0;
	//		}
	//		int y_temp = Random.Range(-1, 2);
	//		if (y_temp + y_p < 0 || y_num <= y_temp + y_p)
	//		{
	//			y_temp = 0;
	//		}

	//		if (x_temp != 0 && y_temp != 0)
	//		{
	//			//if(Random.Range(0,2) == 0)
	//			//{
	//			//	Target_2 = poinnto[x_temp, y_temp + y_p];
	//			//}
	//			//else
	//			//{
	//			//	Target_2 = poinnto[x_temp + x_p, y_temp];
	//			//}
	//			is_naname = true;
	//		}
	//		else
	//		{
	//			Target_2 = Target;
	//			is_naname = false;
	//		}

	//		x_p += x_temp;
	//		y_p += y_temp;
	//		Target = poinnto[y_p, x_p];
	//		Move_Pionnto2 = Target_2;

	//		Debug.Log("Move_Poinnto1" + Move_Poinnto1);
	//		Debug.Log("Move_Pionnto2" + Move_Pionnto2);
	//	}

	//	if (Vector_Size(Target, transform.position) < Speed_Change_Distance)
	//	{
	//		if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
	//		//Now_Speed = Max_Speed * Vector_Size(mae, transform.position) + Lowest_Speed;
	//	}
	//	if (Vector_Size(Move_Poinnto1, transform.position) < Speed_Change_Distance)
	//	{
	//		if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
	//		//Now_Speed = Max_Speed * Vector_Size(Target, transform.position) + Lowest_Speed;
	//	}

	//	transform.position = Moving_To_Target_J(transform.position, Target, Now_Speed, is_naname);

	//	//transform.position = Moving_To_Target_S(transform.position, Target, Now_Speed);
	//	//transform.position = GetPoint(mae, ref tyuukei, Target, Now_Speed);
	//	//transform.position = new Vector3(Mathf.PerlinNoise(Time.time, 2.0f) + 8.0f, Mathf.PerlinNoise(Time.time, 2.0f),0.0f);

	//	if (Attack_Step == 0)
	//	{
	//		Flame++;
	//		if (Flame == 60)
	//		{
	//			for (int i = 0; i < BoundBullet_Rotation.Length; i++)
	//			{
	//				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
	//				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[1].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
	//			}
	//			Attack_Step++;
	//			Flame = 0;
	//		}
	//	}
	//	else if (Attack_Step == 1)
	//	{
	//		Flame++;
	//		if (Flame == 60)
	//		{
	//			for (int i = 0; i < BoundBullet_Rotation.Length; i++)
	//			{
	//				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[2].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
	//				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
	//			}
	//			Attack_Step++;
	//			Flame = 0;
	//		}
	//	}
	//	else if (Attack_Step == 2)
	//	{
	//		Flame++;
	//		if (Flame == 60)
	//		{
	//			Flame = 0;
	//			Attack_Step = 0;
	//			Attack_Type_Instruction++;
	//		}
	//	}
	//}
	#endregion

	#region プレイヤーを追従しバウンド弾_2
	/// <summary>
	/// プレイヤーを追従しバウンド弾_2
	/// </summary>
	private void Player_Tracking_Bound_Bullets_2()
	{
		if (transform.position == Target)
		{
			Attack_Type_Instruction++;

			for (int i = 0; i < BoundBullet_Rotation.Length; i++)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[2].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
			}


			int[] rand_I = new int[3] { -1, 0, 1 };
			maenoiti = transform.position;
			int a_temp = rand_I[Random.Range(0, rand_I.Length)];
			if (8 == a_temp + a_num)
			{
				a_temp = 0;
			}
			else if (a_temp + a_num == -1)
			{
				a_temp = 7;
			}
			int b_temp = rand_I[Random.Range(0, rand_I.Length)];
			if (b_temp + b_num == -1 || 5 == b_temp + b_num)
			{
				b_temp = 0;
			}

			a_num += a_temp;
			b_num += b_temp;
			Target = Pos_set[a_num, b_num];

			IntermediatePosition = new Vector3((maenoiti.x + Target.x) / 2.0f, (maenoiti.y + Target.y) / 2.0f, 0.0f);

			Attack_Step = 0;
		}

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

	#region ターゲット移動
	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target_A(Vector3 origin, Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;       // 移動する前のターゲットとの向き
		Vector3 return_pos = Vector3.zero;              // 返すポジション

		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		if (Vector_Size(return_pos, target) < speed)
		{
			return_pos = target;
			Now_Speed_A = Mini_Speed_A;
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
	//private Vector3 Moving_To_Target_J(Vector3 origin, Vector3 target, float speed, bool naname)
	//{
	//	Vector3 direction = Vector3.zero;       // 移動する前のターゲットとの向き
	//	Vector3 return_pos = Vector3.zero;              // 返すポジション

	//	if ((target.x - origin.x) < 0)
	//	{
	//		direction.x = -1;
	//	}
	//	else if ((target.x - origin.x) == 0)
	//	{
	//		direction.x = 0;
	//	}
	//	else if ((target.x - origin.x) > 0)
	//	{
	//		direction.x = 1;
	//	}

	//	if ((target.y - origin.y) < 0)
	//	{
	//		direction.y = -1;
	//	}
	//	else if ((target.y - origin.y) == 0)
	//	{
	//		direction.y = 0;
	//	}
	//	else if ((target.y - origin.y) > 0)
	//	{
	//		direction.y = 1;
	//	}

	//	if (naname)
	//	{
	//		return_pos.x = origin.x + (direction.x * speed);
	//		//return_pos.y = origin.y + (direction.y * (speed / speed));
	//		return_pos.y = 2.0f / return_pos.x;

	//		//return_pos = Vector3.Slerp(origin, target, speed);
	//	}
	//	else if (!is_naname)
	//	{
	//		return_pos.x = origin.x + (direction.x * speed);
	//		return_pos.y = origin.y + (direction.y * speed);
	//	}

	//	if (Vector_Size(return_pos, target) < speed)
	//	{
	//		return_pos = target;
	//		Now_Speed = 0;
	//	}

	//	return return_pos;
	//}
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

	/// <summary>
	/// 突進攻撃
	/// </summary>
	private void Rush()
	{
		if (Attack_Step == 0)
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Now_player_Traget = Player_Data[0];
			}
			else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Now_player_Traget = Player_Data[Random.Range(0, 1)];
			}
			Attack_Now = true;
			Attack_Step++;
		}
		// プレイヤー追従移動
		if (Attack_Step == 1)
		{
			Flame++;

			Vector3 temp = transform.position;
			if (Now_player_Traget.transform.position.y >= 1.5f)
			{
				temp.y = 1.5f;
			}
			else if (Now_player_Traget.transform.position.y <= -1.5f)
			{
				temp.y = -1.5f;
			}
			else
			{
				temp.y = Now_player_Traget.transform.position.y;
			}

			if (Vector_Size(temp, transform.position) <= Speed_Change_Distance)
			{
				if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
			}
			else if (Vector_Size(temp, transform.position) > Speed_Change_Distance)
			{
				if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
			}

			transform.position = Moving_To_Target(transform.position, temp, Now_Speed);
			Turning();
			//}
			if (Flame == 40)
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 2)
		{
			Flame++;
			transform.Rotate(new Vector3((float)Flame, 0.0f, 0.0f));
			if (Flame == 40)
			{
				maenoiti = transform.position;
				Target = new Vector3(-12.0f, maenoiti.y, maenoiti.z);
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 3)
		{
			if (transform.position != Target)
			{
				if (Vector_Size(Target, transform.position) <= Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}

				transform.position = Moving_To_Target_S(transform.position, Target, Now_Speed * 3.0f);
				transform.Rotate(new Vector3(40.0f, 0.0f, 0.0f));
			}
			else if (transform.position == Target)
			{
				if (transform.eulerAngles.x < -180.0f)
				{
					transform.Rotate(new Vector3(40.0f, 0.0f, 0.0f));
				}
				else if (transform.eulerAngles.x > -180.0f)
				{
					if (transform.rotation != Quaternion.identity)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 40.0f);
					}
					else if (transform.rotation == Quaternion.identity)
					{
						maenoiti = transform.position;
						Target = new Vector3(Pos_set[0, 0].x, transform.position.y, 0.0f);
						Flame = 0;
						Attack_Step++;

					}
				}
			}
		}
		else if (Attack_Step == 4)
		{
			if (transform.position != Target)
			{
				if (Vector_Size(Target, transform.position) <= Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(maenoiti, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}

				transform.position = Moving_To_Target_S(transform.position, Target, Now_Speed * 3.0f);
			}
			else if (transform.position == Target)
			{
				Attack_Step = 0;
				Attack_Type_Instruction++;
				Attack_Now = false;
			}
		}
	}

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
	/// パーティクルの保存
	/// </summary>
	/// <param name="trans"></param>
	private void saiki_shoki(Transform trans)
	{
		for (int i = 0; i < trans.childCount; i++)
		{
			Warp_EF.Add(trans.GetChild(i).GetComponent<ParticleSystem>());
			if (trans.GetChild(i).childCount > 0)
			{
				saiki_shoki(trans.GetChild(i));
			}
		}
	}

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
}