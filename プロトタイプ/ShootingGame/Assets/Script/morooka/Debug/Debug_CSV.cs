using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace DebugLog.FileManager
{
	public class Debug_CSV
	{
		static public void LogSave(string path, string[] header, string[,] log, bool Overwrite)
		{
			// ファイル書き出し
			// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
			// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
			StreamWriter sw = new StreamWriter(@path, Overwrite, Encoding.GetEncoding("Shift_JIS"));
			// ヘッダー出力
			sw.WriteLine(string.Join(",", header));
			// データ出力
			for (int i = 0; i < log.GetLength(0); i++)
			{
				string[] str = new string[log.GetLength(0)];
				for (int j = 0; j < log.GetLength(1); j++)
				{
					str[j] = log[i, j];
				}
				string str2 = string.Join(",", str);
				sw.WriteLine(str2);
			}
			// StreamWriterを閉じる
			sw.Close();

			// ファイル読み込み
			// 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
			StreamReader sr = new StreamReader(@path, Encoding.GetEncoding("Shift_JIS"));
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
	}
}
