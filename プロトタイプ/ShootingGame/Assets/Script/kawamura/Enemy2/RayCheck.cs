using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheck : MonoBehaviour
{
	public GameObject topObj;
	Enemy_FollowGround_Ray topParent_Script;

	public GameObject parentObj;
	RayParent raytopParent_Script;

	public GameObject angleObj;
	AngleChange angleChange_Script;


	RaycastHit hit; //ヒットしたオブジェクト情報

	public float rayDelayMax;
	public float rayDelayCnt;

	public Vector3 groundAngle;
	public Vector3 saveAngle;
	public float angleZ;
	string myName;
	public bool isVertical = false;     //縦
	public bool isHorizontal = false;   //横

	void Start()
    {
		//parentObj = transform.parent.gameObject;
		topParent_Script = topObj.GetComponent<Enemy_FollowGround_Ray>();
		raytopParent_Script = parentObj.GetComponent<RayParent>();
		angleChange_Script = angleObj.GetComponent<AngleChange>();

		myName = gameObject.name;

		if (myName == "Col_Top" || myName == "Col_Under")
		{
			isVertical = true;
		}
		else
		{
			isHorizontal = true;
		}
	}

	void Update()
    {
		RayTest();
		rayDelayCnt++;

	}

	void RayTest()
	{
		int layerMask = 1 << 8;

		layerMask = ~layerMask;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.1f, layerMask))
		{
			//rayDelayCnt++;
			if (hit.collider.tag == "Wall")
			{
				//if (rayDelayCnt > rayDelayMax)
				if (angleChange_Script.delayCnt > angleChange_Script.delayMax)
				{
					groundAngle = Quaternion.FromToRotation(transform.forward, hit.normal).eulerAngles;

					if (isVertical)
					{
						angleZ = groundAngle.z - 180.0f;

					}
					else if (isHorizontal)
					{
						angleZ = groundAngle.z - 270.0f;

					}
					//angleChange_Script.angleZ = angleZ;

					if (angleChange_Script.angleZ != angleZ)
					{
						//angleChange_Script.angleZ = angleZ;
						raytopParent_Script.angleZ = groundAngle.z - 180;
						rayDelayCnt = 0;
						angleChange_Script.delayCnt = 0;
					}

					//if (raytopParent_Script.angleZ != angleZ + 90)
					//{
					//	raytopParent_Script.angleZ = angleZ + 90;
					//}

					if (topParent_Script.angle != angleZ)
					{
						topParent_Script.angle = angleZ;
					}
				}
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			}
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.1F, Color.white);
		}
	}
}
