using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreate : MonoBehaviour
{
	//ワールド座標でのマップの左下の部分
	//ここから敵の配置などをしていく
	private const int up_left_pos_y = 5;
	private const int up_left_pos_x = -8;

	private Vector3 pos;										//マップを作成するときの位置情報取得用
	private string File_name = "E_Pattern";						//csvファイルの名前
	private List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int[][] map;
	//マップ作製に使うオブジェクト
	//リソースフォルダから取得するため、インスペクターは使わない
	private GameObject Enemy;
	private GameObject Player;
	void Start()
    {
		Player = Resources.Load("Player/Player_Demo 1") as GameObject;
		Enemy = Resources.Load("Enemy/Enemy2") as GameObject;
		TextAsset Word = Resources.Load(File_name) as TextAsset;             //csvファイルを入れる変数
		StringReader csv = new StringReader(Word.text);
		while (csv.Peek() > -1)
		{
			string line = csv.ReadLine();
			CsvData.Add(line.Split(','));
		}
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 30; j++)
			{
				Debug.Log(CsvData[i][j] + " " + i + " " + j);
			}
		}
		CreateMap();
		//List<int[]> iList = CsvData.ConvertAll(x => int.Parse(x));
	}
	void CreateMap()
	{
		for (int y = 0; y < 10; y++)
		{
			for (int x = 0; x < 30; x++)
			{
				pos = new Vector3(up_left_pos_x + x * 2, up_left_pos_y - y, 0);
				switch(CsvData[y][x])
				{
					case "0":
						break;
					case "1":
						Instantiate(Player, pos, Quaternion.identity);
						break;
					case "2":
						Instantiate(Enemy, pos, Quaternion.identity);
						break;
					default:
						break;
				}
			}
		}
	}
}
