//作成日2019/08/05
// 一面のボスのレーザー
// 作成者:諸岡勇樹
/*
 * 2019/07/30　レーザーの挙動
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_Laser : bullet_status
{
    // Update is called once per frame
    new void Update()
    {
		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
			|| transform.position.y >= 10.5f || transform.position.y <= -10.5f)
		{
			GameObject obj = gameObject;
			Obj_Storage.Storage_Data.One_Boss_Laser.Set_Parent_Obj(ref obj);
			gameObject.SetActive(false);
		}

	}

	private void LateUpdate()
	{
		Vector3 temp = transform.localPosition;
		temp.x += shot_speed;
		transform.localPosition = temp;
	}

	public void Manual_Start(Transform parent)
	{
		transform.parent = parent;
		//Trail.enabled = true;
	}
}
