/*	久保田達己	
 *	オブジェクトプーリングするためのスクリプト
 *	
 *	更新履歴：
 *	2019/06/02	まず初めに敵キャラをオブジェクトプーリングする。
 *	2019/06/03	プレイヤーをオブジェクトプーリングする。
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
	
	public int create_Num;	//敵キャラの生成数（inspector側にて設定）
	private List<GameObject> Enemy_obj = new List<GameObject>();
	private GameObject Player_obj;  //ゲーム画面のオブジェクトを格納
	private GameObject EnemyPrefab; //敵キャラのプレハブを格納
	private GameObject Player_Prefab;   //プレイヤーのプレハブを格納

	SceneChanger SC;
	void Start()
	{
		EnemyPrefab = Resources.Load("Enemy/Enemy2") as GameObject;		//敵キャラのプレハブを取得
		Player_Prefab = Resources.Load("Player/Player_Item") as GameObject; //プレイヤーのプレハブ取得
		SC = gameObject.GetComponent<SceneChanger>();	//一つのオブジェクトについているほかのスクリプトを取得

		//敵キャラのオブジェクトを生成と同時にリストに格納
		for (int i = 0; i < create_Num; i++)
		{
			Enemy_obj.Add(Instantiate(EnemyPrefab, gameObject.transform.position, Quaternion.identity));
		}
		//プレイヤーを生成し、オブジェクトに格納する
		Player_obj = Instantiate(Player_Prefab, gameObject.transform.position, Quaternion.identity);

		SC.Chara_Get();
	}
	//敵キャラの初期位置変更
	public void EnemyPos_Conversion(int num,Vector3 pos)
	{
		Enemy_obj[num].transform.position = pos;
	}
	//プレイヤーの初期位置変更
	public void PlayrePos_Conversion(Vector3 pos)
	{
		Player_obj.transform.position = pos;
	}
}
