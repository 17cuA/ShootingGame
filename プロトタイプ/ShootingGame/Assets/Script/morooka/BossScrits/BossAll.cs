//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossの全体管理をする
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの格納、各パーツの生存確認
// 2019/05/16：データベースの読み込み
// 2019/05/24：パーツ戦闘不能時の挙動変更
// 2019/05/30：攻撃切り替えタイミング用ボス専用のフレーム管理変数の追加
// 2019/05/30：初期化の処理の発動を他スクリプトに任せる
//----------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using CSV_Management;

public class BossAll : MonoBehaviour
{
	public Animator animationControl;       // アニメーションの管理
	public Renderer ownRenderer;            // 自分のレンダー
	[SerializeField]
	private bool is_last_boss;				// ラスボスかどうか

	public List<BossParts> OwnParts { private set; get; }           // 自分のパーツの管理
	private List<MeshRenderer> PartsRenderer { set; get; }          // 自分のパーツのレンダー
	public Record_Container Status_Data { private set; get; }       // データベースからのデータ
	//public int HP { private set; get; }                             // 自分のヒットポイント
	public int My_Score { private set; get; }                       // 自分の持ちスコア
	public int attack_interval { private set; get; }				// 攻撃インターバル
	public int attack_change { private set; get; }				// 攻撃種類の切り替えインターバル
	public bool move_switch { private set; get; }					// 移動方法の切り替え
	public int Attack_Change_Frame_Cnt { private set; get; }			// 攻撃切り替え用フレームカウンター
	public bool Attack_Change_Flag { private set; get; }			// フラグによる攻撃の切り替え


	//　テストPOP専用
	public GameObject poper;



	private void Awake()
    {
        gameObject.AddComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		PartFactorDeletion();

		// パーツがなくなったとき
		if (Is_PartsAlive())
		{
			//OwnDeletion();
		}
			// 自分がカメラ内に入ったとき
			if (ownRenderer.isVisible)
			{                
				// 格納したパーツの表示
				for (int i = 0; i < PartsRenderer.Count; i++)
				{
					PartsRenderer[i].enabled = true;
				}
				// 自身のレンダーの使用をやめる
				ownRenderer.enabled = false;
				PartsRenderer.Clear();
				move_switch = true;
			}

		Attack_Change_Frame_Cnt++;
	}

	/// <summary>
	/// パーツを取得する再帰関数
	/// </summary>
	/// <param name="objTrans"> トランスフォーム </param>
	void Part_Acquisition(Transform objTrans)
	{
		// 子供を参照する繰り返し
		for (int i = 0; i < objTrans.childCount; i++)
		{
			// リストに情報を入れる
			OwnParts.Add(objTrans.GetChild(i).GetComponent<BossParts>());
			
			//参照した子供に、また子供がいるとき
			if(objTrans.GetChild(i).childCount > 0)
			{
				// Part_Acquisition関数の呼び出し
				Part_Acquisition(objTrans.GetChild(i));
			}
		}
	}

    /// <summary>
    /// パーツが動いていないとき要素の削除
    /// </summary>
    private void PartFactorDeletion()
	{
		//各パーツの確認
		for (int i = 0; i < OwnParts.Count; i++)
		{
			// 動いていないとき
			if (!OwnParts[i].gameObject.active)
			{
				// 要素削除
				OwnParts.RemoveAt(i);
			}
		}
	}

	/// <summary>
	/// 各パーツの生存確認
	/// </summary>
	/// <returns> 無敵でないパーツが生存しないとき true </returns>
	public bool Is_PartsAlive()
	{
		for (int i = 0; i < OwnParts.Count; i++)
		{
            // 無敵でないパーツのとき
			if (!OwnParts[i].invincible)
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// 疑似デストラクタ
	/// </summary>
	private void OwnDeletion()
	{

		Destroy(gameObject);
	}

	/// <summary>
	/// データベースからの読み込み
	/// </summary>
	/// <param name="id"> ID番号 </param>
	public void Extracting_From_BD(int id)
	{
		Status_Data = new Record_Container();
		Status_Data.Set_Data(Game_Master.MY.Boss_Data.SearchAt_ID(id));
	}

	/// <summary>
	/// 攻撃を切り替える時間来たら
	/// </summary>
	/// <returns> 攻撃を切り替える時間が来たら true </returns>
	public bool Attack_Switching_Time()
	{
		if(Attack_Change_Frame_Cnt >= attack_change)
		{
			Attack_Change_Frame_Cnt = 0;
			return true;
		}

		return false;
	}

	public void Attack_kougeki()
	{
		Attack_Change_Frame_Cnt = 0;

	}

	public void BossAll_Start( int boss_id)
	{
		// データベースからデータ出力
		Status_Data = new Record_Container();
		Status_Data.Set_Data(Game_Master.MY.Boss_Data.SearchAt_ID(boss_id));

		//HP = status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.ePARTS_HP);
		My_Score = Status_Data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eSCORE);
		attack_interval = Status_Data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eATTACK_INTERVAL);
		attack_change = Status_Data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eACT_CHANGE);

		animationControl = GetComponent<Animator>();
		ownRenderer = GetComponent<Renderer>();
		ownRenderer.enabled = true;
		OwnParts = new List<BossParts>();
		Part_Acquisition(transform);
		PartsRenderer = new List<MeshRenderer>();
		for (int i = 0; i < OwnParts.Count; i++)
		{
			OwnParts[i].HP = Status_Data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.ePARTS_HP);
			PartsRenderer.Add(OwnParts[i].GetComponent<MeshRenderer>());
			PartsRenderer[i].enabled = false;
		}
		move_switch = false;
		Attack_Change_Frame_Cnt = new int();
		Attack_Change_Frame_Cnt = 0;

		if (is_last_boss)
		{
			//　テストPOP専用
			GameObject obj = Instantiate(poper, transform.position, Quaternion.identity) as GameObject;
			obj.GetComponent<Boss_Pop_Switch>().boss = gameObject;
			gameObject.SetActive(false);
		}
	}
}
