/*
 * 2019/05/27   Rigidbodyの削除
 */
using UnityEngine;

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
    public int Shot_DelayMax;                                   // 弾を打つ時の間隔（最大値::unity側にて設定）
    public int Shot_Delay;                                 // 弾を撃つ時の間隔

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
	public void Damege_Process(int damege)
	{
		hp -= damege;
	}
	/// <summary>
	/// 死んだときに呼び出される
	/// </summary>
	public void Died_Process()
	{
		//体力が1未満だったらオブジェクトの消去
		if (hp < 0)
		{
			//スコア
			Game_Master.MY.Score_Addition(100);
			//爆発処理の作成
			ParticleCreation(gameObject,0);

			//Debug.Log("hei");
			Reset_Status();
			//死んだらゲームオブジェクトを遠くに飛ばす処理
			transform.position = new Vector3(0, 800.0f,0);
			//稼働しないようにする
			gameObject.SetActive(false);
			Debug.Log(gameObject.transform.parent.name + "	Destroy");
		}
	}
	//パーティクルの作成（爆発のみ）
	public void ParticleCreation(GameObject gameObject, int particleID)
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		Instantiate(Obj_Storage.Storage_Data.particle[particleID], gameObject.transform.position, Obj_Storage.Storage_Data.particle[particleID].transform.rotation);
	}
	private void OnTriggerEnter(Collider col)
	{
		bullet_status BS = col.gameObject.GetComponent<bullet_status>();
		Damege_Process((int)BS.attack_damage);
		Debug.Log(gameObject.name + "	damege");
	}
}