using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColCheck : MonoBehaviour
{

	public bool isCheck = false;
	public int hitCnt;
    void Start()
    {
		hitCnt = 0;
    }


    void Update()
    {
		if (hitCnt > 0)
		{
			isCheck = true;
		}
		else
		{
			isCheck = false;
		}
    }


	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			//isCheck = true;
			hitCnt++;
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
			//isCheck = false;
			hitCnt--;
		}
	}


}
