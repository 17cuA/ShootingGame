﻿//作成日2019/07/30
// 一面のボス本番
// 作成者:諸岡勇樹
/*
 * 2019/07/30　グリッド移動の適応
 * 2019/08/02　ボスにレーザー追加
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

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
	[SerializeField, Tooltip("ボスのコア")] private GameObject core;
	[SerializeField, Tooltip("パーツコア")] private One_Boss_Parts[] parts_core;
	[SerializeField, Tooltip("アームのパーツ")] private GameObject[] arm_parts;
	[SerializeField, Tooltip("アームの見た目")] private GameObject[] arm_mesh;
	[SerializeField, Tooltip("ビームまずる")] private GameObject[] muzzles;
	[SerializeField, Tooltip("レーザーのまずる")] private GameObject[] laser_muzzle;
	[SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;

	private One_Boss_Parts Core { get; set; }			// コアのパーツ情報
	public float Max_Speed { get; set; }				// 最大速度
	public float Now_Speed { get; set; }				// 今の速度
	public float Lowest_Speed { get; set; }				// 最小速度
	public float Speed​_Change_Distance {get;set;}		// 速度変更距離

	private Vector3[] Arm_Closed_Position { get; set; }		// アーム閉じいている位置
	private Vector3[] Arm_Open_Position { get; set; }		// アーム開いてる位置
	private Vector3[] Arm_Laser_Pos { get; set; }			// アームのレーザーを撃つ位置
	private Vector3[] Arm_Ini_Rotation { get; set; }		// アームの初期角度
	private Vector3[] Arm_45_Rotation { get; set; }			// アームの45度の角度

	private Vector3 For_body_Upward { get; set; }		// 本体の上向き角度
	private Vector3 For_body_Downward { get; set; }		// 本体の下向き角度

	private GameObject Laser_Prefab { get; set; }		// レーザーのプレハブ
	private uint Flame { get; set; }					// ボス内でのフレーム数
	private float Rotational_Speed { get; set; }		// 回転速度

	private int Attack_Step { get; set; }		// 関数内 攻撃ステップ

	public GameObject[] Player_Data { get; private set; }		// プレイヤーのデータ
	public GameObject Now_player_Traget { get; set; }			// ターゲット情報の保管用
	private int Attack_Type_Instruction { get; set; }			// 攻撃タイプ支持

	private bool End_Flag { get; set; }			// 終わりのフラグ

	private new void Start()
    {
		base.Start();

		Laser_Prefab = Resources.Load("Bullet/Laser") as GameObject;

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
		Max_Speed = speed;
		Now_Speed = Lowest_Speed = Max_Speed / 20.0f;
		for(;Now_Speed <= Max_Speed ;Now_Speed += Lowest_Speed )
		{
			Speed​_Change_Distance += Now_Speed;
		}
		Now_Speed = Lowest_Speed;

		Target = transform.position = new Vector3(10.0f, 0.0f, 0.0f);
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
		Arm_45_Rotation[0] = new Vector3( 45.0f, arm_parts[0].transform.localEulerAngles.y, arm_parts[0].transform.localEulerAngles.z);
		Arm_45_Rotation[1] = new Vector3(360.0f -45.0f, arm_parts[1].transform.localEulerAngles.y, arm_parts[1].transform.localEulerAngles.z);

		For_body_Upward = new Vector3(0.0f, 0.0f, 45.0f);
		For_body_Downward = new Vector3(0.0f, 0.0f, -45.0f);
		Rotational_Speed = speed * 6.5f;

		Attack_Step = 0;

		End_Flag = false;
	}

	private new void Update()
	{
		if (!End_Flag)
		{
			base.Update();
			if (Core.hp < 1)
			{
				//base.Died_Judgment();
				//base.Died_Process();

				foreach (One_Boss_Parts parts in parts_core)
				{
					if (parts.gameObject.activeSelf)
					{
						parts.hp = 1;
					}
				}
			}

			if (!parts_core[0].gameObject.activeSelf && !parts_core[1].gameObject.activeSelf)
			{
				End_Flag = true;
			}

			if (Attack_Type_Instruction < 10)
			{
				Player_Tracking_Movement_Attack();
				//Player_Tracking_Movement_Attack_2();
			}
			else
			{
				//Laser_Clearing();
				Laser_Clearing_2();
				//Laser_Clearing_3();
			}
		}
		else if(End_Flag)
		{
			transform.position += transform.right * speed;
		}
	}

	/// <summary>
	/// レーザーの薙ぎ払い攻撃_1
	/// </summary>
	private void Laser_Clearing()
	{
		// アーム分離
		if (Attack_Step == 0)
		{
			bool[] b = new bool[arm_parts.Length + 1];
			for (int i = 0; i < arm_parts.Length; i++)
			{
				b[i] = true;
				if (arm_parts[i].transform.localPosition != Arm_Laser_Pos[i])
				{
					b[i] = false;
					arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Laser_Pos[i], speed * 1.5f);
				}
			}
			b[2] = true;
			if (transform.position.y != 0.0f)
			{
				b[2] = false;
				Vector3 temp = new Vector3(transform.position.x, 0.0f, 0.0f);
				transform.position = Moving_To_Target(transform.position, temp, speed);
			}

			if (b[0] && b[1] && b[2])
			{
				Attack_Step++;
			}
		}
		else if (Attack_Step == 1)
		{
			Flame++;

			Boss_One_Laser laser = Instantiate(Laser_Prefab, laser_muzzle[0].transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
			laser.Manual_Start(laser_muzzle[0].transform);

			if (Flame >= 30)
			{
				if (arm_parts[0].transform.localEulerAngles != Arm_45_Rotation[0])
				{
					arm_parts[0].transform.localRotation
						= Quaternion.RotateTowards(arm_parts[0].transform.localRotation, Quaternion.Euler(Arm_45_Rotation[0]), speed * 10.0f);
				}
				else if (arm_parts[0].transform.localEulerAngles == Arm_45_Rotation[0])
				{
					Flame = 0;
					Attack_Step++;
				}
			}
		}
		else if (Attack_Step == 2)
		{
			Flame++;
			if (Flame == 40)
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 3)
		{
			Flame++;

			Boss_One_Laser laser = Instantiate(Laser_Prefab, laser_muzzle[1].transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
			laser.Manual_Start(laser_muzzle[1].transform);

			if (Flame >= 30)
			{
				if (arm_parts[1].transform.localEulerAngles != Arm_45_Rotation[1])
				{
					arm_parts[1].transform.localRotation
						= Quaternion.RotateTowards(arm_parts[1].transform.localRotation, Quaternion.Euler(Arm_45_Rotation[1]), speed * 10.0f);
				}
				else if (arm_parts[1].transform.localEulerAngles == Arm_45_Rotation[1])
				{
					Flame = 0;
					Attack_Step++;
				}
			}
		}
		else if (Attack_Step == 4)
		{
			Flame++;
			if (Flame == 40)
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 5)
		{
			Flame++;

			foreach (GameObject obj in laser_muzzle)
			{
				Boss_One_Laser laser = Instantiate(Laser_Prefab, obj.transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
				laser.Manual_Start(obj.transform);
			}

			if (Flame >= 30)
			{
				bool[] b = new bool[arm_parts.Length];

				for (int i = 0; i < arm_parts.Length; i++)
				{
					b[i] = true;
					if (arm_parts[i].transform.localEulerAngles != Arm_Ini_Rotation[i])
					{
						b[i] = false;
						arm_parts[i].transform.localRotation
							= Quaternion.RotateTowards(arm_parts[i].transform.localRotation, Quaternion.Euler(Arm_Ini_Rotation[i]), speed * 10.0f);
					}
				}

				if (b[0] && b[1])
				{
					Flame = 0;
					Attack_Step++;
				}
			}
		}
		else if (Attack_Step == 6)
		{
			Flame++;
			foreach (GameObject obj in laser_muzzle)
			{
				Boss_One_Laser laser = Instantiate(Laser_Prefab, obj.transform.position, Quaternion.identity).GetComponent<Boss_One_Laser>();
				laser.Manual_Start(obj.transform);
			}
			if (Flame == 60)
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 7)
		{
			Flame++;
			if (Flame == 40)
			{
				Flame = 0;
				Attack_Step++;
			}
		}
		else if (Attack_Step == 8)
		{
			bool[] b = new bool[arm_parts.Length];
			for (int i = 0; i < arm_parts.Length; i++)
			{
				b[i] = true;
				if (arm_parts[i].transform.localPosition != Arm_Closed_Position[i])
				{
					b[i] = false;
					arm_parts[i].transform.localPosition = Moving_To_Target(arm_parts[i].transform.localPosition, Arm_Closed_Position[i], speed * 1.5f);
				}
			}
			if (b[0] && b[1])
			{
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				Flame = 0;
			}
		}
	}

	/// <summary>
	/// レーザーの薙ぎ払い攻撃_2
	/// </summary>
	private void Laser_Clearing_2()
	{
		if (Attack_Step == 0)
		{
			if (transform.position.y != 0.0f)
			{
				Vector3 temp = new Vector3(transform.position.x, 0.0f, 0.0f);

				if (Vector_Size(temp, transform.position) <= Speed_Change_Distance)
				{
					if (Now_Speed > Lowest_Speed) Now_Speed -= Lowest_Speed;
				}
				else if (Vector_Size(temp, transform.position) > Speed_Change_Distance)
				{
					if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
				}

				transform.position = Moving_To_Target(transform.position, temp, Now_Speed);
			}
			else if (transform.position.y == 0.0f)
			{
				Attack_Step++;
			}
		}
		if (Attack_Step == 1)
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
		else if (Attack_Step == 2)
		{
			Flame++;
			Laser_Shooting();

			if (Flame >= 30)
			{
				if (transform.rotation != Quaternion.Euler(For_body_Upward))
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Upward), Rotational_Speed);
				}
				else if (transform.rotation == Quaternion.Euler(For_body_Upward))
				{
					Attack_Step++;
					Flame = 0;
				}
			}
		}
		else if (Attack_Step == 3)
		{
			Flame++;
			Laser_Shooting();

			if (Flame >= 30)
			{
				if (transform.rotation != Quaternion.Euler(For_body_Downward))
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(For_body_Downward), Rotational_Speed);
				}
				else if (transform.rotation == Quaternion.Euler(For_body_Downward))
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
				if (transform.rotation != Quaternion.identity)
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Rotational_Speed);
				}
				else if (transform.rotation == Quaternion.identity)
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
			if (Flame == 90)
			{
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				Flame = 0;
			}
		}
	}

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

		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		if (Vector_Size(return_pos, target) <= speed)
		{
			return_pos = target;
			Now_Speed = 0;
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
				Boss_One_Laser laser = Instantiate(Laser_Prefab, obj.transform.position, transform.rotation).GetComponent<Boss_One_Laser>();
				laser.Manual_Start(obj.transform);
			}
		}
	}
	#endregion
}
