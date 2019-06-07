using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : character_status
{
    public bool Item_Flag;
    public GameObject Item;
	private Direction Dn;		//爆発パーティクル呼び出し用情報
    // Start is called before the first frame update
    void Start()
    {
		direction = new Vector3(-1, 0, 0);
		Type = Chara_Type.Enemy;
		hp = 1;
		speed = 0.1f;
		capsuleCollider = GetComponent<CapsuleCollider>();  //カプセルコライダーの情報取得
		transform.eulerAngles = new Vector3(0, -180, 0);
		Dn = gameObject.GetComponent<Direction>();
		HP_Setting();
	}

	// Update is called once per frame
	void Update()
    {
		switch (Game_Master.MY.Management_In_Stage)
		{
			case Game_Master.CONFIGURATION_IN_STAGE.eNORMAL:
				Died_Process(hp);
				transform.position = transform.position + new Vector3(-0.1f, 0, 0) * speed;
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_CUT_IN:
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eBOSS_BUTTLE:
				Died_Process(hp);
				transform.position = transform.position + new Vector3(-0.1f, 0, 0) * speed;
				break;
			case Game_Master.CONFIGURATION_IN_STAGE.eCLEAR:
				break;
			default:
				break;
		}

    }

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.transform.tag == "Player_Bullet")
		{
			//弾のダメージを取得するための弾のステータスの情報取得
			bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
			//弾のダメージの値だけ体力を減らす
			hp -= (int)Bs.attack_damage;
			//------------------------------
		}

	}

}
