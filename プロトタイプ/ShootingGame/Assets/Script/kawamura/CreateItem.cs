using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
	public int childNum;
    void Start()
    {
		childNum = transform.childCount;
    }

    void Update()
    {
        
    }
}
