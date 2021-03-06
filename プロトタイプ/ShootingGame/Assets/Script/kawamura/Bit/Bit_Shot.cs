﻿//作成者：川村良太
//ビットの攻撃スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;
using StorageReference;

public class Bit_Shot : MonoBehaviour
{
	public GameObject playerObj;		//プレイヤーオブジェクト
    public GameObject shot_Mazle;		//弾を放つための地点を指定するためのオブジェクト
	public GameObject laser_Obj;		//レーザーオブジェクト

	Player1 pl1;						//プレイヤースクリプト
	Bit_Formation_3 bf;					//オプションの全般のスクリプト
    public Quaternion Direction;        //オブジェクトの向きを変更する時に使う  
	//public ParticleSystem[] effect_Mazle_Fire = new ParticleSystem[5];  //マズルファイアのエフェクト（unity側の動き）

	public int shotNum;                        //撃った数
	int effectNum;
    public float shot_Delay;                   //撃つディレイ
	public int Shot_DelayMax;                                           // 弾を打つ時の間隔（最大値::unity側にて設定）
	public int Bullet_cnt;          //バレットの発射数をかぞえる変数
	private int Bullet_cnt_Max;     //バレットの発射数の最大値を入れる変数
	public bool isShot = true;          //撃てるか
	public bool isBurst;
    int missileDelayCnt = 0;			//ミサイルのディレイ
    public int shotDelayMax;                   //ショットの間隔
	List<GameObject> bullet_data = new List<GameObject>();
	//bool activeLaser = true;

