﻿//作成日2019/09/05
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
	public EnemyCreate EnemyCreate_Data;        // エネミークリエイトの情報

	// ボスの情報
	public character_status One_Boss_Data { get; set; }			// 1ボスの情報
	public character_status Tow_Boss_Data { get; set; }			// 2ボスの情報
	public character_status Moai_Boss_Data { get; set; }		// モアイの情報

	// 処理したかどうかの確認用
	private bool Is_Processed_One { get; set; }		// 一ボス用
	private bool Is_Processed_Tow { get; set; }			// 二ボス用
	private bool Is_Processed_Moai { get; set; }			// モアイ用
	private bool Is_Processed_Normal { get; set; }		// ふつうの敵用

	private int Boss_Frame_Cnt { get; set; }     // フレーム別けようのカウント(ボス)
	private int Normal_Frame_Cnt { get; set; }		// エネミー軍の削除用
	private bool Is_Set_Start { get; set; }     // 初期化用

	void Start()
    {
		Is_Set_Start = true;
		Boss_Frame_Cnt = 0;
		Normal_Frame_Cnt = 0;
	}

	void Update()
    {
		// 後初期化
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

		// ステージ1のとき
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
		{
			#region ボスの削除
			// 1ボスの削除
			if (One_Boss_Data.Is_Dead && !Is_Processed_One)
			{
				// バウンドバレットの削除
				if (Boss_Frame_Cnt == 0)
				{
					Destroy(Obj_Storage.Storage_Data.One_Boss_BousndBullet.Get_Parent_Obj());
					Obj_Storage.Storage_Data.One_Boss_BousndBullet = null;
					Boss_Frame_Cnt++;
				}
				// レーザーの削除
				else if (Boss_Frame_Cnt == 1)
				{
					Destroy(Obj_Storage.Storage_Data.One_Boss_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.One_Boss_Laser = null;
					Boss_Frame_Cnt = 0;
					Is_Processed_One = true;
				}
			}

			// 2ボスの削除
			if (Tow_Boss_Data.Is_Dead && !Is_Processed_Tow)
			{
				// レーザーの削除
				if (Boss_Frame_Cnt == 0)
				{
					Destroy(Obj_Storage.Storage_Data.Two_Boss_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Two_Boss_Laser = null;
					Is_Processed_Tow = true;
					Boss_Frame_Cnt = 0;
				}
			}

			// モアイの削除
			if (Moai_Boss_Data.Is_Dead && !Is_Processed_Moai)
			{
				// バレットの削除
				if (Boss_Frame_Cnt == 0)
				{
					Destroy(Obj_Storage.Storage_Data.Moai_Bullet.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Bullet = null;
					Boss_Frame_Cnt++;
				}
				// 目レーザーの削除
				else if (Boss_Frame_Cnt == 1)
				{
					Destroy(Obj_Storage.Storage_Data.Moai_Eye_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Eye_Laser = null;
					Boss_Frame_Cnt++;
				}
				// モアイミニの削除
				else if (Boss_Frame_Cnt == 2)
				{
					Destroy(Obj_Storage.Storage_Data.Moai_Mini_Group.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Mini_Group = null;
					Boss_Frame_Cnt++;
				}
				// 口レーザーの削除
				else if (Boss_Frame_Cnt == 3)
				{
					Destroy(Obj_Storage.Storage_Data.Moai_Mouth_Laser.Get_Parent_Obj());
					Obj_Storage.Storage_Data.Moai_Mouth_Laser = null;
					Is_Processed_Moai = true;
					Boss_Frame_Cnt = 0;
				}
			}
			#endregion

			#region ラスボス前に消すもの
			if(EnemyCreate_Data.frameCnt >= 16368 && !Is_Processed_Normal)
			{
				// 隕石削除
				if (Normal_Frame_Cnt == 0) Des_Obj(ref Obj_Storage.Storage_Data.boundMeteors);
				// 突進型削除
				else if (Normal_Frame_Cnt == 1) Des_Obj(ref Obj_Storage.Storage_Data.ClamChowderType_Enemy);
				// UFO型
				else if (Normal_Frame_Cnt == 2) Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group);
				// UFO型アイテム
				else if (Normal_Frame_Cnt == 3) Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot);
				// ハエ型１種
				else if (Normal_Frame_Cnt == 4) Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide);
				// ハエ型2種
				else if (Normal_Frame_Cnt == 5) Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item);
				// ビートル型1種
				else if (Normal_Frame_Cnt == 6) Des_Obj(ref Obj_Storage.Storage_Data.enemy_BeetleGroup);
				//　突進型2種
				else if (Normal_Frame_Cnt == 7) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top);
				//　突進型3種
				else if (Normal_Frame_Cnt == 8) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under);
				//　突進型4種
				else if (Normal_Frame_Cnt == 9) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp);
				//　突進型5種
				else if (Normal_Frame_Cnt == 10) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown);
				//　突進型6種
				else if (Normal_Frame_Cnt == 11) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three);
				//　突進型7種
				else if (Normal_Frame_Cnt == 12) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item);
				//　突進型8種
				else if (Normal_Frame_Cnt == 13) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp);
				//　突進型9種
				else if (Normal_Frame_Cnt == 14) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp);
				else if (Normal_Frame_Cnt == 15) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp);
				else if (Normal_Frame_Cnt == 16) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown);
				else if (Normal_Frame_Cnt == 17) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item);
				else if (Normal_Frame_Cnt == 18) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item);
				else if (Normal_Frame_Cnt == 19) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four);
				else if (Normal_Frame_Cnt == 20) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem);
				else if (Normal_Frame_Cnt == 21) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five);
				else if (Normal_Frame_Cnt == 22) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem);
				else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven);
				else if (Normal_Frame_Cnt == 24) Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight);
				else if (Normal_Frame_Cnt == 25) Des_Obj(ref Obj_Storage.Storage_Data.UfoType_Enemy);
				else if (Normal_Frame_Cnt == 26) Des_Obj(ref Obj_Storage.Storage_Data.UfoType_Item_Enemy);
				else if (Normal_Frame_Cnt == 27) Des_Obj(ref Obj_Storage.Storage_Data.UfoMotherType_Enemy);
				else if (Normal_Frame_Cnt == 28) Des_Obj(ref Obj_Storage.Storage_Data.ClamChowderType_Enemy);
				else if (Normal_Frame_Cnt == 29) Des_Obj(ref Obj_Storage.Storage_Data.OctopusType_Enemy);
				else if (Normal_Frame_Cnt == 30) Des_Obj(ref Obj_Storage.Storage_Data.BeelzebubType_Enemy);
				else if (Normal_Frame_Cnt == 31) Des_Obj(ref Obj_Storage.Storage_Data.BattleShipType_Enemy);
				else if (Normal_Frame_Cnt == 32) Des_Obj(ref Obj_Storage.Storage_Data.StarFish_Enemy);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);
				//else if (Normal_Frame_Cnt == 23) Des_Obj(ref Obj_Storage.Storage_Data.);

			}
			#endregion
		}
	}

	private void Des_Obj(ref Object_Pooling poo)
	{
		Destroy(poo.Get_Parent_Obj());
		poo = null;
		Normal_Frame_Cnt++;
	}
}
