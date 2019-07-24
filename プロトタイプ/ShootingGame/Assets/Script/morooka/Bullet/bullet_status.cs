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
	//public enum Bullet_Type
	//{
	//	Single,
	//	Double,
	//	None
	//}
	//public Bullet_Type Type;
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	public Vector3 Travelling_Direction;    //自分の向き
	[SerializeField]
	private Renderer Bullet_Renderer = null; // 判定したいオブジェクトのrendererへの参照
	protected void Start()
	{
		if(Bullet_Renderer == null) Bullet_Renderer = GetComponent<Renderer>();
		Travelling_Direction = transform.right;
	}

	protected void Update()
	{
		//if(!Bullet_Renderer.isVisible)
		//{
		//	Debug.LogError("消えた？");
		//	gameObject.SetActive(false);
		//}
		if(transform.position.x >= 19.0f || transform.position.x <= -19.0f
			|| transform.position.y >= 5.5f || transform.position.y <= -5.5f)
		{
			gameObject.SetActive(false);
		}
	}

	protected void OnTriggerEnter(Collider col)
	{
		//それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
		if ((gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player"))
		{
			gameObject.SetActive(false);
			//add:0513_takada 爆発エフェクトのテスト
			//AddExplosionProcess();
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();

		}
		else if(gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy")
		{
			gameObject.SetActive(false);
			//add:0513_takada 爆発エフェクトのテスト
			//AddExplosionProcess();
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
		}
		else if(gameObject.tag == "Enemy_Bullet" && gameObject.tag == "Player_Bullet")
		{ }
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
}
