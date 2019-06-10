//4つのビットを生成するスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBit : MonoBehaviour
{
	GameObject playerObj;
	GameObject[] tagObjects;

	GameObject bitObj;
	GameObject bitSet;
	public GameObject bits;

	//public GameObject bit_Up;
	//public GameObject bit_Left;
	//public GameObject bit_Under;
	//public GameObject bit_Right;

	//public int num;

	Bit_Move bm1;
	Bit_Move bm2;

	void Start()
	{
		//bit_Up = Resources.Load("Bit_Top") as GameObject;
		//bit_Left = Resources.Load("Bit_Left") as GameObject;
		//bit_Under = Resources.Load("Bit_Under") as GameObject;
		//bit_Right = Resources.Load("Bit_Right") as GameObject;
		bits = Resources.Load("TwoBits") as GameObject;

	}

	void Update()
	{
		//if (playerObj == null)
		//{
		//	playerObj = GameObject.Find("Player_Demo_1(Clone)");
		//}
		if (Input.GetKeyDown(KeyCode.B)|| Input.GetButtonDown("Fire4"))
		{
			Instantiate(bits, transform.position, transform.rotation);
			//Create();
		}
	}

	void Create()
	{
		//Instantiate(bits, playerObj.transform.position, playerObj.transform.rotation);
		//	switch(num)
		//	{
		//		case 0:
		//			Instantiate(bit_Up, transform.position, transform.rotation);
		//			num++;
		//			break;

		//		case 1:

		//			bitSet = Instantiate(bit_Left, transform.position, transform.rotation);
		//			num++;
		//			break;

		//		case 2:
		//			bitSet = Instantiate(bit_Under, transform.position, transform.rotation);
		//			num++;
		//			break;

		//		case 3:
		//			bitSet = Instantiate(bit_Right, transform.position, transform.rotation);
		//			num++;
		//			break;

		//		default:
		//			break;
		//	}
	}
}
