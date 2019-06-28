//4つビットを管理している親をプレイヤーの子にするスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bits : MonoBehaviour
{
	GameObject playerObject;                //プレイヤーのオブジェクトを入れる
	void Start()
	{
		//playerObject = GameObject.FindGameObjectWithTag("Player");
		//transform.position = playerObject.transform.position;
		//transform.parent = playerObject.transform;
		//transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
	}

	void Update()
	{

	}
}
