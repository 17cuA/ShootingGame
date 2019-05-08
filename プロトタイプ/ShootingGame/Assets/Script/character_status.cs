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
	public Rigidbody Rig;                                       // Rigidbody
	public Vector3 direction;                                   // 向き
	public CapsuleCollider capsuleCollider;                     // cillider
    public int Shot_DelayMax;                                   // 弾を打つ時の間隔（最大値::unity側にて設定）
    public int Shot_Delay;                                 // 弾を撃つ時の間隔
	//public Chara_Type Get_Type(Chara_Type type) { return type; }
	//// Start is called before the first frame update
	//void Start()
	//{
	//	// Rigidbodyを格納
	//	rigidbody = GetComponent<Rigidbody>();
	//	// CapsuleColliderを格納
	//	capsuleCollider = GetComponent<CapsuleCollider>();
	//}

	//// Update is called once per frame
	//void Update()
	//{
	//	direction = transform.forward;

	//}

	//// get関数
	//// hp
	//public float get_hp()
	//{
	//	return hp;
	//}
	//// スピード
	//public float get_speed()
	//{
	//	return speed;
	//}
	//// Rigidbody
	//public Rigidbody get_rigidbody()
	//{
	//	return rigidbody;
	//}
	//// CapsuleCollider
	//public CapsuleCollider get_capsuleCollider()
	//{
	//	return capsuleCollider;
	//}
	//// 向き
	//public Vector3 get_direction()
	//{
	//	return direction;
	//}

	//// add関数
	//// hp
	//public void add_hp(float addHP)
	//{
	//	hp = addHP;
	//}
	//// スピード
	//public void add_speed(float addSpeed)
	//{
	//	speed = addSpeed;
	//}
	//// Rigidbody
	//public void add_rigidbody(Rigidbody addRigidbody)
	//{
	//	rigidbody = addRigidbody;
	//}
	//// CapsuleCollider
	//public void add_capsuleCollider(CapsuleCollider addCapsuleCollider)
	//{
	//	capsuleCollider = addCapsuleCollider;
	//}
	//// 向き
	//public void add_direction(Vector3 addDirection)
	//{
	//	direction = addDirection;
	//}

}