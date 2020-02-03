using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOperation : MonoBehaviour
{
	private List<Player_Bullet> PlayerBullet;
	private List<Enemy_Bullet> EnemyBullet;
	private List<Beam_Bullet> BeamBullet;
	private List<CannonBullet> CannonBullet;

	void Start()
    {
		#region リストに追加
		PlayerBullet = new List<Player_Bullet>();
		EnemyBullet = new List<Enemy_Bullet>();
		BeamBullet = new List<Beam_Bullet>();
		CannonBullet = new List<CannonBullet>();

		PlayerBullet.AddRange(Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		EnemyBullet.AddRange(Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Enemy_Bullet>(true));
		BeamBullet.AddRange(Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj().transform.GetComponentsInChildren<Beam_Bullet>(true));
		CannonBullet.AddRange(Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj().transform.GetComponentsInChildren<CannonBullet>(true));
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
		#region エネミーの弾
		foreach(var Bullet in EnemyBullet)
		{
			Bullet.gameObject.tag = "Enemy_Bullet";
		}
		#endregion
	}

	void Update()
	{
		// プレイヤー弾のupdate
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
					continue;
				}

				Vector3 temp_Pos = Bullet.transform.right.normalized * Bullet.shot_speed;
				Bullet.transform.position += temp_Pos;
			}
		}
		foreach (var Bullet in EnemyBullet)
		{
			if (Bullet.transform.gameObject.activeSelf)
			{
				// 画面外に出たとき
				if (Bullet.transform.position.x >= 18.5f || Bullet.transform.position.x <= -18.5f
					|| Bullet.transform.position.y >= 7.5f || Bullet.transform.position.y <= -7.5f)
				{
					// 非アクティブ化
					Bullet.gameObject.SetActive(false);
					continue;
				}
				Vector3 temp_Pos = Bullet.transform.right.normalized * Bullet.shot_speed;
				Bullet.transform.position += temp_Pos;
			}
		}
	}
}