	//bool activeDouble = false;
	//bool activeBullet = false;

	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		P1_PowerManager.Instance.AddFunction(P1_PowerManager.Power.PowerType.LASER, ActiveLaser);

	}
	private void OnDisable()
	{
		P1_PowerManager.Instance.RemoveFunction(P1_PowerManager.Power.PowerType.LASER, ActiveLaser);

	}

	void Start()
	{
		//撃つ位置取得
		shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		//Bit_Formation_3取得
		bf = gameObject.GetComponent<Bit_Formation_3>();
		//向き入れます,撃つ間隔の最大設定します,
        Direction = transform.rotation;
        shotDelayMax = 5;
		laser_Obj.SetActive(true);		 //レーザーの子供が動かないようにするための変数

	}

	void Update()
	{
		//プレイヤーオブジェクトが入っていなかったら入れてスクリプトも取得
        if(playerObj==null)
        {
            playerObj = GameObject.Find("Player");
            pl1 = playerObj.GetComponent<Player1>();

        }

		//if(pl1.bullet_Type == Player1.Bullet_Type.Laser)
		//{
		//	laser_Obj.SetActive(true);
		//}
		//else
		//{
		//	laser_Obj.SetActive(false);
		//}

		//死んでないくて打てる状態なら
        if (!bf.isDead&& isShot)
		{
			//プレイヤーがレーザー状態の時
			if (pl1.bullet_Type == Player1.Bullet_Type.Laser)
			{
				laser_Obj.SetActive(true);
				//発射ボタンが離されたら
				if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
				{
					//レーザーストップ
					//laser_Obj.SetActive(false);
				}
				//発射ボタンが押されている間
				else if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
				{
					//レーザーを出す
					//laser_Obj.SetActive(true);
					//レーザー時のミサイル発射の処理
					if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
					{
						if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
						{
							Missile_Fire();
						}
					}
				}
			}
			else
			{
				laser_Obj.SetActive(false);
			}

			//if (shot_Delay > pl1.Shot_DelayMax)
			//{
			//	//弾を射出
			//	Bullet_Create();
			//}

			Bullet_Create();

			//ディレイカウントがディレイの最大値より大きくなったら撃てる
			//if (shot_Delay > shotDelayMax)
			//{

			//             //shotNum++;

			//             // 連続で4発まで撃てるようにした
			//             if (shotNum < 5)
			//             {
			//		//プレイヤーの弾のタイプによって撃てるのが変わる
			//		switch(pl1.bullet_Type)
			//		{
			//			case Player1.Bullet_Type.Single:
			//				Single_Fire();
			//				shotNum++;
			//				//Bullet_Create();

			//				break;
			//			case Player1.Bullet_Type.Double:
			//				Double_Fire();
			//				shotNum++;
			//				break;
			//			case Player1.Bullet_Type.Laser:
			//				//laser.Stop();
			//				break;
			//			default:
			//				break;
			//		}
			//                 // ミサイルは別途ディレイの計算と分岐をする
			//                 if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
			//                 {
			//			if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
			//			{
			//				Missile_Fire();
			//			}
			//			//missileDelayCnt = 0;
			//		}
			//		shot_Delay = 0;
			//             }
			//             // 4発撃った後、10フレーム程置く
			//             else if (shotNum == 15)
			//             {
			//                 shotNum = 0;
			//             }
			//	else
			//	{
			//		shotNum++;
			//	}
			//         }				

			shot_Delay++;
		}

		else if(bf.isDead)
		{
		}

		for (int i = 0; i < bullet_data.Count; i++)
		{
			if (!bullet_data[i].activeSelf)
			{
				bullet_data.RemoveAt(i);
			}
		}

		missileDelayCnt++;
    }

	//-----------ここから関数----------------
	public void Bullet_Create()
	{
		//Shot_DelayMax = 2;

		//マニュアル発射の時
		if (!pl1.Is_Change_Auto)
		{
			shotDelayMax = 2;
			if (shot_Delay > Shot_DelayMax)
			{
				if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
				{
					shot_Delay = 0;
					switch (pl1.bullet_Type)
					{
						case Player1.Bullet_Type.Single:
							Single_Fire();
							//effect_Mazle_Fire[effectNum].Play();
							effectNum++;
							break;
						case Player1.Bullet_Type.Double:
							Double_Fire();
							//effect_Mazle_Fire[effectNum].Play();
							effectNum++;
							break;
						default:
							break;
					}
					if (effectNum > 4)
					{
						effectNum = 0;
					}
					if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
					{
						Missile_Fire();
						missileDelayCnt = 0;
					}
					//shot_Delay = 0;
				}
			}
		}
		else
		{
			Shot_DelayMax = 5;
			if (shot_Delay > Shot_DelayMax)
			{
				if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
				{
					isBurst = true;
				}
				if (isBurst)
				{
					// 連続で4発まで撃てるようにした
					if (shotNum < 5)
					{
						switch (pl1.bullet_Type)
						{
							case Player1.Bullet_Type.Single:
								Single_Fire();
								//effect_Mazle_Fire[effectNum].Play();
								effectNum++;
								shotNum++;

								break;
							case Player1.Bullet_Type.Double:
								Double_Fire();
								//effect_Mazle_Fire[effectNum].Play();
								effectNum++;
								shotNum++;

								break;
							default:
								break;
						}
						if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
						{
							Missile_Fire();
							missileDelayCnt = 0;
						}
						shot_Delay = 0;

					}
					// 4発撃った後、10フレーム程置く
					else if (shotNum == 15)
					{
						shotNum = 0;
						effectNum = 0;
						isBurst = false;
					}
					else
					{
						shotNum++;
					}
				}
			}
			if (!isBurst)
			{
				shotNum = 0;
			}
			if (effectNum > 4)
			{
				effectNum = 0;
			}
			
		}
	}

	//単発発射関数
	private void Single_Fire()
	{
		//Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction);
		if (!pl1.Is_Change_Auto)
		{
			//if (/*Bullet_cnt < Bullet_cnt_Max*/ Bullet_cnt < 100)
			//{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction);
				SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
				Bullet_cnt += 1;
			//}

		}
		else
		{
			if (/*Bullet_cnt < Bullet_cnt_Max &&*/ /*Bullet_cnt < 100 &&*/ bullet_data.Count < 10)
			{
				bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction));
				SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
				Bullet_cnt += 1;
			}
		}
		if (Bullet_cnt_Max != 8)
		{
			Bullet_cnt_Max = 8;
		}

		shot_Delay = 0;
	}

	//ダブル発射関数
	private void Double_Fire()
	{
		if (/*Bullet_cnt < Bullet_cnt_Max*/ /*Bullet_cnt < 100 &&*/ bullet_data.Count < 10)
		{
			bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction));
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Quaternion.Euler(0, 0, 45));
			SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
			Bullet_cnt += 2;
		}
		if (Bullet_cnt_Max != 16)
		{
			Bullet_cnt_Max = 16;
		}

		//if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		//{
		//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction);
		//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, /*new Quaternion(-8,1,45,0)Quaternion.Euler(0, 0, 45));
		//	shot_Delay = 0;
		//}
	}

	private void Missile_Fire()
    {
		GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, shot_Mazle.transform.position, Direction);
		obj.GetComponent<Missile>().Setting_On_Reboot(1);
		missileDelayCnt = 0;
	}

	private void ActiveLaser()
	{
		//activeLaser = true;
		//activeDouble = false;
		//activeBullet = false;
		Debug.Log("レーザーに変更");
		//bullet_Type = Bullet_Type.Laser;
	}

}
