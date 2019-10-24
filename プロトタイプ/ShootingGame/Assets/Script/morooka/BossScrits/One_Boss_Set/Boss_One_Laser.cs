//作成日2019/08/05
// 一面のボスのレーザー
// 作成者:諸岡勇樹
/*
 * 2019/07/30　レーザーの挙動
 * 2019/09/07　フレームのレーザー状態の追加
 * 2019/10/17　レーザーの軽量化：void Update()の呼び出し数の減量
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Boss_One_Laser : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数

	public bool IsShoot { get; set; }										// 撃ってよいか
	private List<GameObject> RealityObjects { get; set; }       // レーザーの実態部分
	private Vector3 dir;

	private void Start()
	{
		RealityObjects = new List<GameObject>();
		IsShoot = false;
	}

	void Update()
	{
		// レーザーの打ち出し
		if (IsShoot)
		{
			dir = transform.right;
			dir.z = 0.0f;

			// オブジェクト生成 ------------------------------------------------
			var obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, transform.position, dir);
			RealityObjects.Add(obj);
			obj.transform.parent = transform;
			obj.transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);
			//--------------------------------------------------------------------
		}

		// レーザーのが存在するとき
		if (RealityObjects.Count > 0)
		{ 
			// 移動 --------------------------------------------------------------
			foreach (var obj in RealityObjects)
			{
				Vector3 temp = obj.transform.localPosition;
				temp.x += shot_speed;
				obj.transform.localPosition = temp;

				temp = obj.transform.position;
				temp.z = 0.0f;
				obj.transform.position = temp;
			}
			//--------------------------------------------------------------------

			// 画面外判定 -------------------------------------------------------
			// リストから除外するものリスト
			List<GameObject> tempList = new List<GameObject>();

			// 削除対象検索
			foreach (var obj in RealityObjects)
			{
				// 画面外の時
				if (obj.transform.position.x >= 23.5f || obj.transform.position.x <= -23.5f
						|| obj.transform.position.y >= 8.5f || obj.transform.position.y <= -8.5f)
				{
					// リストから除外するものリストに設定
					tempList.Add(obj);
				}
			}

			// 対象の削除
			foreach(var obj in tempList)
			{                   // 親を戻す
				GameObject temp = obj;
				Obj_Storage.Storage_Data.One_Boss_Laser.Set_Parent_Obj(ref temp);
				// 非アクティブにする
				obj.gameObject.SetActive(false);

				RealityObjects.Remove(obj);
			}
			tempList.Clear();
			//--------------------------------------------------------------------
		}
	}
}
