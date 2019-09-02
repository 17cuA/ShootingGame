//作成者：川村良太
//リング状の敵バレットスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RingBullet : bullet_status
{
    // Start is called before the first frame update
    new void Start()
    {
		base.Start();
		Tag_Change("Enemy_Bullet");

	}

	// Update is called once per frame
	new void Update()
    {
		base.Update();
		Moving_To_Facing();

	}
	private new void OnTriggerEnter(Collider col)
	{
        if (col.tag == "Player_Bullet" || col.gameObject.name == "face" || col.gameObject.name == "mouth")
		{
			gameObject.SetActive(false);
		}
        base.OnTriggerEnter(col);
	}

}
