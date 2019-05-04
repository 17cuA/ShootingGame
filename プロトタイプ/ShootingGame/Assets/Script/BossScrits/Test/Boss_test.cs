using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test : MonoBehaviour
{
    private float rotatingVelocity = 1.0f;
    private Rigidbody rb;

	private GameObject body_;
	private GameObject tate_1;
	private GameObject tate_2;
	private GameObject tate_3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 vector = rb.angularVelocity;
        vector.z = rotatingVelocity;
        rb.angularVelocity = vector;

		body_ = transform.GetChild(0).gameObject;
	}

    // Update is called once per frame
    void Update()
    {
    }
}
