//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossのベースになる挙動
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの格納、各パーツの生存確認
//----------------------------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections.Generic;

public class BossBase : MonoBehaviour
{
	/// <summary>
	/// 自分のパーツ（子供）
	/// </summary>
	private List<BossParts> ownParts { set; get; }

	void Start()
    {
		ownParts = new List<BossParts>();
		for(int i = 0; i < transform.childCount; i++)
		{
			ownParts.Add(transform.GetChild(i).GetComponent<BossParts>());
		}
	}

    void Update()
    {
		PartFactorDeletion();
		
		// パーツがなくなったとき
		if(Is_PartsAlive())
		{
			OwnDeletion();
		}
	}

	/// <summary>
	/// パーツのリストの中が null のとき要素の削除
	/// </summary>
	private void PartFactorDeletion()
	{
		// 各パーツの確認
		for (int i = 0; i < ownParts.Count; i++)
		{
			// null のとき
			if (ownParts[i] == null)
			{
				// 要素削除
				ownParts.RemoveAt(i);
			}
		}
	}

	/// <summary>
	/// パーツの生存確認
	/// </summary>
	/// <returns>パーツがないとき true </returns>
	private bool Is_PartsAlive()
	{
        for(int i = 0; i < ownParts.Count;i++)
        {
            if(!ownParts[i].invincible)
            {
                return false;
            }
        }

        return true;
		//if(ownParts.Count == 0)
		//{
		//	return true;
		//}
		//else
		//{
		//	return false;
		//}
	}

	/// <summary>
	/// 疑似デストラクタ
	/// </summary>
	private void OwnDeletion()
	{

		Destroy(gameObject);
	}
}
