//作成日2019/09/05
// オブジェクトストレージの内容物管理
// 作成者:諸岡勇樹
/*
 * 2019/09/13　ボスの終了時ボス用攻撃削除
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStorage_Control : MonoBehaviour
{
	// ボスの情報
	public character_status One_Boss_Data { get; set; }			// 1ボスの情報
	public character_status Tow_Boss_Data { get; set; }			// 2ボスの情報
	public character_status Moai_Boss_Data { get; set; }			// モアイの情報

	// 処理したかどうかの確認用
	private bool Is_Processed_One { get; set; }		// 一ボス用
	private bool Is_Processed_Tow { get; set; }			// 二ボス用
	private bool Is_Processed_Moai { get; set; }			// モアイ用

	private int Frame_cnt { get; set; }     // フレーム別けようのカウント

	private bool Is_Set_Start { get; set; }		// 初期化用

	void Start()
    {
		Is_Set_Start = true;
		Frame_cnt = 0;
	}

	void Update()
    {
		if(Is_Set_Start)
		{
			if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
			{
				One_Boss_Data = Obj_Storage.Storage_Data.GetBoss(1).GetComponent<character_status>();
				Moai_Boss_Data = Obj_Storage.Storage_Data.GetBoss(3).GetComponent<character_status>();
				Tow_Boss_Data = Obj_Storage.Storage_Data.GetBoss(2).GetComponent<character_status>();

				Is_Processed_One = false;
				Is_Processed_Tow = false;
				Is_Processed_Moai = false;
			}
			else
			{
				enabled = false;
			}
			Is_Set_Start = false;
		}

		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
		{
			// 1ボスの削除
			if (One_Boss_Data.Is_Dead && !Is_Processed_One)
			{
				// バウンドバレットの削除
				if (Frame_cnt == 0)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.One_Boss_BousndBullet.Get_Parent_Obj());
					Obj_Storage.Storage_Data.One_Boss_BousndBullet = null;
					Frame_cnt++;
				}
				// レーザーの削除
				else if (Frame_cnt == 1)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.One_Boss_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.One_Boss_Laser = null;
					Frame_cnt = 0;
				}

				Is_Processed_One = true;
			}

			// 2ボスの削除
			if (Tow_Boss_Data.Is_Dead && !Is_Processed_Tow)
			{
				// レーザーの削除
				if (Frame_cnt == 0)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.Two_Boss_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Two_Boss_Laser = null;
					Frame_cnt = 0;
				}

				Is_Processed_Tow = true;
			}

			// モアイの削除
			if (Moai_Boss_Data.Is_Dead && !Is_Processed_Moai)
			{
				// バレットの削除
				if (Frame_cnt == 0)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.Moai_Bullet.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Bullet = null;
					Frame_cnt++;
				}
				// 目レーザーの削除
				else if (Frame_cnt == 1)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.Moai_Eye_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Eye_Laser = null;
					Frame_cnt++;
				}
				// モアイミニの削除
				else if (Frame_cnt == 2)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.Moai_Mini_Group.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Mini_Group = null;
					Frame_cnt++;
				}
				// 口レーザーの削除
				else if (Frame_cnt == 3)
				{
					Destroy_Obj_Set(Obj_Storage.Storage_Data.Moai_Mouth_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Mouth_Laser = null;
					Frame_cnt = 0;
				}

				Is_Processed_Moai = true;
			}
		}
    }

	/// <summary>
	/// 親と子供の削除
	/// </summary>
	/// <param name="obj"></param>
	private void Destroy_Obj_Set( GameObject obj)
	{
		// 子どもの削除
		// 親の削除
		Destroy(obj);
	}
}
