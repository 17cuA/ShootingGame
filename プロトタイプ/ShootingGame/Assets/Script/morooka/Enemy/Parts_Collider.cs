using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_Collider : MonoBehaviour
{
	private RaycastHit hit;
	private RaycastHit HitObject { get { return hit; } }

	[SerializeField, Tooltip("レイの長さ")]private float RayLength;
	[SerializeField, Tooltip("レイン向き")]private Vector3 RayDirection;

	public bool Is_HitRayCast()
	{
		return Physics.Raycast(transform.position, RayDirection, out hit, RayLength);
	}
}
