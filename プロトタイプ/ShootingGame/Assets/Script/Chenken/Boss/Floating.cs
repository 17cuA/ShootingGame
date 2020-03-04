using UnityEngine;


public class Floating : MonoBehaviour
{
	private float radian = 0;
	[Range(0.01f,0.03f)] public float perRadian = 0.03f;
	[Range(0.3f,1f)]     public float radius = 0.8f;
	private Vector3 oldPos; 
	

	private void Start()
	{
		oldPos = transform.position; 
	}

	private void Update()
	{
		radian += perRadian;
		float dy = Mathf.Cos(radian) * radius; 
		transform.position = oldPos + new Vector3(0, dy, 0);
	}
}