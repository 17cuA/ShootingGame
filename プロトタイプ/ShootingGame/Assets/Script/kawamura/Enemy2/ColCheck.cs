using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColCheck : MonoBehaviour
{

	public bool isCheck = false;
    void Start()
    {
        
    }


    void Update()
    {
        
    }


	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			isCheck = true;
		}
	}
	private void OnTriggerStay(Collider col)
	{
		//if (col.gameObject.tag == "Player")
		//{
		//	isCheck = true;
		//}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			isCheck = false;
		}
	}


}
