using UnityEngine;


public class Floating : MonoBehaviour
{
	private float radian = 90;
	[Range(1f,3f)] public float perRadian = 0.03f;
	[Range(0.3f,1f)]     public float radius = 0.8f;
	private Vector3 oldPos;
	private bool isActive = false;

	private void Start()
	{
		
	}

	private void Update()
	{
		if (isActive)
		{
			float dy = Mathf.Cos(radian * Mathf.Deg2Rad) * radius;
			transform.position = oldPos + new Vector3(0, dy, 0);
			radian += perRadian;
		}
	}

	public void Set()
	{
		oldPos = transform.position;
		isActive = true;
	}
}