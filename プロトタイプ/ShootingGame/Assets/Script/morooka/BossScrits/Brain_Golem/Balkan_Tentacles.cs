//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2020/01/22
//----------------------------------------------------------------------------------------------
// バルカン・レーザー射出触手
//----------------------------------------------------------------------------------------------
// 2020/01/22　バルカン・レーザー射出触手
//----------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Balkan_Tentacles : Tentacles
{
	[SerializeField, Tooltip("攻撃ディレイ")] private float attackDelay;
	[SerializeField, Tooltip("攻撃マズル")] private GameObject muzzle;
	[SerializeField, Tooltip("攻撃時の弾の数")] private int[] numberOfBullets;

	private GameObject Player1 { get; set; }			// プレイヤー1の情報格納
	private GameObject Player2 { get; set; }			// プレイヤー2の情報格納
	private GameObject NowTarget { get; set; }		// 今のターゲット
	private float Timer { get; set; }							// タイマー
	private bool Is_Attack { get; set; }						// 攻撃しているかどうか
	private int AttackStep { get; set; }						// 攻撃の順番仕切り

	new private void Start()
	{
		base.Start();
		Player1 = Obj_Storage.Storage_Data.GetPlayer();
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player2 = Obj_Storage.Storage_Data.GetPlayer2();
		}
		NowTarget = Player1;
	}

	new private void Update()
	{
		base.Update();

		if (Is_Attack)
		{
			Vector3 targetPos = new Vector3();
			if (AttackStep == 0)
			{
				targetPos = NowTarget.transform.position - BaseBone.transform.position;
				Vector3 temp = new Vector3(targetPos.y, -targetPos.x, 0.0f);
				targetPos = temp;
				AttackStep++;
			}
			else if (AttackStep == 2)
			{
				// ターゲットの向きに向ける
				BaseBone.transform.right = Vector3.MoveTowards(BaseBone.transform.right, targetPos, Time.deltaTime);
				if (targetPos == BaseBone.transform.right)
				{
					targetPos = new Vector3(-targetPos.x, -targetPos.y, 0.0f);
					AttackStep++;
				}
			}
			else if (AttackStep == 3)
			{
				BaseBone.transform.right = Vector3.MoveTowards(BaseBone.transform.right, targetPos, Time.deltaTime);
				if (targetPos == BaseBone.transform.right)
				{
					targetPos = Vector3.zero;

					// プレイヤー2がいるとき
					if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
					{
						// 違うプレイヤーが生きていればターゲット変更
						if (NowTarget == Player1 && Player2.activeSelf) NowTarget = Player2;
						if (NowTarget == Player2 && Player1.activeSelf) NowTarget = Player1;
					}

					AttackStep++;
				}
				else if(AttackStep == 4)
				{
					BaseBone.transform.right = Vector3.MoveTowards(BaseBone.transform.right, targetPos, Time.deltaTime);
					if(targetPos == BaseBone.transform.right)
					{
						Timer = 0.0f;
						Is_Attack = false;
						AttackStep = 0;
					}
				}
			}
		}
		else
		{
			Timer += Time.deltaTime;
			if (attackDelay < Timer) Is_Attack = true;
		}
	}

	/// <summary>
	/// 攻撃
	/// </summary>
	private void Attack(Vector3 targetPos)
	{
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eCONTAINER, muzzle.transform.position, targetPos);
		Timer = 0.0f;
		Is_Attack = false;
	}


}
