using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChange2 : MonoBehaviour
{
	public GameObject parentObj;

	public FollowGround3 followGround_Script;
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Euler(0, 0, -followGround_Script.groundAngle);
    }
}
