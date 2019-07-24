﻿/*
 * 2019/05/27   Rigidbodyの削除
 */
using UnityEngine;
using Power;
using UnityEngine.SceneManagement;

public class character_status : MonoBehaviour
{
	protected enum Chara_Type
	{
		Player,
		Enemy,
		None
	}
	protected Chara_Type Type;
	public float speed;                                                 // スピード
	private float speed_Max;
	public int hp;                                                      // 体力
	private int hp_Max;
	public Vector3 direction;                                           // 向き
	public CapsuleCollider capsuleCollider;                             // cillider
	private Rigidbody rigidbody;                                        //rigitbody
	public int Shot_DelayMax;                                           // 弾を打つ時の間隔（最大値::unity側にて設定）
	public int Shot_Delay;                                              // 弾を撃つ時の間隔
	public uint score;                                                  // 保持しているスコア
	private int shield;                                                 //シールド（主にプレイヤーのみ使うと思う）
	public bool activeShield;                                           //現在シールドが発動しているかどうかの判定用（初期値false）
	public int Remaining;                                               //残機（あらかじめ設定）
	public float v_Value;                                               //テクスチャの明るさの増える値
	public int childCnt;
	public Renderer[] object_material;                                  // オブジェクトのマテリアル情報
	public bool isrend = false;
	public bool Is_Dead = false;
	[SerializeField] private Material[] self_material;									//初期マテリアル保存用
	[Header("ダメージ用material設定")]
	public Material white_material;                                    //ダメージくらったときに一瞬のホワイト
	private int framecnt;
	private bool check;
	public void Start()
	{
		//rigidbodyがアタッチされているかどうかを見てされていなかったらアタッチする（Gravityも切る）
		if (!gameObject.GetComponent<Rigidbody>())
		{
			rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
			rigidbody.useGravity = false;
		}
		//CapsuleColliderがついていたら取得する
		if (gameObject.GetComponent<CapsuleCollider>())
		{
			capsuleCollider = GetComponent<CapsuleCollider>();
		}

		if (tag == "Player") Remaining = 3;
		else Remaining = 1;
		white_material = Resources.Load<Material>("Material/Damege_Effect") as Material;
		self_material = new Material[object_material.Length];
		for (int i = 0; i < self_material.Length; i++) self_material[i] = object_material[i].material;
		HP_Setting();
		framecnt = 0;
		check = false;
	}
	public void Update()
	{
		for (int i = 0; i < object_material.Length; i++)
		{
			if (check)
			{
				if (framecnt > 1)
				{
					material_Reset();
					framecnt = 0;
					check = false;
					return;
				}
				framecnt++;
			}
		}

	}
	//初期の体力を保存
	public void HP_Setting()
	{
		hp_Max = hp;
	}
	//再利用可能にするための処理
	public void Reset_Status()
	{
		hp = hp_Max;
	}
	//ダメージを与える関数
	public void Damege_Process(int damege)
	{
		hp -= damege;
	}
	/// <summary>
	/// 死んだときに呼び出される
	/// </summary>
	public void Died_Process()
	{
		if (transform.name == "Middle_Boss" || transform.name == "Enemy_MiddleBoss_Father")
		{
			//スコア
			Game_Master.MY.Score_Addition(score);
			SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[7]);
			//爆発処理の作成
			ParticleCreation(7);
			Is_Dead = true;
			Reset_Status();
		}
		else if (gameObject.tag != "Player")
		{
			//スコア
			Game_Master.MY.Score_Addition(score);
			SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[9]);
			//爆発処理の作成
			ParticleCreation(4);
			Is_Dead = true;
			Reset_Status();

		}
		else
		{
			//爆発処理の作成
			ParticleCreation(0);
			Is_Dead = true;
			Reset_Status();
		}

		//死んだらゲームオブジェクトを遠くに飛ばす処理
		transform.position = new Vector3(0, 800.0f, 0);
		//稼働しないようにする
		//Debug.Log(gameObject.transform.parent.name + "	Destroy");
		material_Reset();
		gameObject.SetActive(false);

	}
	//パーティクルの作成（爆発のみ）
	public GameObject ParticleCreation(int particleID)
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		//Instantiate(Obj_Storage.Storage_Data.particle[particleID], gameObject.transform.position, Obj_Storage.Storage_Data.particle[particleID].transform.rotation);
		GameObject effect = Obj_Storage.Storage_Data.Effects[particleID].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();

		//爆発の位置をランダムに変更
		float range = 1.0f;
		Vector3 temp = new Vector3(Random.Range(-range, range), Random.Range(-range, range),0.0f);
		effect.transform.position = transform.position + temp.normalized;
		/*********************************************************/
		particle.Play();
		return effect;
	}
	//自分以外の玉と当たった時にダメージを食らう
	private void OnTriggerEnter(Collider col)
	{
		if (gameObject.tag == "Player" || tag == "Invincible")
		{
			if (col.tag == "Item")
			{
				var item = col.GetComponent<Item>();
				if (item.itemType != ItemType.Item_KillAllEnemy)
				{
					PowerManager.Instance.Pick();
					SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[5]);
					col.gameObject.SetActive(false);
				}
				else
					PowerManager.Instance.Annihilate();
			}
			if (col.tag == "Enemy_Bullet")
			{
				bullet_status BS = col.gameObject.GetComponent<bullet_status>();
				if(activeShield && shield != 0)
				{
					shield--;
				}
				else
				{
					Damege_Process((int)BS.attack_damage);
				}
			}
			if (col.tag == "Enemy")
			{
				if (activeShield && shield != 0)
				{
					shield--;
				}
				else
				{
					Damege_Process(1);
				}
			}
		}
		if (tag == "Enemy")
		{
			if (col.tag == "Player_Bullet")
			{
				bullet_status BS = col.gameObject.GetComponent<bullet_status>();
				Damege_Process((int)BS.attack_damage);
				Damege_Effect();

			}
			else if(col.gameObject.name == "Player")
			{
				Damege_Process(1);
			}
		}
	}
	//キャラクターが死んだかどうかの判定用関数
	public bool Died_Judgment()
	{
		bool is_died = false;
		if (hp < 1 && Remaining < 1) is_died = true;
		return is_died;
	}
	//キャラクターが死んでいるかどうかの判定用関数
	public bool Dead_Check()
	{
		bool isDead = false;
		if (hp < 1 && Remaining > 0) isDead = true;
		return isDead;
	}
	//明るさを変える関数
	public void HSV_Change()
	{
		v_Value = 1.0f - transform.position.z * 0.015f;

		if (v_Value > 1.0f)
		{
			v_Value = 1.0f;
		}

		foreach (Renderer renderer in object_material)
		{
			renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
		}
	}
	//ダメージを食らうとダメージエフェクトが走るように
	private void Damege_Effect()
	{
		for (int i = 0; i < object_material.Length; i++)
		{
			object_material[i].material = white_material;
			check = true;
			//object_material[i].enabled = false;
		}
	}
	//ダメージを受けた時のエフェクトが元のエフェクトに戻すための関数
	public void material_Reset()
	{
		for (int i = 0; i < object_material.Length; i++)
		{
			object_material[i].material = self_material[i];
			//object_material[i].enabled = true;
		}
	}
	//シールドの値を取得する
	public int Get_Shield()
	{
		return shield;
	}
	//シールドの値設定
	public void Set_Shield(int setnum)
	{
		shield = setnum;
	}
	//キャラクタの設定してある体力を取得するための関数
	public uint Get_Score()
	{
		return score;
	}
}