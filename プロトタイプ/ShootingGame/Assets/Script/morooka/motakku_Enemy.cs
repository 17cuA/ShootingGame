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
	private GameObject[] Shot_Mazle { get; set; }		//攻撃の発射口に使用
	private GameObject[] Beam_Mazle { get; set; }	//ビーム型バレットの発射口

	void Start()
    {
		//Shot_Mazle = new GameObject[];
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
