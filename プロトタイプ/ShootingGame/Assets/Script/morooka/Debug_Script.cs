using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

	public class Debug_Script : MonoBehaviour
	{
		public Debug_Script my;
		static private List<GameObject> object_obj;
		static private List<string> object_str;
		static private List<int> object_num;

		private void Awake()
		{
			my = GetComponent<Debug_Script>();
			object_obj = new List<GameObject>();
			object_str = new List<string>();
			object_num = new List<int>();
		}

		void Update()
		{
			for (int i = 0; object_obj.Count > i; i++)
			{
				if (object_obj[i] == null)
				{
					continue;
				}
				if (object_obj[i].transform.childCount > object_num[i])
				{
					object_num[i] = object_obj[i].transform.childCount;
				}
			}
		}

		private void OnDestroy()
		{
			for (int i = 0; object_str.Count > i; i++)
			{
				Debug.LogError(object_str[i] + " : " + object_num[i]);
			}
		LogSave();
		}

		static public void GetPooling(GameObject obj)
		{
			object_obj.Add(obj);
			object_str.Add(obj.name);
			object_num.Add(obj.transform.childCount);
		}

	public void LogSave()
	{
		// ファイル書き出し
		// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
		// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
		StreamWriter sw = new StreamWriter(@"saveData2.csv", false, Encoding.GetEncoding("Shift_JIS"));
		// ヘッダー出力
		string[] s1 = { "オブジェクト名", "数" };
		string s2 = string.Join(",", s1);
		sw.WriteLine(s2);
		// データ出力
		for (int i = 0; i < object_str.Count; i++)
		{
			string[] str = { object_str[i], object_num[i].ToString() };
			string str2 = string.Join(",", str);
			sw.WriteLine(str2);
		}
		// StreamWriterを閉じる
		sw.Close();

		// ファイル読み込み
		// 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
		StreamReader sr = new StreamReader(@"saveData2.csv", Encoding.GetEncoding("Shift_JIS"));
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
