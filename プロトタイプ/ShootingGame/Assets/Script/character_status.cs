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
	public float hp;                                            // 体力
	private float hp_Max;
	public Vector3 direction;                                   // 向き
	public CapsuleCollider capsuleCollider;                     // cillider
    public int Shot_DelayMax;                                   // 弾を打つ時の間隔（最大値::unity側にて設定）
    public int Shot_Delay;                                 // 弾を撃つ時の間隔
	public Renderer renderer;				//メッシュレンダラーの情報を入れる
	public void Hide_Object()
	{
		renderer.enabled = false;
	}
	public void HP_Setting()
	{
		hp_Max = hp;
	}
	public void Reset_Status()
	{
		hp = hp_Max;
	}
}