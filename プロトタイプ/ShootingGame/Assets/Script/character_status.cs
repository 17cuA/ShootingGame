﻿/*
 * 2019/05/27   Rigidbodyの削除
 */
using UnityEngine;
using Power;
public class character_status : MonoBehaviour
{
	protected enum Chara_Type
	{
		Player,
		Enemy,
		None
	}
	protected Chara_Type Type;
	public float speed;                                         // スピード
	public int hp;                                            // 体力
	private int hp_Max;
	public Vector3 direction;                                   // 向き
	public CapsuleCollider capsuleCollider;                     // cillider
	private Rigidbody rigidbody;                                //rigitbody
	public int Shot_DelayMax;                                   // 弾を打つ時の間隔（最大値::unity側にて設定）
	public int Shot_Delay;                                 // 弾を撃つ時の間隔
	public uint score;							// 保持しているスコア
	public int shield;                                      //シールド（主にプレイヤーのみ使うと思う）
	public bool activeShield;           //現在シールドが発動しているかどうかの判定用（初期値false）

	private void Start()
	{
		rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
		rigidbody.useGravity = false;
		capsuleCollider = GetComponent<CapsuleCollider>();
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
		if (gameObject.tag != "Player")
		{
			//スコア
			Game_Master.MY.Score_Addition(score);
			SE_Manager.SE_Obj.SE_Active(9);
			//爆発処理の作成
			ParticleCreation(5);
		}
		else
		{
			//爆発処理の作成
			ParticleCreation(0);
		}

		//Debug.Log("hei");
		Reset_Status();
		//死んだらゲームオブジェクトを遠くに飛ばす処理
		transform.position = new Vector3(0, 800.0f, 0);
		//稼働しないようにする
		Debug.Log(gameObject.transform.parent.name + "	Destroy");
		gameObject.SetActive(false);

	}
	//パーティクルの作成（爆発のみ）
	public GameObject ParticleCreation(int particleID)
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		//Instantiate(Obj_Storage.Storage_Data.particle[particleID], gameObject.transform.position, Obj_Storage.Storage_Data.particle[particleID].transform.rotation);
		GameObject effect = Obj_Storage.Storage_Data.Effects[particleID].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();
		effect.transform.position = gameObject.transform.position;
		particle.Play();
		return effect;
	}
	//自分以外の玉と当たった時にダメージを食らう
	private void OnTriggerEnter(Collider col)
	{
		if (gameObject.tag == "Player")
		{
			if (col.tag == "Item")
			{
				var item = col.GetComponent<Item>();
				if (item.itemType != ItemType.Item_KillAllEnemy)
				{
					PowerManager.Instance.Pick();
					SE_Manager.SE_Obj.SE_Active(5);
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
		if (gameObject.tag == "Enemy")
		{
			if (col.tag == "Player_Bullet")
			{
				bullet_status BS = col.gameObject.GetComponent<bullet_status>();
				Damege_Process((int)BS.attack_damage);
			}
			else if(col.tag == "Player")
			{
				Damege_Process(1);
			}
		}
	}
}