//作成日2019/06/13
// エネミーのビーム型バレットの管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	ビーム型のバレットの動きの制御
 */
using UnityEngine;

public class Beam_Bullet : bullet_status
{
	void Start()
    {
		base.Start();
		//Tag_Change("Enemy");
	}

	void Update()
    {
		base.Update();
		Moving_To_Facing();
    }
}
