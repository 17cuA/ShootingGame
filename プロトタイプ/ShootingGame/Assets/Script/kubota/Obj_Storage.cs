using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Obj_Storage : MonoBehaviour
{
	public static Obj_Storage Storage_Data; //どこからでも触ることができるようにする変数

	public string NowStage_Name;
	#region プレイヤー関係のプレハブ
	private GameObject Player_Prefab;                           //プレイヤーのプレハブ
	private GameObject player_2_Prefab;                         //プレイヤー2のプレハブ
	private GameObject Player_Missile1;                         //プレイヤー1のミサイルプレハブ
	private GameObject Player_Missile2;							//プレイヤー２のミサイルプレハブ
	private GameObject Bullet_Prefab_P;                         //弾のPrefab情報
	private GameObject BulletPrefab_P2;							//２P用の弾プレハブ情報
	private GameObject BulletPrefab_Option_P1;					//オプション用の球プレハブ情報１P用
	private GameObject BulletPrefab_Option_P2;					//オプション用の弾プレハブ２P用
	private GameObject Option_Prefab;							//オプションのプレハブ
	private GameObject Item_Prefab;                             //パワーアップのアイテムを入れえるための処理
	#endregion

	#region 敵関係のプレハブ

	#region 全体を通して出る敵
	private GameObject Bullet_Prefab_E;                         //エネミーの弾のPrefab情報
	private GameObject UfoType_Enemy_Prefab;                    //UFO型エネミーのプレハブ
	#endregion

	#region ステージ1
	private GameObject Boss1_Prefab;							//ステージ１のボスのプレハブ
	private GameObject Boss2_Prefab;							//ステージ2のボスのプレハブ
	private GameObject Bullet_Prefab_BattleShip;				//バトルシップタイプの弾のPrefab情報
	private GameObject Beam_Bullet_E_Prefab;					//エネミーのビーム型バレットのプレハブ
	private GameObject SmallBeam_Bullet_E_Prefab;               //エネミーの小さいビーム型バレットのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;            //貝型エネミーのプレハブ
    private GameObject ClamChowderType_Enemy_Item_Prefab;            //貝型エネミーのプレハブ
    private GameObject BeelzebubType_Enemy_Prefab;				//ハエ型エネミーのプレハブ
	private GameObject BattleShip_Enemy_Prefab;					//戦艦型エネミーのプレハブ
	private GameObject Star_Fish_Enemy_Prefab;					//ヒトデ型のエネミーのプレハブ
	private GameObject Boss_Middle_Prefab;						//中ボスのプレハブ
	private GameObject Laser_Line_Prefab;						//レーザーのプレハブ
	private GameObject One_Boss_BousndBullet_Prefab;			//ボス1のバウンド弾プレハブ
	private GameObject Moai_Prefab;								//モアイのプレハブ
	private GameObject Moai_Mini_Group_Prefab;					//小さいモアイグループのプレハブ
	private GameObject Moai_Bullet_Prefab;						//モアイのバレットのプレハブ
	#endregion

	#region ステージ2
	private GameObject Discharge_Prefab;                        //敵を生成する敵のプレハブ
	private GameObject Discharged_Prefab;                       //↑から生成された敵のプレハブ
	private GameObject FollowGround_Prefab;                     //地形に沿って動く敵
	private GameObject StagBeetle_Prefab;                       //オプションハンターの敵のプレハブ
	private GameObject Cannon_Prefab;                           //壁配置タイプの大砲
    private GameObject Cannon_Item_Prefab;                      //壁配置タイプの大砲アイテム
    private GameObject OctopusType_Enemy_Prefab;                //タコ型エネミーのプレハブ
    private GameObject Walk_Prefab;                             //歩いて弾を出す敵
	#endregion

	#endregion

	#region エフェクト関係のものすべて
	private GameObject[] Effects_Prefab = new GameObject[17];   //particleのプレハブ
	[HideInInspector] public Object_Pooling[] Effects = new Object_Pooling[17]; //オブジェクトプール
	#endregion

	#region サウンド関係すべて
	[HideInInspector] public AudioClip[] audio_se = new AudioClip[29];    //ＳＥを読み込むための配列
	[HideInInspector] public AudioClip[] PowerUpVoice = new AudioClip[8]; //VOICEを読み込むための配列

	[HideInInspector] public AudioClip[] First_Wireless;
	[HideInInspector] public AudioClip[] Second_Wireless;
	[HideInInspector] public AudioClip[] Third_Wireless;
	[HideInInspector] public AudioClip[] Forth_Wireless;
	[HideInInspector] public AudioClip[] Fifth_Wireless;
	[HideInInspector] public AudioClip[] Sixth_Wireless;
	[HideInInspector] public AudioClip[] Seventh_Wireless;

	#endregion

	#region オブジェクトプールの変数
	//実際に作られるオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Medium_Size_Enemy1;
	public Object_Pooling Player;
	public Object_Pooling Player_2;
	public Object_Pooling Boss_1;
	public Object_Pooling Boss_2;
	public Object_Pooling PlayerBullet;
	public Object_Pooling Player2Bullet;
	public Object_Pooling P1_OptionBullet;
	public Object_Pooling P2_OptionBullet;
	public Object_Pooling PlayerMissile;
	public Object_Pooling PlayerMissile2;
	public Object_Pooling PlayerMissile_TowWay;
	public Object_Pooling EnemyBullet;
	public Object_Pooling BattleShipBullet;
	public Object_Pooling Beam_Bullet_E;
	public Object_Pooling SmallBeam_Bullet_E;
	public Object_Pooling UfoType_Enemy;
	public Object_Pooling ClamChowderType_Enemy;
    public Object_Pooling ClamChowderType_Enemy_Item;
    public Object_Pooling BeelzebubType_Enemy;
	public Object_Pooling BattleShipType_Enemy;
	public Object_Pooling StarFish_Enemy;
	public Object_Pooling Option;
	public Object_Pooling PowerUP_Item;
	public Object_Pooling Boss_Middle;
	public Object_Pooling Laser_Line;
	public Object_Pooling One_Boss_BousndBullet;
	public Object_Pooling Moai;                       //モアイ
	public Object_Pooling Moai_Mini_Group;                       //小さいモアイの群れ
	public Object_Pooling Moai_Bullet;                       //モアイの弾
	#region ステージ2
	public Object_Pooling Discharge_Enemy;			//敵を排出する敵
	public Object_Pooling Discharged_Enemy;			//排出された敵
	public Object_Pooling FollowGround_Enemy;		//地形を這って進む敵
	public Object_Pooling StagBeetle_Enemy;			//オプションハンター
	public Object_Pooling Cannon_Enemy;             //大砲の敵
    public Object_Pooling Cannon_Enemy_Item;        //大砲の敵アイテム
    public Object_Pooling OctopusType_Enemy;        //タコ型の敵
    public Object_Pooling Walk_Enemy;               //歩く敵
	#endregion

	#endregion

	#region 敵のロード
	private GameObject enemy_UFO_Group_prefab;
	private GameObject enemy_UFO_Group_NoneShot_prefab;
	private GameObject enemy_ClamChowder_Group_Two_Top_prefab;
	private GameObject enemy_ClamChowder_Group_Two_Under_prefab;
	private GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab;
	private GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab;
	private GameObject enemy_ClamChowder_Group_Three_prefab;
	private GameObject enemy_ClamChowder_Group_Three_Item_prefab;
	private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab;
	private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab;
	private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab;
	private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab;
	private GameObject enemy_ClamChowder_Group_Four_prefab;
	private GameObject enemy_ClamChowder_Group_Four_NoItem_prefab;
	private GameObject enemy_ClamChowder_Group_Five_prefab;
	private GameObject enemy_ClamChowder_Group_Five_NoItem_prefab;
	private GameObject enemy_ClamChowder_Group_Seven_prefab;
	private GameObject enemy_ClamChowder_Group_Straight_prefab;
	private GameObject enemy_Beelzebub_Group_FourWide_prefab;
	private GameObject enemy_Beelzebub_Group_FourWide_Item_prefab;
	private GameObject enemy_BeetleGroup_prefab;
	private GameObject enemy_BeetleGroup_Three_prefab;
	private GameObject boundMeteors_prefab;
	private GameObject enemy_Bacula_Sixteen_prefab;
	private GameObject enemy_Bacula_FourOnly_prefab;
	private GameObject enemy_ClamChowder_FourTriangle_prefab;
	private GameObject enemy_ClamChowder_FourTriangle_NoItem_prefab;
	private GameObject enemy_Beelzebub_Group_EightNormal_Item_prefab;
	private GameObject enemy_ClamChowder_Group_TwelveStraight_prefab;
	private GameObject enemy_UFO_Group_Five_prefab;
	private GameObject enemy_Beetle_Group_Seven_prefab;
	private GameObject enemy_ClamChowder_Group_SevenStraight_prefab;
	private GameObject enemy_ClamChowder_Group_SixStraight_prefab;
	private GameObject enemy_ClamChowder_Group_UpSevenDiagonal_prefab;
	private GameObject enemy_ClamChowder_Group_DownSevenDiagonal_prefab;
	private GameObject enemy_ClamChowder_Group_TenStraight_prefab;
	private GameObject container_prefab;
    private GameObject containerMove_prefab;
	#endregion

	#region 敵のプーリング
	public Object_Pooling enemy_UFO_Group;
	public Object_Pooling enemy_UFO_Group_NoneShot;
	public Object_Pooling enemy_ClamChowder_Group_Two_Top;
	public Object_Pooling enemy_ClamChowder_Group_Two_Under;
	public Object_Pooling enemy_ClamChowder_Group_TwoWaveOnlyUp;
	public Object_Pooling enemy_ClamChowder_Group_TwoWaveOnlyDown;
	public Object_Pooling enemy_ClamChowder_Group_Three;
	public Object_Pooling enemy_ClamChowder_Group_Three_Item;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
	public Object_Pooling enemy_ClamChowder_Group_Four;
	public Object_Pooling enemy_ClamChowder_Group_Four_NoItem;
	public Object_Pooling enemy_ClamChowder_Group_Five;
	public Object_Pooling enemy_ClamChowder_Group_Five_NoItem;
	public Object_Pooling enemy_ClamChowder_Group_Seven;
	public Object_Pooling enemy_ClamChowder_Group_Straight;
	public Object_Pooling enemy_Beelzebub_Group_FourWide;
	public Object_Pooling enemy_Beelzebub_Group_FourWide_Item;
	public Object_Pooling enemy_BeetleGroup;
	public Object_Pooling enemy_BeetleGroup_Three;
	public Object_Pooling boundMeteors;
	public Object_Pooling enemy_Bacula_Sixteen;
	public Object_Pooling enemy_Bacula_FourOnly;
	public Object_Pooling enemy_ClamChowder_FourTriangle;
	public Object_Pooling enemy_ClamChowder_FourTriangle_NoItem;
	public Object_Pooling enemy_Beelzebub_Group_EightNormal_Item;
	public Object_Pooling enemy_ClamChowder_Group_TwelveStraight;
	public Object_Pooling enemy_UFO_Group_Five;
	public Object_Pooling enemy_Beetle_Group_Seven;
	public Object_Pooling enemy_ClamChowder_Group_SevenStraight;
	public Object_Pooling enemy_ClamChowder_Group_SixStraight;
	public Object_Pooling enemy_ClamChowder_Group_UpSevenDiagonal;
	public Object_Pooling enemy_ClamChowder_Group_DownSevenDiagonal;
	public Object_Pooling enemy_ClamChowder_Group_TenStraight;
	public Object_Pooling Container;
    public Object_Pooling Container_Move;
	//public Object_Pooling enemy_Octopas
	#endregion
	//----------------------------------------------------------

	private void Awake()
	{
		if (Obj_Storage.Storage_Data == null)
		{
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stage_01")
			{
				Obj_Storage.Storage_Data = GetComponent<Obj_Storage>();

				//--------------------------------------------11.25 陳　追加　-----------------------------------------
				UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
				//-----------------------------------------------------------------------------------------------------

				DontDestroyOnLoad(gameObject);
			}
		}
		else
		{
			//Destroy(gameObject);
			//UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;

		}
	}

	//-----------------------------------------------11.25 陳　追加　--------------------------------------------------------------
	//core part of  The Stage change
	private void OnSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
	{
		if (to.name == "Title")
		{
			if(Player != null)
			{
				DeleteOnceGos();
			}
			GetComponent<Game_Master>().ResetScore();
		}
		if( to.name == "Stage_01")
		{
			if (Player == null)
			{
				CreateOnceGos();
			}
			//------------------------------------11.26 陳　追加---------------------------------
			if (!Player.Get_Obj()[0].activeSelf)
			{
				GetComponent<MapCreate>().CreateMap();
			}
			GetComponent<ObjectStorage_Control>().EnemyCreate_Data = GameObject.Find("CreateEnemy").GetComponent<EnemyCreate>();
		}

		if (to.name.Contains("Stage"))
		{
			GetComponent<Game_Master>().Stage_Start();
			CreateSceneChangeGos();
		}

		if(to.name == "GameOver")
		{
			DeleteOnceGos();
		}
		if(to.name == "GameClear")
		{
			DeleteOnceGos();
		}
		if(to.name == "End_roll")
		{
			DeleteOnceGos();
		}
	}

	private void CreateOnceGos()
	{
		Player_Prefab = Resources.Load("Player/Player") as GameObject;
		player_2_Prefab = Resources.Load("Player/Player2") as GameObject;
		Option_Prefab = Resources.Load("Option/Option") as GameObject;       //マルチプルのロード
		Player = new Object_Pooling(Player_Prefab, 1, "Player");                        //プレイヤー生成
		Player_2 = new Object_Pooling(player_2_Prefab, 1, "Player_2");                  //プレイヤー2生成
		Option = new Object_Pooling(Option_Prefab, 4, "Option");

		DontDestroyOnLoad(Player.Get_Parent_Obj());
		DontDestroyOnLoad(Player_2.Get_Parent_Obj());
		DontDestroyOnLoad(Option.Get_Parent_Obj());
	}

	private void CreateSceneChangeGos()
	{
		#region Playerの弾等必須事項
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet_1P") as GameObject;
		BulletPrefab_P2 = Resources.Load("Bullet/Player_Bullet_2P") as GameObject;
		BulletPrefab_Option_P1 = Resources.Load("Bullet/Option_Bullet_1P") as GameObject;
		BulletPrefab_Option_P2 = Resources.Load("Bullet/Option_Bullet_2P") as GameObject;
		Player_Missile1 = Resources.Load("Bullet/Player_Missile") as GameObject;
		Player_Missile2 = Resources.Load("Bullet/Player_Missile2") as GameObject;
		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;

		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 17, "Player1_Bullet");         //プレイヤーのバレットを生成
		Player2Bullet = new Object_Pooling(BulletPrefab_P2, 17, "Player2_Bullet");
		P1_OptionBullet = new Object_Pooling(BulletPrefab_Option_P1, 84, "Option_Bullet_1P");
		P2_OptionBullet = new Object_Pooling(BulletPrefab_Option_P2, 84, "Option_Bullet_2P");
		PlayerMissile = new Object_Pooling(Player_Missile1, 15, "Player_Missile");        //プレイヤーのミサイルの生成
		PlayerMissile2 = new Object_Pooling(Player_Missile2, 15, "Player_Missile2");      //プレイヤー２のミサイルの生成
		PowerUP_Item = new Object_Pooling(Item_Prefab, 19, "PowerUP_Item");

		#endregion
		switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
		{
			case "Stage_01":
				#region 現在のステージの名前

				#endregion

				#region ステージ１
				Boss1_Prefab = Resources.Load("Boss/BigCoreMk2") as GameObject;
				Boss2_Prefab = Resources.Load("Boss/bick_core_mk3") as GameObject;
				Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
				Bullet_Prefab_BattleShip = Resources.Load("Bullet/CannonBullet") as GameObject;
				Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
				SmallBeam_Bullet_E_Prefab = Resources.Load("Bullet/SmallBeam_Bullet") as GameObject;
				UfoType_Enemy_Prefab = Resources.Load("Enemy/Enemy_UFO") as GameObject;
				ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject;
				BeelzebubType_Enemy_Prefab = Resources.Load("Enemy/BeelzebubType_Enemy") as GameObject;
				BattleShip_Enemy_Prefab = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
				Star_Fish_Enemy_Prefab = Resources.Load("Enemy/Enemy_hitode_type") as GameObject;       //ヒトデ型の敵のロード

				Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;     //中ボス
				Laser_Line_Prefab = Resources.Load("Bullet/LaserLine") as GameObject;
				One_Boss_BousndBullet_Prefab = Resources.Load("Bullet/One_Boss_BousndBullet") as GameObject;

				Moai_Prefab = Resources.Load("Boss/Enemy_Moai") as GameObject;          //モアイのロード
				Moai_Mini_Group_Prefab = Resources.Load("Enemy/Enemy_Moai_MiniGroup") as GameObject;          //モアイの群れのロード
				Moai_Bullet_Prefab = Resources.Load("Bullet/Enemy_RingBullet") as GameObject;          //モアイの弾ロード

				#region エフェクトのロード
				Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Explosion/E001_1P");    //プレイヤー爆発
				Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Attachment/A000");      //プレイヤー登場時に使用するジェット噴射
				Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Attachment/A002");      //プレイヤーのマズルファイア
				Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Attachment/A006");      //オプション回収用
				Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Explosion/E100");       //敵キャラの爆発エフェクト
				Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Explosion/E201");       //敵キャラコアシールドの破壊エフェクト
				Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Attachment/A003");      //プレイヤーパワーアップエフェクト
				Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Explosion/E104");       //中ボス爆発
				Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Explosion/E001");       //バグが起きないようにプレイヤーの爆発を仮置き
				Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Explosion/E011");      //使ってない（仮にはいってるだけ）
				Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Explosion/E103");      //戦艦型の爆発
				Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Explosion/E200_1P");      //プレイヤーの弾の着弾時のエフェクト
				Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Other/O001");          //ボス登場時のエフェクト
				Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Explosion/E206");      //隕石の爆発Effect
				Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Other/O005");      //ヒトデ型の出現用
				Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Attachment/A003_2P");      //2Pパワーアップエフェクト
				Effects_Prefab[16] = Resources.Load<GameObject>("Effects/Explosion/E011");      //ミサイルの爆発
				#endregion

				#region SEのロード
				audio_se[0] = Resources.Load<AudioClip>("Sound/Teacher_SE/bacura_hit");             //バキュラに当たった時の高い音用
				audio_se[1] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //隕石の爆発音
				audio_se[2] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_BulletMode_Change");   //ラピッドとバーストの切り替え
				audio_se[3] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_Scream");               //叫ぶ声
				audio_se[4] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Bullet");       //プレイヤーバレット音
				audio_se[5] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_cupcel5 t");     //アイテム取得音(バー移動)
				audio_se[6] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei");   //アイテム使用パワーアップ音(ステータス変化)
				audio_se[7] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Bullet_Hit");         //コアシールドヒット音
				audio_se[8] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //敵の爆発音
				audio_se[9] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Moderate");  //ボスの爆発音
				audio_se[10] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_OptionCatch2");           //ドロップしたオプションの回収
				audio_se[11] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_5(Moyai)");           //モアイの爆発
				audio_se[12] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //ダブルの声使ってない
				audio_se[13] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //リップルレーザーの声使ってない
				audio_se[14] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //オプションの声使ってない
				audio_se[15] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //フォースフィールド（シールド）使ってない
				audio_se[16] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei_neo");          //パワーアップの音(使用しない)
				audio_se[17] = Resources.Load<AudioClip>("Sound/Teacher_SE/gradius_SE_Player_Laser");   //レーザーの発射音
				audio_se[18] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_1(Small)");     //小型爆発
				audio_se[19] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_2(senkan)");    //戦艦タイプの爆発音
				audio_se[20] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Self_destruction");      //プレイヤーの死亡時の音
				audio_se[21] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_Flight");         //プレイヤー登場の音
				audio_se[22] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_3(ModerateBoss)"); //中ボス用の爆発音
				audio_se[23] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display");         //無線受信時
				audio_se[24] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Parmanent");   //無線のボイスの裏で流すよう
				audio_se[25] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Close");   //無線終了時
				audio_se[26] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_No_Hit");               //ボスのボディにあたった
				audio_se[27] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_CoreShield_destruction");    //ボスのコアシールドを壊したとき
				audio_se[28] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_Core_Hit");             //ボスのコアに当たった時
				#endregion

				#region ボイスのロード
				PowerUpVoice[0] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_13");           //アイテム使用時のボイス（スピードアップ）
				PowerUpVoice[1] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_14");           //アイテム使用時のボイス（ミサイル）
				PowerUpVoice[2] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_15");           //アイテム使用時のボイス（ダブル）
				PowerUpVoice[3] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_16");           //アイテム使用時のボイス（レーザー）
				PowerUpVoice[4] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_SE_Option_Multiple");     //アイテム使用時のボイス（オプション）
				PowerUpVoice[5] = Resources.Load<AudioClip>("Sound/VOICE/gradius_SE_PowerUp_Shield");       //アイテム使用時のボイス（フォースフィールド）
				PowerUpVoice[6] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");               //アイテム使用時のボイス（マックススピード）
				PowerUpVoice[7] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20_initial");       //アイテム使用時のボイス（イニットスピード）

				First_Wireless = new AudioClip[1];
				Second_Wireless = new AudioClip[1];
				Third_Wireless = new AudioClip[2];
				Forth_Wireless = new AudioClip[2];
				Fifth_Wireless = new AudioClip[2];
				Sixth_Wireless = new AudioClip[2];
				Seventh_Wireless = new AudioClip[2];

				First_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_001");        //開戦時
				Second_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_002");        //前半ボス前
				Third_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_015");        //前半ボス後1
				Third_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_016");        //前半ぼす後2
				Forth_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_013");           //モアイ1
				Forth_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_014");           //モアイ2
				Fifth_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_015");           //モアイ後１
				Fifth_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_016");           //モアイ後２
				Sixth_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_005");        //後半ボス前1
				Sixth_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_006");        //後半ボス前2
				Seventh_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_007");        //後半ボス後1
				Seventh_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_025");        //後半ボス後2
				#endregion

				enemy_UFO_Group_prefab = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
				enemy_UFO_Group_NoneShot_prefab = Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject;
				enemy_ClamChowder_Group_Two_Top_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
				enemy_ClamChowder_Group_Two_Under_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
				enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUP") as GameObject;
				enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
				enemy_ClamChowder_Group_Three_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three") as GameObject;
				enemy_ClamChowder_Group_Three_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
				enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
				enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
				enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
				enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
				enemy_ClamChowder_Group_Four_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
				enemy_ClamChowder_Group_Four_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
				enemy_ClamChowder_Group_Five_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five") as GameObject;
				enemy_ClamChowder_Group_Five_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
				enemy_ClamChowder_Group_Seven_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
				enemy_ClamChowder_Group_Straight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
				//enemy_MiddleBoss_Father_prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
				enemy_Beelzebub_Group_FourWide_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide") as GameObject;
				enemy_Beelzebub_Group_FourWide_Item_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide_Item") as GameObject;
				enemy_BeetleGroup_prefab = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
				enemy_BeetleGroup_Three_prefab = Resources.Load("Enemy/Enemy_Beetle_Group_Three") as GameObject;
				boundMeteors_prefab = Resources.Load("Enemy/BoundMeteors") as GameObject;
				enemy_Bacula_Sixteen_prefab = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
				enemy_Bacula_FourOnly_prefab = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;
				//9月13日追加
				enemy_ClamChowder_FourTriangle_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle") as GameObject;
				enemy_ClamChowder_FourTriangle_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_NoItem") as GameObject;
				enemy_Beelzebub_Group_EightNormal_Item_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_EightNormal_Item") as GameObject;
				//enemy_ClamChowder_Group_TwelveStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwelveStraight") as GameObject;
				enemy_UFO_Group_Five_prefab = Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject;
				enemy_Beetle_Group_Seven_prefab = Resources.Load("Enemy/Enemy_Beetle_Group_Seven") as GameObject;
				enemy_ClamChowder_Group_SevenStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_SevenStraight") as GameObject;
				enemy_ClamChowder_Group_SixStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_SixStraight") as GameObject;
				enemy_ClamChowder_Group_UpSevenDiagonal_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_UpSevenDiagonal") as GameObject;
				enemy_ClamChowder_Group_DownSevenDiagonal_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_DownSevenDiagonal") as GameObject;
				enemy_ClamChowder_Group_TenStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TenStraight") as GameObject;

				#region エフェクトのプーリング化
				Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");                  //プレイヤーの爆発
				Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");       //プレイヤーが登場するときのジェット噴射
				Effects[2] = new Object_Pooling(Effects_Prefab[2], 1, "Player_Fire");                       //プレイヤーのマズルフラッシュ(使用していない)
				Effects[3] = new Object_Pooling(Effects_Prefab[3], 1, "Player_Bullet");                     //プレイヤーの弾（使用してない）
				Effects[4] = new Object_Pooling(Effects_Prefab[4], 29, "Enemy_explosion");                   //エネミーの死亡時の爆発
				Effects[5] = new Object_Pooling(Effects_Prefab[5], 1, "Enemy_Core_Sheld_explosion");        //エネミーの中ボス以上のコアシールドの爆発エフェクト
				Effects[6] = new Object_Pooling(Effects_Prefab[6], 12, "Player_PowerUP");                    //プレイヤーのパワーアップ時のエフェクト
				Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Boss_explosion");                    //ボス死亡時のエフェクト
				Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Player_PowerUP_Bullet");             //プレイヤーのパワーアップした弾（使用してない）
				Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Enemy_Grain");                       //敵の粒子
				Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Battleship_explosion");            //戦艦の爆発
				Effects[11] = new Object_Pooling(Effects_Prefab[11], 87, "Player_Bullet_impact");            //プレイヤーの弾の着弾時のエフェクト
				Effects[12] = new Object_Pooling(Effects_Prefab[12], 1, "Boss_Appearance");                 //ボス登場時のエフェクト
				Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Meteor_explosion");                    //隕石爆発Effect
				Effects[14] = new Object_Pooling(Effects_Prefab[14], 3, "Boss_Bullet2");                    //ボスの弾その２
				Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "P2_Powerup");                    //2Pパワーアップエフェクト
				Effects[16] = new Object_Pooling(Effects_Prefab[16], 15, "Missile_explosion");       // ミサイルの爆発
				#endregion

				Boss_1 = new Object_Pooling(Boss1_Prefab, 1, "One_Boss");                              //ステージ1のボス生成
				Boss_2 = new Object_Pooling(Boss2_Prefab, 1, "Two_Boss");                               //ステージ2のボス生成
				EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 88, "Enemy_Bullet");          //エネミーのバレットを生成
				Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 12, "Enemy_Beam_Bullet");      // エネミーのビーム型バレットを生成
				SmallBeam_Bullet_E = new Object_Pooling(SmallBeam_Bullet_E_Prefab, 42, "Enemy_SmallBeam_Bullet");      // エネミーの小さいビーム型バレットを生成
				BattleShipBullet = new Object_Pooling(Bullet_Prefab_BattleShip, 20, "BattleShip_Enemy_Bullet"); //戦艦タイプのバレットの生成
				UfoType_Enemy = new Object_Pooling(UfoType_Enemy_Prefab, 1, "UfoType_Enemy");       // UFO型エネミーを生成
				ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");       // 貝型エネミーを生成
                //ClamChowderType_Enemy_Item = new Object_Pooling(ClamChowderType_Enemy_Item_Prefab, 1, "ClamChowderType_Enemy_Item");       // 貝型エネミーを生成
                BeelzebubType_Enemy = new Object_Pooling(BeelzebubType_Enemy_Prefab, 1, "BeelzebubType_Enemy");      //	 ハエ型エネミーを生成
				BattleShipType_Enemy = new Object_Pooling(BattleShip_Enemy_Prefab, 4, "BattleshipType_Enemy");          //戦艦型のエネミーを生成
				StarFish_Enemy = new Object_Pooling(Star_Fish_Enemy_Prefab, 20, "Star_Fish_Enemy");             //ヒトデ型エネミーを生成

				Boss_Middle = new Object_Pooling(Boss_Middle_Prefab, 1, "Middle_Boss");
				Laser_Line = new Object_Pooling(Laser_Line_Prefab, 30, "Laser_Line");
				One_Boss_BousndBullet = new Object_Pooling(One_Boss_BousndBullet_Prefab, 156, "One_Boss_BousndBullet");

				Moai = new Object_Pooling(Moai_Prefab, 1, "Moai");
				Moai_Mini_Group = new Object_Pooling(Moai_Mini_Group_Prefab, 21, "Moai_Mini_Group");
				Moai_Bullet = new Object_Pooling(Moai_Bullet_Prefab, 15, "Moai_Prefab");

				//敵キャラのプーリング化-------------------------------------------------------------------------------
				enemy_UFO_Group = new Object_Pooling(enemy_UFO_Group_prefab, 10, "enemy_UFO_Group");
				enemy_UFO_Group_NoneShot = new Object_Pooling(enemy_UFO_Group_NoneShot_prefab, 4, "enemy_UFO_Group_NoneShot");
				enemy_ClamChowder_Group_Two_Top = new Object_Pooling(enemy_ClamChowder_Group_Two_Top_prefab, 2, "enemy_ClamChowder_Group_Two_Top");
				enemy_ClamChowder_Group_Two_Under = new Object_Pooling(enemy_ClamChowder_Group_Two_Under_prefab, 2, "enemy_ClamChowder_Group_Two_Under");
				enemy_ClamChowder_Group_TwoWaveOnlyUp = new Object_Pooling(enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab, 3, "enemy_ClamChowder_Group_TwoWaveOnlyUp");
				enemy_ClamChowder_Group_TwoWaveOnlyDown = new Object_Pooling(enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab, 3, "enemy_ClamChowder_Group_TwoWaveOnlyDown");
				//enemy_MiddleBoss_Father = new Object_Pooling(enemy_MiddleBoss_Father_prefab, 1, "enemy_MiddleBoss_Father");
				enemy_ClamChowder_Group_Three = new Object_Pooling(enemy_ClamChowder_Group_Three_prefab, 1, "enemy_ClamChowder_Group_Three");
				enemy_ClamChowder_Group_Three_Item = new Object_Pooling(enemy_ClamChowder_Group_Three_Item_prefab, 2, "enemy_ClamChowder_Group_Three_Item");
				enemy_ClamChowder_Group_ThreeWaveOnlyUp = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp");
				enemy_ClamChowder_Group_ThreeWaveOnlyDown = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown");
				enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item");
				enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item");
				enemy_ClamChowder_Group_Four = new Object_Pooling(enemy_ClamChowder_Group_Four_prefab, 1, "enemy_ClamChowder_Group_Four");
				enemy_ClamChowder_Group_Four_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Four_NoItem_prefab, 1, "enemy_ClamChowder_Group_Four_NoItem");
				enemy_ClamChowder_Group_Five = new Object_Pooling(enemy_ClamChowder_Group_Five_prefab, 1, "enemy_ClamChowder_Group_Five");
				enemy_ClamChowder_Group_Five_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Five_NoItem_prefab, 3, "enemy_ClamChowder_Group_Five_NoItem");
				enemy_ClamChowder_Group_Seven = new Object_Pooling(enemy_ClamChowder_Group_Seven_prefab, 2, "enemy_ClamChowder_Group_Seven");
				enemy_ClamChowder_Group_Straight = new Object_Pooling(enemy_ClamChowder_Group_Straight_prefab, 1, "enemy_ClamChowder_Group_Straight");
				enemy_Beelzebub_Group_FourWide = new Object_Pooling(enemy_Beelzebub_Group_FourWide_prefab, 2, "enemy_Beelzebub_Group_FourWide");
				enemy_Beelzebub_Group_FourWide_Item = new Object_Pooling(enemy_Beelzebub_Group_FourWide_Item_prefab, 2, "enemy_Beelzebub_Group_FourWide_Item");
				enemy_BeetleGroup = new Object_Pooling(enemy_BeetleGroup_prefab, 1, "enemy_BeetleGroup");
				enemy_BeetleGroup_Three = new Object_Pooling(enemy_BeetleGroup_Three_prefab, 1, "enemy_BeetleGroup_Three");
				boundMeteors = new Object_Pooling(boundMeteors_prefab, 3, "boundMeteors");
				enemy_Bacula_Sixteen = new Object_Pooling(enemy_Bacula_Sixteen_prefab, 1, "enemy_Bacula_Sixteen");
				enemy_Bacula_FourOnly = new Object_Pooling(enemy_Bacula_FourOnly_prefab, 1, "enemy_Bacula_FourOnly");
				//9月13日追加
				enemy_ClamChowder_FourTriangle = new Object_Pooling(enemy_ClamChowder_FourTriangle_prefab, 2, "enemy_ClamChowder_FourTriangle");
				enemy_ClamChowder_FourTriangle_NoItem = new Object_Pooling(enemy_ClamChowder_FourTriangle_NoItem_prefab, 2, "enemy_ClamChowder_FourTriangle_NoItem");
				enemy_Beelzebub_Group_EightNormal_Item = new Object_Pooling(enemy_Beelzebub_Group_EightNormal_Item_prefab, 1, "enemy_Beelzebub_Group_EightNormal_Item");
				enemy_UFO_Group_Five = new Object_Pooling(enemy_UFO_Group_Five_prefab, 2, "enemy_UFO_Group_Five");
				enemy_Beetle_Group_Seven = new Object_Pooling(enemy_Beetle_Group_Seven_prefab, 1, "enemy_Beetle_Group_Seven");
				enemy_ClamChowder_Group_SevenStraight = new Object_Pooling(enemy_ClamChowder_Group_SevenStraight_prefab, 2, "enemy_ClamChowder_Group_SevenStraight");
				enemy_ClamChowder_Group_SixStraight = new Object_Pooling(enemy_ClamChowder_Group_SixStraight_prefab, 2, "enemy_ClamChowder_Group_SixStraight");
				enemy_ClamChowder_Group_UpSevenDiagonal = new Object_Pooling(enemy_ClamChowder_Group_UpSevenDiagonal_prefab, 2, "Enemy_ClamChowder_Group_UpSevenDiagonal");
				enemy_ClamChowder_Group_DownSevenDiagonal = new Object_Pooling(enemy_ClamChowder_Group_DownSevenDiagonal_prefab, 2, "Enemy_ClamChowder_Group_DownSevenDiagonal");
				enemy_ClamChowder_Group_TenStraight = new Object_Pooling(enemy_ClamChowder_Group_TenStraight_prefab, 6, "Enemy_ClamChowder_Group_TenStraight");

				#endregion
				break;

			case "Stage_02":
				#region ステージ２
				#region レギュラーのバレット系
				Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
				Beam_Bullet_E_Prefab = Resources.Load("Bullet/Bullet_ReflectLaser") as GameObject;		//マンタ型が死んだときに出すレーザー型の弾

				EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 10, "Enemy_Bullet");          //エネミーのバレットを生成
				Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 36, "Enemy_Beam_Bullet");      // マンタ型が死んだときに出すレーザー型の弾

                //3-18陳追加
                //これが無いとステージ2ではレーザーがバグる
                Laser_Line_Prefab = Resources.Load("Bullet/LaserLine") as GameObject;
                Laser_Line = new Object_Pooling(Laser_Line_Prefab, 30, "Laser_Line");
				#endregion

				#region エフェクトのロード
				Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Explosion/E001_1P");    //プレイヤー爆発
				Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Attachment/A000");      //プレイヤー登場時に使用するジェット噴射
				Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Attachment/A002");      //プレイヤーのマズルファイア
				Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Attachment/A006");      //オプション回収用
				Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Explosion/E100");       //敵キャラの爆発エフェクト
				Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Explosion/E201");       //敵キャラコアシールドの破壊エフェクト
				Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Attachment/A003");      //プレイヤーパワーアップエフェクト
				Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Explosion/E104");       //中ボス爆発
				Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Explosion/E001");       //バグが起きないようにプレイヤーの爆発を仮置き
				Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Explosion/E011");      //使ってない（仮にはいってるだけ）
				Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Explosion/E103");      //戦艦型の爆発
				Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Explosion/E200_1P");      //プレイヤーの弾の着弾時のエフェクト
				Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Other/O001");          //ボス登場時のエフェクト
				Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Explosion/E206");      //隕石の爆発Effect
				Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Other/O005");      //ヒトデ型の出現用
				Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Attachment/A003_2P");      //2Pパワーアップエフェクト
				Effects_Prefab[16] = Resources.Load<GameObject>("Effects/Explosion/E011");      //ミサイルの爆発
				#endregion

				#region SEのロード
				audio_se[0] = Resources.Load<AudioClip>("Sound/Teacher_SE/bacura_hit");             //バキュラに当たった時の高い音用
				audio_se[1] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //隕石の爆発音
				audio_se[2] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_BulletMode_Change");   //ラピッドとバーストの切り替え
				audio_se[3] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_Scream");               //叫ぶ声
				audio_se[4] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Bullet");       //プレイヤーバレット音
				audio_se[5] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_cupcel5 t");     //アイテム取得音(バー移動)
				audio_se[6] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei");   //アイテム使用パワーアップ音(ステータス変化)
				audio_se[7] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Bullet_Hit");         //コアシールドヒット音
				audio_se[8] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //敵の爆発音
				audio_se[9] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Moderate");  //ボスの爆発音
				audio_se[10] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_OptionCatch2");           //ドロップしたオプションの回収
				audio_se[11] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_5(Moyai)");           //モアイの爆発
				audio_se[12] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //ダブルの声使ってない
				audio_se[13] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //リップルレーザーの声使ってない
				audio_se[14] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //オプションの声使ってない
				audio_se[15] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //フォースフィールド（シールド）使ってない
				audio_se[16] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei_neo");          //パワーアップの音(使用しない)
				audio_se[17] = Resources.Load<AudioClip>("Sound/Teacher_SE/gradius_SE_Player_Laser");   //レーザーの発射音
				audio_se[18] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_1(Small)");     //小型爆発
				audio_se[19] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_2(senkan)");    //戦艦タイプの爆発音
				audio_se[20] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Self_destruction");      //プレイヤーの死亡時の音
				audio_se[21] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_Flight");         //プレイヤー登場の音
				audio_se[22] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_3(ModerateBoss)"); //中ボス用の爆発音
				audio_se[23] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display");         //無線受信時
				audio_se[24] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Parmanent");   //無線のボイスの裏で流すよう
				audio_se[25] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Close");   //無線終了時
				audio_se[26] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_No_Hit");               //ボスのボディにあたった
				audio_se[27] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_CoreShield_destruction");    //ボスのコアシールドを壊したとき
				audio_se[28] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_Core_Hit");             //ボスのコアに当たった時
				#endregion

				#region ボイスのロード
				PowerUpVoice[0] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_13");           //アイテム使用時のボイス（スピードアップ）
				PowerUpVoice[1] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_14");           //アイテム使用時のボイス（ミサイル）
				PowerUpVoice[2] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_15");           //アイテム使用時のボイス（ダブル）
				PowerUpVoice[3] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_16");           //アイテム使用時のボイス（レーザー）
				PowerUpVoice[4] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_SE_Option_Multiple");     //アイテム使用時のボイス（オプション）
				PowerUpVoice[5] = Resources.Load<AudioClip>("Sound/VOICE/gradius_SE_PowerUp_Shield");       //アイテム使用時のボイス（フォースフィールド）
				PowerUpVoice[6] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");               //アイテム使用時のボイス（マックススピード）
				PowerUpVoice[7] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20_initial");       //アイテム使用時のボイス（イニットスピード）

				First_Wireless = new AudioClip[1];
				Second_Wireless = new AudioClip[1];
				Third_Wireless = new AudioClip[2];
				Forth_Wireless = new AudioClip[1];
				Fifth_Wireless = new AudioClip[2];

				First_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_026");        //開戦時
				Second_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_027");        //研究所はいる前
				Third_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_028");        //脳みそ戦１
				Third_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_028.2");        //脳みそ戦２
				Forth_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_030");           //脳みそ戦後１
				Fifth_Wireless[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_031");           //ラスト脳みそ
				Fifth_Wireless[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_032");        //ラスト脳みそ後1

				#endregion

				#region エフェクトのプーリング化
				Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");                  //プレイヤーの爆発
				Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");       //プレイヤーが登場するときのジェット噴射
				Effects[2] = new Object_Pooling(Effects_Prefab[2], 1, "Player_Fire");                       //プレイヤーのマズルフラッシュ
				Effects[3] = new Object_Pooling(Effects_Prefab[3], 1, "Player_Bullet");                     //プレイヤーの弾（使用してない）
				Effects[4] = new Object_Pooling(Effects_Prefab[4], 1, "Enemy_explosion");                   //エネミーの死亡時の爆発
				Effects[5] = new Object_Pooling(Effects_Prefab[5], 1, "Enemy_Core_Sheld_explosion");        //エネミーの中ボス以上のコアシールドの爆発エフェクト
				Effects[6] = new Object_Pooling(Effects_Prefab[6], 1, "Player_PowerUP");                    //プレイヤーのパワーアップ時のエフェクト
				Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Boss_explosion");                    //ボス死亡時のエフェクト
				Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Player_PowerUP_Bullet");             //プレイヤーのパワーアップした弾（使用してない）
				Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Enemy_Grain");                       //敵の粒子
				Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Battleship_explosion");            //戦艦の爆発
				Effects[11] = new Object_Pooling(Effects_Prefab[11], 1, "Player_Bullet_impact");            //プレイヤーの弾の着弾時のエフェクト
				Effects[12] = new Object_Pooling(Effects_Prefab[12], 1, "Boss_Appearance");                 //ボス登場時のエフェクト
				Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Meteor_explosion");                    //隕石爆発Effect
				Effects[14] = new Object_Pooling(Effects_Prefab[14], 1, "Boss_Bullet2");                    //ボスの弾その２
				Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "P2_Powerup");                    //2Pパワーアップエフェクト
				Effects[16] = new Object_Pooling(Effects_Prefab[16], 1, "Missile_explosion");       // ミサイルの爆発
				#endregion

				#region ステージ2個別ロード
				container_prefab = Resources.Load("Enemy/Container") as GameObject;     //コンテナ
                containerMove_prefab = Resources.Load("Enemy2/Container_Move") as GameObject;     //コンテナ
                Bullet_Prefab_BattleShip = Resources.Load("Bullet/CannonBullet") as GameObject;	//敵の弾
				Discharge_Prefab = Resources.Load("Enemy2/Enemy_Discharge") as GameObject;		//敵を排出する敵
				Discharged_Prefab = Resources.Load("Enemy2/Enemy_Discharged") as GameObject;    //↑が出す敵
				StagBeetle_Prefab = Resources.Load("Enemy2/Enemy_StagBeetle") as GameObject;	//オプションハンター
				FollowGround_Prefab = Resources.Load("Enemy2/Enemy_FollowGround") as GameObject;	//地形に沿って進む敵
				Cannon_Prefab = Resources.Load("Enemy2/Enemy_Taiho") as GameObject;         //壁についている敵
                Cannon_Item_Prefab = Resources.Load("Enemy2/Enemy_Taiho_Item") as GameObject;         //壁についている敵
                OctopusType_Enemy_Prefab = Resources.Load("Enemy2/OctopusType_Enemy") as GameObject;    //タコ型の敵
                ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject; //闘牛
                ClamChowderType_Enemy_Item_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy_Item") as GameObject; //闘牛アイテム
                Walk_Prefab = Resources.Load("Enemy2/Enemy_Walk") as GameObject;    //歩く敵
				Star_Fish_Enemy_Prefab = Resources.Load("Enemy/Enemy_hitode_type") as GameObject;       //ヒトデ型の敵のロード
                enemy_UFO_Group_NoneShot_prefab = Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject;


                #endregion

                #region ステージ2個別プーリング化
                Container = new Object_Pooling(container_prefab, 10, "container");       // アイテムと攻撃をだすコンテナ
                Container_Move = new Object_Pooling(containerMove_prefab, 20, "container_move");       // アイテムと攻撃をだすコンテナ
                BattleShipBullet = new Object_Pooling(Bullet_Prefab_BattleShip, 20, "BattleShip_Enemy_Bullet"); //戦艦タイプのバレットの生成
				Discharge_Enemy = new Object_Pooling(Discharge_Prefab, 10, "Discharge_Enemy");          //敵を生成する敵の生成
				Discharged_Enemy = new Object_Pooling(Discharged_Prefab, 20, "Discharged_Enemy");       //排出する敵が出す敵の生成
				FollowGround_Enemy = new Object_Pooling(FollowGround_Prefab, 30, "FollowGround_Enemy");
				StagBeetle_Enemy = new Object_Pooling(StagBeetle_Prefab, 10, "StagBeetle_Enemy");
				Cannon_Enemy = new Object_Pooling(Cannon_Prefab, 10, "Cannon_Enemy");
                Cannon_Enemy_Item = new Object_Pooling(Cannon_Item_Prefab, 4, "Cannon_Enemy_Item");
                OctopusType_Enemy = new Object_Pooling(OctopusType_Enemy_Prefab, 8, "OctopusType_Enemy");                               // タコ型エネミーを生成
                ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 6, "ClamChowderType_Enemy");       // 貝型エネミーを生成
                ClamChowderType_Enemy_Item = new Object_Pooling(ClamChowderType_Enemy_Item_Prefab, 4, "ClamChowderType_Enemy_Item");       // 貝型エネミーを生成
                Walk_Enemy = new Object_Pooling(Walk_Prefab, 6, "Walk_Enemy");
                StarFish_Enemy = new Object_Pooling(Star_Fish_Enemy_Prefab, 8, "Star_Fish_Enemy");             //ヒトデ型エネミーを生成
                enemy_UFO_Group_NoneShot = new Object_Pooling(enemy_UFO_Group_NoneShot_prefab, 4, "enemy_UFO_Group_NoneShot");

                #endregion


                #endregion
                break;

			default:
				break;
		}
	}
	

	private void DeleteOnceGos()
	{
		if (Player == null)
			return;

		Destroy(Player.Get_Parent_Obj());
		Destroy(Player_2.Get_Parent_Obj());
		Destroy(Option.Get_Parent_Obj());

		Player = null;
		Player_2 = null;
		Option = null;
	}
	//-----------------------------------------------11.25 陳　追加　--------------------------------------------------------------

	//Player１の取得
	public GameObject GetPlayer()
	{
		return Player.Get_Obj()[0];
	}
	//Player２の取得
	public GameObject GetPlayer2()
	{
		return Player_2.Get_Obj()[0];
	}
	//オプションの取得
	public GameObject GetOption()
	{
		return Option.Get_Obj()[0];
	}

	public GameObject GetMiddleBoss()
	{
		return Boss_Middle.Get_Obj()[0];
	}

	public GameObject GetBoss(int bossID)
	{
		GameObject boss = null;
		switch (bossID)
		{
			case 1:
				boss = Boss_1.Get_Obj()[0];
				break;
			case 2:
				boss = Boss_2.Get_Obj()[0];
				break;
			case 3:
				boss = Moai.Get_Obj()[0];
				break;
			default:
				Debug.Log("引数間違えてますねぇ.......");
				break;
		}
		return boss;
	}
}
