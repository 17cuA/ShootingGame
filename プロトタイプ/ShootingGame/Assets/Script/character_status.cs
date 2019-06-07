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

	public void HP_Setting()
	{
		hp_Max = hp;
	}
	public void Reset_Status()
	{
		hp = hp_Max;
	}
	public void Died_Process(int hp)
	{
		//体力が1未満だったらオブジェクトの消去
		if (hp < 1)
		{
			Game_Master.MY.Score_Addition(100);
			ParticleCreation(gameObject,0);

			//Debug.Log("hei");
			Reset_Status();

			gameObject.SetActive(false);

		}
	}
	public void ParticleCreation(GameObject gameObject, int particleID)
	{

		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		Obj_Storage.Storage_Data.particleGameObject = Instantiate(Obj_Storage.Storage_Data.particle[particleID], gameObject.transform.position, Obj_Storage.Storage_Data.particle[particleID].transform.rotation);

	}
}