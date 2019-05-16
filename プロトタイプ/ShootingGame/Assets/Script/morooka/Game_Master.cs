//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/16
//----------------------------------------------------------------------------------------------
// ゲーム全体の管理
//----------------------------------------------------------------------------------------------
// 2019/04/20：フレーム数の格納、計算
// 2019/05/16：ボスのデータベース全ての格納
//----------------------------------------------------------------------------------------------
using UnityEngine;
using CSV_Management;

public class Game_Master : MonoBehaviour
{
	/// <summary>
	/// ボスのデータベースにあるレコード管理
	/// </summary>
	public　enum BOSS_DATA_ELEMENTS
	{
		eID,					// BossのID
		eNAME,					// Boss の名前
		eSCRIPT,				// 各 Boss 個別のスクリプト
		eATTACK_INTERVAL,		// 攻撃と攻撃のインターバル
		eACT_CHANGE,			// 攻撃種類の切り替えインターバル
		ePARTS_HP,				// Boos のパーツのHP
		eSCORE,					// Boss の持つHP
		eBULLET_NAME_1,			// 攻撃パターン1
		eBULLET_NAME_2,			// 攻撃パターン2
		eBULLET_NAME_3,			// 攻撃パターン3
		eBULLET_NAME_4,			// 攻撃パターン4
		eBULLET_NAME_5,			// 攻撃パターン5
		eEFFECT					// エフェクト
	}

	public uint Frame_Count { private set; get; }				// ゲームが開始してからの時間をカウント
    public static Game_Master MY { get; private set; }			// 自分の情報
    public static int Score{ set; get; }						// スコア
	public Database_Manager Boss_Data { private set; get; }		// ボスのデータベース

    void Start()
    {
		if(Boss_Data == null)
		{
			Boss_Data = new Database_Manager();
			Boss_Data.CSVArrangement("Boss/Boss_Data");
		}
		MY = GetComponent<Game_Master>();
    }

    // Update is called once per frame
    void Update()
    {
        Frame_Count++;
    }
}
