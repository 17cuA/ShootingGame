/*	久保田達己	
 *	オブジェクトプーリングするためのスクリプト
 *	
 *	更新履歴：
 *	2019/06/02	まず初めに敵キャラをオブジェクトプーリングする。
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
	
	public int create_Num;	//敵キャラの生成数（inspector側にて設定）
	private List<GameObject> Enemy_obj = new List<GameObject>();
	private GameObject EnemyPrefab;	//敵キャラのプレハブを格納
	void Start()
	{
		EnemyPrefab = Resources.Load("Enemy/Enemy2") as GameObject;		//敵キャラのプレハブを取得
		//敵キャラのオブジェクトを生成と同時にリストに格納
		for(int i = 0; i < create_Num; i++)
		{
			Enemy_obj.Add(Instantiate(EnemyPrefab, gameObject.transform.position, Quaternion.identity));
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void EnemyPos_Conversion(int num,Vector3 pos)
	{
		Enemy_obj[num].transform.position = pos;
	}
}
