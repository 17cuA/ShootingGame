using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MapCreate : MonoBehaviour
{
	//ワールド座標でのマップの左下の部分
	//ここから敵の配置などをしていく
	private const int up_left_pos_y = 5;
	private const int up_left_pos_x = -8;

	//マップの作製時に使う処理
	private Vector3 pos;										//マップを作成するときの位置情報取得用
	private string File_name = "E_Pattern";						//csvファイルの名前
	private List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int column;                                         //配列の列を入れる変数
	private int enemy_cnt;										//移動する敵キャラの数をカウントするための変数
	//マップ作製に使うオブジェクト
	//リソースフォルダから取得するため、インスペクターは使わない
	public GameObject Enemy;
	public GameObject Player;
	public GameObject Boss;
	//シーンを切り替えるときにプレイヤーの死亡情報などを取得するための変数
	private SceneChanger SC;
	//生成したオブジェクトを取得するための変数
	//private Object_Creation OC;
	private Object_Pooling Enemy1;
	void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage")
		{
			Player = Resources.Load("Player/Player_Item") as GameObject;
			Enemy = Resources.Load("Enemy/Enemy2") as GameObject;
			Boss = Resources.Load("Boss/Boss_Test") as GameObject;
			TextAsset Word = Resources.Load("CSV_Folder/" + File_name) as TextAsset;             //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);
			SC = GetComponent<SceneChanger>();
			//OC = GetComponent<Object_Creation>();
			Enemy1 = new Object_Pooling(Enemy, 10, "敵キャラ");					//Enemy(直線のみ)の生成
			//csvフォルダからマップ情報を取得
			//１列ごとに取得
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();
				CsvData.Add(line.Split(','));				//カンマごとに割り振る
			}
			CreateMap();			//マップの作成（各オブジェクトの移動）
			SC.Chara_Get();
			enemy_cnt = 0;
		}
	}
	void CreateMap()
	{
		for (int y = 0; y < CsvData.Count; y++)
		{
			for (int x = 0; x < CsvData[y].Length; x++)
			{
				pos = new Vector3(up_left_pos_x + x * 2, up_left_pos_y - y, 0);
				switch(CsvData[y][x])
				{
					case "0":
						break;
					case "1":
						//OC.PlayrePos_Conversion(pos);
						//Player =Instantiate(Player, pos, Quaternion.identity);
						break;
					case "2":
						Enemy1.Active_Obj();
						Enemy1.
						enemy_cnt++;

						//OC.EnemyPos_Conversion(enemy_cnt,pos);
						//Instantiate(Enemy, pos, Quaternion.identity);
						break;
					case "3":
						Boss = Instantiate(Boss, pos, Quaternion.identity);
						break;
					default:
						break;
				}
			}
		}
	}
	public GameObject GetPlayer()
	{
		return Player;
	}
	public GameObject GetBoss()
	{
		return Boss;
	}
}
