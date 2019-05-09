/*
 * アイテムごとでパワーアップするための性能等を変更できるように作成
 * これを継承するようにスクリプトにつける
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_status : MonoBehaviour
{
	public enum Item_Type
	{
		Shot,
		Bom,
		Shield,
		None,
	}
	public Item_Type Type;
	public float speed;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
