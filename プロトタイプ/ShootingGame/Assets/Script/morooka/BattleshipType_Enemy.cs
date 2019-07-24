//作成日2019/07/08
// 戦艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/08　挟み込み移動と弾を撃つ行動
 * 2019/07/08　まっすぐ移動
 * 2019/07/09　バレットの打ち出しタイミングの変更(ベリーイージー → ノーマル)
 * 2019/07/15　奥行対応
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class BattleshipType_Enemy : character_status
{
	[SerializeField, Header("初期位置")] private Vector3 initial_position;
	[SerializeField, Header("移動変更ポイント")] private Vector3[] moving_change_point;
	[SerializeField, Header("はさみ込むタイプはTrue")] public bool is_sandwich;
	[SerializeField, Header("マズルセット上")] private Transform[] muzzle_set_up;
	[SerializeField, Header("マズルセット下")] private Transform[] muzzle_set_Down;

	public Vector3 Original_Position { get; set; }		// 元の位置
	public int Now_Target { get; set; }					// 現在の移動目標番号
	public Vector3 Move_Facing { get; set; }			// Y軸の移動向き
	public Vector3 Moving_Facing { get; set; }		// 移動向き
	public int Muzzle_Select { get; set; }				// マズル指定
	public List<GameObject> Bullet_Object { get; set; } // 自身が発射した弾の情報の保存
	public List<BattleshipType_Battery> Child_Scriptes { get; set; }		// 子供のスクリプト
	public List<MeshRenderer> Parts_Renderer { get; set; }					// パーツたちのレンダー

	private void Start()
	{
		Original_Position = transform.position = initial_position;
		Now_Target = 0;
		// 初期位置と目標位置のX軸の中間
		Moving_Facing = new Vector3(-1.0f, 0.0f,0.0f);
		Muzzle_Select = 0;
		Shot_Delay = Shot_DelayMax / 3;
		Bullet_Object = new List<GameObject>();
		Child_Scriptes = new List<BattleshipType_Battery>();
		Parts_Renderer = new List<MeshRenderer>();

		for (int i = 0; i < transform.childCount; i++)
		{
			BattleshipType_Battery b = transform.GetChild(i).GetComponent<BattleshipType_Battery>();
			if (b != null)
			{
				Child_Scriptes.Add(b);
				Parts_Renderer.Add(Child_Scriptes[i].GetComponent<MeshRenderer>());
			}
		}
	}

	private void Update()
	{
		HSV_Change();

		//// 移動したい向きに移動
		Vector3 velocity = Moving_Facing.normalized * speed;
		transform.position = transform.position + velocity;
		// 挟み込み型の挙動
		if (is_sandwich)
		{
			// 移動先に近づいたとき
			// ターゲット番号が要素数を超えていないとき
			//if (Vector_Size(transform.position, moving_change_point[Now_Target]) <= speed
			//	&& Now_Target < moving_change_point.Length - 1 )
			if (Vector_Size(transform.position, moving_change_point[Now_Target]) <= speed
				&& Now_Target < moving_change_point.Length - 1 )
			{
				// 位置を指定
				Original_Position = transform.position = moving_change_point[Now_Target];

				// 向きを確認
				Moving_Facing = moving_change_point[Now_Target + 1] - moving_change_point[Now_Target];
				// 別途保存
				Move_Facing = Moving_Facing;
				//// 0にする
				//Moving_Facing = Vector3.zero;
				// ターゲットを次へ
				Now_Target++;
			}
			//else
			//{
			//	transform.position = Moving_To_Target(transform.position, moving_change_point[Now_Target], speed / 5.0f);

			//}
			// Y軸を少しづつ加算するため
			if (Moving_Facing != Move_Facing)
			{
				Vector3 temp = Moving_Facing;
				temp += Move_Facing / 40.0f;
				Moving_Facing = temp;
			}
		}

		// 自身のZ軸が0のとき攻撃する
		if (transform.position.z == 0.0f)
		{
			Shot_Delay++;
			if (Shot_Delay > Shot_DelayMax)
			{
				Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_up[Muzzle_Select].position, -muzzle_set_up[Muzzle_Select].right));
				Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_Down[Muzzle_Select].position, -muzzle_set_Down[Muzzle_Select].right));
				Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_up[Muzzle_Select + 2].position, -muzzle_set_up[Muzzle_Select + 2].right));
				Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_Down[Muzzle_Select + 2].position, -muzzle_set_Down[Muzzle_Select + 2].right));
				Muzzle_Select++;
				if (Muzzle_Select == 2)
				{
					Muzzle_Select = 0;
				}

				Shot_Delay = 0;
			}
		}

		// 保管したバレットの確認
		for (int i = 0; i< Bullet_Object.Count;i++)
		{
			// バレットが起動しているとき
			if (Bullet_Object[i].activeSelf)
			{
				// 機体のX軸移動距離と同じ距離をバレットにも移動させる
				Vector3 pos = Bullet_Object[i].transform.position;
				//pos.x += velocity.x;
				Bullet_Object[i].transform.position = pos;
			}
			// 起動していないとき
			else
			{
				// バレット情報のリリース
				Bullet_Object.RemoveAt(i);
			}
		}

		// 本体がHP０以下のとき
		if (hp <= 0)
		{
			for (int i = 0; i < Child_Scriptes.Count; i++)
			{
				if (Child_Scriptes[i].gameObject.activeSelf)
				{
					Child_Scriptes[i].Died_Process();
				}
			}

			Bullet_Object.Reverse();

			Died_Process();
		}
	}

	void OnEnable()
	{
		transform.position = initial_position;
		Now_Target = 0;
		Shot_Delay = 0;

		if (Child_Scriptes != null)
		{
			// 子供が不能のとき、再起動
			for (int i = 0; i < Child_Scriptes.Count; i++)
			{
				if (!Child_Scriptes[i].gameObject.activeSelf)
				{
					Child_Scriptes[i].ReBoot();

					//Child_Scriptes[i].gameObject.SetActive(true);

					//character_status c = transform.GetChild(i).GetComponent<character_status>();
					//c.Died_Process();
				}
			}
		}
	}

	/// <summary>
	/// ベクトルの長さを出す
	/// </summary>
	/// <param name="a"> 開始座標 </param>
	/// <param name="b"> 目標座標 </param>
	/// <returns></returns>
	private float Vector_Size( Vector3 a, Vector3 b )
	{
		float xx = a.x - b.x;
		float yy = a.y - b.y;
		float zz = a.z - b.z;

		return Mathf.Sqrt(xx * xx + yy * yy + zz * zz);
	}

	/// <summary>
	/// 移動モードの設定(0：上側の移動、1：中心直進、2：下側の移動)
	/// </summary>
	/// <param name="num"></param>
	public void Moving_Mode_Preference(int num)
	{
		switch(num)
		{
			case 0:
				is_sandwich = false;
				break;
			case 1:
				is_sandwich = true;
				break;
			case 2:
				is_sandwich = false;
				for(int i =0;i< moving_change_point.Length;i++)
				{
					moving_change_point[i].y *= -1.0f;
				}
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target(Vector3 origin,Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;		// 移動する前のターゲットとの向き
		Vector3 return_pos	 = Vector3.zero;				// 返すポジション
		
		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		if (Vector_Size(return_pos, target) <= speed)
		{
			return_pos = target;
		}

		return return_pos;
	}
}
