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
	private GameObject Player_Data { set; get; }			// プレイヤーの情報格納用

	private void Start()
	{
		Player_Data = Game_Master.MY.GetComponent<MapCreate>().GetPlayer();
		My_Renderer = GetComponent<SpriteRenderer>();
		Position_After_Death = new Vector2(0.0f, 2.0f);
		Make_Incapacitated();
	}

	void Update()
	{
		if (transform.position.x > 28.0f)
		{
			Vector3 vector = transform.position;
			vector.x -= speed;
			transform.position = vector;
		}
		else if(transform.position.x <= 28.0f && transform.right == Vector3.zero)
		{
			transform.right = transform.position - Player_Data.transform.position;
		}
		else if (transform.position.x <= 28.0f && transform.right != Vector3.zero)
		{
			if (transform.position.x >= -40.0f)
			{
				transform.position = transform.position - transform.right.normalized * speed;
			}
			if (transform.position.x <= -40.0f)
			{
				Make_Incapacitated();
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
	/// 攻撃開始
	/// </summary>
	/// <param name="startPos"> 移動の開始位置 </param>
	public void Attack_Start(Vector2 startPos)
	{
		transform.position = startPos;
		transform.right = Vector3.zero;
	}

	/// <summary>
	/// 行動不能化処理
	/// </summary>
	public void Make_Incapacitated()
	{
		transform.position = Position_After_Death;
		gameObject.SetActive(false);
	}
}
