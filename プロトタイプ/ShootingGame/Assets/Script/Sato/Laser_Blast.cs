using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Blast : MonoBehaviour
{
	public GameObject laser;
	public bool flag = false;
    // Update is called once per frame
    void Update()
    {
		//if (Input.GetMouseButton(1))
		//{
		//	Instantiate(laser,       
		//	transform.position,
		//	transform.rotation);
		//}
		if (Input.GetMouseButton(1))
		{
			flag = true;
		}
		else if (Input.GetMouseButton(0))
		{
			flag = false;
		}
		if (flag==true)
		{
			blast();
		}
	}
	void blast()
	{
		Instantiate(laser,
		transform.position,
		transform.rotation);
	}
}
