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
	private GameObject UfoMotherType_Enemy_Prefab;      // UFO母艦型エネミーのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;        // 貝型エネミーのプレハブ
	private GameObject OctopusType_Enemy_Prefab;        // タコ型エネミーのプレハブ
	private GameObject BeelzebubType_Enemy_Prefab;      // ハエ型エネミーのプレハブ
	public GameObject[] particle = new GameObject[7];       //パーティクルを格納する配列
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
		UfoMotherType_Enemy_Prefab = Resources.Load("Enemy/UfoMotherType_Enemy") as GameObject; 
		ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject; ;
		OctopusType_Enemy_Prefab = Resources.Load("Enemy/OctopusType_Enemy") as GameObject; ;
		BeelzebubType_Enemy_Prefab = Resources.Load("Enemy/BeelzebubType_Enemy") as GameObject; ;
		Option_Prefab = Resources.Load("Option/Option") as GameObject;		//オプションのロード
		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;        //アイテムのロード
		Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;

		Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Effects_000");
		Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Effects_001");
		Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Effects_002");
		Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Effects_003");
		Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Effects_004");     //none
		Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Effects_005");
		Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Effects_006");
		Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Effects_007");
		Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Effects_008");
		Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Effects_009");     //none
		Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Effects_010");
		Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Effects_011");
		Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Effects_012");
		Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Effects_013");
		Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Effects_014");        //none
		Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Effects_015");        //none
		Effects_Prefab[16] = Resources.Load<GameObject>("Effects/Effects_016");        //none
		Effects_Prefab[17] = Resources.Load<GameObject>("Effects/Effects_017");


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
		UfoMotherType_Enemy = new Object_Pooling(UfoMotherType_Enemy_Prefab, 1, "UfoMotherType_Enemy");         // UFO母艦型エネミーを生成
		ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");		// 貝型エネミーを生成
		OctopusType_Enemy = new Object_Pooling(OctopusType_Enemy_Prefab, 1, "OctopusType_Enemy");                               // タコ型エネミーを生成
		BeelzebubType_Enemy = new Object_Pooling(BeelzebubType_Enemy_Prefab, 1, "BeelzebubType_Enemy");      //	 ハエ型エネミーを生成
		Option = new Object_Pooling(Option_Prefab, 4, "Option");
		PowerUP_Item = new Object_Pooling(Item_Prefab, 10, "PowerUP_Item");
		//effect---------------------------------------------------------------------------------------------
		Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");						//プレイヤーの爆発
		Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");       //プレイヤーが登場するときのジェット噴射
		Effects[2] = new Object_Pooling(Effects_Prefab[2], 1, "Player_injection");						//プレイヤーのジェット噴射
		Effects[3] = new Object_Pooling(Effects_Prefab[3], 2, "Player_Fire");							//プレイヤーのマズルフラッシュ
		Effects[4] = new Object_Pooling(Effects_Prefab[4], 1, "Player_Bullet");						   //プレイヤーの弾（使用してない）
		Effects[5] = new Object_Pooling(Effects_Prefab[5], 5, "Enemy_explosion");					//エネミーの死亡時の爆発
		Effects[6] = new Object_Pooling(Effects_Prefab[6], 1, "Enemy_Core_Sheld_explosion");	//エネミーの中ボス以上のコアシールドの爆発エフェクト
		Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Player_PowerUP");				   //プレイヤーのパワーアップ時のエフェクト
		Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Boss_explosion");					 //ボス死亡時のエフェクト
		Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Player_PowerUP_Bullet");			//プレイヤーのパワーアップした弾（使用してない）
		Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Enemy_Grain");					//敵の粒子
		Effects[11] = new Object_Pooling(Effects_Prefab[11], 1, "Big_explosion");					 //大爆発
		Effects[12] = new Object_Pooling(Effects_Prefab[12], 4, "Player_Bullet_impact");		//プレイヤーの弾の着弾時のエフェクト
		Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Boss_Appearance");			  //ボス登場時のエフェクト
		Effects[14] = new Object_Pooling(Effects_Prefab[14], 1, "Boss_Bullet1");					//ボスの弾その１
		Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "Boss_Bullet2");				 //ボスの弾その２
		Effects[16] = new Object_Pooling(Effects_Prefab[16], 1, "Boss_Bullet3");				//ボスの弾その3
		Effects[17] = new Object_Pooling(Effects_Prefab[17], 1, "Player_Sheld");			   //プレイヤーのシールド
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
