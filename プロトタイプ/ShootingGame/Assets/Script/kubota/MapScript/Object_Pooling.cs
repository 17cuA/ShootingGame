/*	久保田達己	
 *	オブジェクトプーリングするためのスクリプト
 *	
 *	更新履歴：
 *	2019/06/02	まず初めに敵キャラをオブジェクトプーリングする。
 *	2019/06/03	プレイヤーをオブジェクトプーリングする。
 *				作り直し。
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object_Pooling
{
	List<GameObject> obj = new List<GameObject>();

	public Object_Pooling(GameObject Create_obj, int Create_num, string name)
	{
		for (int i = 0; i < Create_num; i++)
		{
			GameObject z = new GameObject();
			z.name = "ObjectPooling";
			GameObject gameObject = Object.Instantiate(Create_obj);
			gameObject.name = name;
			gameObject.transform.parent = z.transform;
			gameObject.SetActive(false);
			obj.Add(gameObject);
		}

	}
}