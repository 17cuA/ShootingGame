//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossの全体管理をする
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの格納、各パーツの生存確認
// 2019/05/16：データベースの読み込み
//----------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using CSV_Management;

public class BossAll : MonoBehaviour
{
	public Animator animationControl;       // アニメーションの管理
	public Renderer ownRenderer;            // 自分のレンダー

	public List<BossParts> OwnParts { private set; get; }           // 自分のパーツの管理
	private List<MeshRenderer> PartsRenderer { set; get; }          // 自分のパーツのレンダー
	public Record_Container status_data { private set; get; }       // データベースからのデータ
	//public int HP { private set; get; }                             // 自分のヒットポイント
	public int My_Score { private set; get; }                       // 自分の持ちスコア
	public float attack_interval { private set; get; }				// 攻撃インターバル
	public float attack_change { private set; get; }				// 攻撃種類の切り替えインターバル
	public bool move_switch { private set; get; }					// 移動方法の切り替え

	private void Awake()
    {
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<Rigidbody>();
    }

    void Start()
    {
		// データベースからデータ出力
		status_data = new Record_Container();
		status_data.Set_Data(Game_Master.MY.Boss_Data.SearchAt_ID(1));

		//HP = status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.ePARTS_HP);
		My_Score = status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eSCORE);
		attack_interval = (float)status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eATTACK_INTERVAL);
		attack_change = (float)status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.eACT_CHANGE);

		GetComponent<Rigidbody>().useGravity = false;
		animationControl = GetComponent<Animator>();
		ownRenderer = GetComponent<Renderer>();
        ownRenderer.enabled = true;
		OwnParts = new List<BossParts>();
		Part_Acquisition(transform);
		PartsRenderer = new List<MeshRenderer>();
		for(int i = 0; i < OwnParts.Count; i++)
		{
			OwnParts[i].HP = status_data.ToInt((int)Game_Master.BOSS_DATA_ELEMENTS.ePARTS_HP);
			PartsRenderer.Add(OwnParts[i].GetComponent<MeshRenderer>());
			PartsRenderer[i].enabled = false;
		}
		move_switch = false;
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
	/// パーツのリストの中が null のとき要素の削除
	/// </summary>
	private void PartFactorDeletion()
	{
		//各パーツの確認
		for (int i = 0; i < OwnParts.Count; i++)
		{
			// null のとき
			if (OwnParts[i].HP <= 0)
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
		status_data = new Record_Container();
		status_data.Set_Data(Game_Master.MY.Boss_Data.SearchAt_ID(id));
	}
}
