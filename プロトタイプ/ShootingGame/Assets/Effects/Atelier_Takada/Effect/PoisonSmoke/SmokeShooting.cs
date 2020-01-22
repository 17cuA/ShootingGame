using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeShooting : MonoBehaviour
{
	public GameObject smokePrefab;      //使用する煙のプレハブ
	[SerializeField]
	private float elapsedTime;      //経過時間
	private float interval;         //周期
	public Vector2 intervalRange;   //周期の範囲

	public float coordinateRange;   //横座標の範囲

	public GameObject collectSmoke; //煙をまとめる親
	public float shootingSpeed = 1000;// 発射速度
	public float maxDistance = 100f;    //親との最大距離


	void Start()
	{
		interval = Random.Range(intervalRange.x, intervalRange.y);  //新規インターバル
		elapsedTime = 0.0f;
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;

		if (elapsedTime > interval)
		{
			ShootSmoke();

			interval = Random.Range(intervalRange.x, intervalRange.y);  //新規インターバル
			elapsedTime = 0.0f;
		}
	}
	//射出
	void ShootSmoke()
	{
		//生成座標の差異
		float displacement= Random.Range(coordinateRange,-coordinateRange);
		Vector3 generatingCoordinate = transform.position;
		generatingCoordinate.x += displacement;

		// 弾丸の複製
		GameObject smoke = Instantiate(smokePrefab, generatingCoordinate, transform.rotation);
		//回転をリセット
		smoke.transform.rotation = new Quaternion(0, 0, 0, 0);
		//子にする
		smoke.transform.parent = collectSmoke.transform;
		//データの保存
		smoke.GetComponent<PoisonSmoke>().SetParentData(gameObject, maxDistance,transform.forward * shootingSpeed);
	}
}