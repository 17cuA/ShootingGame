using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionLaser : MonoBehaviour
{
	public BoxCollider boxCollider;     //コライダー
	public float colliderMaxSize;       //コライダーの最大のサイズ
	public float addColliderLength;     //加算するコライダーの長さ

	public bool isHit;                  //当たり判定

	public List<ParticleSystem> particleSystemList = new List<ParticleSystem>();

	void Start()
	{
		isHit = false;
		boxCollider = GetComponent<BoxCollider>();
	}

	void Update()
	{
		if (isHit)
		{
			ShortenCollider();
		}
		else
		{
			ExtendCollider();
		}
	}
	void OnTriggerEnter(Collider collider)
	{
		isHit = true;
	}
	void OnTriggerExit(Collider collider)
	{
		isHit = false;
	}

	//伸ばす処理
	void ExtendCollider()
	{
		if(boxCollider.size.x < colliderMaxSize)
		{
			//長さ変更
			Vector3 boxColliderSize = boxCollider.size;
			boxColliderSize.x += addColliderLength;

			if (colliderMaxSize < boxColliderSize.x)
			{
				boxColliderSize.x = colliderMaxSize;
			}

			//中心点変更
			Vector3 boxColliderCenter = boxCollider.center;
			boxColliderCenter.x = boxColliderSize.x / 2.0f;

			//数値更新
			boxCollider.size = boxColliderSize;
			boxCollider.center = boxColliderCenter;
		}

		ShortenParticle();
	}

	//縮める処理
	void ShortenCollider()
	{
		if (0 < boxCollider.size.x)
		{
			//長さ変更
			Vector3 boxColliderSize = boxCollider.size;
			boxColliderSize.x -= addColliderLength;

			if ( boxColliderSize.x < 0)
			{
				boxColliderSize.x = 0;
			}

			//中心点変更
			Vector3 boxColliderCenter = boxCollider.center;
			boxColliderCenter.x = boxColliderSize.x / 2.0f;

			//数値更新
			boxCollider.size = boxColliderSize;
			boxCollider.center = boxColliderCenter;
		}

		ShortenParticle();
	}

	//particleの長さ変更
	void ShortenParticle()
	{
		for(int i = 0;i< particleSystemList.Count; i++)
		{
			float startLifetime = boxCollider.size.x / colliderMaxSize;
			particleSystemList[i].startLifetime = startLifetime;
		}
	}
}
