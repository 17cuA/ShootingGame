using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
	private List<Transform> transformsList { get; set; }		// 子どもトランスフォームの保存

	private void Awake()
	{
		transformsList = new List<Transform>();
		GetTransforms();
	}

    void Update()
    {
		foreach(var temp in transformsList)
		{
			// 範囲内のとき
			if (temp.position.x < 30.0f && temp.position.x > -30.0f
				&& temp.position.y < 10.0f && temp.position.y > -10.0f)
			{
				temp.gameObject.SetActive(true);
			}
			// それ以外のとき
			else
			{
				temp.gameObject.SetActive(false);
			}
		}
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
}
