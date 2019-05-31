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

	private Vector2 Position_After_Death { set; get; }

	private void Start()
	{
		Position_After_Death = new Vector2(0.0f, 2.0f);
		Make_Incapacitated();
	}

	void Update()
    {
			if(transform.position.x >= -40.0f)
			{
				Vector2 tmp = transform.position;
				tmp.x -= speed;
				transform.position = tmp;
			}
			if(transform.position.x <= -40.0f)
			{
				Make_Incapacitated();
			}
    }

	private void OnTriggerEnter(Collider other)
	{
		// プレイヤーの弾と衝突したとき
		if(other.tag == "Player_Bullet")
		{
			Game_Master.MY.Score_Addition(my_score);
			Make_Incapacitated();
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
