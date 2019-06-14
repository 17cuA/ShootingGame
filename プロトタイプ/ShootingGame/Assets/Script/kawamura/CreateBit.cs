//4つのビットを生成するスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBit : MonoBehaviour
{
	GameObject playerObj;
	GameObject[] tagObjects;
	GameObject[] followPosObjcs = new GameObject[4];

	GameObject bitObj;
	GameObject bitSet;
	//public GameObject bits;

	//Vector3[] followPositions = new Vector3[4];

	public GameObject bit_First;
	public GameObject bit_Second;
	public GameObject bit_Third;
	public GameObject bit_Fourth;

	public int num;

	Bit_Move bm1;
	Bit_Move bm2;

	void Start()
	{
		bit_First  = Resources.Load("Bit_First") as GameObject;
		bit_Second = Resources.Load("Bit_Second") as GameObject;
		bit_Third  = Resources.Load("Bit_Third") as GameObject;
		bit_Fourth = Resources.Load("Bit_Fourth") as GameObject;
		//bits = Resources.Load("TwoBits") as GameObject;

		followPosObjcs[0] = GameObject.Find("FollowPosFirst");
		followPosObjcs[1] = GameObject.Find("FollowPosSecond");
		followPosObjcs[2] = GameObject.Find("FollowPosThird");
		followPosObjcs[3] = GameObject.Find("FollowPosFourth");

	}

	void Update()
	{
		//if (playerObj == null)
		//{
		//	playerObj = GameObject.Find("Player_Demo_1(Clone)");
		//}
		if (Input.GetKeyDown(KeyCode.B)|| Input.GetButtonDown("Fire4"))
		{

			//Instantiate(bits, transform.position, transform.rotation);
			Create();
		}
	}

	void Create()
	{
		//Instantiate(bits, playerObj.transform.position, playerObj.transform.rotation);
		switch (num)
		{
			case 0:
				Instantiate(bit_First, followPosObjcs[0].transform.position, transform.rotation);
				num++;
				break;

			case 1:

				bitSet = Instantiate(bit_Second, followPosObjcs[0].transform.position, transform.rotation);
				num++;
				break;

			case 2:
				bitSet = Instantiate(bit_Third, followPosObjcs[0].transform.position, transform.rotation);
				num++;
				break;

			case 3:
				bitSet = Instantiate(bit_Fourth, followPosObjcs[0].transform.position, transform.rotation);
				num++;
				break;

			default:
				break;
		}
	}
}
