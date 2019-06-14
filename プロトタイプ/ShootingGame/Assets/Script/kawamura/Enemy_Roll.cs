using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Roll : MonoBehaviour
{
	float rotaZ;
    void Start()
    {
        
    }

    void Update()
    {
		transform.rotation = Quaternion.Euler(0, 0, rotaZ);
		rotaZ += 18.0f;
    }
}
