//作成日2019/04/22
//弾の移動管理
//作成者:佐藤翼
using UnityEngine;

public class bullet_status : MonoBehaviour
{
	public enum Bullet_Type
	{
		Single,
		Double,
		None
	}
	public Bullet_Type Type;
	private Rigidbody rb;//rigidbody、物理系
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	private float DTime;        //デストロイまでの時間のカウント
	private int Destroy_Time;   //デストロイする時間
	private Vector3 progressVector_F;       //　自身の進行ベクトル
	public Vector3 ProgressVector_F //　自身の進行ベクトル
	{
		get
		{
			return progressVector_F;
		}
	}
	private Renderer Bullet_Renderer; // 判定したいオブジェクトのrendererへの参照
	void Start()
	{
		rb = this.GetComponent<Rigidbody>();//このオブジェクトのrigidbodyを取得
		DTime = 0;
		Destroy_Time = 1;
		MovementDirectionSpecification(transform.right);
		Bullet_Renderer = GetComponent<Renderer>();
	}

	void Update()
	{
		//DTime += Time.deltaTime;
		//if (DTime > Destroy_Time) Destroy(gameObject);

		//if (transform.position.x > 8.8f || transform.position.x < -8)
		//{
		//	Destroy(gameObject);
		//}
		if (!Bullet_Renderer.isVisible)
		{
			Destroy(gameObject);
		}
	}
	/// <summary>
	/// 移動向きの指定
	/// 移動向きの指定、進行方向にオブジェクトの前（フロント）を合わせる
	/// </summary>
	/// <param name="vector"></param>
	private void MovementDirectionSpecification(Vector3 vector)
	{
		//　進行ベクトルの変更
		progressVector_F = vector;
		//　方進行方向に力を加える
		rb.velocity = progressVector_F.normalized * shot_speed;
		//　進行方向に向きを合わせる
		transform.right = progressVector_F;
	}
	private void OnTriggerEnter(Collider col)
	{
		//それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
		if (gameObject.name == "Enemy_Bullet(Clone)" && col.gameObject.tag == "Player")
		{
			Destroy(gameObject);

			//add:0513_takada 爆発エフェクトのテスト
			AddExplosionProcess();
		}
		if (gameObject.name == "Player_Bullet(Clone)" && col.gameObject.tag == "Enemy")
		{
			Destroy(gameObject);

			//add:0513_takada 爆発エフェクトのテスト
			AddExplosionProcess();
		}
	}

	private void AddExplosionProcess()
	{
		ParticleManagement particleManagementCS;
		particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
		//particleManagementCS.ParticleCreation(0, transform.position);
	}
}
