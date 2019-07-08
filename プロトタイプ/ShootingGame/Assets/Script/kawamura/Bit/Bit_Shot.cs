//ビットの攻撃スクリプト
//プレイヤーのShot_DelayMaxを参照してプレイヤーの2発おきに攻撃する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;
using StorageReference;

public class Bit_Shot : MonoBehaviour
{
	public GameObject playerObj;
    public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト
	GameObject laserObj;


	public ParticleSystem laser;            //レーザーのパーティクルを取得するための変数
	public Line_Beam line_beam;

	Player1 pl1;
	Bit_Formation_3 bf;
    public Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
    int shotNum;
    float shot_Delay;

	public bool isShot = true;
    int missileDelayCnt = 0;
    int shotDelayMax;

	bool activeLaser = true;
	bool activeDouble = false;
	bool activeBullet = false;


	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		Power.PowerManager.Instance.AddFunction(Power.PowerManager.Power.PowerType.LASER, ActiveLaser);

	}
	private void OnDisable()
	{
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.LASER, ActiveLaser);

	}

	void Start()
	{

		laserObj= transform.FindChild("Lasers").gameObject;
		line_beam = laserObj.GetComponent<Line_Beam>();

		shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;

		bf = gameObject.GetComponent<Bit_Formation_3>();
        //shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
        Direction = transform.rotation;
        shotDelayMax = 5;
		laser.Stop();


	}

	void Update()
	{
        if(playerObj==null)
        {
            playerObj = GameObject.Find("Player");
            pl1 = playerObj.GetComponent<Player1>();

        }
        if (!bf.isDead)
		{
			if (isShot)
			{
				if (pl1.bullet_Type == Player1.Bullet_Type.Laser)
				{
					if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
					{
						//line_beam.disableEffect();
						laser.Stop();
					}

					else if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
					{
						laser.Play();
						line_beam.shot();
						//laser.Play();
						//line_beam.shot();

					}
				}

				else if (shot_Delay > shotDelayMax)
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

								break;
							case Player1.Bullet_Type.Double:
								Double_Fire();

								break;
							case Player1.Bullet_Type.Laser:
								laser.Stop();
								break;
							default:
								break;

						}
                        // ミサイルは別途ディレイの計算と分岐をする
                        if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
                        {
                            Missile_Fire();
                            //missileDelayCnt = 0;
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
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, shot_Mazle.transform.position, Direction);

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
		if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, transform.position, Direction);
			obj.GetComponent<Missile>().Setting_On_Reboot(1);
			missileDelayCnt = 0;
		}
	}
	private void ActiveLaser()
	{
		activeLaser = true;
		activeDouble = false;
		activeBullet = false;
		Debug.Log("レーザーに変更");
		//bullet_Type = Bullet_Type.Laser;
	}

}
