//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/16
//----------------------------------------------------------------------------------------------
// ゲーム全体の管理
//----------------------------------------------------------------------------------------------
// 2019/04/20：フレーム数の格納、計算
// 2019/05/16：ボスのデータベース全ての格納
// 2019/05/24：ゲーム中の切り替え
// 2019/05/30：ボスのアニメーション許可
//----------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using CSV_Management;

public class Game_Master : MonoBehaviour
{
    /// <summary>
    /// ボスのデータベースにあるレコード管理
    /// </summary>
    public enum BOSS_DATA_ELEMENTS
    {
        eID,                    // BossのID
        eNAME,                  // Boss の名前
        eSCRIPT,                // 各 Boss 個別のスクリプト
        eATTACK_INTERVAL,       // 攻撃と攻撃のインターバル
        eACT_CHANGE,            // 攻撃種類の切り替えインターバル
        ePARTS_HP,              // Boos のパーツのHP
        eSCORE,                 // Boss の持つHP
        eBULLET_NAME_1,         // 攻撃パターン1
        eBULLET_NAME_2,         // 攻撃パターン2
        eBULLET_NAME_3,         // 攻撃パターン3
        eBULLET_NAME_4,         // 攻撃パターン4
        eBULLET_NAME_5,         // 攻撃パターン5
        eEFFECT                 // エフェクト
    }

    /// <summary>
    /// エネミーのデータベースにあるレコード管理
    /// </summary>
    public enum ENEMY_DATA_ELEMENTS
    {
        eID,                    // Enemy のID
        eNAME,                  // Enemy の名前
        eHP,                    // HP
        eATTACK_INTERVAL,		// 攻撃と攻撃のインターバル
        eBULLET,                // 弾
        eEFFECT					// エフェクト
    }

	/// <summary>
	/// ステージ内のセグメント管理
	/// </summary>
    public enum CONFIGURATION_IN_STAGE
    {
        eNORMAL,	
        eBOSS_CUT_IN,
        eBOSS_BUTTLE,
        eCLEAR,
    }

	/// <summary>
	/// 生成オブジェクトの指定用
	/// </summary>
	public enum OBJECT_NAME
	{
		ePLAYER_BULLET,		// プレイヤーのバレット
		ePLAYER_MISSILE,		// プレイヤーのミサイル
		ePLAYER_LASER,		// プレイヤーのレーザー
		ePLAYER_TowWay,		// プレイヤーの2ウェイミサイル
		ePOWERUP_ITEM,		//パワーアップアイテム
		eENEMY_BULLET,		// エネミーのバレット
		eENEMY_BEAM,			// エネミーのビーム
		eENEMY_LASER,			// エネミーのレーザー
		/////////////////////////////////////////////////////////////////
		ePLAYER,										// プレイヤー
		eENEMY_NUM1,								// エネミー1番
		eUFOTYPE_ENEMY,                         // UFOタイプエネミー
		eUFOTYPE_ENEMY_ITEM,                // UFOタイプエネミーのアイテム落とす版
		eUFOMOTHERTYPE_ENEMY,			// UFO母艦タイプエネミー
		eBEELZEBUBTYPE_ENEMY,				// ハエ型エネミー
		eCLAMCHOWDERTYPE_ENEMY,		// 貝型エネミー
		eOCTOPUSTYPE_ENEMY,					// タコ型エネミー
	}

	public uint Frame_Count{private set; get;}                  // ゲームが開始してからの時間をカウント
    public static Game_Master MY{get; private set;}             // 自分の情報
    public uint display_score{private set; get;}                // 表示スコア
    public Database_Manager Boss_Data{private set; get;}        // ボスのデータベース
    public Database_Manager Enemy_Data{private set; get;}       // エネミーのデータベース
    public CONFIGURATION_IN_STAGE Management_In_Stage{set; get;}// ステージ内管理
    public Score_Display _Display{private set; get;}			// スコア表示をするため用
	public bool Is_Completed_For_Warning_Animation { set; get; }							// WARNING アニメーションの終了用
	public string[] Name_List {  get; private set; }

	private void Awake()
	{
		if(Name_List == null)
		{
			Database_Manager database_ = new Database_Manager("CSV_Folder/Obaject_Name");
			Name_List = new string[database_.Database_Array.GetLength(0)];
			Name_List = database_.goreco(0);
		}
		if (Boss_Data == null)
		{
			Boss_Data = new Database_Manager("Boss/Boss_Data");
		}
		MY = GetComponent<Game_Master>();
	}

	void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
                break;
            case "Stage":
                Stage_Start();
                break;
            case "GameOver":
                break;
            case "GameClear":
                break;
        }
    }

    void Update()
    {
        Frame_Count++;

        if(Input.GetKeyDown(KeyCode.C))
        {
            Management_In_Stage = CONFIGURATION_IN_STAGE.eBOSS_CUT_IN;
        }
    }

    /// <summary>
    /// 取得スコア
    /// </summary>
    /// <param name="addition"> 加算数 </param>
    public void Score_Addition(uint addition)
    {
		display_score += addition;
		_Display.Object_To_Display.Character_Preference(display_score.ToString("D10"));
    }

    /// <summary>
    /// ステージシーンのスタート
    /// </summary>
    private void Stage_Start()
    {
        Management_In_Stage = CONFIGURATION_IN_STAGE.eNORMAL;

        _Display = GameObject.Find("Score_Display").GetComponent<Score_Display>();
		Is_Completed_For_Warning_Animation = false;
    }
}
