﻿/*
 * オブジェクトやステータス情報などを保管しておく場所
 * 久保田達己
 * 更新履歴
 * 2019/06/06	とりあえずの作成
 * 2019/07/03	SEの追加
 * 2019/07/12	VOICEの追加
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Obj_Storage : MonoBehaviour
{
	public static Obj_Storage Storage_Data;

	//マップ作製に使うプレハブ
	//リソースフォルダから取得するため、インスペクターは使わない
	private GameObject Enemy_Prefab;							//敵キャラのプレハブ
	private GameObject Medium_Enemy_Prefab;				//中型エネミーのプレハブ
	private GameObject Player_Prefab;                           //プレイヤーのプレハブ
	private GameObject player_2_Prefab;							//プレイヤー2のプレハブ
	private GameObject Player_Missile_Prefab;					//プレイヤーのミサイルプレハブ
	private GameObject Player_Missile_Tow_Way_Prefab;		//プレイヤーのミサイル（上下に行くやつ）
	private GameObject Boss1_Prefab;                                //ステージ１のボスのプレハブ
	private GameObject Boss2_Prefab;								//ステージ2のボスのプレハブ
	private GameObject Bullet_Prefab_P;							//弾のPrefab情報
    private GameObject BulletPrefab_P2;            //２P用の弾プレハブ情報
    private GameObject BulletPrefab_Option;         //オプション用の球プレハブ情報
	private GameObject Bullet_Prefab_E;							//エネミーの弾のPrefab情報
    private GameObject Bullet_Prefab_BattleShip;        // バトルシップタイプの弾のPrefab情報
    private GameObject Beam_Bullet_E_Prefab;					//エネミーのビーム型バレットのプレハブ
	private GameObject UfoType_Enemy_Prefab;				// UFO型エネミーのプレハブ
	private GameObject UfoType_Enemy_Item_Prefab;			// UFO型エネミー（アイテムドロップ）
	private GameObject UfoMotherType_Enemy_Prefab;		// UFO母艦型エネミーのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;	// 貝型エネミーのプレハブ
	private GameObject OctopusType_Enemy_Prefab;			// タコ型エネミーのプレハブ
	private GameObject BeelzebubType_Enemy_Prefab;		// ハエ型エネミーのプレハブ
    private GameObject BattleShip_Enemy_Prefab;     // 戦艦型エネミーのプレハブ
	private GameObject Option_Prefab;							//オプションのプレハブ
	private GameObject Item_Prefab;								//パワーアップのアイテムを入れえるための処理
	private GameObject[] Effects_Prefab = new GameObject[18];  //particleのプレハブ
	private GameObject Boss_Middle_Prefab;                      //中ボスのプレハブ
	private GameObject Laser_Line_Prefab;               // レーザーのプレハブ
	private GameObject One_Boss_Laser_Prefab;                   // ボスのレーザープレハブ
	private GameObject One_Boss_BousndBullet_Prefab;		// ボスのバウンド弾プレハブ
	//実際に作られたオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Medium_Size_Enemy1;
	public Object_Pooling Player;
	public Object_Pooling Player_2;
	public Object_Pooling Boss_1;
	public Object_Pooling Boss_2;
	public Object_Pooling PlayerBullet;
    public Object_Pooling Player2Bullet;
    public Object_Pooling OptionBullet;
	public Object_Pooling PlayerMissile;
	public Object_Pooling PlayerMissile_TowWay;
	public Object_Pooling EnemyBullet;
    public Object_Pooling BattleShipBullet;
	public Object_Pooling Beam_Bullet_E;
	public Object_Pooling UfoType_Enemy;
	public Object_Pooling UfoType_Item_Enemy;
	public Object_Pooling UfoMotherType_Enemy;
	public Object_Pooling ClamChowderType_Enemy;
	public Object_Pooling OctopusType_Enemy;
	public Object_Pooling BeelzebubType_Enemy;
    public Object_Pooling BattleShipType_Enemy;
	public Object_Pooling Option;
	public Object_Pooling PowerUP_Item;
	public Object_Pooling Boss_Middle;
	public Object_Pooling Laser_Line;
	public Object_Pooling One_Boss_Laser;
	public Object_Pooling One_Boss_BousndBullet;
	//effect関係-----------------------------------------------------
	public Object_Pooling[] Effects = new Object_Pooling[18];
	//マップの作製時に使う処理
	public Vector3 pos;                                        //マップを作成するときの位置情報取得用
	private string File_name = "E_Pattern";                     //csvファイルの名前
	private string File_name2 = "E_Pattern2";
	public List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int column;                                         //配列の列を入れる変数

	public AudioClip[] audio_se = new AudioClip[20];    //ＳＥを読み込むための配列
	public AudioClip[] audio_voice = new AudioClip[26]; //VOICEを読み込むための配列

	//仮データ置き場（のちにプーリング化を施す）-------------------------------------------------------------
	public GameObject enemy_UFO_Group_prefab;
	public GameObject enemy_ClamChowder_Group_Four_prefab;
	public GameObject enemy_ClamChowder_Group_Two_Top_prefab;
	public GameObject enemy_ClamChowder_Group_Two_Under_prefab;
	public GameObject enemy_ClamChowder_Group_Three_Item_prefab;
	public GameObject enemy_ClamChowder_Group_Seven_prefab;
	//public GameObject enemy_MiddleBoss_Father_prefab;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab;
	public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab;

	public Object_Pooling enemy_UFO_Group;
	public Object_Pooling enemy_ClamChowder_Group_Four;
	public Object_Pooling enemy_ClamChowder_Group_Two_Top;
	public Object_Pooling enemy_ClamChowder_Group_Two_Under;
	public Object_Pooling enemy_ClamChowder_Group_Three_Item;
	public Object_Pooling enemy_ClamChowder_Group_Seven;
	//public Object_Pooling enemy_MiddleBoss_Father;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
	public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;

	//----------------------------------------------------------
	private void Awake()
	{
		Storage_Data = GetComponent<Obj_Storage>();
	}

	void Start()
    {

		Player_Prefab = Resources.Load("Player/Player") as GameObject;
		player_2_Prefab = Resources.Load("Player/Player2") as GameObject;
		Enemy_Prefab = Resources.Load("Enemy/Enemy2") as GameObject;
		Medium_Enemy_Prefab = Resources.Load("Enemy/Medium_Size_Enemy") as GameObject;
		Boss1_Prefab = Resources.Load("Boss/BigCoreMk2") as GameObject;
		Boss2_Prefab = Resources.Load("Boss/Tow_Boss") as GameObject;
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet_2") as GameObject;
        BulletPrefab_P2 = Resources.Load("Bullet/Player_Bullet") as GameObject;
        BulletPrefab_Option = Resources.Load("Bullet/Option_Bullet") as GameObject;
        Player_Missile_Prefab = Resources.Load("Bullet/Player_Missile") as GameObject;
		Player_Missile_Tow_Way_Prefab = Resources.Load("Bullet/PlayerMissile_TowWay") as GameObject;
		Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
        Bullet_Prefab_BattleShip = Resources.Load("Bullet/GameObject") as GameObject;
        Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
		UfoType_Enemy_Prefab = Resources.Load("Enemy/Enemy_UFO") as GameObject;
		UfoType_Enemy_Item_Prefab = Resources.Load("Enemy/UfoType_Enemy_Item") as GameObject;
		UfoMotherType_Enemy_Prefab = Resources.Load("Enemy/UfoMotherType_Enemy") as GameObject; 
		ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject;
		OctopusType_Enemy_Prefab = Resources.Load("Enemy/OctopusType_Enemy") as GameObject; ;
		BeelzebubType_Enemy_Prefab = Resources.Load("Enemy/BeelzebubType_Enemy") as GameObject; ;
        BattleShip_Enemy_Prefab = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject; ;
        Option_Prefab = Resources.Load("Option/Option") as GameObject;		//オプションのロード
		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;        //アイテムのロード
		Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
		Laser_Line_Prefab = Resources.Load("Bullet/LaserLine") as GameObject;
		One_Boss_Laser_Prefab = Resources.Load("Bullet/One_Boss_LaserLine") as GameObject;
		One_Boss_BousndBullet_Prefab = Resources.Load("Bullet/One_Boss_BousndBullet") as GameObject;

		Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Explosion/E001");	//プレイヤー爆発
		Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Attachment/A000");		//プレイヤー登場時に使用するジェット噴射
		Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Attachment/A002");	//プレイヤーのマズルファイア
		Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Explosion/E001");  //バグが起きないようにプレイヤーの爆発を仮置き
		Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Explosion/E100");	//敵キャラの爆発エフェクト
		Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Explosion/E201");	//敵キャラコアシールドの破壊エフェクト
		Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Attachment/A003");	//プレイヤーパワーアップエフェクト
		Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Explosion/E104");	//中ボス爆発
		Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Explosion/E001");      //バグが起きないようにプレイヤーの爆発を仮置き
		Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Attachment/A110");			//敵の粒子
		Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Explosion/E103");		//戦艦型の爆発
		Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Explosion/E200");		//プレイヤーの弾の着弾時のエフェクト
		Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Other/O001");		//ボス登場時のエフェクト
		Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Explosion/E001");        //バグが起きないようにプレイヤーの爆発を仮置き
		Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Explosion/E001");        //バグが起きないようにプレイヤーの爆発を仮置き
		Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Explosion/E001");        //バグが起きないようにプレイヤーの爆発を仮置き

		audio_se[0] = Resources.Load<AudioClip>("Sound/SE/01_gradius_se_intro");
		audio_se[1] = Resources.Load<AudioClip>("Sound/SE/04_gradius_se_credit");
		audio_se[2] = Resources.Load<AudioClip>("Sound/SE/05_gradius_se_SelectMove");
		audio_se[3] = Resources.Load<AudioClip>("Sound/SE/06_gradius_se_Select_OK");
		audio_se[4] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Bullet");
		audio_se[5] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_cupcel5 t");	//アイテム取得音
		audio_se[6] = Resources.Load<AudioClip>("Sound/SE/09_gradius_se_zakoenemy_Destroyed");
		audio_se[7] = Resources.Load<AudioClip>("Sound/SE/10_gradius_se_Shot_Hit");
		audio_se[8] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");	//敵の爆発音
		audio_se[9] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Moderate");	//ボスの爆発音
		//装備セレクトで使用するもの------------------------------------------------------
		audio_se[10] = Resources.Load<AudioClip>("Sound/SE/13_gradius_se_SpeedUp");			//スピードアップの声
		audio_se[11] = Resources.Load<AudioClip>("Sound/SE/14_gradius_se_LASER");			//レーザー攻撃の声
		audio_se[12] = Resources.Load<AudioClip>("Sound/SE/15_gradius_se_Double");			//ダブルの声
		audio_se[13] = Resources.Load<AudioClip>("Sound/SE/16_gradius_se_LIPLE_LASER");		//リップルレーザーの声
		audio_se[14] = Resources.Load<AudioClip>("Sound/SE/17_gradius_se_OPTION");			//オプションの声
		audio_se[15] = Resources.Load<AudioClip>("Sound/SE/18_gradius_se_FORCE_FIELD");		//フォースフィールド（シールド）
		audio_se[16] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_powerup");			//
		audio_se[17] = Resources.Load<AudioClip>("Sound/Teacher_SE/gradius_SE_Player_Laser");	//レーザーの発射音
        audio_se[18] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_1(Small)"); //小型爆発
        audio_se[19] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_2(senkan)");//戦艦タイプの爆発音
        //------------------------------------------------------------------------------
        audio_voice[0] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_01");
		audio_voice[1] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_02");
		audio_voice[2] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_03");
		audio_voice[3] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_04");
		audio_voice[4] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_05");
		audio_voice[5] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_06");
		audio_voice[6] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_07");
		audio_voice[7] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_08");
		audio_voice[8] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_09");
		audio_voice[9] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_10");
		audio_voice[10] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_11");
		audio_voice[11] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_12");
		audio_voice[12] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_13");		//アイテム使用時のボイス（スピードアップ）
		audio_voice[13] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_14");		//アイテム使用時のボイス（ミサイル）
		audio_voice[14] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_15");		//アイテム使用時のボイス（ダブル）
		audio_voice[15] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_16");		//アイテム使用時のボイス（レーザー）
		audio_voice[16] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_17");		//アイテム使用時のボイス（オプション）
		audio_voice[17] = Resources.Load<AudioClip>("Sound/VOICE/gradius_SE_PowerUp_Shield");		//アイテム使用時のボイス（フォースフィールド）
		audio_voice[18] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");		//アイテム使用時のボイス（マックススピード）
		audio_voice[19] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20");		//アイテム使用時のボイス（イニットスピード）
		audio_voice[20] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_21");
		audio_voice[21] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_22");
		audio_voice[22] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_23");
		audio_voice[23] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_24");
		audio_voice[24] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_25");
		audio_voice[25] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_18_cut");
		//--------------------------------------------------------------------------------------------------------
		enemy_UFO_Group_prefab = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
		enemy_ClamChowder_Group_Four_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
		enemy_ClamChowder_Group_Two_Top_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
		enemy_ClamChowder_Group_Two_Under_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
		enemy_ClamChowder_Group_Three_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
		enemy_ClamChowder_Group_Seven_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
		//enemy_MiddleBoss_Father_prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
		enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
		//--------------------------------------------------------------------------------------------------------

		Player = new Object_Pooling(Player_Prefab, 1, "Player");                        //プレイヤー生成
		Player_2 = new Object_Pooling(player_2_Prefab, 1, "Player_2");					//プレイヤー2生成
		Enemy1 = new Object_Pooling(Enemy_Prefab, 10, "Enemy_Straight");                 //Enemy(直線のみ)の生成
		Boss_1 = new Object_Pooling(Boss1_Prefab, 1, "One_Boss");                              //ステージ1のボス生成
		Boss_2 = new Object_Pooling(Boss2_Prefab, 1, "Two_Boss");								//ステージ2のボス生成
		Medium_Size_Enemy1 = new Object_Pooling(Medium_Enemy_Prefab, 1, "Medium");
		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 5, "Player_Bullet");         //プレイヤーのバレットを生成
        Player2Bullet = new Object_Pooling(BulletPrefab_P2, 5, "Player2_Bullet");
        OptionBullet = new Object_Pooling(BulletPrefab_Option, 10, "Option_Bullet");
		PlayerMissile = new Object_Pooling(Player_Missile_Prefab, 20, "Player_Missile");        //プレイヤーのミサイルの生成
		PlayerMissile_TowWay = new Object_Pooling(Player_Missile_Tow_Way_Prefab, 20, "PlayerMissile_TowWay");
		EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 20, "Enemy_Bullet");          //エネミーのバレットを生成
		Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 20, "Enemy_Beam_Bullet");      // エネミーのビーム型バレットを生成
        BattleShipBullet = new Object_Pooling(Bullet_Prefab_BattleShip, 20, "BattleShip_Enemy_Bullet"); //戦艦タイプのバレットの生成
        UfoType_Enemy = new Object_Pooling(UfoType_Enemy_Prefab, 1, "UfoType_Enemy");       // UFO型エネミーを生成
		UfoType_Item_Enemy = new Object_Pooling(UfoType_Enemy_Item_Prefab, 5, "UfoType_Item_Enemy");	//UFO型のエネミーでアイテムを落とすやつを生成
		UfoMotherType_Enemy = new Object_Pooling(UfoMotherType_Enemy_Prefab, 1, "UfoMotherType_Enemy");         // UFO母艦型エネミーを生成
		ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");		// 貝型エネミーを生成
		OctopusType_Enemy = new Object_Pooling(OctopusType_Enemy_Prefab, 1, "OctopusType_Enemy");                               // タコ型エネミーを生成
		BeelzebubType_Enemy = new Object_Pooling(BeelzebubType_Enemy_Prefab, 1, "BeelzebubType_Enemy");      //	 ハエ型エネミーを生成
        BattleShipType_Enemy = new Object_Pooling(BattleShip_Enemy_Prefab, 4, "BattleshipType_Enemy");
        Option = new Object_Pooling(Option_Prefab, 4, "Option");
		PowerUP_Item = new Object_Pooling(Item_Prefab, 10, "PowerUP_Item");
		Boss_Middle = new Object_Pooling(Boss_Middle_Prefab, 1, "Middle_Boss");
		Laser_Line = new Object_Pooling(Laser_Line_Prefab, 30, "Laser_Line");
		One_Boss_Laser = new Object_Pooling(One_Boss_Laser_Prefab, 100, "One_Boss_Laser");
		One_Boss_BousndBullet = new Object_Pooling(One_Boss_BousndBullet_Prefab, 20, "One_Boss_BousndBullet");
		//effect---------------------------------------------------------------------------------------------
		Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");					//プレイヤーの爆発
		Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");		//プレイヤーが登場するときのジェット噴射
		Effects[2] = new Object_Pooling(Effects_Prefab[2], 2, "Player_Fire");						//プレイヤーのマズルフラッシュ
		Effects[3] = new Object_Pooling(Effects_Prefab[3], 1, "Player_Bullet");						//プレイヤーの弾（使用してない）
		Effects[4] = new Object_Pooling(Effects_Prefab[4], 5, "Enemy_explosion");					//エネミーの死亡時の爆発
		Effects[5] = new Object_Pooling(Effects_Prefab[5], 1, "Enemy_Core_Sheld_explosion");		//エネミーの中ボス以上のコアシールドの爆発エフェクト
		Effects[6] = new Object_Pooling(Effects_Prefab[6], 1, "Player_PowerUP");					//プレイヤーのパワーアップ時のエフェクト
		Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Boss_explosion");					//ボス死亡時のエフェクト
		Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Player_PowerUP_Bullet");				//プレイヤーのパワーアップした弾（使用してない）
		Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Enemy_Grain");						//敵の粒子
		Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Battleship_explosion");			//戦艦の爆発
		Effects[11] = new Object_Pooling(Effects_Prefab[11], 4, "Player_Bullet_impact");			//プレイヤーの弾の着弾時のエフェクト
		Effects[12] = new Object_Pooling(Effects_Prefab[12], 1, "Boss_Appearance");					//ボス登場時のエフェクト
		Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Boss_Bullet1");					//ボスの弾その１
		Effects[14] = new Object_Pooling(Effects_Prefab[14], 1, "Boss_Bullet2");					//ボスの弾その２
		Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "Boss_Bullet3");					//ボスの弾その3
		//---------------------------------------------------------------------------------------------------
		//敵キャラのプーリング化-------------------------------------------------------------------------------
		enemy_UFO_Group = new Object_Pooling(enemy_UFO_Group_prefab,1, "enemy_UFO_Group");
		enemy_ClamChowder_Group_Four = new Object_Pooling(enemy_ClamChowder_Group_Four_prefab, 1, "enemy_ClamChowder_Group_Four");
		enemy_ClamChowder_Group_Two_Top = new Object_Pooling(enemy_ClamChowder_Group_Two_Top_prefab, 1, "enemy_ClamChowder_Group_Two_Top");
		enemy_ClamChowder_Group_Two_Under = new Object_Pooling(enemy_ClamChowder_Group_Two_Under_prefab, 1, "enemy_ClamChowder_Group_Two_Under");
		enemy_ClamChowder_Group_Three_Item = new Object_Pooling(enemy_ClamChowder_Group_Three_Item_prefab, 1, "enemy_ClamChowder_Group_Three_Item");
		enemy_ClamChowder_Group_Seven = new Object_Pooling(enemy_ClamChowder_Group_Seven_prefab, 1, "enemy_ClamChowder_Group_Seven");
		//enemy_MiddleBoss_Father = new Object_Pooling(enemy_MiddleBoss_Father_prefab, 1, "enemy_MiddleBoss_Father");
		enemy_ClamChowder_Group_ThreeWaveOnlyUp = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp");
		enemy_ClamChowder_Group_ThreeWaveOnlyDown = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown");
		enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item");
		enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item");
		//-----------------------------------------------------------------------------------------------------
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			TextAsset Word = Resources.Load("CSV_Folder/" + File_name) as TextAsset;            //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);                                     //読み込んだデータをcsvの変数の中に格納
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();
				CsvData.Add(line.Split(','));               //カンマごとに割り振る
			}
		}
		else
		{
			TextAsset Word = Resources.Load("CSV_Folder/" + File_name2) as TextAsset;            //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);                                     //読み込んだデータをcsvの変数の中に格納
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();
				CsvData.Add(line.Split(','));               //カンマごとに割り振る
			}
		}
	}

	public GameObject GetPlayer()
	{
		return Player.Get_Obj()[0];
	}
	public GameObject GetPlayer2()
	{
		return Player_2.Get_Obj()[0];
	}

	public GameObject GetBoss(int bossID)
	{
		GameObject boss = null;
		switch(bossID)
		{
			case 1:
				boss = Boss_1.Get_Obj()[0];
				break;
			case 2:
				boss = Boss_2.Get_Obj()[0];
				break;
			default:
				Debug.Log("引数違いますよ");
				break;
		}
		return boss;
	}
}
