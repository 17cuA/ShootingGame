/*
 * オブジェクトやステータス情報などを保管しておく場所
 * 久保田達己
 * 更新履歴
 * 2019/06/06	とりあえずの作成
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
	private GameObject Enemy_Prefab;        //敵キャラのプレハブ
	private GameObject Medium_Enemy_Prefab;	//中型エネミーのプレハブ
	private GameObject Player_Prefab;       //プレイヤーのプレハブ
	private GameObject Player_Missile_Prefab;       //プレイヤーのミサイルプレハブ
	private GameObject Player_Missile_Tow_Way_Prefab;
	private GameObject Boss_Prefab;			//ボスのプレハブ
	private GameObject Bullet_Prefab_P;     //弾のPrefab情報
	private GameObject Bullet_Prefab_E;     //エネミーの弾のPrefab情報
	private GameObject Beam_Bullet_E_Prefab;        //エネミーのビーム型バレットのプレハブ
	private GameObject UfoType_Enemy_Prefab;        // UFO型エネミーのプレハブ
	private GameObject UfoType_Enemy_Item_Prefab;	// UFO型エネミー（アイテムドロップ）
	private GameObject UfoMotherType_Enemy_Prefab;      // UFO母艦型エネミーのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;        // 貝型エネミーのプレハブ
	private GameObject OctopusType_Enemy_Prefab;        // タコ型エネミーのプレハブ
	private GameObject BeelzebubType_Enemy_Prefab;      // ハエ型エネミーのプレハブ
	private GameObject Option_Prefab;                   //オプションのプレハブ
	private GameObject Item_Prefab;                     //パワーアップのアイテムを入れえるための処理
	private GameObject[] Effects_Prefab = new GameObject[18];  //particleのプレハブ
	private GameObject Boss_Middle_Prefab;						//中ボスのプレハブ

	//実際に作られたオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Medium_Size_Enemy1;
	public Object_Pooling Player;
	public Object_Pooling Boss;
	public Object_Pooling PlayerBullet;
	public Object_Pooling PlayerMissile;
	public Object_Pooling PlayerMissile_TowWay;
	public Object_Pooling EnemyBullet;
	public Object_Pooling Beam_Bullet_E;
	public Object_Pooling UfoType_Enemy;
	public Object_Pooling UfoType_Item_Enemy;
	public Object_Pooling UfoMotherType_Enemy;
	public Object_Pooling ClamChowderType_Enemy;
	public Object_Pooling OctopusType_Enemy;
	public Object_Pooling BeelzebubType_Enemy;
	public Object_Pooling Option;
	public Object_Pooling PowerUP_Item;
	public Object_Pooling Boss_Middle;
	//effect関係-----------------------------------------------------
	public Object_Pooling[] Effects = new Object_Pooling[18];
	//マップの作製時に使う処理
	public Vector3 pos;                                        //マップを作成するときの位置情報取得用
	private string File_name = "E_Pattern";                     //csvファイルの名前
	public List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int column;                                         //配列の列を入れる変数

	public AudioClip[] audio_se = new AudioClip[16];    //ＳＥを読み込むための配列
	public AudioClip[] audio_voice = new AudioClip[25];	//VOICEを読み込むための配列

	private void Awake()
	{
		Storage_Data = GetComponent<Obj_Storage>();
	}

	void Start()
    {
		Player_Prefab = Resources.Load("Player/Player") as GameObject;
		Enemy_Prefab = Resources.Load("Enemy/Enemy2") as GameObject;
		Medium_Enemy_Prefab = Resources.Load("Enemy/Medium_Size_Enemy") as GameObject;
		//Boss_Prefab = Resources.Load("Boss/Boss_Test") as GameObject;
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet") as GameObject;
		Player_Missile_Prefab = Resources.Load("Bullet/Player_Missile") as GameObject;
		Player_Missile_Tow_Way_Prefab = Resources.Load("Bullet/PlayerMissile_TowWay") as GameObject;
		Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
		Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
		UfoType_Enemy_Prefab = Resources.Load("Enemy/UfoType_Enemy") as GameObject;
		UfoType_Enemy_Item_Prefab = Resources.Load("Enemy/UfoType_Enemy_Item") as GameObject;
		UfoMotherType_Enemy_Prefab = Resources.Load("Enemy/UfoMotherType_Enemy") as GameObject; 
		ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject;
		OctopusType_Enemy_Prefab = Resources.Load("Enemy/OctopusType_Enemy") as GameObject; ;
		BeelzebubType_Enemy_Prefab = Resources.Load("Enemy/BeelzebubType_Enemy") as GameObject; ;
		Option_Prefab = Resources.Load("Option/Option") as GameObject;		//オプションのロード
		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;        //アイテムのロード
		Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;

		Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Single/S00");	//プレイヤー爆発
		Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Reuse/R00");	//プレイヤー登場時に使用するジェット噴射
		Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Reuse/R01");	//プレイヤーのマズルファイア
		Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Effects_004");  //none
		Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Single/S01");	//敵キャラの爆発エフェクト
		Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Single/S02");	//敵キャラコアシールドの破壊エフェクト
		Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Reuse/R02");	//プレイヤーパワーアップエフェクト
		Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Single/S03");	//ボス爆発
		Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Effects_009");		//none
		Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Loop/L01");			//敵の粒子
		Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Single/S04");		//大爆発
		Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Reuse/R03");		//プレイヤーの弾の着弾時のエフェクト
		Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Single/S06");		//ボス登場時のエフェクト
		Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Effects_014");        //none
		Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Effects_015");        //none
		Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Effects_016");        //none

		audio_se[0] = Resources.Load<AudioClip>("Sound/SE/01_gradius_se_intro");
		audio_se[1] = Resources.Load<AudioClip>("Sound/SE/04_gradius_se_credit");
		audio_se[2] = Resources.Load<AudioClip>("Sound/SE/05_gradius_se_SelectMove");
		audio_se[3] = Resources.Load<AudioClip>("Sound/SE/06_gradius_se_Select_OK");
		audio_se[4] = Resources.Load<AudioClip>("Sound/SE/07_gradius_se_Shot");
		audio_se[5] = Resources.Load<AudioClip>("Sound/SE/08_gradius_se_ItemGet");
		audio_se[6] = Resources.Load<AudioClip>("Sound/SE/09_gradius_se_zakoenemy_Destroyed");
		audio_se[7] = Resources.Load<AudioClip>("Sound/SE/10_gradius_se_Shot_Hit");
		audio_se[8] = Resources.Load<AudioClip>("Sound/SE/11_gradius_se_Explosion");
		audio_se[9] = Resources.Load<AudioClip>("Sound/SE/12_gradius_se_BossExplosion");
		//装備セレクトで使用するもの------------------------------------------------------
		audio_se[10] = Resources.Load<AudioClip>("Sound/SE/13_gradius_se_SpeedUp");			//スピードアップの声
		audio_se[11] = Resources.Load<AudioClip>("Sound/SE/14_gradius_se_LASER");			//レーザー攻撃の声
		audio_se[12] = Resources.Load<AudioClip>("Sound/SE/15_gradius_se_Double");			//ダブルの声
		audio_se[13] = Resources.Load<AudioClip>("Sound/SE/16_gradius_se_LIPLE_LASER");		//リップルレーザーの声
		audio_se[14] = Resources.Load<AudioClip>("Sound/SE/17_gradius_se_OPTION");			//オプションの声
		audio_se[15] = Resources.Load<AudioClip>("Sound/SE/18_gradius_se_FORCE_FIELD");		//フォースフィールド（シールド）
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
		audio_voice[17] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_18");		//アイテム使用時のボイス（フォースフィールド）
		audio_voice[18] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");		//アイテム使用時のボイス（マックススピード）
		audio_voice[19] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20");		//アイテム使用時のボイス（イニットスピード）
		audio_voice[20] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_21");
		audio_voice[21] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_22");
		audio_voice[22] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_23");
		audio_voice[23] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_24");
		audio_voice[24] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_25");

		Player = new Object_Pooling(Player_Prefab, 1, "Player");                        //プレイヤー生成
		Enemy1 = new Object_Pooling(Enemy_Prefab, 10, "Enemy_Straight");                 //Enemy(直線のみ)の生成
		//Boss = new Object_Pooling(Boss_Prefab, 1, "Boss");                              //ボス生成
		Medium_Size_Enemy1 = new Object_Pooling(Medium_Enemy_Prefab, 1, "Medium");
		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 5, "Player_Bullet");         //プレイヤーのバレットを生成
		PlayerMissile = new Object_Pooling(Player_Missile_Prefab, 20, "Player_Missile");        //プレイヤーのミサイルの生成
		PlayerMissile_TowWay = new Object_Pooling(Player_Missile_Tow_Way_Prefab, 20, "PlayerMissile_TowWay");
		EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 20, "Enemy_Bullet");          //エネミーのバレットを生成
		Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 20, "Enemy_Beam_Bullet");      // エネミーのビーム型バレットを生成
		UfoType_Enemy = new Object_Pooling(UfoType_Enemy_Prefab, 1, "UfoType_Enemy");       // UFO型エネミーを生成
		UfoType_Item_Enemy = new Object_Pooling(UfoType_Enemy_Item_Prefab, 5, "UfoType_Item_Enemy");	//UFO型のエネミーでアイテムを落とすやつを生成
		UfoMotherType_Enemy = new Object_Pooling(UfoMotherType_Enemy_Prefab, 1, "UfoMotherType_Enemy");         // UFO母艦型エネミーを生成
		ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");		// 貝型エネミーを生成
		OctopusType_Enemy = new Object_Pooling(OctopusType_Enemy_Prefab, 1, "OctopusType_Enemy");                               // タコ型エネミーを生成
		BeelzebubType_Enemy = new Object_Pooling(BeelzebubType_Enemy_Prefab, 1, "BeelzebubType_Enemy");      //	 ハエ型エネミーを生成
		Option = new Object_Pooling(Option_Prefab, 4, "Option");
		PowerUP_Item = new Object_Pooling(Item_Prefab, 10, "PowerUP_Item");
		//effect---------------------------------------------------------------------------------------------
		Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");                      //プレイヤーの爆発
		Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");       //プレイヤーが登場するときのジェット噴射
		Effects[2] = new Object_Pooling(Effects_Prefab[2], 2, "Player_Fire");                           //プレイヤーのマズルフラッシュ
		Effects[3] = new Object_Pooling(Effects_Prefab[3], 1, "Player_Bullet");                        //プレイヤーの弾（使用してない）
		Effects[4] = new Object_Pooling(Effects_Prefab[4], 5, "Enemy_explosion");                   //エネミーの死亡時の爆発
		Effects[5] = new Object_Pooling(Effects_Prefab[5], 1, "Enemy_Core_Sheld_explosion");    //エネミーの中ボス以上のコアシールドの爆発エフェクト
		Effects[6] = new Object_Pooling(Effects_Prefab[6], 1, "Player_PowerUP");                   //プレイヤーのパワーアップ時のエフェクト
		Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Boss_explosion");                     //ボス死亡時のエフェクト
		Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Player_PowerUP_Bullet");         //プレイヤーのパワーアップした弾（使用してない）
		Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Enemy_Grain");                 //敵の粒子
		Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Big_explosion");                    //大爆発
		Effects[11] = new Object_Pooling(Effects_Prefab[11], 4, "Player_Bullet_impact");        //プレイヤーの弾の着弾時のエフェクト
		Effects[12] = new Object_Pooling(Effects_Prefab[12], 1, "Boss_Appearance");           //ボス登場時のエフェクト
		Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Boss_Bullet1");                    //ボスの弾その１
		Effects[14] = new Object_Pooling(Effects_Prefab[14], 1, "Boss_Bullet2");                 //ボスの弾その２
		Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "Boss_Bullet3");                //ボスの弾その3
		//---------------------------------------------------------------------------------------------------
		TextAsset Word = Resources.Load("CSV_Folder/" + File_name) as TextAsset;             //csvファイルを入れる変数
		StringReader csv = new StringReader(Word.text);										//読み込んだデータをcsvの変数の中に格納
		while (csv.Peek() > -1)
		{
			string line = csv.ReadLine();
			CsvData.Add(line.Split(','));               //カンマごとに割り振る
		}


	
	}

	public Object_Pooling  GetPlayer()
	{
		return Player;
	}
	public Object_Pooling GetBoss()
	{
		return Boss;
	}
}
