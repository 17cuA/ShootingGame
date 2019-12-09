using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpireLaser : MonoBehaviour
{
	public BoxCollider boxCollider;		//コライダー


	void Start()
    {
		boxCollider = GetComponent<BoxCollider>();

    }

    void Update()
    {
        
    }
}
