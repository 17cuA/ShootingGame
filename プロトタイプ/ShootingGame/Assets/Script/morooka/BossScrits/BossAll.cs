//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossの全体管理をする
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの格納、各パーツの生存確認
//----------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

public class BossAll : MonoBehaviour
{
	public Animator animationControl;		// アニメーションの管理
	public int score;									// 自分のスコア
	public Renderer ownRenderer;				// 自分のレンダー

	private List<BossParts> OwnParts { set; get; }						// 自分のパーツの管理
	private List<MeshRenderer> PartsRenderer { set; get; }		// 自分のパーツのレンダー

	void Start()
    {
		animationControl = GetComponent<Animator>();
		ownRenderer = GetComponent<Renderer>();
        ownRenderer.enabled = true;
		OwnParts = new List<BossParts>();
		Part_Acquisition(transform);

		PartsRenderer = new List<MeshRenderer>();
		for(int i = 0; i < OwnParts.Count; i++)
		{
			PartsRenderer.Add(OwnParts[i].GetComponent<MeshRenderer>());
			PartsRenderer[i].enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
    {
		PartFactorDeletion();

		// パーツがなくなったとき
		if (Is_PartsAlive())
		{
			OwnDeletion();
		}

        // 自分がカメラ内に入ったとき
		if(ownRenderer.isVisible)
		{
            // 格納したパーツの表示
			for (int i = 0; i < PartsRenderer.Count;i++)
			{
				PartsRenderer[i].enabled = true;
			}
            // 自身のレンダーの使用をやめる
            ownRenderer.enabled = false;
			PartsRenderer.Clear();
		}
	}

	/// <summary>
	/// パーツを取得する再帰関数
	/// </summary>
	/// <param name="objTrans"> トランスフォーム </param>
	void Part_Acquisition(Transform objTrans)
	{
		// 子供を参照する繰り返し
		for (int i = 0; i < objTrans.childCount; i++)
		{
			// リストに情報を入れる
			OwnParts.Add(objTrans.GetChild(i).GetComponent<BossParts>());
			
			//参照した子供に、また子供がいるとき
			if(objTrans.GetChild(i).childCount > 0)
			{
				// Part_Acquisition関数の呼び出し
				Part_Acquisition(objTrans.GetChild(i));
			}
		}
	}

	/// <summary>
	/// パーツのリストの中が null のとき要素の削除
	/// </summary>
	private void PartFactorDeletion()
	{
		//各パーツの確認
		for (int i = 0; i < OwnParts.Count; i++)
		{
			// null のとき
			if (OwnParts[i] == null)
			{
				// 要素削除
				OwnParts.RemoveAt(i);
			}
		}
	}

	/// <summary>
	/// 各パーツの生存確認
	/// </summary>
	/// <returns> 無敵でないパーツが生存しないとき true </returns>
	private bool Is_PartsAlive()
	{
		for (int i = 0; i < OwnParts.Count; i++)
		{
            // 無敵でないパーツのとき
			if (!OwnParts[i].invincible)
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// 疑似デストラクタ
	/// </summary>
	private void OwnDeletion()
	{

		Destroy(gameObject);
	}
}
