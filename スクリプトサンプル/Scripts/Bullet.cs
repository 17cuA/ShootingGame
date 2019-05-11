using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	int frame;
	Vector3 velodity = Vector3.zero;
	public void Init(Vector3 direction) { velodity = direction * 5f; }
	private void Update()
	{
		transform.localPosition += velodity * Time.deltaTime;
		if (++frame > 30)
		{
			Destroy(gameObject);
		}
	}
}
