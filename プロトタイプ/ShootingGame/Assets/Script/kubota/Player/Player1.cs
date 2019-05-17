using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : character_status
{
	private GameObject Bullet;      //弾のPrefab情報
	private const float number_Of_Directions = 1.0f;    //方向などを決める時使う定数
	private Vector3 vector3;    //進む方向を決める時に使う
	private float x;    //x座標の移動する時に使う変数
	private float y;    //y座標の移動する時に使う変数
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  
	public int Remaining;		//プレイヤーの残機（Unity側の設定）
    public GameObject shot_Mazle;
    	public enum Bullet_Type　　//弾の種類
	{
		Single,
		Diffusion,
		Three_Point_Burst
	}
	public Bullet_Type bullet_Type;　//弾の種類を変更
	void Start()
	{
		Bullet = Resources.Load("Player_Bullet") as GameObject;
		//各種値の初期化とアタッチされているコンポーネントの情報を取得
		Rig = GetComponent<Rigidbody>();
		capsuleCollider = GetComponent<CapsuleCollider>();
        shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		transform.eulerAngles = new Vector3(-30, 0, 0);
		vector3 = Vector3.zero;
		Direction = transform.rotation;
		Rig.velocity = vector3;
		hp = 10;
		Type = Chara_Type.Player;
		//-----------------------------------------------------------------
        bullet_Type = Bullet_Type.Single;　　//初期状態をsingleに
	}

	void Update()
	{
		//対応したボタンを押すとプレイヤーの方向がかわる（後ろを向く）
		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire1"))
		{
			//方向転換させる関数の呼び出し
			Change_In_Direction();
		}
		Player_Move();
		//体力が０になると死ぬ処理
		//Died_Judgment();
        //弾の発射（Fire2かSpaceキーで撃てる）
        if (Shot_Delay > Shot_DelayMax)
        {
            Bullet_Create();
        }
		Shot_Delay++;
	}
    //collisionの時はisTriggerにチェックを入れないこと
    //コライダーが当たった時の処理
    private void OnTriggerEnter(Collider col)
    {
        //敵の弾に当たった時に使う処理
        if (col.gameObject.tag == "Enemy_Bullet")
        {
            //敵の弾の攻撃力を取得し、プレイヤーの体力を減らす
            bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
            hp -= Bs.attack_damage;
        }
		if (col.gameObject.tag == "Enemy") hp--;
        if (col.gameObject.tag == "Item") bullet_Type = Bullet_Type.Diffusion;
    }
	//コントローラーの操作
	private void Player_Move()
	{
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		vector3 = new Vector3(x, y, 0);
		Rig.velocity = vector3 * speed;
	}
	
	//プレイヤーの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1,0, 0);
		transform.rotation = Direction;
	}
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
                    Diffusion_Fire();
                    break;
                case Bullet_Type.Three_Point_Burst:
                    Triple_Fire();
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
		Instantiate
		(
			Bullet,
			shot_Mazle.transform.position,
			transform.rotation
		);
	}
	private void Diffusion_Fire()
	{
		Instantiate(Bullet,shot_Mazle.transform.position,transform.rotation);
		Instantiate(Bullet, shot_Mazle.transform.position, shot_Mazle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 30.0f) * Direction);
		Instantiate(Bullet, shot_Mazle.transform.position, shot_Mazle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -30.0f) * Direction);
	}
	private void Triple_Fire()
	{
		Instantiate(Bullet, shot_Mazle.transform.position, transform.rotation);
	}
}
