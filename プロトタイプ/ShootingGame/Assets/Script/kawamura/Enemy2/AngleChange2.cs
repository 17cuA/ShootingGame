using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChange2 : MonoBehaviour
{
	public GameObject parentObj;

	public FollowGround3 followGround_Script;
	public float angleZ;
	public float angleZ_ChangeValue;


	//
	RaycastHit hit; //ヒットしたオブジェクト情報
	public Vector3 angleTest;
	public Vector3 rayDirection;
	//

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (followGround_Script.direcState == FollowGround3.DirectionState.Left)
		{
			if (followGround_Script.isHitP)
			{
				angleZ = -followGround_Script.groundAngle;
			}
			else
			{
				angleZ += angleZ_ChangeValue;
				followGround_Script.groundAngle = angleZ;
			}
		}
		else if (followGround_Script.direcState == FollowGround3.DirectionState.Right)
		{
			if (followGround_Script.isHitP)
			{
				angleZ = followGround_Script.groundAngle;
			}
			else
			{
				angleZ -= angleZ_ChangeValue;
				followGround_Script.groundAngle = angleZ;
			}
		}
		transform.rotation = Quaternion.Euler(0, 0, angleZ);

		RayTest();
	}

		void RayTest()
	{
		int layerMask = 1 << 8;

		layerMask = ~layerMask;

		if (Physics.Raycast(transform.position, -transform.up, out hit,0.5f, layerMask))
		{
			//rayDelayCnt++;
			if (hit.collider.tag == "Wall")
			{
				//if (rayDelayCnt > rayDelayMax)
					angleTest = Quaternion.FromToRotation(-transform.up, hit.normal).eulerAngles;

					//angleChange_Script.angleZ = angleZ;


					//if (raytopParent_Script.angleZ != angleZ + 90)
					//{
					//	raytopParent_Script.angleZ = angleZ + 90;
					//}

				Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
			}
		}
		else
		{
			Debug.DrawRay(transform.position, -transform.up * 0.5F, Color.white);
		}
	}
}
