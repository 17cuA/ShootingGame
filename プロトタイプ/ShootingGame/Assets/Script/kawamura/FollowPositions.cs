using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositions : MonoBehaviour
{
	GameObject playerObj;

	bool check = false;
    void Start()
    {
        
    }

	void Update()
    {
		if(playerObj==null)
		{
			if(GameObject.FindGameObjectWithTag("Player"))
			{
				playerObj = GameObject.FindGameObjectWithTag("Player");
				check = true;
			}
		}
        

		if(check)
		{
			if(Input.GetButtonUp("Bit_Freeze"))
			{
				transform.parent = null;
			}
			else if (Input.GetButton("Bit_Freeze"))
			{
				transform.parent = playerObj.transform;
			}

		}
	}
}
