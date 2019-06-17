//作成日2019/06/13
// motakku_Enemyの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/14 攻撃の追加
 */
using UnityEngine;
using StorageReference;
using System.Collections.Generic;
public class motakku_Enemy : character_status
{
	private const int Beam_Muzzle_Cnt = 2;		// ビーム型のバレット
	private const int Bullet_Muzzle_Cnt = 6;
	private List<GameObject> Beam_Mazle { get; set; }		//ビーム型バレットの発射口
	private List<GameObject> Shot_Mazle { get; set; }       //攻撃の発射口に使用

	void Start()
    {
		Beam_Mazle = new List<GameObject>();
		Shot_Mazle = new List<GameObject>();

		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject obj = transform.GetChild(i).gameObject;

			if(obj.name == "Muzzle_Side")
			{
				Beam_Mazle.Add(obj.gameObject);
			}
			else if(obj.name == "Muzzle_Front")
			{
				Shot_Mazle.Add(obj.gameObject);
			}
		}
	}
	void Update()
    {
       if(Game_Master.MY.Frame_Count % Shot_DelayMax == 0)
		{
			Object_Instantiation.Object_Reboot("Player_Missile", transform.position, Quaternion.identity);
		}
    }
}
