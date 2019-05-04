using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreate : MonoBehaviour
{
	//ワールド座標でのマップの左下の部分
	//ここから敵の配置などをしていく
	private const int bottom_left_pos_y = -3;
	private const int bottom_left_pos_x = -8;

	private Vector3 Pos;
	private string File_name = "E_Pattern";						//csvファイルの名前
	private List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int[][] map;
	void Start()
    {
		TextAsset Word = Resources.Load(File_name) as TextAsset; ;             //csvファイルを入れる変数
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
		//List<int[]> iList = CsvData.ConvertAll(x => int.Parse(x));
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
