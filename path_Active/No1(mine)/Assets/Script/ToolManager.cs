using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

[ExecuteInEditMode]     //playモードじゃなくても、スクリプトが動くようにするもの
public class ToolManager : MonoBehaviour
{
	[Header("セーブをして、csvファイルに書き出すのかどうか")]
	public bool IS_Save;
	public LineCreater LC;
	public string save_name;
	private string Extension;
	private StreamWriter sw;

	[Header("敵キャラの動きを確認するかどうか")]
	public bool Enemy_Test;
	[SerializeField]private GameObject Enemy;
	//private int frame = 0;
	//private int frame_Max;
	private int cnt = 0;
	[Header("動かす敵キャラの数")]
	public int Enemy_Num;


	private void OnGUI()
	{
		if (IS_Save) Save_Pos();
		if (Enemy_Test)
		{
			Enemy_Action();
		}
	}

	private void Update()
	{

		//エディタ全体の再描画
		//EditorApplication.QueuePlayerLoopUpdate();
	}
	void Enemy_Action()
	{
		//if (frame > frame_Max)
		//{
		//	Enemy.transform.position = LC.lineRenderer.GetPosition(cnt);
		//	cnt++;
		//	frame = 0;
		//	if(cnt >= LC.lineRenderer.positionCount - 1)
		//	{
		//		cnt = 0;
		//	}
		//}
		//frame++;
		Enemy.transform.position = LC.lineRenderer.GetPosition(cnt);
		cnt++;
		if(cnt >= LC.lineRenderer.positionCount - 1)
		{
			cnt = 0;
		}

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
		}
		// StreamWriterを閉じる
		sw.Close();
		Debug.Log("Save		completed");
		Debug.Log("一度画面を切り替えるか、AssetFolderの中のResourcesを確認してください");
		IS_Save = false;
	}
}
