//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/11/29
//----------------------------------------------------------------------------------------------
// ステージ内の全シャッター管理
//----------------------------------------------------------------------------------------------
// 2019/11/29　シャッターの保存と削除
// 2019/12/02　前半、後半生成物別け
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter_Manager : MonoBehaviour
{
	[SerializeField, Tooltip("シャッター")] private List<character_status> shutterList;

	private List<character_status> CandidateForDeletion;        // 削除候補

	private void Start()
	{
		CandidateForDeletion = new List<character_status>();
	}

	void Update()
    {
		// シャッターのHPが０のとき、削除候補に挙げる
		foreach (var obj in shutterList)
		{
			if (obj.hp <= 0)
			{
				CandidateForDeletion.Add(obj);
			}
		}

		// 削除候補の削除
		foreach(var obj in CandidateForDeletion)
		{
			shutterList.Remove(obj);
			Destroy(obj.gameObject);
		}
		CandidateForDeletion.Clear();

		// 管理するシャッターがなくなったとき、自分削除
		if(shutterList.Count == 0)
		{
			Destroy(gameObject);
		}
    }
}
