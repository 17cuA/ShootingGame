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
	public EnemyCreate EnemyCreate_Data;        // エネミークリエイトの情報

	// ボスの情報
	public character_status Medium_Boss_Data { get; set; }		// 中ボスの情報
	public character_status One_Boss_Data { get; set; }			// 1ボスの情報
	public character_status Tow_Boss_Data { get; set; }			// 2ボスの情報
	public character_status Moai_Boss_Data { get; set; }		// モアイの情報

	private bool Is_Processed_Normal { get; set; }		// ふつうのオブジェクトの削除

	private int Boss_Frame_Cnt { get; set; }     // フレーム別けようのカウント(ボス)
	private int Normal_Frame_Cnt { get; set; }		// エネミー軍の削除用
	private bool Is_Set_Start { get; set; }     // 初期化用
	bool flag;

	private string[] name = new string[9]
	{
		"Enemy_ClamChowder_Group_TenStraight(Clone)",
		"Enemy_ClamChowder_Group_TenStraight(Clone)",
		"BattleshipType_Enemy(Clone)",
		"BattleshipType_Enemy(Clone)",
		"Enemy_ClamChowder_Group_UpSevenDiagonal(Clone)",
		"Enemy_ClamChowder_Group_DownSevenDiagonal(Clone)",
		"BattleshipType_Enemy(Clone)",
		"Enemy_Star_Fish_Spowner(Clone)",
		"Enemy_ClamChowder_Group_ThreeStraight(Clone)",
	};

	void Start()
    {
		Is_Set_Start = true;
		Is_Processed_Normal = false;
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
				Medium_Boss_Data = Obj_Storage.Storage_Data.Boss_Middle.Get_Obj()[0].GetComponent<character_status>();
				One_Boss_Data = Obj_Storage.Storage_Data.GetBoss(1).GetComponent<character_status>();
				Moai_Boss_Data = Obj_Storage.Storage_Data.GetBoss(3).GetComponent<character_status>();
				Tow_Boss_Data = Obj_Storage.Storage_Data.GetBoss(2).GetComponent<character_status>();
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
			// 中ボスの削除
			if (Medium_Boss_Data != null)
			{
				if (Medium_Boss_Data.Is_Dead)
				{
					Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_Middle);
					Boss_Frame_Cnt = 0;
				}
			}

			// 1ボスの削除
			if (One_Boss_Data != null)
			{
				if (One_Boss_Data.Is_Dead)
				{
					// バウンドバレットの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.One_Boss_BousndBullet);
					}
					// レーザーの削除
					else if (Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.One_Boss_Laser);
					}
					else if (Boss_Frame_Cnt == 2)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_1);
						Boss_Frame_Cnt = 0;
					}
				}
			}
			// 2ボスの削除
			if (Tow_Boss_Data != null)
			{
				if (Tow_Boss_Data.Is_Dead)
				{
					// レーザーの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Two_Boss_Laser);
					}
					else if(Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_2);
						Boss_Frame_Cnt = 0;
					}
				}
			}

			// モアイの削除
			if (Moai_Boss_Data != null)
			{
				if (Moai_Boss_Data.Is_Dead)
				{
					// バレットの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Bullet);
					}
					// 目レーザーの削除
					else if (Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Eye_Laser);
					}
					// モアイミニの削除
					else if (Boss_Frame_Cnt == 2)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Mini_Group);
					}
					// 口レーザーの削除
					else if (Boss_Frame_Cnt == 3)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Mouth_Laser);
					}
					else if(Boss_Frame_Cnt == 4)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai);
						Boss_Frame_Cnt = 0;
					}
				}
			}
			#endregion

			#region　無線時
			if (Wireless_sinario.Is_using_wireless && !flag)
			{
				if (Normal_Frame_Cnt == 0)
				{
					foreach (var s in name)
					{
						GameObject obj = GameObject.Find(s);
						if (obj != null)
						{
							Destroy(obj);
						}
					}
					Normal_Frame_Cnt++;
				}
				else if(Normal_Frame_Cnt == 1)
				{
					foreach (var pool in Obj_Storage.Storage_Data.Effects)
					{
						for (int i = 1; i < pool.Get_Obj().Count; i++)
						{
							Destroy(pool.Get_Obj()[i]);
							pool.Get_Obj().RemoveAt(i);
						}
					}
				}


				flag = true;
			}
			else
			{
				flag = false;
			}
			#endregion

			#region ラスボス前に消すもの
			if (EnemyCreate_Data.frameCnt >= 13797 && !Is_Processed_Normal)
			{
				// 隕石削除
				if (Normal_Frame_Cnt == 0)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.boundMeteors);
				}
				// 突進型削除
				else if (Normal_Frame_Cnt == 1)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.ClamChowderType_Enemy);
				}
				// UFO型	
				else if (Normal_Frame_Cnt == 2)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group);
				}
				// UFO型アイテム
				else if (Normal_Frame_Cnt == 3)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot);
				}
				// ハエ型１種
				else if (Normal_Frame_Cnt == 4)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide);
				}
				// ハエ型2種
				else if (Normal_Frame_Cnt == 5)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item);
				}
				// ビートル型1種
				else if (Normal_Frame_Cnt == 6)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_BeetleGroup);
				}
				//　突進型2種	
				else if (Normal_Frame_Cnt == 7)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top);
				}
				//　突進型3種
				else if (Normal_Frame_Cnt == 8)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under);
				}
				//　突進型4種
				else if (Normal_Frame_Cnt == 9)
				{
					Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp);
				}
				//　突進型5種	
				else if (Normal_Frame_Cnt == 10)
				{ Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown); }

				//　突進型6種	
				else if (Normal_Frame_Cnt == 11) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three); }
				//　突進型7種					 {																										   }
				else if (Normal_Frame_Cnt == 12) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item); }
				//　突進型8種					 {																										   }
				else if (Normal_Frame_Cnt == 13) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp); }
				//　突進型9種					 {																										   }
				else if (Normal_Frame_Cnt == 14) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown); }
				else if (Normal_Frame_Cnt == 15) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item); }
				else if (Normal_Frame_Cnt == 16) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item); }
				else if (Normal_Frame_Cnt == 17) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four); }
				else if (Normal_Frame_Cnt == 18) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem); }
				else if (Normal_Frame_Cnt == 19) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five); }
				else if (Normal_Frame_Cnt == 20) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem); }
				else if (Normal_Frame_Cnt == 21) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven); }
				else if (Normal_Frame_Cnt == 22) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight); }
				else if (Normal_Frame_Cnt == 23) { Des_Obj(ref Obj_Storage.Storage_Data.UfoType_Enemy); }
				else if (Normal_Frame_Cnt == 24) { Des_Obj(ref Obj_Storage.Storage_Data.UfoType_Item_Enemy); }
				else if (Normal_Frame_Cnt == 25) { Des_Obj(ref Obj_Storage.Storage_Data.UfoMotherType_Enemy); }
				else if (Normal_Frame_Cnt == 26) { Des_Obj(ref Obj_Storage.Storage_Data.OctopusType_Enemy); }
				else if (Normal_Frame_Cnt == 27) { Des_Obj(ref Obj_Storage.Storage_Data.BeelzebubType_Enemy); }
				else if (Normal_Frame_Cnt == 28) { Des_Obj(ref Obj_Storage.Storage_Data.BattleShipType_Enemy); }
				else if (Normal_Frame_Cnt == 29) { Des_Obj(ref Obj_Storage.Storage_Data.StarFish_Enemy); }
				else if (Normal_Frame_Cnt == 30) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle); }
				else if (Normal_Frame_Cnt == 31) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle_NoItem); }
				else if (Normal_Frame_Cnt == 32) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_EightNormal_Item); }
				else if (Normal_Frame_Cnt == 33) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group_Five); }
				else if (Normal_Frame_Cnt == 34) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beetle_Group_Seven); }
				else if (Normal_Frame_Cnt == 35) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SevenStraight); }
				else if (Normal_Frame_Cnt == 36) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SixStraight); }
				else if (Normal_Frame_Cnt == 37) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_UpSevenDiagonal); }
				else if (Normal_Frame_Cnt == 38) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_DownSevenDiagonal); }
				else if (Normal_Frame_Cnt == 39) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TenStraight); Is_Processed_Normal = true; }
				else if (Normal_Frame_Cnt == 40) { Des_Obj(ref Obj_Storage.Storage_Data.SmallBeam_Bullet_E); }
				else if (Normal_Frame_Cnt == 41) { Des_Obj(ref Obj_Storage.Storage_Data.BattleShipBullet); }
				else if (Normal_Frame_Cnt == 42) { Des_Obj(ref Obj_Storage.Storage_Data.PowerUP_Item); }
				else if (Normal_Frame_Cnt == 43){ Des_Obj(ref Obj_Storage.Storage_Data.Effects[13]);
			}
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
		Debug.Log(poo.Get_Parent_Obj().name);
		Destroy(poo.Get_Parent_Obj().gameObject);
		Normal_Frame_Cnt++;
	}
	private void Des_Obj_B(ref Object_Pooling poo)
	{
		Debug.Log(poo.Get_Parent_Obj().name);
		Destroy(poo.Get_Parent_Obj().gameObject);
		Boss_Frame_Cnt++;
	}
}
