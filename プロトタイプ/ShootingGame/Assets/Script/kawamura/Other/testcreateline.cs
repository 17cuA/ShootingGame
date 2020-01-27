using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcreateline : MonoBehaviour
{
	public GameObject obj;
	
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Createeeeeeeeeeeeeeeee()
	{
		Instantiate(obj, transform.position, transform.rotation);
	}
}
