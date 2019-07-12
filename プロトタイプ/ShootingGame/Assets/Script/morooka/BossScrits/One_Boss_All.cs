//作成日2019/06/13
// 一面のボスの管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	HP管理
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class One_Boss_All : character_status
{
	// 移動方向
	private enum MOVING_DISTANCE
	{
		eRIGHT,					// 右
		eRIGHT_UP,			// 右斜め上
		eUP,						// 上
		eLEFT_UP,				// 左斜め上
		eLEFT,					// 左
		eLEFT_DOWN,		// 左斜め下
		eDOWN,				// 下
		eRIGHT_DOWN,		// 右斜め下
	}

	// Unity側で変更用変数
	//------------------------------------------------------------
	[Header("ボスの構成パーツ")]
	[SerializeField, Tooltip("ボスの本体")]						private One_Boss_Parts boss_body;
	[SerializeField, Tooltip("ボスのコア")]						private One_Boss_Parts boss_core;
	[SerializeField, Tooltip("ボスのオプション")]				private One_Boss_Parts[] boss_option;
	[SerializeField, Tooltip("ボスのオプションの設置台")]	private One_Boss_Parts[] boss_option_table;
	[SerializeField, Tooltip("ボスの本体にあるマズル")]		private Transform[] beam_mazle;
	[SerializeField, Tooltip("ボスのオプションの親")]			private Transform boss_option_center;

	[Header("ボスの操作に使用")]
	[SerializeField, Tooltip("残りHPパーセント")]		private float remaining_hp_percent;
	[SerializeField, Tooltip("初期コアカラー")]			private Color initial_core_color;
	[SerializeField, Tooltip("ピンチのコアカラー")]	private Color pinch_core_color;
	[SerializeField, Tooltip("上のポイント")]				private Vector2 upper_point;
	[SerializeField, Tooltip("上中のポイント")]			private Vector2 upper_in_point;
	[SerializeField, Tooltip("中のポイント")]				private Vector2 in_point;
	[SerializeField, Tooltip("下中のポイント")]			private Vector2 under_in_point;
	[SerializeField, Tooltip("下のポイント")]				private Vector2 under_point;
	[SerializeField, Tooltip("ビームの最大数")]			private int beam_max;
	[SerializeField, Tooltip("回転角度")]					private int rotating_velocity;
	[SerializeField, Tooltip("六角形の大きさ")]			private float hexagonal_size;
	//------------------------------------------------------------

	public One_Boss_Parts Boss_Body { get; private set; }						// ボスの本体
	public One_Boss_Parts Boss_Core { get; private set; }						// ボスのコア
	public One_Boss_Parts[] Boss_Option { get; private set; }				// ボスのオプション
	public One_Boss_Parts[] Boss_Option_Table { get; private set; }		// ボスの武装(台)
	public Transform[] Initial_Beam_Mazle { get; private set; }				// ボスのマズル
	public Transform Boss_Option_Center { get; private set; }				// ボスのオプションの中心位置

	private int Active_Flame { get; set; }									// ボスが起動されてからのフレーム数
	private int Initial_HP { get; set; }										// ボスの初期HP
	private Material Core_Material { get; set; }							// コアの色を管理するマテリアル
	private List<Vector3> Moving_Target_Point { get; set; }		// 移動ターゲットのポジションまとめるリスト
	private Vector3 Now_Target { get; set; }								// 今の移動したい場所
	private float Rotating_Velocity { get; set; }							// 回転速度
	private int Beam_Cnt { get; set; }										// ビームの数
	private int Original_Position_Num { get; set; }						// 移動開始前の位置番号
	private int Now_Positon_Num { get; set; }							// 移動したい場所の位置番号
	private int Attack_Step { get; set; }										// 攻撃手順支持

	private Vector3 Initial_Boss_Option_Center { get; set; }					// オプションの中心の初期位置
	private Vector3[] Facing_Hexagonal_Option { get; set; }					// オプションの六角形の向き
	private Vector3[] Position_Hexagonal_Option { get; set; }					// オプションの六角形の位置
	private List<Vector3> Initial_Boss_Option_Table_Pos { get; set; }		// オプション台の初期位置
	private List<Vector3> Initial_Boss_Option_Pos { get; set; }				// オプションの初期位置
	private List<Vector3> Muzzle_Facing { get; set; }							// マズルの初期の向き
	private List<Vector3> Through_Direction { get; set; }						// 直進方向

	void Start()
    {
		Through_Direction = new List<Vector3>();
		Through_Direction.Add(new Vector3(1.0f,0.0f,0.0f));
		Through_Direction.Add(new Vector3(1.0f,1.0f,0.0f));
		Through_Direction.Add(new Vector3(0.0f,1.0f,0.0f));
		Through_Direction.Add(new Vector3(-1.0f,1.0f,0.0f));
		Through_Direction.Add(new Vector3(-1.0f,0.0f,0.0f));
		Through_Direction.Add(new Vector3(-1.0f,-1.0f,0.0f));
		Through_Direction.Add(new Vector3(0.0f,-1.0f,0.0f));
		Through_Direction.Add(new Vector3(1.0f,-1.0f,0.0f));

		// 各パーツの保持
		Boss_Body = boss_body;
		Boss_Core = boss_core;
		Boss_Option = boss_option;
		Boss_Option_Table = boss_option_table;
		Initial_Beam_Mazle = beam_mazle;
		Boss_Option_Center = boss_option_center;

		// コアのカラーの取得
		Core_Material = Boss_Core.GetComponent<MeshRenderer>().material;
		Core_Material.color = initial_core_color;

		// 最大HPの取得
		Initial_HP = Boss_Core.hp = hp;

		// 移動したいポイントの配列化
		Moving_Target_Point = new List<Vector3>();
		Moving_Target_Point.Add(in_point);
		Moving_Target_Point.Add(upper_point);
		Moving_Target_Point.Add(under_point);
		Moving_Target_Point.Add(upper_in_point);
		Moving_Target_Point.Add(under_in_point);

		// 初期位置の保持と移動ターゲットの変更
		Original_Position_Num = 0;
		Moving_Target_Change();

		// カウンターのリセット
		Beam_Cnt = 0;
		Attack_Step = 0;

		// オプションテーブルの非アクティブ化
		Boss_Option_Table[0].gameObject.SetActive(false);
		Boss_Option_Table[1].gameObject.SetActive(false);

		// マズルの初期向き保持
		Muzzle_Facing = new List<Vector3>();
		Muzzle_Facing.Add(Initial_Beam_Mazle[0].right);
		Muzzle_Facing.Add(Initial_Beam_Mazle[1].right);

		// オプション台の初期位置保持
		Initial_Boss_Option_Table_Pos = new List<Vector3>();
		Initial_Boss_Option_Table_Pos.Add(Boss_Option_Table[0].transform.localPosition);
		Initial_Boss_Option_Table_Pos.Add(Boss_Option_Table[1].transform.localPosition);

		// オプションの初期位置保持
		Initial_Boss_Option_Pos = new List<Vector3>();
		for (int i = 0;i< Boss_Option.Length; i++)
		{
			Initial_Boss_Option_Pos.Add(Boss_Option[i].transform.localPosition);
		}

		// ポジションの中心の初期位置所持と非アクティブ化
		Initial_Boss_Option_Center = Boss_Option_Center.localPosition;
		Boss_Option_Center.gameObject.SetActive(false);

		// 回転地の保持
		Rotating_Velocity = rotating_velocity;

		// オプションの六角形の向き設定
		Facing_Hexagonal_Option = new Vector3[Boss_Option.Length];
		Position_Hexagonal_Option = new Vector3[Boss_Option.Length];
		Facing_Hexagonal_Option[0] = new Vector3(0.0f, 5.0f, 0.0f);
		Facing_Hexagonal_Option[1] = new Vector3(4.330127f, 2.5f, 0.0f);
		Facing_Hexagonal_Option[2] = new Vector3(4.330127f, -2.5f, 0.0f);
		Facing_Hexagonal_Option[3] = new Vector3(0.0f, -5.0f, 0.0f);
		Facing_Hexagonal_Option[4] = new Vector3(-4.330127f, -2.5f, 0.0f);
		Facing_Hexagonal_Option[5] = new Vector3(-4.330127f, 2.5f, 0.0f);
		for(int i = 0; i < Facing_Hexagonal_Option.Length; i++)
		{
			Position_Hexagonal_Option[i] = Facing_Hexagonal_Option[i].normalized * hexagonal_size;
		}
	}

	void Update()
	{

		Boss_Debug();

		//if(Boss_Core.hp <= 0)
		//{
		//	Died_Process();
		//}

		// のこりHPの確認
		float now_percent = (float)Boss_Core.hp / (float)Initial_HP;
		// 一定HP以上のとき
		if (now_percent > remaining_hp_percent / 100.0f)
		{
			//Boss_Body.transform.Rotate(new Vector3(rotating_velocity,0.0f,0.0f));

			// 移動したい場所が今の位置と違うとき
			if (transform.position != Now_Target)
			{
				transform.position = Vector3.MoveTowards(transform.position, Now_Target, speed);
			}
			// 移動したい場所が今の位置と同じとき
			else if (transform.position == Now_Target)
			{
				Shot_Delay++;
				#region 保留
				//// 一定時間たったとき
				//if (Shot_Delay > Shot_DelayMax)
				//{
				//	// 一拍おく
				//	if (Attack_Step == 0)
				//	{
				//		Attack_Step++;
				//		Shot_Delay = 0;
				//	}
				//	// ビーム攻撃上
				//	else if (Attack_Step == 1 || Attack_Step == 3)
				//	{
				//		if (Beam_Cnt < beam_max)
				//		{
				//			Shoot_Beam(0);
				//			Beam_Cnt++;
				//			Shot_Delay /= 20;
				//		}
				//		else
				//		{
				//			Beam_Cnt = 0;
				//			Attack_Step++;
				//			Shot_Delay /= 2;
				//		}
				//	}
				//	// ビーム攻撃下
				//	else if (Attack_Step == 2 || Attack_Step == 4)
				//	{
				//		if (Beam_Cnt < beam_max)
				//		{
				//			Shoot_Beam(1);
				//			Beam_Cnt++;
				//			Shot_Delay /= 20;
				//		}
				//		else
				//		{
				//			Beam_Cnt = 0;
				//			Attack_Step++;
				//			Shot_Delay /= 2;
				//		}
				//	}
				//	// 次の位置決定
				//	else if (Attack_Step == 5)
				//	{
				//		Now_Target = Moving_Target_Point[Random.Range(0, Moving_Target_Point.Count)];
				//		Rotation_Speed_Change();
				//		Shot_Delay = 0;
				//		Beam_Cnt = 0;

				//		if (Random.Range(0, 2) == 0)
				//		{
				//			Attack_Step = 7;
				//		}
				//		else
				//		{
				//			Attack_Step = 0;
				//		}
				//	}

				//	else if (Attack_Step == 7)
				//	{
				//		if (Now_Target != Moving_Target_Point[0])
				//		{
				//			Now_Target = Moving_Target_Point[0];
				//		}
				//		else
				//		{
				//			Shoot_Beam(0);
				//			Shoot_Beam(1);

				//			Vector3 temp_right = Beam_Mazle[0].right;
				//			temp_right.y++;
				//			Beam_Mazle[0].right = temp_right;

				//			temp_right = Beam_Mazle[1].right;
				//			temp_right.y--;
				//			Beam_Mazle[1].right = temp_right;

				//			Shot_Delay /= 3;
				//			Beam_Cnt++;
				//		}

				//		if (Beam_Cnt == 3)
				//		{
				//			Debug.Log("asdfg");
				//			Attack_Step = 5;
				//			Shot_Delay = 0;
				//			Vector3 temp_right = Beam_Mazle[0].right;
				//			temp_right.y -= 3.0f;
				//			Beam_Mazle[0].right = temp_right;

				//			temp_right = Beam_Mazle[1].right;
				//			temp_right.y += 3.0f;
				//			Beam_Mazle[1].right = temp_right;
				//		}
				//	}
				//}
				#endregion

				// 攻撃可能のとき
				if (Shot_Delay > Shot_DelayMax)
				{
					// ビームの半分の動き
					// ビームを撃ったらマズルの回転
					if (Attack_Step < beam_max / 2)
					{
						//Shoot_Beam(0);
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[0].transform.position, Initial_Beam_Mazle[0].transform.right);
						Initial_Beam_Mazle[0].transform.Rotate(new Vector3(0.0f, 0.0f, Rotating_Velocity));

						//Shoot_Beam(1);
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[1].transform.position, Initial_Beam_Mazle[1].transform.right);
						Initial_Beam_Mazle[1].transform.Rotate(new Vector3(0.0f, 0.0f, -Rotating_Velocity));

						Attack_Step++;
					}
					// ビームのもう半分の動き
					// ビームを撃ったらマズルの回転
					else if (Attack_Step >= beam_max / 2 && Attack_Step < beam_max)
					{
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[0].transform.position, Initial_Beam_Mazle[0].transform.right);
						Initial_Beam_Mazle[0].transform.Rotate(new Vector3(0.0f, 0.0f, -Rotating_Velocity));

						//Shoot_Beam(1);
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[1].transform.position, Initial_Beam_Mazle[1].transform.right);
						Initial_Beam_Mazle[1].transform.Rotate(new Vector3(0.0f, 0.0f, Rotating_Velocity));

						Attack_Step++;
					}
					// ビームを撃ち切ったとき
					else
					{
						// リセット
						Attack_Step = 0;
						Initial_Beam_Mazle[0].transform.right = Muzzle_Facing[0];
						Initial_Beam_Mazle[1].transform.right = Muzzle_Facing[1];

						// 次の位置はランダム、今の位置と違う位置が指定されるまで続ける
						Moving_Target_Change();
					}
					Shot_Delay = 0;
				}
			}
		}
		// 一定HP以下のとき
		else
		{
			// コアの色が変わっていないとき
			if (Core_Material.color != pinch_core_color)
			{
				// コアの色を変える
				Core_Material.color = pinch_core_color;
				Now_Target = new Vector3(14.0f, 0.0f, 0.0f);
				Attack_Step = 0;
			}
			// 攻撃準備
			if (Attack_Step == 0)
			{
				// 本体の移動
				if (transform.position != Now_Target)
				{
					transform.position = Vector3.MoveTowards(transform.position, Now_Target, speed * 3.0f);
				}
				// 装備の設置
				else if (transform.position == Now_Target)
				{
					//	アクティブでないとき
					if (!Boss_Option_Table[0].gameObject.activeSelf
						|| !Boss_Option_Table[1].gameObject.activeSelf
						|| !Boss_Option_Center.gameObject.activeSelf)
					{
						// オプション台アクティブ化、位置を変える
						Boss_Option_Table[0].gameObject.SetActive(true);
						Vector3 vector = Boss_Option_Table[0].transform.localPosition;
						vector.x += 25.0f;
						Boss_Option_Table[0].transform.localPosition = vector;

						// オプション台アクティブ化、位置を変える
						Boss_Option_Table[1].gameObject.SetActive(true);
						vector = Boss_Option_Table[1].transform.localPosition;
						vector.x += 25.0f;
						Boss_Option_Table[1].transform.localPosition = vector;

						// オプションのアクティブ化、位置を変える
						Boss_Option_Center.gameObject.SetActive(true);
						vector = Boss_Option_Center.localPosition;
						vector.x += 25.0f;
						Boss_Option_Center.localPosition = vector;
					}
					// アクティブのとき
					else if (Boss_Option_Table[0].gameObject.activeSelf
						|| Boss_Option_Table[1].gameObject.activeSelf
						|| Boss_Option_Center.gameObject.activeSelf)
					{
						// オプション台,オプション、所定の位置へ移動
						if (Boss_Option_Table[0].transform.localPosition != Initial_Boss_Option_Table_Pos[0]
							|| Boss_Option_Table[1].transform.localPosition != Initial_Boss_Option_Table_Pos[1]
							|| Boss_Option_Center.transform.localPosition != Initial_Boss_Option_Center)
						{
							Boss_Option_Table[0].transform.localPosition = Vector3.MoveTowards(Boss_Option_Table[0].transform.localPosition, Initial_Boss_Option_Table_Pos[0], speed * 5);
							Boss_Option_Table[1].transform.localPosition = Vector3.MoveTowards(Boss_Option_Table[1].transform.localPosition, Initial_Boss_Option_Table_Pos[1], speed * 5);
							Boss_Option_Center.localPosition = Vector3.MoveTowards(Boss_Option_Center.localPosition, Initial_Boss_Option_Center, speed * 5);
						}
						// 指定の位置についたとき
						else if (Boss_Option_Table[0].transform.localPosition == Initial_Boss_Option_Table_Pos[0]
							|| Boss_Option_Table[1].transform.localPosition == Initial_Boss_Option_Table_Pos[1]
							|| Boss_Option_Center.localPosition == Initial_Boss_Option_Center)
						{
							Attack_Step++;
						}
					}
				}
			}
			// 攻撃開始
			else if (Attack_Step == 1)
			{
				// 10個のバレットを撃つ
				if (Beam_Cnt < 10)
				{
					Shot_Delay++;
					if (Shot_Delay > Shot_DelayMax)
					{
						// プレイヤーにマズルを向ける
						Vector3 target_dir = Obj_Storage.Storage_Data.GetPlayer().transform.position - Initial_Beam_Mazle[0].position;
						Initial_Beam_Mazle[0].right = target_dir;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, Initial_Beam_Mazle[0].position, Initial_Beam_Mazle[0].right);

						// プレイヤーにマズルを向ける
						target_dir = Obj_Storage.Storage_Data.GetPlayer().transform.position - Initial_Beam_Mazle[1].position;
						Initial_Beam_Mazle[1].right = target_dir;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, Initial_Beam_Mazle[1].position, Initial_Beam_Mazle[1].right);

						Shot_Delay = 0;
						Beam_Cnt++;
					}
				}
				else
				{
					Beam_Cnt = 0;
					Attack_Step++;
				}
			}
			else if (Attack_Step == 2)
			{
				if(Make_Options_Hexagon())
				{
					Attack_Step++;
				}
			}
			else if(Attack_Step == 3)
			{
				Boss_Option_Center.localPosition = Boss_Option_Center.localPosition + Through_Direction[(int)MOVING_DISTANCE.eLEFT].normalized * speed;
			}
			else if(Attack_Step == 4)
			{
				if (Options_Initial_Position_Move())
				{
					Attack_Step = 1;
				}
			}
		}
	}

	/// <summary>
	/// オプションの六角形への移動
	/// </summary>
	/// <returns> 移動したらTRUE </returns>
	private bool Make_Options_Hexagon()
	{
		bool installation_complete = true;
		for (int i = 0; i < Boss_Option.Length; i++)
		{
			if (Boss_Option[i].transform.localPosition != Facing_Hexagonal_Option[i])
			{
				Boss_Option[i].transform.localPosition = Vector3.MoveTowards(Boss_Option[i].transform.localPosition, Facing_Hexagonal_Option[i], speed * 6);
				installation_complete = false;
			}
		}
		return installation_complete;
	}

	/// <summary>
	/// オプションの初期位置への移動
	/// </summary>
	/// <returns> 移動したらTRUE </returns>
	private bool Options_Initial_Position_Move()
	{
		bool installation_complete = true;
		for (int i = 0; i < Boss_Option.Length; i++)
		{
			if (Boss_Option[i].transform.localPosition != Initial_Boss_Option_Pos[i])
			{
				Boss_Option[i].transform.localPosition = Vector3.MoveTowards(Boss_Option[i].transform.localPosition, Initial_Boss_Option_Pos[i], speed * 6);
				installation_complete = false;
			}
		}

		return installation_complete;
	}

	/// <summary>
	/// 今と違う位置に移動
	/// </summary>
	private void Moving_Target_Change()
	{
		// 同じ位置になるまで繰り返す
		do
		{
			Now_Positon_Num = Random.Range(0, Moving_Target_Point.Count);
		} while (Original_Position_Num == Now_Positon_Num);
		Now_Target = Moving_Target_Point[Now_Positon_Num];
		Original_Position_Num = Now_Positon_Num;
		//Now_Positon_Num = Random.Range(0, Moving_Target_Point.Count);
		//Now_Target = Moving_Target_Point[Now_Positon_Num];
		////Rotating_Velocity = (Moving_Target_Point[Now_Positon_Num].y - Moving_Target_Point[Original_Position_Num].y) / 360.0f;
		//Original_Position_Num = Now_Positon_Num;
	}

	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target(Vector3 origin,Vector3 target, float speed)
	{
		Vector3 moving_facing		= Vector3.zero;		// 移動する前のターゲットとの向き
		Vector3 facing_after_moved	= Vector3.zero;		// 移動した後のターゲットとの向き
		Vector3 return_pos			= Vector3.zero;		// 返すポジション
		
		moving_facing = origin - target;
		print(moving_facing);
		return_pos = origin + (moving_facing.normalized * speed);
		print(return_pos);
		facing_after_moved = return_pos - target;
		print(facing_after_moved);

		//Vector2 v1 = moving_facing.normalized;
		//Vector2 v2 = facing_after_moved.normalized;

		float inner_product = Vector3.Dot(moving_facing,facing_after_moved);
		//float inner_product = v1.x * v2.x + v1.y * v2.y;

		// ターゲットとの向きが移動前と違うとき(ターゲットを超えてしまうとき)
		if (inner_product <= 0)
		{
			// ターゲットの位置を上書き
			return_pos = target;
		}
		else
		{
			//Debug.LogWarning(v1 + ":" + v2);
			Debug.LogError(inner_product);
		}

		return return_pos;
	}

	private void Boss_Debug()
	{
		if (Input.GetKey(KeyCode.B))
		{
			if (Input.GetKey(KeyCode.H))
			{
				hp = Initial_HP / 2;
				Debug.Log("Boss_HP_Harf");
			}
			else if (Input.GetKey(KeyCode.Z))
			{
				hp = 0;
				Debug.Log("Boss_HP_Zero");
			}
			else if (Input.GetKey(KeyCode.F))
			{
				hp = Initial_HP;
				Debug.Log("Boss_HP_Full");
			}
			else if(Input.GetKey(KeyCode.Alpha1))
			{
				Attack_Step++;
			}
			else if(Input.GetKey(KeyCode.Alpha0))
			{
				Attack_Step = 0;
			}
		}
	}

	/// <summary>
	/// ビームを撃つ
	/// </summary>
	/// <param name="Muzzle_Number"> マズルの番号 </param>
	private void Shoot_Beam(int Muzzle_Number)
	{
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[Muzzle_Number].position, Initial_Beam_Mazle[Muzzle_Number].right);
		Muzzle_Facing[Muzzle_Number] = new Vector3(Muzzle_Facing[Muzzle_Number].x, Initial_Beam_Mazle[Muzzle_Number].right.y + 0.4f, Muzzle_Facing[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[Muzzle_Number].position, Muzzle_Facing[Muzzle_Number]);
		Muzzle_Facing[Muzzle_Number] = new Vector3(Muzzle_Facing[Muzzle_Number].x, Initial_Beam_Mazle[Muzzle_Number].right.y + 1, Muzzle_Facing[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[Muzzle_Number].position, Muzzle_Facing[Muzzle_Number]);
		Muzzle_Facing[Muzzle_Number] = new Vector3(Muzzle_Facing[Muzzle_Number].x, Initial_Beam_Mazle[Muzzle_Number].right.y - 0.4f, Muzzle_Facing[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[Muzzle_Number].position, Muzzle_Facing[Muzzle_Number]);
		Muzzle_Facing[Muzzle_Number] = new Vector3(Muzzle_Facing[Muzzle_Number].x, Initial_Beam_Mazle[Muzzle_Number].right.y - 1, Muzzle_Facing[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Initial_Beam_Mazle[Muzzle_Number].position, Muzzle_Facing[Muzzle_Number]);
	}
}
