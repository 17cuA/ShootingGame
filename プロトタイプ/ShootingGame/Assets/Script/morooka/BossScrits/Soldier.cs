//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/30
//----------------------------------------------------------------------------------------------
// 兵隊蜂の処理
//----------------------------------------------------------------------------------------------
// 2019/05/30：移動処理
// 2019/05/31：クラス名変更 Soldier_Beeー＞Soldier
//----------------------------------------------------------------------------------------------
using UnityEngine;

public class Soldier : MonoBehaviour
{
	[SerializeField]
	[Header("移動速度")]
	private float speed;
	[SerializeField]
	[Header("スコア")]
	private uint my_score;

	private Renderer My_Renderer { set; get; }				// 自分のレンダー
	private Vector2 Position_After_Death { set; get; }		// 死亡判定後の位置
	private Vector3 direction { set; get; }					// 自分の向き
	private GameObject Player_Data { set; get; }            // プレイヤーの情報格納用
	public bool Is_Attack_Completed { private set; get; }
	public uint Delay = 0;
	private uint cnt;
	private int num = 0;

	private void Start()
	{
		Player_Data = Game_Master.MY.GetComponent<MapCreate>().GetPlayer();
		My_Renderer = GetComponent<SpriteRenderer>();
		Position_After_Death = new Vector2(50.0f, 50.0f);
		Make_Incapacitated();
		gameObject.SetActive(false);
	}

	void Update()
	{
			if (transform.position.x > 24.0f)
			{
				Vector3 vector = transform.position;
				vector.x -= speed;
				transform.position = vector;
				cnt = Game_Master.MY.Frame_Count + Delay;
			}
			else if (transform.position.x <= 24.0f && num == 0)
			{
				if (Game_Master.MY.Frame_Count == cnt)
				{
					transform.right = transform.position - Player_Data.transform.position;
					num++;
				}
			}
			else if (transform.position.x <= 24.0f && num == 1)
			{
				if (transform.position.x >= -40.0f)
				{
					transform.position = transform.position - transform.right.normalized * speed;
				}
				if (transform.position.x <= -40.0f)
				{
					//Make_Incapacitated();
					Is_Attack_Completed = false;
				}
			}
	}

	private void OnTriggerEnter(Collider other)
	{
		// プレイヤーの弾と衝突したとき
		if(other.tag == "Player_Bullet")
		{
			Game_Master.MY.Score_Addition(my_score);
			Make_Incapacitated();
			Destroy(other.gameObject);
		}
		else if(other.tag == "Player")
		{
			Make_Incapacitated();
		}
	}

	/// <summary>
	/// 攻撃用意
	/// </summary>
	/// <param name="startPos"> 移動の開始位置 </param>
	public void Attack_Preparation(Vector2 startPos, uint receiving_delay)
	{
		My_Renderer.enabled = true;
		transform.position = startPos;
		transform.right = Vector3.zero;
		Delay = receiving_delay;
		Is_Attack_Completed = true;
	}

	/// <summary>
	/// 行動不能化処理
	/// </summary>
	public void Make_Incapacitated()
	{
		num = 0;
		transform.position = Position_After_Death;
		My_Renderer.enabled = false;
		Is_Attack_Completed = false;
	}
}
