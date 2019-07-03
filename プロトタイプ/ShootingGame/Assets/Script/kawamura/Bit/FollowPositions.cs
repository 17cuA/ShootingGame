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
			if(GameObject.Find("Player"))
			{
				playerObj = GameObject.Find("Player");
				check = true;
			}
		}
        

		if(check)
		{
			if(Input.GetButtonUp("Bit_Freeze")||Input.GetKeyUp(KeyCode.Y))
			{
				transform.parent = null;
			}
			else if (Input.GetButton("Bit_Freeze") || Input.GetKey(KeyCode.Y))
			{
				transform.parent = playerObj.transform;

			}

		}
	}
}
