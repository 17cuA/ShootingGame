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

	private BossAll BA { set; get; }							// ボスの基本情報
	private bool Is_Soldier_Attack_Finished { set; get; }		// 兵隊攻撃が終わっているか
	public Soldier[,] Soldier_Bees_S { set; get; }				// 兵隊の script 情報(攻撃パターン１情報)
	public GameObject[,] Soldier_Bees_G { set; get; }			// 兵隊の object 情報(攻撃パターン１情報)
	private GameObject[] Bullet { set; get; }					// 弾(攻撃パターン２情報)
	private GameObject Laser { set; get; }						// レーザー(攻撃パターン３情報)
	private BEE_ATTACK Now_Attack { set; get; }					// 現在の攻撃種類の情報

	void Start()
    {
		BA = GetComponent<BossAll>();
		BA.BossAll_Start(2);

		Soldier_Bees_S = new Soldier[soldier_Line, soldier_menber];
		Soldier_Bees_G = new GameObject[soldier_Line, soldier_menber];
		for(int i = 0; i < soldier_Line; i++)
		{
			for (int j = 0; j < soldier_menber; j++)
			{
				GameObject obj = Resources.Load(BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_1]) as GameObject;
				Soldier_Bees_G[i,j] = Instantiate(obj, transform.position,Quaternion.identity);
				Soldier_Bees_S[i,j] = Soldier_Bees_G[i,j].GetComponent<Soldier>();
			}
		}
		Bullet = new GameObject[20];
		for (int i = 0; i < Bullet.Length; i++)
		{
			GameObject obj = Resources.Load(BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_2]) as GameObject;
			Bullet[i] = Instantiate(obj, transform.position, Quaternion.identity);
		}
		//Laser = new GameObject();
		//Laser = Resources.Load("morooka/" + BA.Status_Data.Own_Record[(int)Game_Master.BOSS_DATA_ELEMENTS.eBULLET_NAME_3]) as GameObject;
	}

    void Update()
    {
		// 兵隊機の攻撃が終わっているとき
		if (Is_Soldier_Attack_Finished)
		{
			// 攻撃インターバル超えたとき
			if (BA.attack_interval <= BA.Attack_Change_Frame_Cnt)
			{
				// 各ラインのY軸の数値獲得
				float[] y_pos = new float[soldier_Line] { (Random.Range(-2.5f, -0.5f)), (Random.Range(0.5f, 1.5f)), (Random.Range(2.5f, 4.5f)) };

				// ライン用の繰り返し
				for (int i = 0; i < soldier_Line; i++)
				{
					// X軸の数値獲得
					float x_pos = Random.Range(0.0f, 10.0f) + 40.0f;

					// メンバー用の繰り返し
					for (int j = 0; j < soldier_menber; j++)
					{
						Soldier_Bees_G[i, j].gameObject.SetActive(true);
						Soldier_Bees_S[i, j].Attack_Start(new Vector3(x_pos, y_pos[i], 0.0f));

						// 次のメンバーは今のメンバーの後ろに配置
						x_pos += 2.0f;
					}
				}
				Is_Soldier_Attack_Finished = false;
			}
		}

		// 兵隊機の攻撃が続いているとき
		if(!Is_Soldier_Attack_Finished)
		{
			// 兵隊機の生存を確認
			if(Is_Soldier_Alive())
			{
				// 兵隊機がすべて死んでいるので攻撃終わり
				Is_Soldier_Attack_Finished = true;
				// インターバルを数え始めさせる
				BA.Attack_Termination();
			}
		}
    }

	/// <summary>
	/// 兵隊機が動いているかの確認
	/// </summary>
	/// <returns> 動いていれば false　動いていなければ true </returns>
	private bool Is_Soldier_Alive()
	{
		// すべての兵隊機の確認
		foreach (GameObject obj in Soldier_Bees_G)
		{
			if(obj.activeSelf)
			{
				return false;
			}
		}
		return true;
	}
}
