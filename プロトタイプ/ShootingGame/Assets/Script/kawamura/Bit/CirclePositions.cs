using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePositions : MonoBehaviour
{
	GameObject playerObj;
    void Start()
    {
        
    }

    void Update()
    {
		if (playerObj == null)
		{
			playerObj = Obj_Storage.Storage_Data.GetPlayer();
			transform.parent = playerObj.transform;
			transform.position = playerObj.transform.position;
		}

	}
}
