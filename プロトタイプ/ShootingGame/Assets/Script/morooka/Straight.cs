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
		hp = 1;
		capsuleCollider = GetComponent<CapsuleCollider>();  //カプセルコライダーの情報取得
		transform.eulerAngles = new Vector3(0, -180, 0);
	}

	// Update is called once per frame
	void Update()
    {
		Died_Process(hp);
        transform.position = transform.position + new Vector3(-1, 0, 0) * speed;
    }
	void Died_Process(float hp)
	{
		//体力が1未満だったらオブジェクトの消去
		if (hp < 1)
        { 
          Destroy(gameObject);
            Game_Master.MY.Score_Addition(100);
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
			//------------------------------
		}
	}
}
