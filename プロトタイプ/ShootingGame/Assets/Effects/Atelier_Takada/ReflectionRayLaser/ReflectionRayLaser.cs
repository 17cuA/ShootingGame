using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionRayLaser : MonoBehaviour
{
	public int reflectingCount = 5;   //レーザーの反射回数

	//レーザーのオブジェクト
	public RayLaser rayLaser;
	public List<RayLaser> rayLaserList = new List<RayLaser>();
	float rayLength = 40;   //レイの長さ

	void Start()
	{
		//最初の1つの生成
		RayLaser newLaser;
		newLaser = Instantiate(rayLaser, transform.position, transform.rotation);
		newLaser.transform.parent = transform;
		rayLaserList.Add(newLaser);
	}

	void Update()
	{
		DebugRay();

		//反射生成
		if (rayLaserList.Count < reflectingCount && rayLaserList[rayLaserList.Count - 1].PassIsHitting())
		{
			creatRayLaser(rayLaserList[rayLaserList.Count - 1]);
		}

		//レーザーの座標と回転を更新
		updateCoordinate();
	}

	//レーザーの座標と回転を更新
	void updateCoordinate()
	{
		for (int i = 1; i < rayLaserList.Count; i++)
		{
			rayLaserList[i].transform.position = rayLaserList[i - 1].PassVector().point;

			Quaternion newLaserRotation;
			newLaserRotation = new Quaternion
				(
				0,
				0,
				0,
				0
				);
			rayLaserList[i].transform.localRotation = newLaserRotation;
		}
	}

	//レーザーの生成
	void creatRayLaser(RayLaser criteriaRayLaser)
	{
		//レーザーの生成
		RayLaser newLaser;

		newLaser = Instantiate
			(
			rayLaser,
			criteriaRayLaser.transform.position,
			criteriaRayLaser.transform.rotation
			);
		newLaser.transform.parent = transform;
		rayLaserList.Add(newLaser);
	}

	//自身の向きを表示
	void DebugRay()
	{
		Ray ray;
		//レイの設定
		ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
		//rayの可視化
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayLength, Color.yellow);
	}
}
