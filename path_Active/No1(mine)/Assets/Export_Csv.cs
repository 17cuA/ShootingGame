using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class Export_Csv : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		// ファイル書き出し
		// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
		// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
		StreamWriter sw = new StreamWriter(@"saveData.csv", false, Encoding.GetEncoding("Shift_JIS"));
		// ヘッダー出力
		string[] s1 = { "プレイヤー名", "記録" };
		string s2 = string.Join(",", s1);
		sw.WriteLine(s2);
		// データ出力
		for (int i = 0; i < 3; i++)
		{
			string[] str = { "tatsu", "" + (i + 1) };
			string str2 = string.Join(",", str);
			sw.WriteLine(str2);
		}
		// StreamWriterを閉じる
		sw.Close();


		// ファイル読み込み
		// 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
		StreamReader sr = new StreamReader(@"saveData.csv", Encoding.GetEncoding("Shift_JIS"));
		string line;
		// 行がnullじゃない間(つまり次の行がある場合は)、処理をする
		while ((line = sr.ReadLine()) != null)
		{
			// コンソールに出力
			Debug.Log(line);
		}
		// StreamReaderを閉じる
		sr.Close();
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
