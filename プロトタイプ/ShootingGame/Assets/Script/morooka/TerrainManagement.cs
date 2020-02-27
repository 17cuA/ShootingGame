using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using DebugLog.FileManager;

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
		public void set_inY(float valum)
		{
			in_y = valum;
		}
		public void set_outY(float valum)
		{
			out_y = valum;
		}
	}

	public ConfirmationInObjectCamera[] positionInCamera;

	private List<Transform> transformsList { get; set; }        // 子どもトランスフォームの保存
	private List<Renderer> renderers { get; set; }
	private List<RenderMonitoring> rm { get; set; }

	private void Awake()
	{
		renderers = new List<Renderer>(transform.GetComponentsInChildren<MeshRenderer>(true));
		rm = new List<RenderMonitoring>();
		foreach (Renderer ren in renderers)
		{
			rm.Add(ren.gameObject.AddComponent<RenderMonitoring>());
		}
	}
	private void OnDestroy()
	{
		Dictionary<string, OriginalVector4> so = new Dictionary<string, OriginalVector4>();
		foreach(var rmTemp in rm)
		{
			if(so.ContainsKey(rmTemp.name))
			{
				if (so[rmTemp.name].in_x < rmTemp.v4.in_x) so[rmTemp.name].set_inX(rmTemp.v4.in_x);
				if (so[rmTemp.name].in_y < rmTemp.v4.in_y) so[rmTemp.name].set_inY(rmTemp.v4.in_y);
				if (so[rmTemp.name].out_x > rmTemp.v4.out_x) so[rmTemp.name].set_outX(rmTemp.v4.out_x);
				if (so[rmTemp.name].out_y > rmTemp.v4.out_y) so[rmTemp.name].set_outY(rmTemp.v4.out_y);
			}
		}

		List<string[]> st = new List<string[]>();
		foreach(var fsda in so)
		{
			st.Add(new string[5] {fsda.Key, fsda.Value.in_x.ToString(), fsda.Value.in_y.ToString(), fsda.Value.out_x.ToString(), fsda.Value.out_y.ToString() });
		}

		string[,] st1 = new string[st.Count, st[0].Length];

		for(int i = 0; i < st.Count; i++)
		{
			for(int j = 0; j< st[i].Length; j++)
			{
				st1[i, j] = st[i][j];
			}
		}

		string[] st2 = new string [5] {"名前", "in_x", "in_y", "out_x", "out_y" };

		Debug_CSV.LogSave("Debug.csv", st2, st1, false);
	}
}

[System.Serializable]
public class ConfirmationInObjectCamera
{
	public string ObjectName;				// オブジェクトの名前
	public TerrainManagement.OriginalVector4 positionInCamera;      // カメラ内に入るポジション保存
}
