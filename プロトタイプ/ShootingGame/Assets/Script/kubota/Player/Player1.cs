/*
 * プレイヤーの挙動作成
 * 久保田 達己
 * 
 * 2019/05/28	カットイン時は操作できないようにした
 * 2019/06/07	陳さんの作ったパワーアップ処理統合
 */
using UnityEngine;
//using Power;
public class Player1 : character_status
{
	private const float number_Of_Directions = 1.0f;    //方向などを決める時使う定数
	private Vector3 vector3;    //進む方向を決める時に使う
	private float x;    //x座標の移動する時に使う変数
	private float y;    //y座標の移動する時に使う変数
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
	public int Remaining;		//プレイヤーの残機（Unity側の設定）
    public GameObject shot_Mazle;       //プレイヤーが弾を放つための地点を指定するためのオブジェクト
	public float energy;						 //レーザー打つためのエネルギー
	public float energy_Max;            //エネルギーの最大値
	private Obj_Storage OS;             //ストレージからバレットの情報取得
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
        bullet_Type = Bullet_Type.Single;　　//初期状態をsingleに
	}

	void Update()
	{
		//パワーマネージャー更新
		//PowerManager.Instance.OnUpdate(Time.deltaTime);

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
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
				break;
			default:
				break;
		}
		Shot_Delay++;
	}
    //collisionの時はisTriggerにチェックを入れないこと
    //コライダーが当たった時の処理
    private void OnTriggerEnter(Collider col)
    {
		////アイテムの場合
		//if (col.tag == "Item")
		//{
		//	//アイテムのパワータイプを取得
		//	PowerType type = col.GetComponent<Item>().powerType;

		//	//外からのアイテム再取得時の処理　
		//	//() => { Debug.Log("イベント発生！依頼関数実行"); };
		//	//上記部分を含め処理する

		//	//PowerManager.Instance.Pick(type);実行する前に、依頼関数をイベントに入れておけば、同時に実行することができる
		//	//パワー内部　＋　パワー外部　同時に実行
		//	//何故なら、パワーアップする時、内部データに影響するだけでなく、外部（エフェクト、音再生とか）も影響する

		//	//新たに生成したパワーをパワーマネージャーで管理
		//	PowerManager.Instance.Pick(type);
		//}
		//弾の場合
		if (col.tag == "Enemy_Bullet")
		{
			////シールドがある場合
			//if (PowerManager.Instance.HasPower(PowerType.POWER_SHIELD))
			//{
			//	//シールドまだ消滅してない場合
			//	if (!PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).IsLost)
			//	{
			//		//シールドのHp　-1
			//		//変更必要
			//		int value = PowerManager.Instance.GetPower(PowerType.POWER_SHIELD).value--;
			//		//Debug.Log(value);
			//	}
			//}
			////　シールドがない場合
			//else
			//{
			//敵の弾の攻撃力を取得し、プレイヤーの体力を減らす
			bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
			hp -= (int)Bs.attack_damage;

			//}
		}
		if (col.gameObject.tag == "Enemy") hp--;
    }
	//コントローラーの操作
	private void Player_Move()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		vector3 = new Vector3(x, y, 0);
        transform.position = transform.position + vector3 * Time.deltaTime * speed;
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
}
