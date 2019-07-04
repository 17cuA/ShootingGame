//ビットの攻撃スクリプト
//プレイヤーのShot_DelayMaxを参照してプレイヤーの2発おきに攻撃する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Bit_Shot : MonoBehaviour
{
	public GameObject playerObj;
    public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト

    Player1 pl1;
	Bit_Formation_3 bf;
    public Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
    int shotNum;
    float shot_Delay;

	public bool isShot = true;
    int missileDelayCnt = 0;
    int shotDelayMax;
	void Start()
	{
		shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;

		bf = gameObject.GetComponent<Bit_Formation_3>();
        //shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
        Direction = transform.rotation;
        shotDelayMax = 5;
	}

	void Update()
	{
        if(playerObj==null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
            pl1 = playerObj.GetComponent<Player1>();

        }
        if (!bf.isDead)
		{
			if (isShot)
			{
				if (shot_Delay > shotDelayMax)
				{
                    shotNum++;

                    // 連続で4発まで撃てるようにした
                    if (shotNum < 5)
                    {
						switch(pl1.bullet_Type)
						{
							case Player1.Bullet_Type.Single:
								Single_Fire();
								//Bullet_Create();

								pl1.ParticleCreation(3);

								break;
							case Player1.Bullet_Type.Double:
								Double_Fire();
								pl1.ParticleCreation(3);

								break;
							case Player1.Bullet_Type.Laser:
								break;
							default:
								break;

						}
                        // ミサイルは別途ディレイの計算と分岐をする
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
                    }
                }				
			}
			shot_Delay++;
		}

		missileDelayCnt++;
    }
	public void Bullet_Create()
	{
		//if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		//{
		//	Single_Fire();
		//	shot_Delay = 0;
		//}
	}

	private void Single_Fire()
	{
		if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, transform.position, Direction);

			//GameObject Bullet = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
			//pl1.transform.rotation = pl1.Direction;
			//Bullet.transform.position = gameObject.transform.position;
			//gameObject.transform.rotation = Direction;
			//gameObject.transform.position = shot_Mazle.transform.position;

			shot_Delay = 0;
		}

	}
	private void Double_Fire()
	{
		if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, transform.position, Direction);
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, transform.position, /*new Quaternion(-8,1,45,0)*/Quaternion.Euler(0, 0, 45));
			shot_Delay = 0;
		}

	}

	private void Missile_Fire()
    {
        GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, transform.position, Direction);
        obj.GetComponent<Missile>().Setting_On_Reboot(1);
    }

}
