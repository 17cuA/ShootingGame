using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Fish_Spown : MonoBehaviour
{
	int frame;      //生成タイミングを計る用
	public int frameMax;    //生成のタイミングを設定する用
	int attack_type;    //攻撃対象をプレイヤーの数に応じて変更するため

	Vector3[,] pos = new Vector3[32,42];	//出現位置を入れておくもの

	float xpos = 0.28f;		//x軸の倍率
	float ypos = 0.18f;     //y軸の倍率

	int x = 0;  //配列のｘを変更するための変数
	int y = 0;	//配列のｙを変更するための変数

	private void Start()
	{
		//for(int i = 0; i < 32; i++)
		//{
		//	for(int j = 0; j < 42; j++)
		//	{
		//		pos[i, j] = new Vector3(-1 + xpos * j, 3 - ypos * i, 0);
		//	}
		//}
	}
	private void OnEnable()
	{
		frame = 0;
		attack_type = 0;
	}

    void Update()
    {

		Respown_Enemy();
    }
	void Respown_Enemy()
	{
		frame++;

		if(frame > frameMax)
		{
			
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
				ESF.transform.position = new Vector3(0,0,0);
				ESF.Attack_Target_Decision(attack_type % 2);
				attack_type++;
				//x++;
				//y++;
				frame = 0;
			}
			else
			{
				Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
				ESF.transform.position = new Vector3(0, 0, 0);
				//ESF.Attack_Target_Decision(attack_type % 2);
				//x++;
				//y++;
				frame = 0;
			}

			if(x > 31)
			{
				x = 0;
			}
			if(y > 41)
			{
				y = 0;
			}
		}
	}
}
