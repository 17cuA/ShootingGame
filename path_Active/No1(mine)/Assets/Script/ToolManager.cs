using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

[ExecuteInEditMode]     //playモードじゃなくても、スクリプトが動くようにするもの
public class ToolManager : MonoBehaviour
{
	[Header("セーブをして、csvファイルに書き出すのかどうか")]
	public bool IS_Save;
	[Header("敵キャラの動きを確認するかどうか")]
	public bool Enemy_Test;
	[Header("動かす敵キャラの数")]
	public int Enemy_Num;

	public LineCreater LC;
	public string save_name;
	private string Extension;
	public StreamWriter sw;
	private string[][] St_Data;
	public float[][] Fl_Data;
	public List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private void OnGUI()
	{
		if (IS_Save) Save_Pos();
	}




	// Use this for initialization
	void Save_Pos()
	{
		//ラインレンダラーの情報取得
		Extension = ".csv";

		if (save_name == null) save_name = "Enemy_Action";
		// ファイル書き出し
		// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
		// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
		sw = new StreamWriter(@"Assets/Resources/" + save_name + Extension, false, Encoding.GetEncoding("Shift_JIS"));

		// データ出力
		for (int i = 0; i < LC.lineRenderer.positionCount; i++)
		{
			//LineRendererをもとに各情報を取得、さらに鍵括弧が入ってしまうためその括弧をなくす処理
			sw.WriteLine(LC.lineRenderer.GetPosition(i).ToString().Replace("(", "").Replace(")", ""));
			Debug.Log("hei");
			//St_Data = sw.WriteLine(LC.lineRenderer.GetPosition(i).ToString().Replace("(", "").Replace(")", ""));

		}
		// StreamWriterを閉じる
		sw.Close();

		//TextAsset Word = Resources.Load("test") as TextAsset;             //csvファイルを入れる変数
		//StringReader csv = new StringReader(Word.text);                                     //読み込んだデータをcsvの変数の中に格納
		//while (csv.Peek() > -1)
		//{
		//	string line = csv.ReadLine();
		//	CsvData.Add(line.Split(','));               //カンマごとに割り振る
		//}
		IS_Save = false;
	}
}
