using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_BulletController : MonoBehaviour
{
	public float rotateSpeed;
	public bool isRotateRight = true;


    private void Update()
    {
        if(isRotateRight)
			transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
		else
			transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
    }
}
