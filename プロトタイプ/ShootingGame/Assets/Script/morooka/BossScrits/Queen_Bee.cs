//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/30
//----------------------------------------------------------------------------------------------
// 中ボス 蜂型エネミー 女王バチ挙動
//----------------------------------------------------------------------------------------------
// 2019/05/30：変数宣言とイナム作成
// 2019/05/31：兵隊機の生成
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Bee : MonoBehaviour
{
	// 蜂型だけの攻撃管理イナム
	private enum BEE_ATTACK
	{
		eSOLDIER_BEE,   // 兵隊バチ
		eBULLET,        // 弾
		eLASER,         // レーザー
	}

	private float speed = 2.0f;                 // 移動速度
	private const int soldier_Line = 3;			// 兵隊のライン数
	private const int soldier_menber = 5;		// 各ラインのメンバー数

	private BossAll BA { set; get; }                         // ボスの基本情報
	public Soldier[][] Soldier_Bees_S; /*{ set; get; }*/		// 兵隊の script 情報(攻撃パターン１情報)
	public GameObject[][] Soldier_Bees_G; /*{ set; get; }*/		// 兵隊の object 情報(攻撃パターン１情報)
	private GameObject[] Bullet { set; get; }                // 弾(攻撃パターン２情報)
	private GameObject Laser { set; get; }                  // レーザー(攻撃パターン３情報)
	private BEE_ATTACK Now_Attack { set; get; }				// 現在の攻撃種類の情報

	void Start()
    {
		BA = GetComponent<BossAll>();
		BA.BossAll_Start(2);

		Soldier_Bees_S = new Soldier[soldier_Line][];
		Soldier_Bees_G = new GameObject[soldier_Line][];
		for(int i = 0; i < soldier_Line; i++)
		{
			Soldier_Bees_S[i] = new Soldier[soldier_menber];
			Soldier_Bees_G[i] = new GameObject[soldier_menber];
			for (int j = 0; j < soldier_menber; j++)
			{
				Soldier_Bees_G[i][j] = Resources.Load(BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_1]) as GameObject;
				Instantiate(Soldier_Bees_G[i][j], transform.position,Quaternion.identity);
				Soldier_Bees_S[i][j] = Soldier_Bees_G[i][j].GetComponent<Soldier>();
			}
		}
		Bullet = new GameObject[20];
		for (int i = 0; i < Bullet.Length; i++)
		{
			Bullet[i] = Resources.Load(BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_2]) as GameObject;
		}
		//Laser = new GameObject();
		//Laser = Resources.Load("morooka/" + BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_3]) as GameObject;
	}

    void Update()
    {
        if(BA.attack_interval <= BA.Attack_Change_Frame_Cnt)
		{
			// 各ラインのY軸の数値獲得
			float[] y_pos = new float[soldier_Line] { (Random.Range(0.0f, 3.0f) + -3.5f), (Random.Range(0.0f, 3.0f) + 0.5f), (Random.Range(0.0f, 3.0f) + 3.5f) };

			// ライン用の繰り返し
			for (int i = 0;i < soldier_Line; i++)
			{
				// X軸の数値獲得
				float x_pos = Random.Range(0.0f, 10.0f) + 40.0f;

				// メンバー用の繰り返し
				for (int j = 0; j < soldier_menber; j++)
				{
					print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
					Soldier_Bees_G[i][j].gameObject.SetActive(true);
					Soldier_Bees_S[i][j].Attack_Start(new Vector3(x_pos, y_pos[i], 0.0f));

					// 次のメンバーは今のメンバーの後ろに配置
					x_pos += 2.0f;
				}
			}
		}		
    }
}
