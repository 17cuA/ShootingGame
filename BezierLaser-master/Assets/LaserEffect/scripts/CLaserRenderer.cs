using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CBezier))]
public class CLaserRenderer : MonoBehaviour {

	[SerializeField]
	int node_count;
	public Transform start;
	public Transform target;

	LineRenderer line_renderer;
	CapsuleCollider capsule;

	public float LineWidth;

	CBezier bezier;

	void Awake()
	{
		line_renderer = gameObject.GetComponent<LineRenderer> ();
		bezier = gameObject.GetComponent<CBezier> ();
		capsule = gameObject.AddComponent<CapsuleCollider>();
		capsule.radius = LineWidth / 2;
		capsule.center = Vector3.zero;
		capsule.direction = 2; // Z-axis for easier "LookAt" orientation
		set_vertex_count (node_count + 1);
	}


	void set_vertex_count(int count)
	{
		line_renderer.positionCount=count;
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i <= node_count; ++i)
		{
			Vector3 to = bezier.bezier(i / (float)node_count);
			line_renderer.SetPosition (i, to);
		}
		line_renderer.SetPosition(0, start.position);
		line_renderer.SetPosition(1, target.position);
		capsule.transform.position = start.position + (target.position - start.position) / 2;
		capsule.transform.LookAt(start.position);
		capsule.height = (target.position - start.position).magnitude;
	}
}
