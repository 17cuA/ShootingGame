using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : character_status
{
    public bool Item_Flag;
    public GameObject Item;
    // Start is called before the first frame update
    void Start()
    {
		direction = new Vector3(-1, 0, 0);
		Type = Chara_Type.Enemy;
		speed = 1;
		hp = 1;
		Rig = GetComponent<Rigidbody>();					//rigidbodyの情報取得
		capsuleCollider = GetComponent<CapsuleCollider>();  //カプセルコライダーの情報取得
		transform.eulerAngles = new Vector3(0, -180, 0);
	}

	// Update is called once per frame
	void Update()
    {
		Rig.velocity = direction * speed;
		Died_Process(hp);
    }
	void Died_Process(float hp)
	{
		//体力が1未満だったらオブジェクトの消去
		if (hp < 1)
        { 
          Destroy(gameObject);
            if (Item_Flag==true)
            {
            Instantiate(Item, transform.position, transform.rotation);
            }
        }
	}
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.transform.name == "Player_Bullet(Clone)")
		{
			//弾のダメージを取得するための弾のステータスの情報取得
			bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
			//弾のダメージの値だけ体力を減らす
			hp -= Bs.attack_damage;
		}
        //----------------------------------------------------------
        if (col.gameObject.tag == "Beam") hp--;
        if (col.gameObject.transform.name == "Beam_Particle") hp--;
         //----------------------------------------------------------

	}
 	void OnParticleCollision(GameObject obj)
    {
		
        if(obj.gameObject.tag==("Beam"))
        { 
            hp--;
        }
        else if(obj.gameObject.tag==("wall"))
        { 
           
        }
	}
}
