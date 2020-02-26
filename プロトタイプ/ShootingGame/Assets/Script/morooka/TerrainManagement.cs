using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagement : MonoBehaviour
{
	public ConfirmationInObjectCamera[] positionInCamera;
	private List<Transform> transformsList { get; set; }		// 子どもトランスフォームの保存
	private List<Renderer> renderers { get; set; }

	private void Awake()
	{
		transformsList = new List<Transform>();
		//renderers = new List<Renderer>(transform.GetComponentsInChildren<MeshRenderer>(true));
		GetTransforms();
	}

    void Update()
    {
		foreach (var temp in transformsList)
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

		//foreach (Renderer ren in renderers )
		//{
		//	if(!ren.isVisible)
		//	{
		//		ren.enabled = false;
		//	}
		//	else if(ren.isVisible)
		//	{
		//		ren.enabled = true;
		//	}
		//}
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

[System.Serializable]
public class ConfirmationInObjectCamera
{
	public string ObjectName;				// オブジェクトの名前
	public Vector4 positionInCamera;		// カメラ内に入るポジション保存
}
