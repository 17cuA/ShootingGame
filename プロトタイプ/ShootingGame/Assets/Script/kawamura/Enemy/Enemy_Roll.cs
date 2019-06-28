using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Roll : MonoBehaviour
{
	public float rotaX;
	public float rotaY;
	public float rotaZ;

	public float rotaX_Value = 0;
	public float rotaY_Value = 0;
	public float rotaZ_Value = 0;

    void Start()
    {
		rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;
		rotaZ = transform.eulerAngles.z;
    }

    void Update()
    {
		transform.rotation = Quaternion.Euler(rotaX, rotaY, rotaZ);

		rotaX += rotaX_Value;
		rotaY += rotaY_Value;
		rotaZ += rotaZ_Value;
    }
}
