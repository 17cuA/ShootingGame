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
    new void Update()
    {
        if(hp < 1)
		{
			Died_Process();
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position, Quaternion.identity);
		}

		base.Update();
	}
}
