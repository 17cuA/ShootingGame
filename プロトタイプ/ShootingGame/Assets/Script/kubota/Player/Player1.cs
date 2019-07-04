/*
 * プレイヤーの挙動作成
 * 久保田 達己
 * 
 * 2019/05/28	カットイン時は操作できないようにした
 * 2019/06/07	陳さんの作ったパワーアップ処理統合
 */
using UnityEngine;
using Power;
using StorageReference;

//using Power;
public class Player1 : character_status
{
	private const float number_Of_Directions = 1.0f;    //方向などを決める時使う定数
	private Vector3 vector3;    //進む方向を決める時に使う
	private float x;    //x座標の移動する時に使う変数
	private float y;    //y座標の移動する時に使う変数
	public Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
    public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト
	private Obj_Storage OS;             //ストレージからバレットの情報取得
	public int Remaining;                                        //残機（あらかじめ設定）
	public int invincible_time;              //無敵時間計測用
	public int invincible_Max;          //無敵時間最大時間
	public bool invincible;             //無敵時間帯かどうか
	public Material material;			//この機体のマテリアル（これをいじくって透明化等を行う）
	private Color first_color;			//初期の色を保存しておくようの画像
	public bool activeMissile;        //ミサイルは導入されたかどうか
	public bool activeLaser;            //現在の攻撃がレーザーかどうかの判定（初期false）
	public bool activeBullet;           //現在の攻撃が弾丸かどうかの判定用（初期true）
	public bool activeDouble;           //現在の攻撃が弾丸の二連かどうかの判定用（初期false）
	public int bitIndex = 0;        //オプションの数

	[Tooltip("ジェット噴射の位置情報を入れる")]
	public GameObject Injection_pos;			//ジェット噴射の位置情報を入れる変数(unity側にて設定)
	private GameObject injection;               //ジェット噴射のエフェクトをオブジェクトとして取得するための変数（生成時に取得）（移動などをするときに使用）
	public GameObject Shield_pos;				//シールドの位置情報を入れる変数（unity側にて設定）
	private GameObject Shield_Effect;			//シールドのエフェクトを移動するためのにオブジェクトとして取得 （移動などをするときに使用）

	public float swing_facing;				// 旋回向き
	public int shoot_number;				//弾を連続して撃った時の数をカウントするための変数

	private GameObject[] effect_mazlefrash = new GameObject[3];		//マズルフラッシュのエフェクトをオブジェクトとして取得するための変数
	public ParticleSystem laser;			//レーザーのパーティクルを取得するための変数

	private int missile_dilay_cnt;				// ミサイルの発射間隔カウンター
	public int missile_dilay_max;               // ミサイルの発射間隔

	public Line_Beam line_beam;

	public enum Bullet_Type　　//弾の種類
	{
		Single,
		Double,
		Laser,
	}
	public Bullet_Type bullet_Type; //弾の種類を変更

