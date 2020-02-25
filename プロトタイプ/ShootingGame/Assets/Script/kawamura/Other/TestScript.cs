using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

	public float xxx;
	public float yyy;
	public float zzz;

	public CharacterController charaCol;
	public Vector3 kakudo = Vector3.zero;

	//試し
	[System.NonSerialized]
	public Vector3 groundNormal = Vector3.zero;

	private Vector3 lastGroundNormal = Vector3.zero;

	[System.NonSerialized]
	public Vector3 lastHitPoint = new Vector3(Mathf.Infinity, 0, 0);

	public float groundAngle = 0;
	//試し終わり


	void Start()
    {
		//// まず地面に向かってレイキャストして衝突点の情報取得
		//RaycastHit hit;
		//Physics.Raycast(transform.position, Vector3.down, out hit);

		//// 高さを地面(衝突点)に合わせる
		//transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
		//// 角度も合わせる
		//transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

		xxx = transform.position.x;
		yyy = transform.position.y;
		zzz = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
		xxx += ControllerDevice.GetAxis("Horizontal", ePadNumber.ePlayer1);
		zzz += ControllerDevice.GetAxis("Vertical", ePadNumber.ePlayer1);
		yyy = Terrain.activeTerrain.SampleHeight(transform.position) + 2.0f;

		transform.position = new Vector3(xxx, yyy, zzz);
	}

	//試し
	void GetGround(ControllerColliderHit hit)
	{
		if (hit.normal.y > 0 && hit.moveDirection.y < 0)
		{
			if ((hit.point - lastHitPoint).sqrMagnitude > 0.001f || lastGroundNormal == Vector3.zero)
			{
				groundNormal = hit.normal;
			}
			else
			{
				groundNormal = lastGroundNormal;
			}

			lastHitPoint = hit.point;
		}
	}
}
