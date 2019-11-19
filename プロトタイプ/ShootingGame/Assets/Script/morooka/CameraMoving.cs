using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
	private Player1 Player_1 { get; set; }           // プレイヤーの情報
	private Player2 Player_2 { get; set; }           // プレイヤーの情報

	private Vector3 LastPosition {get;set;}						// 前フレームの位置
	private Vector3 MovementDifference { get; set; }        // 移動量の差

	Vector3 rarara;

	void Start()
    {
		switch (Scene_Manager.Manager.Now_Scene)
		{
			case Scene_Manager.SCENE_NAME.eSTAGE_01:
				enabled = false;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_02:
				enabled = true;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_03:
				enabled = false;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_04:
				enabled = false;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_05:
				enabled = false;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_06:
				enabled = false;
				break;
			case Scene_Manager.SCENE_NAME.eSTAGE_07:
				enabled = false;
				break;
			default:
				break;
		}

		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player_1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		}
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player_1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			Player_2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
		}

		LastPosition = transform.position;
		rarara = transform.position;
	}

	private void Update()
	{
		if(rarara != transform.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, rarara, Time.deltaTime);
		}
		else
		{
			rarara.x += Random.Range(-5.0f, 5.0f);
			rarara.y += Random.Range(-5.0f, 5.0f);
		}
	}

	private void LateUpdate()
	{
		// 前のフレームとの移動量算出
		MovementDifference = transform.position - LastPosition;
		LastPosition = transform.position;

		// プレイヤー、プレイヤーバレットに移動量を加算
		// プレイヤーが1のとき
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			// プレイヤー
			Player_1.transform.position += MovementDifference;
			Player_1.target += MovementDifference;

			// プレイヤーバレット
			foreach (var bullet in Obj_Storage.Storage_Data.PlayerBullet.Get_Obj())
			{
				// アクティブのとき
				if (bullet.activeSelf)
				{
					Vector3 temp = bullet.transform.position;
					temp.x += MovementDifference.x;
					bullet.transform.position = temp;
				}
			}
		}
		// プレイヤーが2のとき
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			// プレイヤー
			Player_1.transform.position += MovementDifference;
			Player_2.transform.position += MovementDifference;
			Player_1.target += MovementDifference;
			Player_2.target += MovementDifference;

			//プレイヤーバレット
			List<GameObject> obj = new List<GameObject>();
			obj.AddRange(Obj_Storage.Storage_Data.PlayerBullet.Get_Obj());
			obj.AddRange(Obj_Storage.Storage_Data.Player2Bullet.Get_Obj());
			foreach (var bullet in obj)
			{
				// アクティブのとき
				if (bullet.activeSelf)
				{
					Vector3 temp = bullet.transform.position;
					temp.x += MovementDifference.x;
					bullet.transform.position = temp;
				}
			}
		}
	}
}
