/*
 * 製作者：久保田達己
 * LineRendererからベジェ曲線の位置情報を取得
 * このデータをEnemyの動きに連動させる
 * デバックモードをするとこのスクリプトが動きだす
 * 読み込みは別のスクリプトにて動かす
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class Export_Csv : MonoBehaviour
{
	LineCreater LC;
	public string save_name;
	private string Extension;
	// Use this for initialization
	void Start()
	{
		//ラインレンダラーの情報取得
		LC = gameObject.GetComponent<LineCreater>();
		Extension = ".csv";

		if (save_name == null) save_name = "Enemy_Action";
		// ファイル書き出し
		// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
		// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
		StreamWriter sw = new StreamWriter(@"Assets/CSV/" + save_name + Extension, false, Encoding.GetEncoding("Shift_JIS"));
		// データ出力
		for (int i = 0; i < LC.LineRenderer.positionCount; i++)
		{
			//LineRendererをもとに各情報を取得、さらに鍵括弧が入ってしまうためその括弧をなくす処理
			sw.WriteLine(LC.LineRenderer.GetPosition(i).ToString().Replace("(","").Replace(")",""));
		}
		// StreamWriterを閉じる
		sw.Close();
	}
}
