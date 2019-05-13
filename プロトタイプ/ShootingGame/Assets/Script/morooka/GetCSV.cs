using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GetCSV : MonoBehaviour
{
    /// <summary>
    /// レコードの読み込み
    /// </summary>
    /// <param name="Array"> 二次配列のテーブル </param>
    /// <returns> 一時配列のレコード </returns>
    public string RecordLoading(string[,] Array)
    {

        return Array[0,0];
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

        string[,] returningString = new string[CsvData.Count,CsvData[0].Length];        // リターン用の変数

        for(int i = 0; i < CsvData.Count; i++)
        {
            for(int j = 0; j < CsvData[0].Length;j++)
            {
                returningString[i,j] = CsvData[i][j];
            }
        }

        return returningString;
    }
}
