using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLaser : MonoBehaviour
{
	Ray ray;                     //レイ
	RaycastHit hitObject;   //ヒットしたオブジェクト情報
	float rayLength = 40;   //レイの長さ
	bool isHitting;	//障害物に当たっている状態

	public BoxCollider boxCollider; //BoxCollider

	public ParticleSystem buringParticle;		//着弾火炎のオブジェクト

	public List<ParticleSystem> particleSystemList = new List<ParticleSystem>();    //レーザーのパーティクル
	float startLifetime = 0.25f;        //レーザーの最大の生存時間(基準)


	void Start()
	{
		boxCollider = GetComponent<BoxCollider>();
		buringParticle.Stop();
		isHitting = false;
	}

	void Update()
	{
		isHitting = true;

		//レイの設定
		ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
		//rayの可視化
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayLength, Color.red);

		//コライダー長さ変更
		Vector3 boxColliderSize = new Vector3(-rayLength,1,1);
		//中心点変更
		Vector3 boxColliderCenter = new Vector3(-rayLength/2, 0, 0);

		float laserLifetime = startLifetime;
		for (int i = 0; i < particleSystemList.Count; i++)
		{
			particleSystemList[i].startLifetime = laserLifetime;
		}
		//レイキャスト（原点, 飛ばす方向, 衝突した情報, 長さ）
		if (Physics.Raycast(ray, out hitObject, rayLength))
		{
			//当たったオブジェクトとの距離
			float interval = Vector3.Distance(transform.position, hitObject.point);

			laserLifetime = startLifetime * (interval / rayLength);
			for (int i = 0; i < particleSystemList.Count; i++)
			{
				particleSystemList[i].startLifetime = laserLifetime;
			}

			boxColliderSize = new Vector3(-interval, 1, 1);
			boxColliderCenter = new Vector3(-interval / 2, 0, 0);

			//着弾処理
			//エフェクト移動
			buringParticle.gameObject.transform.position = hitObject.point;
			//注視
			buringParticle.gameObject.transform.LookAt(this.gameObject.transform);
			//再生
			if (!buringParticle.isPlaying)
			{
				buringParticle.Play();
			}
		}
		else
		{
			isHitting = false;

			if (buringParticle.isPlaying)
			{
				buringParticle.Stop();
			}
		}
		//数値更新
		boxCollider.size = boxColliderSize;
		boxCollider.center = boxColliderCenter;
	}

	//当たっているかの判定
	public bool PassIsHitting()
	{
		return isHitting;
	}
	//当たった座標の取得
	public RaycastHit PassVector()
	{
		return hitObject;
	}
}
