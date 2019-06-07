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
	private GameObject Enemy_Prefab;
	private GameObject Player_Prefab;
	private GameObject Boss_Prefab;
	private GameObject Bullet_Prefab;      //弾のPrefab情報

	public GameObject[] particle = new GameObject[7];		//パーティクルを格納する配列
	public GameObject particleGameObject;

	//実際に作られたオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Player;
	public Object_Pooling Boss;
	public Object_Pooling PlayerBullet;
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
		Boss_Prefab = Resources.Load("Boss/Boss_Test") as GameObject;
		Bullet_Prefab = Resources.Load("Player_Bullet") as GameObject;

		particle[0] = Resources.Load<GameObject>("Effects/Particle_1唐揚げ爆発");
		particle[1] = Resources.Load<GameObject>("Effects/Particle_2黒煙");
		particle[2] = Resources.Load<GameObject>("Effects/Particle_3エネルギー弾");
		particle[3] = Resources.Load<GameObject>("Effects/Particle_4衝撃波");
		particle[4] = Resources.Load<GameObject>("Effects/Particle_5箱爆発");
		particle[5] = Resources.Load<GameObject>("Effects/Particle_6通路");
		particle[6] = Resources.Load<GameObject>("Effects/Particle_7汎用煙");

		Enemy1 = new Object_Pooling(Enemy_Prefab, 10, "Enemy_Straight");                 //Enemy(直線のみ)の生成
		Player = new Object_Pooling(Player_Prefab, 1, "Player");
		Boss = new Object_Pooling(Boss_Prefab, 1, "Boss");
		PlayerBullet = new Object_Pooling(Bullet_Prefab, 5, "Player_Bullet");

		TextAsset Word = Resources.Load("CSV_Folder/" + File_name) as TextAsset;             //csvファイルを入れる変数
		StringReader csv = new StringReader(Word.text);
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
