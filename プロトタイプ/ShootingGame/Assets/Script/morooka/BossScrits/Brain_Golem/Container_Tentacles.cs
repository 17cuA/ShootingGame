//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2020/01/22
//----------------------------------------------------------------------------------------------
// コンテナ射出触手
//----------------------------------------------------------------------------------------------
// 2020/01/22　コンテナ射出
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Container_Tentacles : Tentacles
{
	[SerializeField, Tooltip("攻撃ディレイ")] private float attackDelay;
	[SerializeField, Tooltip("攻撃マズル")] private GameObject muzzle;
	[SerializeField, Tooltip("弾撃つ間隔")] private int shootingInterval_Max;

	private Vector2 TargetPos { get; set; }                 // ターゲットの位置
	private int Player_Layer;

	private RaycastHit hitObject;
	private int shootingInterval_Cnt;
	new private void Start()
	{
		base.Start();
		AnimName.Add("Open");
		AnimName.Add("Close");
		shootingInterval_Cnt = 0;
		Player_Layer = 1 << Obj_Storage.Storage_Data.GetPlayer().layer;
	}

	new private void Update()
	{
		base.Update();

		if (Is_Attack)
		{
			if(ActionStep == 0)
			{
				// ターゲットの向きに向ける
				TargetPos = VectorChange_3To2(BaseBone.transform.position - NowTarget.transform.position);
				BaseBone.transform.right = Vector2.MoveTowards(BaseBone.transform.right, TargetPos, Time.deltaTime * 3.0f);

				// ターゲットに向きを合わせたら
				Debug.DrawRay(muzzle.transform.position, -muzzle.transform.right * 20.0f, Color.red, 5);
				if (Physics.Raycast(muzzle.transform.position, -muzzle.transform.right, out hitObject, 20.0f, Player_Layer))
				{
					if (hitObject.transform.tag == "Player")
					{
						ActionStep++;
						A_Animation.Blend("Open");
					}
				}
			}
			else if(ActionStep == 1)
			{
				if (!A_Animation.IsPlaying("Open"))
				{
					// 攻撃
					var Container = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eCONTAINER, muzzle.transform.position, muzzle.transform.forward);
					Rigidbody rigidbody = Container.GetComponent<Rigidbody>();
					rigidbody.velocity = VectorChange_3To2(-TargetPos / 5.0f);
					ItemBox box = Container.GetComponent<ItemBox>();
					box.Is_LateralRotation = true;

					// プレイヤー2がいるとき
					if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
					{
						// 違うプレイヤーが生きていればターゲット変更
						if (NowTarget == Player1 && Player2.activeSelf) NowTarget = Player2;
						if (NowTarget == Player2 && Player1.activeSelf) NowTarget = Player1;
					}
					ActionStep++;
					A_Animation.Blend("Close");
				}
			}
			else if(ActionStep == 2)
			{
				shootingInterval_Cnt++;
				if (shootingInterval_Cnt > shootingInterval_Max)
				{
					// ターゲットの向きに向ける
					TargetPos = VectorChange_3To2(BaseBone.transform.position - NowTarget.transform.position);
					BaseBone.transform.right = Vector2.MoveTowards(BaseBone.transform.right, TargetPos, Time.deltaTime * 3.0f);

					// ターゲットに向きを合わせたら
					Debug.DrawRay(muzzle.transform.position, -muzzle.transform.right * 20.0f, Color.red, 5);
					if (Physics.Raycast(muzzle.transform.position, VectorChange_3To2(-muzzle.transform.right), out hitObject, 20.0f))
					{
						// 攻撃
						var Container = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eCONTAINER, muzzle.transform.position, muzzle.transform.forward);
						Rigidbody rigidbody = Container.GetComponent<Rigidbody>();
						rigidbody.velocity = VectorChange_3To2(-TargetPos / 5.0f);
						ItemBox box = Container.GetComponent<ItemBox>();
						box.Is_LateralRotation = true;

						ActionStep++;
						shootingInterval_Cnt = 0;
					}
				}
			}
			else if(ActionStep == 3)
			{
				// ターゲットの向きに向ける
				TargetPos = VectorChange_3To2(BaseBone.transform.position - NowTarget.transform.position);
				BaseBone.transform.right = Vector2.MoveTowards(BaseBone.transform.right, TargetPos, Time.deltaTime * 3.0f);

				// ターゲットに向きを合わせたら
				Debug.DrawRay(muzzle.transform.position, -muzzle.transform.right * 20.0f, Color.red, 5);
				if (Physics.Raycast(muzzle.transform.position, VectorChange_3To2(-muzzle.transform.right), out hitObject, 20.0f))
				{
					shootingInterval_Cnt++;
					if (shootingInterval_Cnt > shootingInterval_Max)
					{
						// 攻撃
						var Container = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eCONTAINER, muzzle.transform.position, muzzle.transform.forward);
						Rigidbody rigidbody = Container.GetComponent<Rigidbody>();
						rigidbody.velocity = VectorChange_3To2(-TargetPos / 5.0f);
						ItemBox box = Container.GetComponent<ItemBox>();
						box.Is_LateralRotation = true;

						ActionStep++;
						shootingInterval_Cnt = 0;
					}
				}
			}
			else if(ActionStep == 4)
			{
				if(!A_Animation.IsPlaying("Close"))
				{
					ActionStep = 0;
					Timer = 0.0f;
					Is_Attack = false;
				}
			}
		}
		else
		{
			Timer += Time.deltaTime;
			if (attackDelay < Timer) Is_Attack = true;
		}
	}
}
