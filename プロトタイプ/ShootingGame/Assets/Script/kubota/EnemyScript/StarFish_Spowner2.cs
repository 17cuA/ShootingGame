using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFish_Spowner2 : MonoBehaviour
{
	int frame;      //生成タイミングを計る用
	public int frameMax;    //生成のタイミングを設定する用
	private int attack_type;    //攻撃対象をプレイヤーの数に応じて変更するため

	Vector3[] pos = new Vector3[4];    //出現位置を入れておくもの
	private int Create_Pos_cnt;             //生成位置を決めるための

	public int Active_Frame;    //活動しているフレーム数
	[Header("動き続ける時間を設定")]
	public int Active_Frame_Max;            //活動しているフレーム最大数

	private GameObject P1_obj;
	private GameObject P2_obj;

	// Start is called before the first frame update
	void Start()
    {
		//生成位置を決められたポジションに配置
		pos[0] = new Vector3(4.5f, 2.9f, 0.0f);
		pos[1] = new Vector3(-5.5f, -3f, 0.0f);
		pos[2] = new Vector3(4.5f, -3f, 0.0f);
		pos[3] = new Vector3(-5.5f, 2.9f, 0.0f);

		//各値の初期化
		frame = 0;
		attack_type = 0;
		Create_Pos_cnt = 0;
		Active_Frame = 0;
		P1_obj = Obj_Storage.Storage_Data.GetPlayer();
		P2_obj = Obj_Storage.Storage_Data.GetPlayer2();
	}

	// Update is called once per frame
	void Update()
    {
		if (Active_Frame > Active_Frame_Max)
		{
			Active_Frame = 0;
			gameObject.SetActive(false);
		}
		Active_Frame++;
		//------------------------------------------------
		//生成処理
		Respown_Enemy();
	}
	void Respown_Enemy()
	{
		frame++;
		if (Create_Pos_cnt >= pos.Length) Create_Pos_cnt = 0;
		if (frame > frameMax)
		{
			//一人プレイか二人プレイかによって生成を変える
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				//稼働にし、規定の位置に移動させる
				for(int i = 0; i < pos.Length; i++)
				{
					GameObject effect = Obj_Storage.Storage_Data.Effects[14].Active_Obj();
					effect.transform.position = pos[Create_Pos_cnt];
					Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
					ESF.transform.position = pos[Create_Pos_cnt];
					//-------------------------------------------
					//生成したポジションを保存
					//ここでやらなければ、生成位置がずれてしまうため。
					ESF.firstPos = ESF.transform.position;
					//攻撃のターゲットを設定
					if (attack_type % 2 == 0)
					{
						ESF.TargetNumber = 1;
					}
					else
					{
						ESF.TargetNumber = 2;
					}
					ESF.Attack_Target_Decision(attack_type % 2);
					attack_type++;
					Create_Pos_cnt++;

				}
				frame = 0;
			}
			else
			{
				for (int i = 0; i < pos.Length; i++)
				{
					//稼働にし、規定の位置に移動させる-------------------------------------
					GameObject effect = Obj_Storage.Storage_Data.Effects[14].Active_Obj();
					effect.transform.position = pos[Create_Pos_cnt];
					//--------------------------------------------------------------
					Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
					ESF.transform.position = pos[Create_Pos_cnt];
					ESF.firstPos = ESF.transform.position;
					ESF.TargetNumber = 1;
					Create_Pos_cnt++;
				}
				frame = 0;
			}
		}
	}
}
