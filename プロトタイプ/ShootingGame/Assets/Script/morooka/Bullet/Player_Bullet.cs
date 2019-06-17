//作成日2019/06/13
// プレイヤーのショットの制御
// 作成者:諸岡勇樹
/*
 * 2019/06/14 バレットの基底クラスの継承、動きの追加
 */
using UnityEngine;

public class Player_Bullet : bullet_status
{
    void Start()
    {
		gameObject.tag = "Player_Bullet";
	}

    // Update is called once per frame
    void Update()
    {
		Moving_To_Facing();
	}
}
