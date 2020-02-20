using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOperation : MonoBehaviour
{
	static public BulletOperation BP { get; set; }

	private List<Player_Bullet> PlayerBullet;
	private List<bullet_status> OperationTarget;

	private int BulletMax_Player1_Bullet;
	private int BulletMax_Player2_Bullet;
	private int BulletMax_Option1_Bullet;
	private int BulletMax_Option2_Bullet;
	private int BulletMax_Enemy_Bullet;
	private int BulletMax_Beam_Bullet;
	private int BulletMax_CannonBullet;

	void Start()
    {
		BP = GetComponent<BulletOperation>();

		#region リストに追加
		PlayerBullet = new List<Player_Bullet>();
		OperationTarget = new List<bullet_status>();

		PlayerBullet.AddRange(Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));

		OperationTarget.AddRange(Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Enemy_Bullet>(true));
		OperationTarget.AddRange(Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.GetComponentsInChildren<Beam_Bullet>(true));
		OperationTarget.AddRange(Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.GetComponentsInChildren<CannonBullet>(true));

		BulletMax_Player1_Bullet = Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.childCount;
		BulletMax_Player2_Bullet = Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.childCount;
		BulletMax_Option1_Bullet = Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.childCount;
		BulletMax_Option2_Bullet = Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.childCount;
		BulletMax_Enemy_Bullet = Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.childCount;
		BulletMax_Beam_Bullet = Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.childCount;
		BulletMax_CannonBullet = Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.childCount;
		#endregion

		#region プレイヤーの弾
		foreach (var Bullet in PlayerBullet)
		{
			Bullet.Travelling_Direction = Bullet.transform.right;

			switch (Bullet.Bullet_Type)
			{
				case bullet_status.Type.Player1:
					Bullet.P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
					Bullet.Player_Number = 1;
					break;
				case bullet_status.Type.Player2:
					Bullet.P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
					Bullet.Player_Number = 2;
					break;
				case bullet_status.Type.Player1_Option:
					Bullet.bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
					Bullet.Player_Number = 1;
					break;
				case bullet_status.Type.Player2_Option:
					Bullet.bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
					Bullet.Player_Number = 2;
					break;
				default:
					break;
			}

			Bullet.transform.gameObject.tag = "Player_Bullet";
		}
		#endregion
	}

	void LateUpdate()
	{
		// 使用中弾リスト
		var useBulletList = new List<bullet_status>();

		// プレイヤー弾の画面外処理
		foreach (var Bullet in PlayerBullet)
		{
			if (Bullet.transform.gameObject.activeSelf)
			{
				// 画面外に出たとき
				if (Bullet.transform.position.x >= 18.5f || Bullet.transform.position.x <= -18.5f
					|| Bullet.transform.position.y >= 7.5f || Bullet.transform.position.y <= -7.5f)
				{
					switch (Bullet.Bullet_Type)
					{
						case bullet_status.Type.Player1:
							if (Bullet.P1.Bullet_cnt > 0) Bullet.P1.Bullet_cnt--;
							break;
						case bullet_status.Type.Player2:
							if (Bullet.P2.Bullet_cnt > 0) Bullet.P2.Bullet_cnt--;
							break;
						case bullet_status.Type.Player1_Option:
							if (Bullet.bShot.Bullet_cnt > 0) Bullet.bShot.Bullet_cnt--;
							break;
						case bullet_status.Type.Player2_Option:
							if (Bullet.bShot.Bullet_cnt > 0) Bullet.bShot.Bullet_cnt--;
							break;
						default:
							break;
					}
					// 非アクティブ化
					Bullet.gameObject.SetActive(false);
				}
				else
				{
					// 使用中弾リストに追加
					useBulletList.Add(Bullet);
				}
			}
		}

		// その他弾の画面外処理
		foreach (var Bullet in OperationTarget)
		{
			if (Bullet.transform.gameObject.activeSelf)
			{
				// 画面外に出たとき
				if (Bullet.transform.position.x >= 18.5f || Bullet.transform.position.x <= -18.5f
					|| Bullet.transform.position.y >= 7.5f || Bullet.transform.position.y <= -7.5f)
				{
					// 非アクティブ化
					Bullet.gameObject.SetActive(false);
				}
				else
				{
					// 使用中弾リストに追加
					useBulletList.Add(Bullet);
				}
			}
		}

		// 使用中の弾の移動
		foreach(var Bullet in useBulletList)
		{
			if (Bullet.transform.gameObject.activeSelf)
			{
				Vector3 temp_Pos = Bullet.transform.right.normalized * Bullet.shot_speed;
				Bullet.transform.position += temp_Pos;
			}
		}

		if (OperationTarget.Count < BulletMax_Enemy_Bullet + BulletMax_Beam_Bullet + BulletMax_CannonBullet) return;

		if (BulletMax_Player1_Bullet < Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.childCount)
		{
			for (int i = BulletMax_Player1_Bullet; i < Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.childCount; i++)
			{
				PlayerBullet.Add(Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<Player_Bullet>());
				BulletMax_Player1_Bullet++;
			}
		}
		if (BulletMax_Player2_Bullet < Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.childCount)
		{
			for(int i = BulletMax_Player2_Bullet; i < Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.childCount; i++)
			{
				PlayerBullet.Add(Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<Player_Bullet>());
				BulletMax_Player2_Bullet++;
			}
		}
		if (BulletMax_Option1_Bullet < Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.childCount)
		{
			for (int i = BulletMax_Option1_Bullet; i < Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.childCount; i++)
			{
				PlayerBullet.Add(Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<Player_Bullet>());
				BulletMax_Option1_Bullet++;
			}
		}
		if (BulletMax_Option2_Bullet < Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.childCount)
		{
			for(int i = BulletMax_Option2_Bullet; i < Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.childCount; i++)
			{
				PlayerBullet.Add(Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<Player_Bullet>());
				BulletMax_Option2_Bullet++;
			}
		}
		if (BulletMax_Enemy_Bullet < Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.childCount)
		{
			for(int i = BulletMax_Enemy_Bullet; i < Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.childCount; i++)
			{
				OperationTarget.Add(Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<bullet_status>());
				BulletMax_Enemy_Bullet++;
			}
		}
		if(BulletMax_Beam_Bullet < Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.childCount)
		{
			for(int i = BulletMax_Beam_Bullet; i < Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.childCount; i++)
			{
				OperationTarget.Add(Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.GetChild(i).GetComponent<bullet_status>());
				BulletMax_Beam_Bullet++;
			}
		}
		if(BulletMax_CannonBullet < Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.childCount)
		{
			for(int i = BulletMax_CannonBullet; i < Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.childCount; i++)
			{
				OperationTarget.Add(Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.GetChild(i).GetComponent<bullet_status>());
				BulletMax_CannonBullet++;
			}
		}
	}

	 public void DuplicateRemoval_OperationTarget(ref Object_Pooling poo)
	{
		var obj = poo.Get_Parent_Obj();
		foreach(var State in obj.transform.GetComponentsInChildren<bullet_status>(true))
		{
			OperationTarget.Remove(State);
		}
	}
}
