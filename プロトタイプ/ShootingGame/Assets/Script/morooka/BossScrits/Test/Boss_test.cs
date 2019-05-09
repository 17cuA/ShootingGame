using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test : MonoBehaviour
{
	private Rigidbody rb2;
	private float speed = 1.0f;

    void Start()
    {
		rb2 = GetComponent<Rigidbody>();
		Vector3 vector3 = transform.right.normalized * speed * -1;
		rb2.velocity = vector3;
	}

    void Update()
    {
		if(transform.position.x <= 8.5f)
		{
			rb2.velocity = Vector3.zero;
		}
    }
}
