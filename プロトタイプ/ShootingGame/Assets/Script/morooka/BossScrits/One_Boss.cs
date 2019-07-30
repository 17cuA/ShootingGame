//作成日2019/07/30
// 一面のボス本番
// 作成者:諸岡勇樹
/*
 * 2019/07/30　グリッド移動の適応
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss : character_status
{
	/*
	 * 2019/07/30　グリッド移動
	 */
	//-----------------------------------------------------------------------------------------------------------------------------//
	Vector3 MOVEX = new Vector3(0.175f, 0, 0); // x軸方向に１マス移動するときの距離
	Vector3 MOVEY = new Vector3(0, 0.175f, 0); // y軸方向に１マス移動するときの距離
	Vector3 Target { get; set; }     // 入力受付時、移動後の位置を算出して保存 
	Vector3 Prev_Pos { get; set; }     // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存
	//-----------------------------------------------------------------------------------------------------------------------------//

	[SerializeField] private GameObject core;
	[SerializeField] private GameObject[] Muzzles;

	private One_Boss_Parts Core { get; set; }

	public float Max_Speed { get; set; }
	public float Now_Speed { get; set; }
	public float Lowest_Speed { get; set; }
	public float Speed​_Change_Distance {get;set;}

	public GameObject Player_Data { get; private set; }		// プレイヤーのデータ
    // Start is called before the first frame update
    private new void Start()
    {
		base.Start();

		Core = core.GetComponent<One_Boss_Parts>();
		Player_Data = Obj_Storage.Storage_Data.GetPlayer();

		Max_Speed = speed;
		Now_Speed = Lowest_Speed = Max_Speed / 20.0f;
		for(;Now_Speed <= Max_Speed ;Now_Speed += Lowest_Speed )
		{
			Speed​_Change_Distance += Now_Speed;
		}
		Now_Speed = Lowest_Speed;

		Target = transform.position = Vector3.zero;
    }

    // Update is called once per frame
    private new void Update()
    {
		base.Update();
		if (Core.hp < 1)
		{
			base.Died_Judgment();
			base.Died_Process();
		}

		Player_Tracking_Movement_Attack();
	}

	private void Player_Tracking_Movement_Attack()
	{
		Vector3 temp = transform.position;
		temp.y = Player_Data.transform.position.y;

		if(Player_Data.transform.position.y != transform.position.y)
		{
			if(Target == transform.position)
			{
				Prev_Pos = Target;
				if(transform.position.y > Player_Data.transform.position.y)
				{
					Target = transform.position - MOVEY;
				}
				else if(transform.position.y < Player_Data.transform.position.y)
				{
					Target = transform.position + MOVEY;
				}
			}

			if (Vector_Size(temp, transform.position) < Speed_Change_Distance)
			{
				if(Now_Speed > Lowest_Speed)	Now_Speed -= Lowest_Speed;
			}
			else if(Vector_Size(temp, transform.position) >= Speed_Change_Distance)
			{
				if (Now_Speed < Max_Speed) Now_Speed += Lowest_Speed;
			}
			transform.position = Moving_To_Target(transform.position, Target, Now_Speed);
		}
		else if(Player_Data.transform.position.y == transform.position.y)
		{

		}
	}

	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target(Vector3 origin, Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;       // 移動する前のターゲットとの向き
		Vector3 return_pos = Vector3.zero;              // 返すポジション

		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		if (Vector_Size(return_pos, target) <= speed)
		{
			return_pos = target;
		}

		return return_pos;
	}

	/// <summary>
	/// ベクトルの長さを出す
	/// </summary>
	/// <param name="a"> 開始座標 </param>
	/// <param name="b"> 目標座標 </param>
	/// <returns></returns>
	private float Vector_Size(Vector3 a, Vector3 b)
	{
		float xx = a.x - b.x;
		float yy = a.y - b.y;
		float zz = a.z - b.z;

		return Mathf.Sqrt(xx * xx + yy * yy + zz * zz);
	}
}
