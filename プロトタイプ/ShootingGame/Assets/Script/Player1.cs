using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : character_status
{
	private const float number_Of_Directions = 1.0f;    //方向などを決める時使う定数

	private Vector3 vector3;    //進む方向を決める時に使う
	private float x;    //x座標の移動する時に使う変数
	private float y;    //y座標の移動する時に使う変数
	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

	public int Remaining;		//プレイヤーの残機（Unity側の設定）
	void Start()
	{
		//各種値の初期化とアタッチされているコンポーネントの情報を取得
		Rig = GetComponent<Rigidbody>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		transform.eulerAngles = new Vector3(0, 90, -30);
		vector3 = Vector3.zero;
		Direction = transform.rotation;
		Rig.velocity = vector3;
		hp = 999;
		Type = Chara_Type.Player;
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
		if (hp < 1) Destroy(gameObject);
	}
	//collisionの時はisTriggerにチェックを入れないこと
	//コライダーが当たった時の処理
	public void OnCollisionEnter(Collision col)
	{
		//敵の弾に当たった時に使う処理
		if (col.gameObject.tag == "Enemy_Bullet")
		{
			//敵の弾の攻撃力を取得し、プレイヤーの体力を減らす
			bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
			hp -= Bs.attack_damage;
		}
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
		Direction *= new Quaternion(0, -1, 0, 0);
		transform.rotation = Direction;
	}
}
