using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using DebugLog.FileManager;

public class TerrainManagement : MonoBehaviour
{
	public ConfirmationInObjectCamera[] positionInCamera;
	Dictionary<string, Vector2> ForJudgment;
	private List<Transform> transformsList { get; set; }        // 子どもトランスフォームの保存
	private void Awake()
	{
		Renderer[] r = transform.GetComponentsInChildren<Renderer>();
		transformsList = new List<Transform>();
		foreach(var tt in r)
		{
			transformsList.Add(tt.transform);
		}

		ForJudgment = new Dictionary<string, Vector2>();
		foreach(var pc in positionInCamera)
		{
			ForJudgment.Add(pc.ObjectName, pc.positionInCamera);
		}
	}

	private void Update()
	{
		foreach(Transform temp in transformsList)
		{
			if(temp.position.x <= ForJudgment[temp.name].x+5.0f && temp.position.y <= ForJudgment[temp.name].y+7.0f 
				&& temp.position.x >= -ForJudgment[temp.name].x-5.0f && temp.position.y >= -ForJudgment[temp.name].y-7.0f)
			{
				temp.gameObject.SetActive(true);
			}
			else
			{
				temp.gameObject.SetActive(false);
			}
		}
	}
}

[System.Serializable]
public class ConfirmationInObjectCamera
{
	public string ObjectName;				// オブジェクトの名前
	public Vector2 positionInCamera;      // カメラ内に入るポジション保存
}
