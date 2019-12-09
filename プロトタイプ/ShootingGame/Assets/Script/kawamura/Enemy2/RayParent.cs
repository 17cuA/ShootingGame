using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayParent : MonoBehaviour
{
	public float angleZ;


    void Start()
    {
        
    }

    void Update()
    {
		transform.rotation = Quaternion.Euler(0, 0, angleZ);
	}
}
