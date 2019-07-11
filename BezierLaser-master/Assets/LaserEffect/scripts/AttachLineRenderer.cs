using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLineRenderer : MonoBehaviour
{
	public Transform start;
	public Transform target;

	LineRenderer line;
	CapsuleCollider capsule;

	public float LineWidth; // use the same as you set in the line renderer.

	void Start()
	{
		line = GetComponent<LineRenderer>();
		capsule = GetComponent<CapsuleCollider>();
		capsule.radius = LineWidth / 2;
		capsule.center = Vector3.zero;
		capsule.direction = 2; // Z-axis for easier "LookAt" orientation
	}

	void Update()
	{
		line.SetPosition(0, start.position);
		line.SetPosition(1, target.position);

		capsule.transform.position = start.position + (target.position - start.position) / 2;
		capsule.transform.LookAt(start.position);
		capsule.height = (target.position - start.position).magnitude;
	}
}
