using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_FollowPos : MonoBehaviour
{
	GameObject playerObj;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj==null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");
			transform.position=playerObj.transform.position;

		}
    }
}
