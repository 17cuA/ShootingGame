//作成日2019/06/13
// 一面のボスの管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	HP管理
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private GameObject[] beam_mazle;
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
	[Header("回転速度")]
	private float rotating_velocity;
	[SerializeField]
	[Header("上のポイント")]
	private Vector2 upper_point;
	[SerializeField]
	[Header("中のポイント")]
	private Vector2 in_point;
	[SerializeField]
	[Header("下のポイント")]
	private Vector2 under_point;
	//------------------------------------------------------------

	public One_Boss_Parts Boss_Body { get; private set; }                       // ボスの本体
	public One_Boss_Parts Boss_Core { get; private set; }                       // ボスのコア
	public One_Boss_Parts[] Boss_Option { get; private set; }                   // ボスのオプション
	public One_Boss_Parts[] Boss_Option_Table { get; private set; }         // ボスの武装(台)
	public GameObject[] Beam_Mazle { get; private set; }                        // ボスのマズル

	private int Active_Flame { get; set; }
	private int Initial_HP { get; set; }
	private Material Core_Material { get; set; }
	private List<Vector3> Moving_Target_Point{get; set;}

	void Start()
    {
		//base.Start();
		Boss_Body = boss_body;
		Boss_Core = boss_core;
		Boss_Option = boss_option;
		Boss_Option_Table = boss_option_table;
		Beam_Mazle = beam_mazle;

		Core_Material = Boss_Core.GetComponent<Material>();
		Core_Material.color = initial_core_color;
		Initial_HP = hp;

		Moving_Target_Point = new List<Vector3>();
		Moving_Target_Point.Add(upper_point);
		Moving_Target_Point.Add(in_point);
		Moving_Target_Point.Add(under_point);
	}

    void Update()
    {
		// 一定HP以上のとき
		if((hp / Initial_HP) > remaining_hp_percent)
		{
			transform.Rotate(new Vector3(rotating_velocity,0.0f,0.0f));
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
}
