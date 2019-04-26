using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test : MonoBehaviour
{
    float rotatingVelocity = 1.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 vector = rb.angularVelocity;
        vector.z = rotatingVelocity;
        rb.angularVelocity = vector;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
