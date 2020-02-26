using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
 	public struct OriginalVector4
	{
		public float in_x;
		public float in_y;
		public float out_x;
		public float out_y;

		public void set_inX(float valum)
		{
			in_x = valum;
		}
		public void set_outX(float valum)
		{
			out_x = valum;
		}
	}

	public ConfirmationInObjectCamera[] positionInCamera;
	private Dictionary<string, OriginalVector4> stradadada;

	private List<Transform> transformsList { get; set; }		// 子どもトランスフォームの保存
	private List<Renderer> renderers { get; set; }

	private void Awake()
	{
		transformsList = new List<Transform>();
		renderers = new List<Renderer>(transform.GetComponentsInChildren<MeshRenderer>(true));
		stradadada = new Dictionary<string, OriginalVector4>();
		foreach (var g in positionInCamera)
		{
			stradadada.Add(g.ObjectName, g.positionInCamera);
		}

		GetTransforms();
	}

    void Update()
    {
		//foreach (var temp in transformsList)
		//{
		//	// 範囲内のとき
		//	if (temp.position.x < 30.0f && temp.position.x > -30.0f
		//		&& temp.position.y < 10.0f && temp.position.y > -10.0f)
		//	{
		//		temp.gameObject.SetActive(true);
		//	}
		//	// それ以外のとき
		//	else
		//	{
		//		temp.gameObject.SetActive(false);
		//	}
		//}

		foreach (Renderer ren in renderers)
		{
			if (!ren.isVisible)
			{
				if(stradadada[ren.name].out_x > ren.transform.position.x)
				{

					stradadada[ren.name].set_outX(ren.transform.position.x);
				}
			}
			else if (ren.isVisible)
			{
			}
		}
	}

	private void OnDestroy()
	{
		LogSave(stradadada);
	}

	/// <summary>
	/// 子どもトランスフォームの取得
	/// </summary>
	void GetTransforms()
	{
		foreach(Transform temp in transform)
		{
			transformsList.Add(temp);
		}
	}

	public void LogSave(Dictionary<string, OriginalVector4> object_str)
	{
		// ファイル書き出し
		// 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
		// 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
		StreamWriter sw = new StreamWriter(@"Debug.csv", true, Encoding.GetEncoding("Shift_JIS"));
		// ヘッダー出力
		string[] s1 = { "オブジェクト名", "in_x", "in_y","out_x","out_y" };
		string s2 = string.Join(",", s1);
		sw.WriteLine(s2);
		// データ出力
		for (int i = 0; i < positionInCamera.Length; i++)
		{
			string[] str = { positionInCamera[i].ObjectName, object_str[positionInCamera[i].ObjectName].in_x.ToString(),
				object_str[positionInCamera[i].ObjectName].in_y.ToString(),
				object_str[positionInCamera[i].ObjectName].out_x.ToString(),
				object_str[positionInCamera[i].ObjectName].out_y.ToString() };
			string str2 = string.Join(",", str);
			sw.WriteLine(str2);
		}
		// StreamWriterを閉じる
		sw.Close();

		// ファイル読み込み
		// 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
		StreamReader sr = new StreamReader(@"Debug.csv", Encoding.GetEncoding("Shift_JIS"));
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

[System.Serializable]
public class ConfirmationInObjectCamera
{
	public string ObjectName;				// オブジェクトの名前
	public TerrainManagement.OriginalVector4 positionInCamera;		// カメラ内に入るポジション保存
}
