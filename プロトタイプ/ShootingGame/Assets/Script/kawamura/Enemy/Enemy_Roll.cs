using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Roll : MonoBehaviour
{
	float rotaX;
	float rotaZ;

	public float rotaX_Value;
	public float rotaZ_Value;

    void Start()
    {
        
    }

    void Update()
    {
		transform.rotation = Quaternion.Euler(rotaX, 0, rotaZ);
		rotaX += rotaX_Value;
		rotaZ += rotaZ_Value;
    }
}
