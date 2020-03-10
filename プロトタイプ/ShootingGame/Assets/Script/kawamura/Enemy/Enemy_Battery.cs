﻿//作成者：川村良太
//固定砲台のスクリプト　今は特に動きがないので死ぬ処理だけ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Battery : character_status
{
	public CannonAngle angle_Script;            //プレイヤーの方向を向くスクリプト取得用（死亡時攻撃に使う） パブリックで入れて
	public Quaternion diedAttackRota;           //死んだ時に出す弾の角度範囲
	public bool Died_Attack = false;

	public bool haveItem = false;

	new void Start()
    {
		base.Start();
    }

    new void Update()
    {
		if (hp < 1)
		{
			if (Died_Attack)
			{
				//死亡時攻撃の処理
				int bulletSpread = 15;      //角度を広げるための変数
				for (int i = 0; i < 3; i++)
				{
					//diedAttack_RotaZ = Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue);
					//diedAttack_Transform.rotation = Quaternion.Euler(0, 0, diedAttack_RotaZ);
					//diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));

					diedAttackRota = Quaternion.Euler(0, 0, angle_Script.degree + bulletSpread);

					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);
					bulletSpread -= 15;     //広げる角度を変える
				}

				//diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
				//Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			}

			if (haveItem)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
			}
			Died_Process();
		}
		else if (transform.position.x < -21)
		{
			gameObject.SetActive(false);
		}


		base.Update();
    }

	private void Reset()
	{
		
	}
}
