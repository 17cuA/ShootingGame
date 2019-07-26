//作成日2019/07/09
// UFO母艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　UFO型エネミーの放出
 * 2019/07/10　敵の各門6体ずつ放出にする
 * 2019/07/10　1/24でアイテムの敵生成（アイテムの敵がプーリングになし、現状空き）
 * 2019/07/11　放出したエネミーがすべてX軸を同じ量だけ移動できるようにする
 * 2019/07/15　奥行対応
 */
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class UfoMotherType_Enemy : character_status
{
	[SerializeField,Header("回転速度")]				private float rotating_velocity;				// 回転速度
	[SerializeField,Header("エネミー放出孔")]			private Transform[] shot_mazle;		// エネミーを放出するための穴
	[SerializeField,Header("放出のインターバル")]		private int release_interval;			// エネミーの放出のインターバル
	[SerializeField, Header("安全位置_Z軸のみ")]		private float safety_position;			// 安全位置_Z軸のみ

	private int Sortie_Number { get; set; }                     // 一度に放出する、一つのエネミー放出孔から出るエネミー数
	private int Shot_Cnt { get; set; }							// 放出したエネミーのカウント
	private int Interval_Cnt { get; set; }						// エネミー放出のインターバルのカウント
	private List<GameObject> Released_Enemy { get; set; }		// 放出した後のエネミーの保存
	private Vector3 Moving_Direction { get; set; }				// 機体の移動方向
	private int Depth_Move_Interval_Cnt { get; set; }			// 奥行移動用インターバルカウント
	private bool Is_AttackEnd { get; set; }                     // 攻撃終了しているかどうか

	public float Initial_Speed { get; set; }                // 初速(最低速度)
	public float Max_Speed { get; set; }                    // 最大速度
	public float Deceleration_Distance { get; set; }        // 加減速開始移動量
	public Vector3 Original_Position { get; set; }      // 元の位置
	public Vector3 Target_Position { get; set; }		// 次の位置

	new void Start()
	{
		Sortie_Number = 6;
		Shot_Cnt = Sortie_Number;
		Interval_Cnt = 0;
		Released_Enemy = new List<GameObject>();
		Moving_Direction = new Vector3(-1.0f, 0.0f, 0.0f);

		if(transform.position.z != safety_position)
		{
			Vector3 temp = transform.position;
			temp.z = safety_position;
			Original_Position = transform.position = temp;
		}
		Is_AttackEnd = false;

		// 加減速用初期化群
		Max_Speed = speed;
		speed = Initial_Speed = speed / 60.0f;
		for (int i = 0; i < 60; i++)
		{
			Deceleration_Distance += speed;
			speed += Initial_Speed;
		}
	}

	new void Update()
	{
		HSV_Change();

		if(hp < 0)
		{
			Died_Process();
		}

		// 攻撃が終わっていないとき
		if(!Is_AttackEnd)
		{
			// 攻撃開始
			if (Shot_Cnt < Sortie_Number)
			{
				Shot_Delay++;
				// エネミーの放出間隔を超えたとき
				if (Shot_Delay > Shot_DelayMax)
				{
					for (int i = 0; i < shot_mazle.Length; i++)
					{
						// 一度の放出で1体の確率でアイテムを持つエネミーを放出
						if (Random.Range(0, Sortie_Number * shot_mazle.Length) == 0)
						{
							Released_Enemy.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY_ITEM, shot_mazle[i].position, -shot_mazle[i].right));
						}
						// 普通のエネミーを放出
						else
						{
							Released_Enemy.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY, shot_mazle[i].position, -shot_mazle[i].right));
						}
					}
					Shot_Delay = 0;
					Shot_Cnt++;

				}
			}
			else if (Shot_Cnt == Sortie_Number)
			{
				Vector3 temp = Original_Position = transform.position;
				temp.z = safety_position;
				Target_Position = temp;

				Is_AttackEnd = true;
			}
		}
		// 放出が終わっているとき
		else if (Is_AttackEnd)
		{
			if (Interval_Cnt < release_interval)
			{
				if (transform.position != Original_Position)
				{
					//transform.position = Vector3.MoveTowards(transform.position, temp_2, speed * 5.0f);

					if (Vector_Size(transform.position, Original_Position) < Deceleration_Distance)
					{
						if (speed < Max_Speed) speed += Initial_Speed;
					}
					else if (Vector_Size(transform.position, Target_Position) < Deceleration_Distance)
					{
						if (speed > Initial_Speed) speed -= Initial_Speed;
					}
					transform.position = Moving_To_Target(transform.position, Target_Position, speed);
				}
				if (transform.position == Target_Position)
				{
					Interval_Cnt++;
					speed = Initial_Speed;
				}
			}
			//インターバルを超えたとき
			else if (Interval_Cnt == release_interval)
			{
				// Z軸が0になるまで移動
				Vector3 temp_1 = transform.position;
				temp_1.z = 0.0f;
				if (transform.position != temp_1)
				{
					//transform.position = Vector3.MoveTowards(transform.position, temp_1, speed * 5.0f);

					if (Vector_Size(transform.position, Original_Position) < Deceleration_Distance)
					{
						if (speed < Max_Speed) speed += Initial_Speed;
					}
					else if (Vector_Size(transform.position, temp_1) < Deceleration_Distance)
					{
						if (speed > Initial_Speed) speed -= Initial_Speed;
					}
					transform.position = Moving_To_Target(transform.position, temp_1, speed);
				}
				if (transform.position == temp_1)
				{
					Interval_Cnt = 0;
					Shot_Cnt = 0;
					Original_Position = transform.position;
					Is_AttackEnd = false;
					speed = Initial_Speed;
				}
			}
		}

		// 機体の回転
		transform.Rotate(new Vector3(0.0f, 0.0f, rotating_velocity));

		// 機体の移動したい方向に移動
		Vector3 total_move_direction = Moving_Direction.normalized * speed;
		transform.position = transform.position + total_move_direction;

		// 放出したエネミーの生存確認
		for (int i = 0; i < Released_Enemy.Count; i++)
		{
			// 生存時、期待と同じだけ＋αに移動する
			if (Released_Enemy[i].activeSelf)
			{
				Released_Enemy[i].transform.position += total_move_direction;
			}
			// 生存していないときリリース
			else
			{
				Released_Enemy.RemoveAt(i);
			}
		}
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
}
