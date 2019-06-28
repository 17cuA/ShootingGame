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
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
    public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト
	private Obj_Storage OS;             //ストレージからバレットの情報取得
	public int Remaining;                                        //残機（あらかじめ設定）
	public int invincible_time;              //無敵時間計測用
	public int invincible_Max;          //無敵時間最大時間
	public bool invincible;             //無敵時間帯かどうか
	public Material material;
	private Color first_color;
	public enum Bullet_Type　　//弾の種類
	{
		Single,
		Diffusion,
		Three_Point_Burst
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
		PowerManager.Instance.AddFunction(PowerManager.Power.PowerType.OPTION, CreateBit);
	}
	//プレイヤーのアクティブが切られたら呼び出される
	private void OnDisable()
	{
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.SPEEDUP, SpeedUp);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.MISSILE, ActiveMissile);
		PowerManager.Instance.RemoveFunction(PowerManager.Power.PowerType.OPTION, CreateBit);
	}
	void Start()
	{
		//OS =GameObject.Find("GameMaster").GetComponent 
		//各種値の初期化とアタッチされているコンポーネントの情報を取得
        shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		transform.eulerAngles = new Vector3(-30, 0, 0);
		vector3 = Vector3.zero;
		Direction = transform.rotation;
		hp = 10;
		HP_Setting();
		Type = Chara_Type.Player;
		//-----------------------------------------------------------------
        bullet_Type = Bullet_Type.Single;  //初期状態をsingleに
		direction = transform.position;
		first_color = material.color;
	}

	void Update()
	{
		//パワーマネージャー更新
		//PowerManager.Instance.OnUpdate(Time.deltaTime);
		if(hp < 1)
		{
			Remaining--;
			if (Remaining < 1)
			{
				Died_Process();
			}
			else
			{
				Reset_Status();
				gameObject.transform.position = direction;
				invincible = true;
				invincible_time = 0;
			}
		}
		if(Input.GetKeyDown(KeyCode.X))
		{
			invincible_time = 0;
			//Debug.Log("hei");
		}
		Invincible();
		switch (Game_Master.MY.Management_In_Stage)
		{
			case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
				//プレイヤーの移動処理
				Player_Move();
				//体力が０になると死ぬ処理
				//Died_Judgment();
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
		Shot_Delay++;
	}
	//コントローラーの操作
	private void Player_Move()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		vector3 = new Vector3(x, y, 0);
        transform.position = transform.position + vector3 * Time.deltaTime * speed;
	}
	//無敵時間（色の点滅も含め）
	private void Invincible()
	{
		//既定の時間より短ければ点滅を
		if (invincible_time <= invincible_Max)
		{
			invincible_time++;          //フレーム管理
			capsuleCollider.enabled = false;	//規定のコライダーをオフに変更
			if (invincible_time % 20 == 0) invincible = !invincible;	//透明にするかしないかの判定用変数を変える
			if (invincible) material.color = Color.clear;				//色を透明に
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
	public void Bullet_Create()
	{
		if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space))
		{
          switch (bullet_Type)
            {
                case Bullet_Type.Single:
                    Single_Fire();
                    break;
                case Bullet_Type.Diffusion:
                    //Diffusion_Fire();
                    break;
                case Bullet_Type.Three_Point_Burst:
                   // Triple_Fire();
                    Invoke("Triple_Fire", 0.1f);
                    Invoke("Triple_Fire", 0.2f);
                    break;
                default:
                    break;
            }
            Shot_Delay = 0;
		}
	}

    private void Single_Fire()
	{
		GameObject gameObject =Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
		gameObject.transform.rotation = Direction;
		gameObject.transform.position = shot_Mazle.transform.position;
	}
	//private void Diffusion_Fire()
	//{
	//	Instantiate(Obj_Storage.Storage_Data.PlayerBullet,shot_Mazle.transform.position,transform.rotation);
	//	Instantiate(Obj_Storage.Storage_Data.PlayerBullet, shot_Mazle.transform.position, shot_Mazle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Direction);
	//	Instantiate(Obj_Storage.Storage_Data.PlayerBullet, shot_Mazle.transform.position, shot_Mazle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -30.0f) * Direction);
	//}
	//private void Triple_Fire()
	//{
	//	Instantiate(Obj_Storage.Storage_Data.PlayerBullet, shot_Mazle.transform.position, transform.rotation);
	//}
	private void SpeedUp()
	{
		speed += speed;
		Debug.Log("スピードUP");
	}
	//ミサイルをアクティブに
	private void ActiveMissile()
	{
		activeMissile = true;
		Debug.Log("ミサイル導入");
	}
	//オプションをアクティブに
	private void CreateBit()
	{
		switch (bitIndex)
		{
			case 0:
			case 1:
			case 2:
			case 3:
				//bitGameObject[bitIndex] = Instantiate(bitsPrefabs[bitIndex], transform.position, transform.rotation) as GameObject;
				//bitIndex++;
				break;
			default:
				break;
		}
		Debug.Log("ビットン生成");
	}

}
