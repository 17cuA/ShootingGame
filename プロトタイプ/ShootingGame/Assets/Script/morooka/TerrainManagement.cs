using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
	private List<MeshRenderer> TerrainRenderer { get; set; }

	private void Awake()
	{
		TerrainRenderer = new List<MeshRenderer>();
		GetMeshRenderer(transform);
	}

    void Update()
    {
        foreach(MeshRenderer renderer in TerrainRenderer)
		{
			// 範囲外のとき
			if(renderer.transform.position.x < 22.0f && renderer.transform.position.x > -22.0f
				&& renderer.transform.position.y < 15.0f && renderer.transform.position.y > -15.0f)
			{
				renderer.enabled = true;
				Debug.Log(renderer.transform.position + " : true");
			}
			// それ以外のとき
			{
				renderer.enabled = false;
				Debug.Log(renderer.transform.position + " : false");
			}
		}
	}

	/// <summary>
	/// メッシュレンダー情報の取得
	/// </summary>
	/// <param name="transform"> オブジェクトのトランスフォーム </param>
	void GetMeshRenderer(Transform transform)
	{
		// 子供情報のループ
		foreach (Transform temp1 in transform)
		{
			// 子供のメッシュレンダーの取得
			MeshRenderer temp2 = temp1.GetComponent<MeshRenderer>();
			// 子供がメッシュレンダーを持っていれば、保存
			if (temp2 != null)
			{
				TerrainRenderer.Add(temp2);
			}

			// 孫(tempの子供)がいるとき
			if (temp1.childCount > 0)
			{
				// 孫(tempの子供)のレンダー取得開始
				GetMeshRenderer(temp1);
			}
		}
	}
}
