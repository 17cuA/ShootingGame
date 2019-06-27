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
	private GameObject Beam_Bullet_E_Prefab;		//エネミーのビーム型バレットのプレハブ
	public GameObject[] particle = new GameObject[7];		//パーティクルを格納する配列

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
		Player_Prefab = Resources.Load("Player/Player_Demo_1") as GameObject;
		Enemy_Prefab = Resources.Load("Enemy/Enemy2") as GameObject;
		Medium_Enemy_Prefab = Resources.Load("Enemy/Medium_Size_Enemy") as GameObject;
		Boss_Prefab = Resources.Load("Boss/Boss_Test") as GameObject;
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet") as GameObject;
		Player_Missile_Prefab = Resources.Load("Bullet/Player_Missile") as GameObject;
		Player_Missile_Tow_Way_Prefab = Resources.Load("Bullet/PlayerMissile_TowWay") as GameObject;
		Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
		Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
		particle[0] = Resources.Load<GameObject>("Effects/Particle_1唐揚げ爆発");
		particle[1] = Resources.Load<GameObject>("Effects/Particle_2黒煙");
		particle[2] = Resources.Load<GameObject>("Effects/Particle_3エネルギー弾");
		particle[3] = Resources.Load<GameObject>("Effects/Particle_4衝撃波");
		particle[4] = Resources.Load<GameObject>("Effects/Particle_5箱爆発");
		particle[5] = Resources.Load<GameObject>("Effects/Particle_6通路");
		particle[6] = Resources.Load<GameObject>("Effects/Particle_7汎用煙");

		Enemy1 = new Object_Pooling(Enemy_Prefab, 10, "Enemy_Straight");                 //Enemy(直線のみ)の生成
		Medium_Size_Enemy1 = new Object_Pooling(Medium_Enemy_Prefab, 1, "Medium");
		Player = new Object_Pooling(Player_Prefab, 1, "Player");						//プレイヤー生成
		Boss = new Object_Pooling(Boss_Prefab, 1, "Boss");								//ボス生成
		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 5, "Player_Bullet");         //プレイヤーのバレットを生成
		PlayerMissile = new Object_Pooling(Player_Missile_Prefab, 20, "Player_Missile");        //プレイヤーのミサイルの生成
		PlayerMissile_TowWay = new Object_Pooling(Player_Missile_Tow_Way_Prefab, 20, "PlayerMissile_TowWay");
		EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 20, "Enemy_Bullet");          //エネミーのバレットを生成
		Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 20, "Enemy_Beam_Bullet");		// エネミーのビーム型バレットを生成

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
