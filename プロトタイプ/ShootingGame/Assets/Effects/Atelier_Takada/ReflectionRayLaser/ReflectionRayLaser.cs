using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionRayLaser : MonoBehaviour
{
	public GameObject reflectionRayLaserManager;        //ReflectionRayLaserの生成先
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
		newLaser.transform.parent = reflectionRayLaserManager.transform;
		rayLaserList.Add(newLaser);
	}

	void Update()
	{
		DebugRay();

		//反射生成
		if (rayLaserList.Count < reflectingCount && rayLaserList[rayLaserList.Count - 1].PassIsHitting())
		{
			CreatRayLaser(rayLaserList[rayLaserList.Count - 1]);
		}

		//レーザーの座標と回転を更新
		UpdateCoordinate();

		//レーザーの削除
		//DeleteRayLaser();

	}

	//レーザーの座標と回転を更新
	void UpdateCoordinate()
	{
		rayLaserList[0].transform.position = transform.position;
		rayLaserList[0].transform.rotation = transform.rotation;

		for (int i = 1; i < rayLaserList.Count; i++)
		{
			rayLaserList[i].transform.position = rayLaserList[i - 1].PassVector().point;

			Vector3 newLaserRotation = rayLaserList[i - 1].transform.localEulerAngles;
			newLaserRotation.z *= -1;
			rayLaserList[i].transform.rotation = Quaternion.Euler
				(
				newLaserRotation.x,
				newLaserRotation.y,
				newLaserRotation.z
				);
		}
	}

	//レーザーの生成
	void CreatRayLaser(RayLaser criteriaRayLaser)
	{
		//レーザーの生成
		RayLaser newLaser;

		newLaser = Instantiate
			(
			rayLaser,
			criteriaRayLaser.transform.position,
			criteriaRayLaser.transform.rotation
			);
		newLaser.transform.parent = reflectionRayLaserManager.transform;
		rayLaserList.Add(newLaser);
	}

	//レーザーの削除
	void DeleteRayLaser()
	{
		int unused = 0;
		for(int i = 0;i<= rayLaserList.Count; i++)
		{
			if (!rayLaserList[i].PassIsHitting())
			{
				unused = i;
				break;
			}
		}

		for (int i = rayLaserList.Count; unused < i; i--)
		{
				Destroy(rayLaserList[i].gameObject);
				rayLaserList.RemoveAt(i);
		}
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
