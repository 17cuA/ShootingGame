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
	// Unity側で変更用変数
	//------------------------------------------------------------
	[Header("ボスの構成パーツ")]
	[SerializeField, Tooltip("ボスの本体")] private One_Boss_Parts boss_body;
	[SerializeField, Tooltip("ボスのコア")] private One_Boss_Parts boss_core;
	[SerializeField, Tooltip("ボスのオプション")] private One_Boss_Parts[] boss_option;
	[SerializeField, Tooltip("ボスのオプションの設置台")] private One_Boss_Parts[] boss_option_table;
	[SerializeField, Tooltip("ボスの本体にあるマズル")] private Transform[] beam_mazle;
	[Header("ボスの操作に使用")]
	[SerializeField, Tooltip("残りHPパーセント")] private int remaining_hp_percent;
	[SerializeField, Tooltip("初期コアカラー")] private Color initial_core_color;
	[SerializeField, Tooltip("ピンチのコアカラー")] private Color pinch_core_color;
	[SerializeField, Tooltip("上のポイント")] private Vector2 upper_point;
	[SerializeField, Tooltip("上中のポイント")] private Vector2 upper_in_point;
	[SerializeField, Tooltip("中のポイント")] private Vector2 in_point;
	[SerializeField, Tooltip("下中のポイント")] private Vector2 under_in_point;
	[SerializeField, Tooltip("下のポイント")] private Vector2 under_point;
	[SerializeField, Tooltip("ビームの最大数")] private int beam_max;
	//------------------------------------------------------------

	public One_Boss_Parts Boss_Body { get; private set; }                       // ボスの本体
	public One_Boss_Parts Boss_Core { get; private set; }                       // ボスのコア
	public One_Boss_Parts[] Boss_Option { get; private set; }               // ボスのオプション
	public One_Boss_Parts[] Boss_Option_Table { get; private set; }     // ボスの武装(台)
	public Transform[] Beam_Mazle { get; private set; }                         // ボスのマズル

	private int Active_Flame { get; set; }                              // ボスが起動されてからのフレーム数
	private int Initial_HP { get; set; }                                    // ボスの初期HP
	private Material Core_Material { get; set; }                        // コアの色を管理するマテリアル
	private List<Vector3> Moving_Target_Point { get; set; }     // 移動ターゲットのポジションまとめるリスト
	private Vector3 Now_Target { get; set; }                            // 今の移動したい場所
	private float Rotating_Velocity { get; set; }                       // 回転速度
	private int Beam_Cnt { get; set; }                                  // ビームの数
	private int Original_Position_Num { get; set; }                 // 移動開始前の位置番号
	private int Now_Positon_Num { get; set; }                       // 移動したい場所の位置番号
	private int Attack_Step { get; set; }                                   // 攻撃手順支持
	private List<Vector3> Initial_Boss_Option_Table_Pos { get; set; }
	private List<Vector3> muki { get; set; }
	//private Vector3
	void Start()
    {
		//base.Start();
		Boss_Body = boss_body;
		Boss_Core = boss_core;
		Boss_Option = boss_option;
		Boss_Option_Table = boss_option_table;
		Beam_Mazle = beam_mazle;

		Core_Material = Boss_Core.GetComponent<MeshRenderer>().material;
		Core_Material.color = initial_core_color;
		Initial_HP = hp;

		Moving_Target_Point = new List<Vector3>();
		Moving_Target_Point.Add(in_point);
		Moving_Target_Point.Add(upper_point);
		Moving_Target_Point.Add(under_point);
		Moving_Target_Point.Add(upper_in_point);
		Moving_Target_Point.Add(under_in_point);
		Original_Position_Num = 0;
		Rotation_Speed_Change();

		Beam_Cnt = 0;
		Attack_Step = 0;
		Boss_Option_Table[0].gameObject.SetActive(false);
		Boss_Option_Table[1].gameObject.SetActive(false);

		muki = new List<Vector3>();
		muki.Add(Beam_Mazle[0].right);
		muki.Add(Beam_Mazle[1].right);

		Initial_Boss_Option_Table_Pos = new List<Vector3>();
		Initial_Boss_Option_Table_Pos.Add(Boss_Option_Table[0].transform.localPosition);
		Initial_Boss_Option_Table_Pos.Add(Boss_Option_Table[1].transform.localPosition);
	}

    void Update()
    {
		Boss_Debug();

		// 一定HP以上のとき
		if ((hp / Initial_HP) > (remaining_hp_percent / 100))
		{
			//Boss_Body.transform.Rotate(new Vector3(rotating_velocity,0.0f,0.0f));

			// 移動したい場所が今の位置と違うとき
			if(transform.position != Now_Target)
			{
				transform.position = Vector3.MoveTowards(transform.position, Now_Target, speed);
				//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-360.0f, 0.0f, 0.0f), speed);
				//transform.position = Moving_To_Target(transform.position, Now_Target, speed);
				//Boss_Body.transform.Rotate(new Vector3(Rotating_Velocity, 0.0f, 0.0f));
				//Debug.LogError(Rotating_Velocity);
			}
			// 移動したい場所が今の位置と同じとき
			else if (transform.position == Now_Target)
			{
				Shot_Delay++;
				// 一定時間たったとき
				if (Shot_Delay > Shot_DelayMax)
				{
					// 一拍おく
					if (Attack_Step == 0)
					{
						Attack_Step++;
						Shot_Delay = 0;
					}
					// ビーム攻撃上
					else if (Attack_Step == 1 || Attack_Step == 3)
					{

						Debug.Log(Beam_Cnt);
						Debug.Log(beam_max);

						if (Beam_Cnt < beam_max)
						{
							//Vector2 temp_pos = Beam_Mazle[0].position;

							Shoot_Beam(0);
							Beam_Cnt++;
							Shot_Delay /= 20;
						}
						else
						{
							Beam_Cnt = 0;
							Attack_Step++;
							Shot_Delay /= 2;
						}
					}
					// ビーム攻撃下
					else if (Attack_Step == 2 || Attack_Step == 4)
					{
						if (Beam_Cnt < beam_max)
						{
							//Vector2 temp_pos = Beam_Mazle[0].position;

							Shoot_Beam(1);
							Beam_Cnt++;
							Shot_Delay /= 20;
						}
						else
						{
							Beam_Cnt = 0;
							Attack_Step++;
							Shot_Delay /= 2;
						}
					}
					// 次の位置決定
					else if (Attack_Step == 5)
					{
						Now_Target = Moving_Target_Point[Random.Range(0, Moving_Target_Point.Count)];
						Rotation_Speed_Change();
						Shot_Delay = 0;
						Beam_Cnt = 0;

						if (Random.Range(0, 1) == 0)
						{
							Attack_Step = 7;
						}
						else
						{
							Attack_Step = 0;
						}
					}

					else if (Attack_Step == 7)
					{
						if (Now_Target != Moving_Target_Point[0])
						{
							Now_Target = Moving_Target_Point[0];
						}
						else
						{
							Shoot_Beam(0);
							Shoot_Beam(1);

							Vector3 temp_right = Beam_Mazle[0].right;
							temp_right.y++;
							Beam_Mazle[0].right = temp_right;

							temp_right = Beam_Mazle[1].right;
							temp_right.y--;
							Beam_Mazle[1].right = temp_right;

							Shot_Delay /= 3;
							Beam_Cnt++;
						}

						if (Beam_Cnt == 3)
						{

							Debug.Log("asdfg");
							Attack_Step = 5;
							Shot_Delay = 0;
							Vector3 temp_right = Beam_Mazle[0].right;
							temp_right.y -= 3.0f;
							Beam_Mazle[0].right = temp_right;

							temp_right = Beam_Mazle[1].right;
							temp_right.y += 3.0f;
							Beam_Mazle[1].right = temp_right;
						}
					}
				}
			}
		}
		// 一定HP以下のとき
		else
		{
			// コアの色が変わっていないとき
			if(Core_Material.color != pinch_core_color)
			{
				// コアの色を変える
				Core_Material.color = pinch_core_color;
				Now_Target = Moving_Target_Point[0];
				Attack_Step = 0;
			}
			// 攻撃準備
			if(Attack_Step == 0)
			{
				// 本体の移動
				if(transform.position != Now_Target)
				{
					transform.position = Vector3.MoveTowards(transform.position, Now_Target, speed);
				}
				// 装備の設置
				else if(transform.position == Now_Target)
				{
					if (Boss_Option_Table[0].gameObject.activeSelf == false || Boss_Option_Table[1].gameObject.activeSelf == false)
					{
						Boss_Option_Table[0].gameObject.SetActive(true);
						Vector3 vector = Boss_Option_Table[0].transform.position;
						vector.x = 25.0f;
						Boss_Option_Table[0].transform.position = vector;

						Boss_Option_Table[1].gameObject.SetActive(true);
						vector = Boss_Option_Table[1].transform.position;
						vector.x = 25.0f;
						Boss_Option_Table[1].transform.position = vector;
					}
					else if (Boss_Option_Table[0].gameObject.activeSelf && true 
						|| Boss_Option_Table[1].gameObject.activeSelf == true)
					{
						if (Boss_Option_Table[0].transform.position != Initial_Boss_Option_Table_Pos[0] || Boss_Option_Table[1].transform.position != Initial_Boss_Option_Table_Pos[1])
						{
							Boss_Option_Table[0].transform.position = Vector3.MoveTowards(Boss_Option_Table[0].transform.position, Initial_Boss_Option_Table_Pos[0], speed * 5);
							Boss_Option_Table[1].transform.position = Vector3.MoveTowards(Boss_Option_Table[1].transform.position, Initial_Boss_Option_Table_Pos[1], speed * 5);
						}
						else if(Boss_Option_Table[0].transform.position == Initial_Boss_Option_Table_Pos[0] || Boss_Option_Table[1].transform.position == Initial_Boss_Option_Table_Pos[1])
						{
							Attack_Step++;
						}
					}
				}
			}
			//
			else if(Attack_Step == 1)
			{
				if(Beam_Cnt < beam_max)
				{
					Shot_Delay++;
					if(Shot_Delay > Shot_DelayMax)
					{
						Vector3 target_dir = Obj_Storage.Storage_Data.GetPlayer().Get_Obj()[0].transform.position - Beam_Mazle[0].position;
						Beam_Mazle[0].right = target_dir;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, Beam_Mazle[0].position, Beam_Mazle[0].right);

						target_dir = Obj_Storage.Storage_Data.GetPlayer().Get_Obj()[0].transform.position - Beam_Mazle[1].position;
						Beam_Mazle[1].right = target_dir;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, Beam_Mazle[1].position, Beam_Mazle[1].right);
						Shot_Delay = 0;
					}
				}
			}
		}
    }

	private void Rotation_Speed_Change()
	{
		Now_Positon_Num = Random.Range(0, Moving_Target_Point.Count);
		Now_Target = Moving_Target_Point[Now_Positon_Num];
		//Rotating_Velocity = (Moving_Target_Point[Now_Positon_Num].y - Moving_Target_Point[Original_Position_Num].y) / 360.0f;
		Original_Position_Num = Now_Positon_Num;
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
				Debug.Log("Boss_ＨＰ_Harf");
			}
			else if (Input.GetKey(KeyCode.Z))
			{
				hp = 0;
				Debug.Log("Boss_ＨＰ_Zero");
			}
			else if (Input.GetKey(KeyCode.F))
			{
				hp = Initial_HP;
				Debug.Log("Boss_ＨＰ_Full");
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
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Beam_Mazle[Muzzle_Number].position, Beam_Mazle[Muzzle_Number].right);
		muki[Muzzle_Number] = new Vector3(muki[Muzzle_Number].x, Beam_Mazle[Muzzle_Number].right.y + 0.4f, muki[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Beam_Mazle[Muzzle_Number].position, muki[Muzzle_Number]);
		muki[Muzzle_Number] = new Vector3(muki[Muzzle_Number].x, Beam_Mazle[Muzzle_Number].right.y + 1, muki[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Beam_Mazle[Muzzle_Number].position, muki[Muzzle_Number]);
		muki[Muzzle_Number] = new Vector3(muki[Muzzle_Number].x, Beam_Mazle[Muzzle_Number].right.y - 0.4f, muki[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Beam_Mazle[Muzzle_Number].position, muki[Muzzle_Number]);
		muki[Muzzle_Number] = new Vector3(muki[Muzzle_Number].x, Beam_Mazle[Muzzle_Number].right.y - 1, muki[Muzzle_Number].z);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Beam_Mazle[Muzzle_Number].position, muki[Muzzle_Number]);
	}
}
