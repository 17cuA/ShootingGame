using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
	public GameObject createObj;
	void Start()
    {
        
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.C))
		{
			Instantiate(createObj, transform.position, transform.rotation);
		}
    }
}
