//作成日2019/07/08
// 戦艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/08　挟み込み移動と弾を撃つ行動
 * 2019/07/08　まっすぐ移動
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class BattleshipType_Enemy : character_status
{
	[SerializeField, Header("初期位置")] private Vector3 initial_position;
	[SerializeField, Header("移動変更ポイント")] private Vector3[] moving_change_point;
	[SerializeField, Header("はさみ込むタイプはTrue")] private bool is_sandwich;
	[SerializeField, Header("マズルセット上")] private Transform[] muzzle_set_up;
	[SerializeField, Header("マズルセット下")] private Transform[] muzzle_set_Down;

	public int Now_Target { get; set; }					// 現在の移動目標番号
	public float Y_Move_Facing { get; set; }			// Y軸の移動向き
	public Vector3 Moving_Facing { get; set; }		// 移動向き
	public int Muzzle_Select { get; set; }				// マズル指定
	public List<GameObject> Bullet_Object { get; set; }

	void Start()
	{
		transform.position = initial_position;
		Now_Target = 0;
		// 初期位置と目標位置のX軸の中間
		Moving_Facing = new Vector3(-1.0f, 0.0f,0.0f);
		Muzzle_Select = 0;
		Shot_Delay = Shot_DelayMax / 3;
		Bullet_Object = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		// 移動したい向きに移動
		Vector3 velocity = Moving_Facing.normalized * speed;
		transform.position = transform.position + velocity;
		// 挟み込み型の挙動
		if (is_sandwich)
		{
			// X軸が移動先に近づいたとき
			// Y軸が移動先に近づいたとき
			// ターゲット番号が要素数を超えていないとき
			//if (Mathf.Abs(transform.position.x - moving_change_point[Now_Target].x) < speed
			//	&& Mathf.Abs(transform.position.y - moving_change_point[Now_Target].y) < speed
			//	&& Now_Target < moving_change_point.Length - 1)			
			if (Vector_Size(transform.position, moving_change_point[Now_Target]) <= speed
				&& Now_Target < moving_change_point.Length - 1)
			{
				// 向きを確認
				Moving_Facing = moving_change_point[Now_Target + 1] - moving_change_point[Now_Target];
				// Y軸の向きのみ別途保存
				Y_Move_Facing = Moving_Facing.y;
				// Y軸のみ0にする
				Vector3 temp = Moving_Facing;
				temp.y = 0;
				Moving_Facing = temp;
				// ターゲットを次へ
				Now_Target++;
			}
			// Y軸を少しづつ加算するため
			if (Moving_Facing.y != Y_Move_Facing)
			{
				Vector3 temp = Moving_Facing;
				temp.y += Y_Move_Facing / 20.0f;
				Moving_Facing = temp;
			}
		}

		Shot_Delay++;
		if (Shot_Delay > Shot_DelayMax)
		{
			Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_up[Muzzle_Select].position, muzzle_set_up[Muzzle_Select].right));
			Bullet_Object.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, muzzle_set_Down[Muzzle_Select].position, muzzle_set_Down[Muzzle_Select].right));
			Muzzle_Select++;
			if (Muzzle_Select == muzzle_set_up.Length)
			{
				Muzzle_Select = 0;
				Shot_Delay = 0;
			}
			else
			{
				Shot_Delay = Shot_DelayMax - 60;
			}
		}

		// 保管したバレットの確認
		for(int i = 0; i< Bullet_Object.Count;i++)
		{
			// バレットが起動しているとき
			if (Bullet_Object[i].activeSelf)
			{
				// 機体のX軸移動距離と同じ距離をバレットにも移動させる
				Vector3 pos = Bullet_Object[i].transform.position;
				pos.x += velocity.x;
				Bullet_Object[i].transform.position = pos;
			}
			// 起動していないとき
			else
			{
				// バレット情報のリリース
				Bullet_Object.RemoveAt(i);
			}
		}
	}

	void OnEnable()
	{
		transform.position = initial_position;
		Now_Target = 0;
		Shot_Delay = Shot_DelayMax / 3;
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
		float yy = a.y - a.y;

		return Mathf.Sqrt(xx * xx + yy * yy);
	}
}
