using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheck : MonoBehaviour
{
	RaycastHit hit; //ヒットしたオブジェクト情報

	public Vector3 groundAngle;

	void Start()
    {
        
    }

    void Update()
    {

		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask = 1 << 8;

		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		layerMask = ~layerMask;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.0f, layerMask))
		{

			if (hit.collider.tag == "Player")
			{
				groundAngle = Quaternion.FromToRotation(transform.forward, hit.normal).eulerAngles;
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

			}
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1.0F, Color.white);
		}

	}
}