	public void Awake()
	{
		//ここでプレイヤーが取得できる全てのパワーをパワーマネージャーに入れとく
		//PowerManager.Instance.AddPower(new Power_Shield(PowerType.POWER_SHIELD, 3));
		//PowerManager.Instance.AddPower(new Power_BulletUpgrade(PowerType.POWER_BULLET_UPGRADE, 5));

		////説明は113行目に移行
		//PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).onPickCallBack += () => { Debug.Log("イベント発生！依頼関数実行"); };
	}
	//プレイヤーがアクティブになった瞬間に呼び出される
	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.SPEEDUP, SpeedUp);
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.MISSILE, ActiveMissile);
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.DOUBLE, ActiveDouble);
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.LASER, ActiveLaser);
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.OPTION, CreateBit);
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.SHIELD, ActiveShield);

	}
	//プレイヤーのアクティブが切られたら呼び出される
	private void OnDisable()
	{
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.SPEEDUP, SpeedUp);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.MISSILE, ActiveMissile);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.DOUBLE, ActiveDouble);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.LASER, ActiveLaser);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.OPTION, CreateBit);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.SHIELD, ActiveShield);

	}
	void Start()
	{
		//OS =GameObject.Find("GameMaster").GetComponent 
		//各種値の初期化とアタッチされているコンポーネントの情報を取得
        shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		//transform.eulerAngles = new Vector3(-30, 0, 0);
		vector3 = Vector3.zero;
		Direction = transform.rotation;
		hp = 1;
		HP_Setting();
		Type = Chara_Type.Player;
		//-----------------------------------------------------------------
        bullet_Type = Bullet_Type.Single;  //初期状態をsingleに
		direction = transform.position;
		first_color = material.color;
		injection = Obj_Storage.Storage_Data.Effects[2].Active_Obj();
		shield = 3;
		//プレイヤーの各弾や強化のものの判定用変数に初期値の設定
		activeBullet = true;
		activeDouble = false;
		activeLaser = false;
		activeShield = false;
		activeMissile = false;
		laser.Stop();
	}

	void Update()
	{
		//-------------------------------
		//デバックの工程
		if(Input.GetKeyDown(KeyCode.A)) Obj_Storage.Storage_Data.PowerUP_Item.Active_Obj();
		if (Input.GetKeyDown(KeyCode.X))
		{
			invincible_time = 0;
			//Debug.Log("hei");
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Damege_Process(1);
		}
		if(Input.GetKey(KeyCode.Z))
		{
			laser.Play();
		}
		if(Input.GetKeyUp(KeyCode.Z))
		{
			laser.Stop();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			PowerManager.Instance.Pick();
		}
		//---------------------------
		//パワーマネージャー更新
		//PowerManager.Instance.OnUpdate(Time.deltaTime);

		if (activeShield)
		{
			Shield_Effect.transform.position = Shield_pos.transform.position;
		}
		if(shield < 1)
		{
			PowerManager.Instance.ResetShieldPower();
			Shield_Effect.SetActive(false);
		}
		if (hp < 1)
		{
			Remaining--;
			if (Remaining < 1)
			{
				Died_Process();
			}
			else
			{
				ParticleCreation(0);		//爆発のエフェクト発動
				Reset_Status();             //体力の修正
				PowerManager.Instance.ResetAllPower();
				gameObject.transform.position = direction;      //初期位置に戻す
				//ParticleSystem particle = Shield_Effect.GetComponent<ParticleSystem>();
				//particle.Stop();				//effectを止める
				invincible = false;			//無敵状態にするかどうかの処理
				invincible_time = 0;		//無敵時間のカウントする用の変数の初期化
			}
		}
		Invincible();
		switch (Game_Master.MY.Management_In_Stage)
		{
			case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
				//プレイヤーの移動処理
				Player_Move();
				//パワーアップの処理
				if(Input.GetKeyDown(KeyCode.X) || Input.GetButton("Fire2"))
				{
					PowerManager.Instance.Upgrade();
					GameObject effect = Obj_Storage.Storage_Data.Effects[7].Active_Obj();
					ParticleSystem particle = effect.GetComponent<ParticleSystem>();
					effect.transform.position = gameObject.transform.position;
					particle.Play();
				}
				//体力が０になると死ぬ処理
				//Died_Judgment();
				if(bullet_Type == Bullet_Type.Laser)
				{
					if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space))
					{
						laser.Play();
						line_beam.shot();

					}
					if (Input.GetButtonUp("Fire2") || Input.GetKeyUp(KeyCode.Space))
					{
						laser.Stop();
					}
				}
				//弾の発射（Fire2かSpaceキーで撃てる）
				if (Shot_Delay > Shot_DelayMax)
				{
					Bullet_Create();
				}
				if (Input.GetKeyDown(KeyCode.Z))
				{
					Damege_Process(1);
					Debug.Log("Player_HP	" + hp);
				}
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTTLE:
				//プレイヤーの移動処理
				Player_Move();
				//体力が０になると死ぬ処理
				//Died_Judgment();
				//弾の発射（Fire2かSpaceキーで撃てる）
				if (Shot_Delay > Shot_DelayMax)
				{
					Bullet_Create();
				}
				if (Input.GetKeyDown(KeyCode.Z)) Damege_Process(1);

				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
				break;
			default:
				break;
		}

		// 通常のバレットのディレイ計算
		Shot_Delay++;
		// ミサイルのディレイ計算
		missile_dilay_cnt++;
	}
	//コントローラーの操作
	private void Player_Move()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		vector3 = new Vector3(x, y, 0);
		#region
		//if(y < 0)
		//{
		//	if(transform.rotation.x < 20.0f && transform.rotation.x > -20.0f)
		//	{
		//		transform.eulerAngles += new Vector3(y, 0, 0);
		//	}
		//}
		#endregion
		// プレイヤー機体の旋回
		// プレイヤーの向き(Y軸の正負)で角度算出
		if (transform.eulerAngles.x != (swing_facing * y))
		{
			// 参考にしたURL↓
			// https://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
			// Unity にある Mathf.LerpAngle 関数を使用
			float angle = Mathf.LerpAngle(0.0f, (swing_facing * y), Time.time);
			transform.eulerAngles = new Vector3(angle, 0, 0);
		}

		transform.position = transform.position + vector3 * Time.deltaTime * speed;
		injection.transform.position = Injection_pos.transform.position;
		//injection.transform.position = Injection_pos;
	}
	//無敵時間（色の点滅も含め）
	private void Invincible()
	{
		//既定の時間より短ければ点滅を
		if (invincible_time <= invincible_Max)
		{
			invincible_time++;          //フレーム管理
			capsuleCollider.enabled = false;	//規定のコライダーをオフに変更
			if (invincible_time % 5 == 0) invincible = !invincible;	//透明にするかしないかの判定用変数を変える
			if (!invincible) material.color = Color.clear;				//色を透明に
			else material.color = first_color;							//初期の色に変更
		}
		else
		{
			material.color = first_color;	//初期の色に戻す
			capsuleCollider.enabled = true;	//
		}
	}
	//プレイヤーの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1,0, 0);
		transform.rotation = Direction;
	}
    //プレイヤーが死んだかどうかの判定用関数
	public bool Died_Judgment()
	{
		bool is_died = false;
		if (hp < 1) is_died = true;
		return is_died;
	}
	//弾の発射
	public void Bullet_Create()
	{
		if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			shoot_number++;

			// 連続で4発まで撃てるようにした
			if (shoot_number < 5)
			{
				switch (bullet_Type)
				{
					case Bullet_Type.Single:
						Single_Fire();
						SE_Manager.SE_Obj.SE_Active(4);
						ParticleCreation(3);

						break;
					case Bullet_Type.Double:
						Double_Fire();
						SE_Manager.SE_Obj.SE_Active(4);
						ParticleCreation(3);

						break;
					default:
						break;
				}
				// ミサイルは別途ディレイの計算と分岐をする
				if (activeMissile && missile_dilay_cnt > missile_dilay_max)
				{
					Missile_Fire();
					missile_dilay_cnt = 0;
				}
				Shot_Delay = 0;
			}
			// 4発撃った後、10フレーム程置く
			else if (shoot_number == 15)
			{
				shoot_number = 0;
			}
		}
		else
		{
			switch (bullet_Type)
			{
				case Bullet_Type.Single:
					break;
				case Bullet_Type.Double:
					break;
				case Bullet_Type.Laser:
					laser.Stop();
					break;
				default:
					break;
			}
		}
	}

    private void Single_Fire()
	{
		GameObject gameObject =Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
		gameObject.transform.rotation = Direction;
		gameObject.transform.position = shot_Mazle.transform.position;
	}
	private void Double_Fire()
	{
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, shot_Mazle.transform.position, Direction);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_BULLET, shot_Mazle.transform.position, /*new Quaternion(-8,1,45,0)*/Quaternion.Euler(0,0,45));
	}
	private void Laser_Fire()
	{
		laser.Play();
	}
	//	ミサイルの発射
	private void Missile_Fire()
	{
		GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, shot_Mazle.transform.position, Direction);
		obj.GetComponent<Missile>().Setting_On_Reboot(1);
	}
	//プレイヤーの速度上昇
	private void SpeedUp()
	{
		speed *= 1.2f;
		Debug.Log("スピードUP");
	}
	//ミサイルをアクティブに
	private void ActiveMissile()
	{
		activeMissile = true;
		Debug.Log("ミサイル導入");
	}
	private void ActiveDouble()
	{
		activeDouble = true;
		Debug.Log("ダブル導入");
		activeBullet = false;
		activeLaser = false;
		bullet_Type = Bullet_Type.Double;
	}
	//レーザーを打てるように
	private void ActiveLaser()
	{
		activeLaser = true;
		activeDouble = false;
		activeBullet = false;
		Debug.Log("レーザーに変更");
		bullet_Type = Bullet_Type.Laser;
	}
	//シールドの発動
	private void ActiveShield()
	{
		activeShield = true;            //シールドが発動するかどうかの判定
		shield = 3;
		Shield_Effect = Obj_Storage.Storage_Data.Effects[17].Active_Obj();			//エフェクトをオンの状態に
		ParticleSystem particle = Shield_Effect.GetComponent<ParticleSystem>();	//パーティクルの再生するかどうかを動かすため
		particle.Play();				//パーティクルの稼働
		Debug.Log("シールド発動");
	}
	//オプションをアクティブに
	private void CreateBit()
	{
		//オプションをそれぞれアクティブに
		switch (bitIndex)
		{
			case 0:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 1:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 2:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			case 3:
				Obj_Storage.Storage_Data.Option.Active_Obj();
				bitIndex++;
				break;
			default:
				break;
		}
		Debug.Log("ビットン生成");
	}
}
