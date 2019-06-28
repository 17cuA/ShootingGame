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
	[SerializeField]
	private One_Boss_Parts boss_body;
	[SerializeField]
	private One_Boss_Parts boss_core;
	[SerializeField]
	private One_Boss_Parts[] boss_option;
	[SerializeField]
	private One_Boss_Parts[] boss_option_table;
	[SerializeField]
	private Transform[] beam_mazle;
	[SerializeField]
	[Header("残りHPパーセント")]
	private int remaining_hp_percent;
	[SerializeField]
	[Header("初期コアカラー")]
	private Color initial_core_color;
	[SerializeField]
	[Header("ピンチのコアカラー")]
	private Color pinch_core_color;
	[SerializeField]
	[Header("上のポイント")]
	private Vector2 upper_point;
	[SerializeField]
	[Header("上中のポイント")]
	private Vector2 upper_in_point;
	[SerializeField]
	[Header("中のポイント")]
	private Vector2 in_point;
	[SerializeField]
	[Header("下中のポイント")]
	private Vector2 under_in_point;
	[SerializeField]
	[Header("下のポイント")]
	private Vector2 under_point;
	//------------------------------------------------------------

	public One_Boss_Parts Boss_Body { get; private set; }						// ボスの本体
	public One_Boss_Parts Boss_Core { get; private set; }						// ボスのコア
	public One_Boss_Parts[] Boss_Option { get; private set; }				// ボスのオプション
	public One_Boss_Parts[] Boss_Option_Table { get; private set; }		// ボスの武装(台)
	public Transform[] Beam_Mazle { get; private set; }							// ボスのマズル

	private int Active_Flame { get; set; }
	private int Initial_HP { get; set; }
	private Material Core_Material { get; set; }
	private List<Vector3> Moving_Target_Point{get; set;}
	private Vector3 Now_Target { get; set; }
	private float Rotating_Velocity { get; set; }

	private int Original_Position_Num { get; set; }
	private int Now_Positon_Num { get; set; }

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
		Moving_Target_Point.Add(upper_point);
		Moving_Target_Point.Add(in_point);
		Moving_Target_Point.Add(under_point);
		Moving_Target_Point.Add(upper_in_point);
		Moving_Target_Point.Add(under_in_point);
		Original_Position_Num = 0;
		Rotation_Speed_Change();
	}

    void Update()
    {
		Shot_Delay++;
		// 一定HP以上のとき
		if((hp / Initial_HP) > (remaining_hp_percent / 100))
		{
			//Boss_Body.transform.Rotate(new Vector3(rotating_velocity,0.0f,0.0f));

			if(transform.position != Now_Target)
			{
				//transform.position = Vector3.MoveTowards(transform.position, Now_Target, speed);
				transform.position = Moving_To_Target(transform.position, Now_Target, speed);
				Boss_Body.transform.Rotate(new Vector3(Rotating_Velocity, 0.0f, 0.0f));
				//Debug.LogError(Rotating_Velocity);
			}
			else if(transform.position == Now_Target)
			{
				//Now_Target = Moving_Target_Point[Random.Range(0, Moving_Target_Point.Count)];
				Rotation_Speed_Change();
			}

			if(Shot_Delay > Shot_DelayMax)
			{
				Vector2 temp_pos = Beam_Mazle[0].position;
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, temp_pos, Beam_Mazle[0].right);
				temp_pos = Beam_Mazle[1].position;
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, temp_pos, Beam_Mazle[1].right);
				Shot_Delay = 0;
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
			}
		}
    }

	private void Rotation_Speed_Change()
	{
		Now_Positon_Num = Random.Range(0, Moving_Target_Point.Count);
		Now_Target = Moving_Target_Point[Now_Positon_Num];
		Rotating_Velocity = (Moving_Target_Point[Now_Positon_Num].y - Moving_Target_Point[Original_Position_Num].y) / 360.0f;
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
		Vector3 moving_facing = Vector3.zero;				// 移動する前のターゲットとの向き
		Vector3 facing_after_moved = Vector3.zero;		// 移動した後のターゲットとの向き
		Vector3 return_pos = Vector3.zero;						// 返すポジション
		
		moving_facing = origin - target;
		return_pos = origin + moving_facing.normalized * speed;	
		facing_after_moved = return_pos - target;

		float inner_product = moving_facing.x * facing_after_moved.x + moving_facing.y * facing_after_moved.y + moving_facing.z * facing_after_moved.z;

		// ターゲットとの向きが移動前と違うとき(ターゲットを超えてしまうとき)
		if (inner_product <= 0)
		{
			// ターゲットの位置を上書き
			return_pos = target;
		}
		else
		{
			Debug.LogError(inner_product);
		}

		return return_pos;
	}
}
