//単体でアイテムを落とす敵用のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	public GameObject item;
	Vector3 itemPos;
	bool isQuitting=false;
	void Start()
    {
		item = Resources.Load("Item/Item_Test") as GameObject;

	}

	void Update()
    {
		//アイテムの生成位置更新
		itemPos = transform.position;
    }
	void OnApplicationQuit()

	{

		isQuitting = true;

	}
	private void OnDisable()
	{
		if(!isQuitting)
		Instantiate(item, itemPos, transform.rotation);
	}

}
