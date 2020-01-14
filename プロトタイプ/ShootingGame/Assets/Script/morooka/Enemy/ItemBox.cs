//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2020/01/09
//----------------------------------------------------------------------------------------------
// アイテムボックス
//----------------------------------------------------------------------------------------------
//　2020/01/09　破壊時アイテム生成
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class ItemBox : character_status
{
	Vector3[] angles = new Vector3[6]
	{
		new Vector3(0.0f,5.0f,0.0f),
		new Vector3(5.0f,5.0f,0.0f),
		new Vector3(5.0f,0.0f,0.0f),
		new Vector3(5.0f,-5.0f,0.0f),
		new Vector3(0.0f,-5.0f,0.0f),
		new Vector3(-5.0f,-5.0f,0.0f),
	};

	new void Update()
    {
        if(hp < 1)
		{
			// アイテム生成
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position, Quaternion.identity);
			// 弾発射
			foreach (var angle in angles)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, angle);
			}
			Died_Process();
		}

		base.Update();
	}
}
