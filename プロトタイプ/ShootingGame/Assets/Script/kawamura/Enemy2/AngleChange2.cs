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
	public Vector3 checkNomal;
	//

	Vector3 raypos;	//レイの位置がずれるから調整用

	public bool aaa = false;
	public bool bbb = false;
	public bool ccc = false;
	public bool ddd = false;
	public bool eee = false;
	public bool fff = false;
	public bool ggg = false;
	public bool hhh = false;

	void Start()
	{

	}

	void Update()
	{
		raypos = new Vector3(transform.position.x - 0.0333f, transform.position.y, transform.position.z);
		if (followGround_Script.direcState == FollowGround3.DirectionState.Left)
		{
			if (followGround_Script.isHitP)
			{
				//angleZ = -followGround_Script.groundAngle;
				//angleZ = -followGround_Script.angle_sin;

				if (followGround_Script.normalVector.y > 0 && followGround_Script.normalVector.x > 0)
				{
					angleZ = -followGround_Script.angle_sin;
					aaa = true;
				}
				else if (followGround_Script.normalVector.y < 0 && followGround_Script.normalVector.x > 0)
				{
					bbb = true;
					angleZ = -followGround_Script.groundAngle;
				}
				else if (followGround_Script.normalVector.y < 0 && followGround_Script.normalVector.x < 0)
				{
					ccc = true;

				}
				else if (followGround_Script.normalVector.y > 0 && followGround_Script.normalVector.x < 0)
				{
					ddd = true;
					angleZ = -followGround_Script.angle_cos;
				}
				else if (followGround_Script.normalVector.y == 0 && followGround_Script.normalVector.x > 0)
				{
					eee = true;
					angleZ = -followGround_Script.angle_cos;
				}
				else if (followGround_Script.normalVector.y == 0 && followGround_Script.normalVector.x < 0)
				{
					fff = true;
					angleZ = followGround_Script.angle_cos;
				}
				else if (followGround_Script.normalVector.y > 0 && followGround_Script.normalVector.x == 0)
				{
					ggg = true;
					angleZ = 0;
				}
				else if (followGround_Script.normalVector.y < 0 && followGround_Script.normalVector.x == 0)
				{
					hhh = true;
					angleZ = 180f;
				}

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

		if (Physics.Raycast(raypos, -transform.up, out hit, 0.5f, layerMask))
		{
			//rayDelayCnt++;
			if (hit.collider.tag == "Wall")
			{
				//if (rayDelayCnt > rayDelayMax)
				angleTest = Quaternion.FromToRotation(-transform.up, hit.normal).eulerAngles;
				//checkNomal = hit.normal;
				//angleChange_Script.angleZ = angleZ;


				//if (raytopParent_Script.angleZ != angleZ + 90)
				//{
				//	raytopParent_Script.angleZ = angleZ + 90;
				//}

				Debug.DrawRay(raypos, -transform.up * hit.distance, Color.red);
			}
		}
		else
		{
			Debug.DrawRay(raypos, -transform.up * 0.5F, Color.white);
			followGround_Script.isHitP = false;
		}
	}
}
