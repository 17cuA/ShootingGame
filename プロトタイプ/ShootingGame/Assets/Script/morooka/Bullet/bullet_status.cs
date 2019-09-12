//作成日2019/04/22
//弾の移動管理
//作成者:佐藤翼
/*
 * 2019/06/06	バレットの挙動をオブジェクトプーリングの形式に変更しました
 * 2019/06/13	継承用クラスに変更
 * 2019/06/26	レンダーの初期化法の変更
 */
using System;
using System.Collections;
using UnityEngine;

public class bullet_status : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	public Vector3 Travelling_Direction;    //自分の向き
	[SerializeField]
	private Renderer Bullet_Renderer = null; // 判定したいオブジェクトのrendererへの参照
    public Player1 P1 { get; private set; }
    public Player2 P2 { get; private set; }
    public Bit_Shot bShot { get; set; }
	public int Player_Number { get;  set; }

	public enum Type
	{
		None,
		Player1,
		Player2, 
		Player1_Option,
		Player2_Option,
		Enemy,
	}

	public Type Bullet_Type;        //各キャラクタの弾かどうかを判定する変数

	private void Awake()
	{
		gameObject.AddComponent<PauseComponent>();
	}
	protected void Start()
	{
		if(Bullet_Renderer == null) Bullet_Renderer = GetComponent<Renderer>();
		Travelling_Direction = transform.right;

		switch (Bullet_Type)
		{
			case Type.Player1:
				P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
				Player_Number = 1;
				break;
			case Type.Player2:
				P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
				Player_Number = 2;
				break;
			case Type.Player1_Option:
                //bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
				break;
			case Type.Player2_Option:
                //bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
                break;
			case Type.Enemy:
				break;
			case Type.None:
				break;
			default:
				break;
		}
		//if (gameObject.name == "Player1_Bullet")
  //      {
  //          P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		//	Player_Number = 1;
		//}
  //      else if (gameObject.name == "Player2_Bullet")
  //      {
  //          P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
		//	Player_Number = 2;
		//}
	}

	protected void Update()
	{
		if (transform.position.x >= 19.0f || transform.position.x <= -19.0f
			|| transform.position.y >= 10.5f || transform.position.y <= -10.5f)
		{
			if (gameObject.tag == "Player_Bullet")
			{
				switch (Bullet_Type)
				{
					case Type.Player1:
						if (P1.Bullet_cnt > 0)P1.Bullet_cnt--;
						break;
					case Type.Player2:
						if (P2.Bullet_cnt > 0)P2.Bullet_cnt--;
						break;
					case Type.Player1_Option:
						if (bShot.Bullet_cnt > 0)bShot.Bullet_cnt--;
						break;
					case Type.Player2_Option:
						if (bShot.Bullet_cnt > 0)bShot.Bullet_cnt--;
						break;
					case Type.Enemy:
						break;
					case Type.None:
						break;
					default:
						break;
				}
			}
			gameObject.SetActive(false);
		}
	}

	protected void OnTriggerEnter(Collider col)
	{
        //それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
        if (gameObject.tag == "Enemy_Bullet" && (col.gameObject.name == "Enemy_Meteor_One" || col.gameObject.name == "Enemy_Meteor_Two" || col.gameObject.name == "Enemy_Meteor_Three" || col.gameObject.name == "Enemy_Meteor_four" || col.gameObject.name == "Enemy_Meteor_Five"))
        {
            gameObject.SetActive(false);

        }
        else if ((gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player"))
        {
            gameObject.SetActive(false);
            //add:0513_takada 爆発エフェクトのテスト
            //AddExplosionProcess();
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
        }
		else if(gameObject.tag == "Player_Bullet " && col.name == "Bacula")
		{
			character_status obj = col.GetComponent<character_status>();
			if (obj != null)
			{
				obj.Opponent = Player_Number;
			}
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
			gameObject.SetActive(false);
			Player_Bullet_Des();
		}
        else if (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy")
        {
            //add:0513_takada 爆発エフェクトのテスト
            //AddExplosionProcess();
            character_status obj = col.GetComponent<character_status>();
            if (obj != null)
            {
                obj.Opponent = Player_Number;
            }
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
			Player_Bullet_Des();
			//if (P1 != null) P1.Bullet_cnt--;
			//if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			//{
			//    if (P2 != null) P2.Bullet_cnt--;
			//}
			gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Boss_Gard")
        {
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
            gameObject.SetActive(false);
        }
	}

	/// <summary>
	/// 爆発エフェクト生成
	/// </summary>
	protected void AddExplosionProcess()
	{
		ParticleManagement particleManagementCS;
		particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	}

	/// <summary>
	/// 向きの変更
	/// </summary>
	/// <param name="_Dir"> 向きたい向き </param>
	protected void Moving_Facing_Preference(Vector3 _Dir)
	{
		transform.right = _Dir;
		Travelling_Direction = _Dir;
	}

	/// <summary>
	/// 移動したい向きのみ変更
	/// </summary>
	/// <param name="_Dir"> 移動方向 </param>
	protected void FacingChange(Vector3 _Dir)
	{
		Travelling_Direction = _Dir;
	}

	/// <summary>
	/// 向いている方向に移動
	/// </summary>
	protected void Moving_To_Facing()
	{
		Vector3 temp_Pos = transform.right.normalized * shot_speed;
		transform.position += temp_Pos;
	}

	/// <summary>
	/// 進行方向に移動
	/// </summary>
	public void Moving_To_Travelling_Direction()
	{
		Vector3 tempPos = Travelling_Direction.normalized * shot_speed;
		transform.position += tempPos;
	}

	/// <summary>
	/// タグの変更
	/// </summary>
	/// <param name="tag_name"> 変更したいタグ名 </param>
	protected void Tag_Change(string tag_name)
	{
		gameObject.tag = tag_name;
	}

	public void Player_Bullet_Des()
	{
		switch (Bullet_Type)
		{
			case Type.None:
				break;
			case Type.Player1:
				P1.Bullet_cnt = 0;
				if(P1.Is_Change_Auto)
				{
					P1.shoot_number = 0;
				}
				break;
			case Type.Player2:
				P2.Bullet_cnt = 0;
				if(P2.Is_Change_Auto)
				{
					P2.shoot_number = 0;
				}
				break;
			case Type.Player1_Option:
				bShot.Bullet_cnt = 0;
				break;
			case Type.Player2_Option:
				bShot.Bullet_cnt = 0;
				break;
			case Type.Enemy:
				break;
			default:
				break;
		}
	}
}
