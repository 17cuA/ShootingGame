//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/30
//----------------------------------------------------------------------------------------------
// 兵隊蜂の処理
//----------------------------------------------------------------------------------------------
// 2019/05/30：移動処理
//----------------------------------------------------------------------------------------------
using UnityEngine;

public class Soldier_Bee : MonoBehaviour
{
	[SerializeField]
	[Header("移動速度")]
	private float speed;
	[SerializeField]
	[Header("スコア")]
	private uint my_score;

	private bool is_alive { set; get; }
	private MeshRenderer MyRender { set; get; }


	void Start()
    {
		is_alive = false;
		MyRender = GetComponent<MeshRenderer>();
		MyRender.enabled = false;
    }

    void Update()
    {
        if(is_alive)
		{

		}
    }

	private void OnTriggerEnter(Collider other)
	{
		// プレイヤーの弾と衝突したとき
		if(other.tag == "Player_Bullet")
		{
			is_alive = false;
			MyRender.enabled = false;
			Game_Master.MY.Score_Addition(my_score);
		}
	}

	public void Attack_Start()
	{
		is_alive = true;
		MyRender.enabled = true;
	}
}
