//作成日2019/04/22
//弾の移動管理
//作成者:佐藤翼
/*
 * 2019/06/06	バレットの挙動をオブジェクトプーリングの形式に変更しました
 */
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
	private Renderer Bullet_Renderer; // 判定したいオブジェクトのrendererへの参照
	void Start()
	{
		Bullet_Renderer = GetComponent<Renderer>();
		//gameObject.SetActive(false);
	}

	void Update()
	{
		transform.position = transform.position +( transform.right.normalized * shot_speed);

		if (!Bullet_Renderer.isVisible)
		{
			gameObject.SetActive(false);
		}
	}
	private void OnTriggerEnter(Collider col)
	{
		//それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
		if (gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);

			//add:0513_takada 爆発エフェクトのテスト
			AddExplosionProcess();
		}
		if (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy")
		{
			gameObject.SetActive(false);

			//add:0513_takada 爆発エフェクトのテスト
			AddExplosionProcess();
		}
	}

	private void AddExplosionProcess()
	{
		ParticleManagement particleManagementCS;
		particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	}
}
