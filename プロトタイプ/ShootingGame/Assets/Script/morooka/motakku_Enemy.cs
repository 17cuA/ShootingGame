//作成日2019/06/13
// motakku_Enemyの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/14 攻撃の追加
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class motakku_Enemy : character_status
{
	private const int Beam_Muzzle_Cnt = 2;		// ビーム型のバレット
	private const int Bullet_Muzzle_Cnt = 6;
	private GameObject[] Beam_Mazle { get; set; }		//ビーム型バレットの発射口
	private GameObject[] Shot_Mazle { get; set; }       //攻撃の発射口に使用

	void Start()
    {
		Beam_Mazle = new GameObject[Beam_Muzzle_Cnt];
		Shot_Mazle = new GameObject[Bullet_Muzzle_Cnt];
		for (int i = 0; i < transform.childCount; i++)
		{
			Shot_Mazle[i] = transform.GetChild(i).gameObject;
		}
	}
void func()
{

}
	void Update()
    {
       
    }
}
