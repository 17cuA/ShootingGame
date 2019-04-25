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
	private float DTime;		//デストロイまでの時間のカウント
	private int Destroy_Time;   //デストロイする時間
	private Vector3 progressVector_F;       //　自身の進行ベクトル
	public Vector3 ProgressVector_F
	{
		get { return progressVector_F; }
	}
	void Start()
	{
		rb = this.GetComponent<Rigidbody>();//このオブジェクトのrigidbodyを取得
		DTime = 0;
		Destroy_Time = 2;
		MovementDirectionSpecification(transform.right);
	}

	void Update()
	{
		DTime += Time.deltaTime;
		if (DTime > Destroy_Time) Destroy(gameObject);
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
	private void OnCollisionEnter(Collision col)
	{
		Destroy(gameObject);
	}
}
