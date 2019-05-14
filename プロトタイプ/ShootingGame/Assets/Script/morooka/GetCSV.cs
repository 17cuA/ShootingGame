using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Load_CSV
{
	public class GetCSV
	{
		/// <summary>
		/// レコードの検索
		/// </summary>
		/// <param name="db_array"> 検索するデータベース（二次配列） </param>
		/// <param name="search_Content"> 検索したい内容 </param>
		/// <returns> 検索した内容があったレコードの配列 </returns>
		public string[] RecordLoading(string[,] db_array, string search_Content)
		{
			// 返す文字列の配列
			string[] returning_string = new string[db_array.GetLength(0)];

			// 列の繰り返し
			for (int i = 0; i < db_array.Length; i++)
			{
				// 行の繰り返し
				for (int j = 0; j < db_array.GetLength(i); j++)
				{
					// 配列のコピー
					if (db_array[i, j] == search_Content)
					{
						for (int l = 0; l < db_array.GetLength(i); l++)
						{
							returning_string[l] = db_array[i, l];
						}

						return returning_string;
					}
				}
			}

			return returning_string;
		}

		/// <summary>
		/// レコードの検索
		/// </summary>
		/// <param name="resourcesName"> リソースにあるデータベースの名前 </param>
		/// <param name="search_Content"> 検索したいワード </param>
		/// <returns> 検索した内容があったレコードの配列 </returns>
		public string[] RecordLoading(string resourcesName, string search_Content)
		{
			return RecordLoading(CSVArrangement(resourcesName), search_Content);
		}

		/// <summary>
		/// カラムの検索
		/// </summary>
		/// <param name="db_array"> 検索するデータベース（二次配列） </param>
		/// <param name="search_Content"> 検索したい内容 </param>
		/// <returns> 検索した内容があったカラムの配列 </returns>
		public string[] ColumnLoading(string[,] db_array, string search_Content)
		{
			// 返す文字列の配列
			string[] returning_string = new string[db_array.Length];

			// 列の繰り返し
			for (int i = 0; i < db_array.Length; i++)
			{
				// 行の繰り返し
				for (int j = 0; j < db_array.GetLength(i); j++)
				{
					if (db_array[i, j] == search_Content)
					{
						// 配列のコピー
						for (int l = 0; l < db_array.Length; l++)
						{
							returning_string[l] = db_array[l, j];
						}

						return returning_string;
					}
				}
			}

			return returning_string;
		}

		/// <summary>
		/// カラムの検索
		/// </summary>
		/// <param name="resourcesName"> リソースにあるデータベースの名前 </param>
		/// <param name="search_Content"> 検索したいワード </param>
		/// <returns> 検索した内容があったカラムの配列 </returns>
		public string[] ColumnLoading(string resourcesName, string search_Content)
		{
			return ColumnLoading(CSVArrangement(resourcesName), search_Content);
		}

		/// <summary>
		/// CSVデータの読み込み
		/// </summary>
		/// <param name="resourcesName"> リソース内のファイル名（パス） </param>
		/// <returns> string型の2次配列化 </returns>
		public string[,] CSVArrangement(string resourcesName)
		{
			List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
			TextAsset Word = Resources.Load(resourcesName) as TextAsset;             //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();       // 1行分の読み込み
				CsvData.Add(line.Split(','));       // 1行を列に分ける
			}

			string[,] returningString = new string[CsvData.Count, CsvData[0].Length];        // リターン用の変数

			// 二次配列への変更
			for (int i = 0; i < CsvData.Count; i++)
			{
				for (int j = 0; j < CsvData[0].Length; j++)
				{
					returningString[i, j] = CsvData[i][j];
				}
			}

			return returningString;
		}
	}
}