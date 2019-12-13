using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserColliderManager : MonoBehaviour
{
	//コライダーの任意方向への変化のステータス
	[System.Serializable]
	public struct CoordinateChangeStatus
	{
		public AnimationCurve colliderSize;     //任意方向へサイズ変化のカーブ
		public bool coliderCenterFixed;         //コライダーの中心の固定の可否
		public float coordinateFixed;           //固定する場合のコライダーの中心の座標
		public float maxColliderSize;           //コライダーの最大サイズ
	}

	public float playTime;              //全体の再生時間
	public float elapsedTime;           //経過時間

	public bool isLoop;                 //loopの可否

	[SerializeField]
	public CoordinateChangeStatus xDirectionCoordinateChangeStatus; //X方向
	[SerializeField]
	public CoordinateChangeStatus yDirectionCoordinateChangeStatus; //Y方向
	[SerializeField]
	public CoordinateChangeStatus zDirectionCoordinateChangeStatus; //Z方向

	private BoxCollider boxCollider;     //コライダーコンポーネント

	void Start()
	{
		elapsedTime = 0.0f;
		boxCollider = GetComponent<BoxCollider>();

		xDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
		yDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
		zDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
	}

	void Update()
	{
		//経過時間の加算
		elapsedTime += Time.deltaTime;

		//長さ変更処理
		//経過時間に対応したグラフの数値を取得
		Vector3 boxColliderSize;
		boxColliderSize.x = (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		boxColliderSize.y = (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		boxColliderSize.z = (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		boxCollider.size = boxColliderSize;

		//最大値の保存
		if (xDirectionCoordinateChangeStatus.maxColliderSize < (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			xDirectionCoordinateChangeStatus.maxColliderSize = (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		if (yDirectionCoordinateChangeStatus.maxColliderSize < (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			yDirectionCoordinateChangeStatus.maxColliderSize = (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		if (zDirectionCoordinateChangeStatus.maxColliderSize < (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			zDirectionCoordinateChangeStatus.maxColliderSize = (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);

		//中心点変更処理
		Vector3 boxColliderCenter = boxColliderSize / 2.0f;

		//中心点固定
		if (xDirectionCoordinateChangeStatus.coliderCenterFixed) boxColliderCenter.x = xDirectionCoordinateChangeStatus.coordinateFixed;
		if (yDirectionCoordinateChangeStatus.coliderCenterFixed) boxColliderCenter.y = yDirectionCoordinateChangeStatus.coordinateFixed;
		if (zDirectionCoordinateChangeStatus.coliderCenterFixed) boxColliderCenter.z = zDirectionCoordinateChangeStatus.coordinateFixed;

		//再生前半
		if (playTime / 2.0f >= elapsedTime)
		{
			boxCollider.center = boxColliderCenter;
		}
		//後半
		else if (playTime > elapsedTime)
		{
			if (!xDirectionCoordinateChangeStatus.coliderCenterFixed)
				boxColliderCenter.x = xDirectionCoordinateChangeStatus.maxColliderSize - boxColliderCenter.x;
			if (!yDirectionCoordinateChangeStatus.coliderCenterFixed)
				boxColliderCenter.y = yDirectionCoordinateChangeStatus.maxColliderSize - boxColliderCenter.y;
			if (!zDirectionCoordinateChangeStatus.coliderCenterFixed)
				boxColliderCenter.z = zDirectionCoordinateChangeStatus.maxColliderSize - boxColliderCenter.z;

			boxCollider.center = boxColliderCenter;
		}

		//再生時間時間経過後
		else if (elapsedTime >= playTime)
		{
			//変数リセット
			elapsedTime = 0.0f;
			xDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
			yDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
			zDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;

			if (!isLoop)
			{
				//非表示にして待機
				gameObject.SetActive(false);
			}
		}
	}
}
