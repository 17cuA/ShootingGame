using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
	public float speed = 5f;

    void Update()
    {
		transform.position += Vector3.right * Time.deltaTime * speed;
    }

	private void OnTriggerEnter(Collider col)
	{
		if(col.name == "Hammer_v1.0")
		{
			Destroy(gameObject);
		}
	}
}
