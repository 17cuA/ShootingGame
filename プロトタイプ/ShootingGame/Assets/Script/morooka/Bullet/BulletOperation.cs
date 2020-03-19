using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOperation : MonoBehaviour
{
	static public BulletOperation BP { get; set; }

	private List<Player_Bullet> PlayerBullet;

	private List<GameObject> OperationTarget;
	private Dictionary<string, float> bulletSpeed;

	void Start()
    {
		BP = GetComponent<BulletOperation>();

		#region リストに追加
		PlayerBullet = new List<Player_Bullet>();
		OperationTarget = new List<GameObject>();
		bulletSpeed = new Dictionary<string, float>();

		bulletSpeed.Add("Enemy_Beam_Bullet", 0.3f);
		bulletSpeed.Add("Enemy_Bullet", 0.1f);
		bulletSpeed.Add("BattleShip_Enemy_Bullet", 0.1f);

		PlayerBullet.AddRange(Obj_Storage.Storage_Data.PlayerBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.Player2Bullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P1_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));
		PlayerBullet.AddRange(Obj_Storage.Storage_Data.P2_OptionBullet.Get_Parent_Obj().transform.GetComponentsInChildren<Player_Bullet>(true));

		OperationTarget.Add(Obj_Storage.Storage_Data.Beam_Bullet_E.Get_Parent_Obj());
		OperationTarget.Add(Obj_Storage.Storage_Data.EnemyBullet.Get_Parent_Obj());
		OperationTarget.Add(Obj_Storage.Storage_Data.BattleShipBullet.Get_Parent_Obj());
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
					Vector3 temp = Bullet.transform.right.normalized * 0.45f;
					Bullet.transform.position += temp;
				}
			}
		}

		// エネミーの弾挙動
		for(int i = 0; i < OperationTarget.Count;i++)
		{
			if(OperationTarget[i] == null)
			{
				OperationTarget.Remove(OperationTarget[i]);
				continue;
			}

			for(int j = 0; j < OperationTarget[i].transform.childCount;j++)
			{
				GameObject g = OperationTarget[i].transform.GetChild(j).gameObject;
				if (g.activeSelf)
                {
                    if (g.transform.position.x >= 18.5f || g.transform.position.x <= -18.5f
						 || g.transform.position.y >= 7.5f || g.transform.position.y <= -7.5f)
                    {
						// 非アクティブ化
						g.gameObject.SetActive(false);
					}
					else
					{
						Vector3 temp_Pos = g.transform.right.normalized * bulletSpeed[g.name];
						g.transform.position += temp_Pos;

					}
				}
            }
		}
	}
}
