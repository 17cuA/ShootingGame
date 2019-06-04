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
using System.Linq;
using UnityEngine;

public class Object_Pooling
{
	//生成するキャラクタを入れるリスト
	List<GameObject> obj = new List<GameObject>();
	GameObject z = new GameObject();        //親オブジェクトの作成
	string obj_name;
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="Create_obj">生成したいオブジェクト</param>
	/// <param name="Create_num">生成したい数</param>
	/// <param name="name">そのオブジェクトの名前</param>
	public Object_Pooling(GameObject Create_obj, int Create_num, string name)
	{
		//親オブジェクトの名前を変更
		z.name = "ObjectPooling";
		//生成したい数だけ処理を回す
		for (int i = 0; i < Create_num; i++)
		{
			GameObject gameObject = Object.Instantiate(Create_obj);
			gameObject.name = name;		//名前を変更	
			gameObject.transform.parent = z.transform;			//生成してある親オブジェクトの子供にする
			gameObject.SetActive(false);									//SetActiveをfalseにして、動かなく、表示されないようにした。
			obj.Add(gameObject);												// 生成をした値を配列に入れる
		}
	}

	public List<GameObject> Get_Obj()
	{
		return obj;
	}
	public void Active_Obj(int element)
	{
		obj[element].SetActive(true);

		if(element >obj.Count())
		{
			int num = element - obj.Count();
			for(int i = 0; i < num;i++)
			{
				//Create_newObj(obj_name, obj[0], z);
			}
		}
	}

	public void Delete_Obj(int element)
	{
		obj[element].SetActive(false);
	}
	/// <summary>
	/// オブジェクトを生成して、各種設定したのちリストに追加する処理
	/// </summary>
	/// <param name="name">オブジェクトに入れる名前</param>
	/// <param name="Create_Obj">作成するオブジェクト</param>
	/// <param name="Parent_Obj">親オブジェクトにするもの</param>
	/// <param name="Is_Active">SetActiveをtrueにするのかfalseにするのかを変更する</param>
	private void Create_newObj(string name ,GameObject Create_Obj,GameObject Parent_Obj,bool Is_Active)
	{
		GameObject gameObject = Object.Instantiate(Create_Obj);
		gameObject.name = name;
		gameObject.transform.parent = Parent_Obj.transform;//生成してある親オブジェクトの子供にする
		gameObject.SetActive(Is_Active);
		obj.Add(gameObject);                                            // 生成をした値を配列に入れる
	}
}