using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_Collider : MonoBehaviour
{
	private RaycastHit hit;

	[SerializeField, Tooltip("レイの長さ")]private float RayLength;

	public bool Is_HitRayCast { get { return Physics.Raycast(transform.position, transform.up, out hit, RayLength); } }
	public RaycastHit HitObject { get { return hit; } }

	private void LateUpdate()
	{
		Debug.DrawRay(transform.position, transform.up, Color.red, RayLength);
	}
}
