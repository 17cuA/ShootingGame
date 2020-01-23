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
	Vector3[] angles = new Vector3[8]
	{
		new Vector3(0.0f,5.0f,0.0f),		// 00:00
		new Vector3(5.0f,5.0f,0.0f),		// 00:30

		new Vector3(5.0f,0.0f,0.0f),		// 03:00
		new Vector3(5.0f,-5.0f,0.0f),		// 04:30

		new Vector3(0.0f,-5.0f,0.0f),		// 06:00
		new Vector3(-5.0f,-5.0f,0.0f),		// 07:30

		new Vector3(-5.0f,0.0f,0.0f),		// 09:00
		new Vector3(-5.0f,5.0f,0.0f),		// 10:30
	};

	public bool Is_LateralRotation { get; set; }                        // 横回転する？

	new void Update()
    {
        if(hp < 1)
		{
			// 5分の1の確率で
			if (Random.Range(0, 4) == 0)
			{
				// アイテム生成
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position, Quaternion.identity);
			}
			// 弾発射
			foreach (var angle in angles)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, angle);
			}
			Died_Process();
		}

		if(Is_LateralRotation)
		{
			transform.Rotate(new Vector3(0.0f,5.0f,0.0f));
		}

		base.Update();
	}
}
