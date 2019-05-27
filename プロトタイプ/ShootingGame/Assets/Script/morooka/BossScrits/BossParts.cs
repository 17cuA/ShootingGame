//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossのパーツの挙動
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの情報の格納
// 2019/05/04：ダメージを受けた時の発光表現
// 2019/05/24：画面外時当たり判定をなくす
// 2019/05/24：HP 0 時の挙動変更
//----------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class BossParts : MonoBehaviour
{
    public bool invincible;				// 無敵確認

	public Renderer My { private set; get;}				        // 自分のレンダー
	public Color OriginalColor {private set; get; }             // 自分の元の色の格納
    public Collider Collider_Storing { private set; get;}       // 自分のコライダーの格納
	public int HP { set; get; }                                 // HP

	void Start()
    {
		My = GetComponent<Renderer>();
		OriginalColor = My.material.color;
        Collider_Storing = GetComponent<Collider>();
        Collider_Storing.enabled = false;
    }

    void Update()
    {
        // 表示中
        if(My.isVisible)
        {
            DisplayingProcess();
        }
        else
        {
        }
    }

	/// <summary>
	/// コライダー
	/// </summary>
	/// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        ColliderDisplayingProcess(other);
    }
	/// <summary>
	/// ダメージ表現のコルーチン
	/// </summary>
	/// <returns></returns>
    IEnumerator Effect()
    {
        // マテリアルのカラーを白くする
		My.material.color = Color.white;
		// ディレイの時間
		yield return new WaitForSeconds(0.1f);
        // マテリアルのカラーを元に戻す
		My.material.color = OriginalColor;
	}

    /// <summary>
    /// 表示中の処理
    /// </summary>
    private void DisplayingProcess()
    {
        // コライダーが動いていないとき
        if (!Collider_Storing.enabled)
        {
            Collider_Storing.enabled = true;        // コライダー起動
        }

        // HPが1以下の時
        if (HP < 1)
        {
            Game_Master.MY.Score_Addition(1500);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// コライダー用の表示中の処理
    /// </summary>
    /// <param name="other">衝突した相手</param>
    private void ColliderDisplayingProcess(Collider other)
    {
        // 当たった相手がプレイヤーの攻撃のとき
        if (other.tag == "Player_Bullet")
        {
            // 相手の削除
            Destroy(other.gameObject);
            //　自分が無敵でないとき
            if (!invincible)
            {
                StartCoroutine(Effect());
                HP -= (int)other.GetComponent<bullet_status>().attack_damage;
            }
        }
    }
}