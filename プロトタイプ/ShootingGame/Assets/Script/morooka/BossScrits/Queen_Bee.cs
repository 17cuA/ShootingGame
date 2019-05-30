//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/30
//----------------------------------------------------------------------------------------------
// 中ボス 蜂型エネミー 女王バチ挙動
//----------------------------------------------------------------------------------------------
// 2019/04/20：HPで死亡まで作成
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Bee : MonoBehaviour
{
	// 蜂型だけの攻撃管理イナム
	private enum BEE_ATTACK
	{
		eSOLDIER_BEE,	// 兵隊バチ
		eBULLET,		// 弾
		eLASER,			// レーザー
	}

	private float speed = 2.0f;                 // 移動速度

	private BossAll BA { set; get; }                         // ボスの基本情報
	private Soldier_Bee[] Soldier_Bees { set; get; }          // 兵隊蜂の情報(攻撃パターン１情報)
	private GameObject[] Bullet { set; get; }                // 弾(攻撃パターン２情報)
	private GameObject Laser { set; get; }                  // レーザー(攻撃パターン３情報)
	private BEE_ATTACK Now_Attack { set; get; }				// 

	void Start()
    {
		BA = GetComponent<BossAll>();
		BA.BossAll_Start(2);
		Soldier_Bees = new Soldier_Bee[15];
		for(int i = 0; i < Soldier_Bees.Length; i++)
		{
			Soldier_Bees[i] = Resources.Load("morooka/" + BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_1]) as Soldier_Bee;
		}
		Bullet = new GameObject[20];
		for (int i = 0; i < Bullet.Length; i++)
		{
			Bullet[i] = Resources.Load("morooka/" + BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_2]) as GameObject;
		}
		Laser = new GameObject();
		Laser = Resources.Load("morooka/" + BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_3]) as GameObject;
	}

    void Update()
    {
        if(BA.attack_interval <= BA.Attack_Change_Frame_Cnt)
		{
			foreach(Soldier_Bee bee in Soldier_Bees)
			{
				bee.Attack_Start();
			}
		}

		
    }
}
