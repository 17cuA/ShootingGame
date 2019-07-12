//単体でアイテムを落とす敵用のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class DropItem : MonoBehaviour
{
	public GameObject item;
	Vector3 itemPos;
    public bool isQuitting = false;

    private void Awake()
    {
        item = Resources.Load("Item/Item_Test") as GameObject;
        isQuitting = true;
    }

    private void OnEnable()
    {
        isQuitting = false;
    }

    void Start()
    {
        isQuitting = true;
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
        {
            Instantiate(item, itemPos, transform.rotation);
            //Object_Instantiation.Object_Reboot("PowerUP_Item", itemPos, transform.rotation);
        }
    }

}
