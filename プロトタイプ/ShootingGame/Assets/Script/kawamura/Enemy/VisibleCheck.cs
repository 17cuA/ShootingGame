using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleCheck : MonoBehaviour
{
	public Renderer renderer;

	
    void Start()
    {
		renderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        
    }
}
