//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossのパーツの挙動
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの情報の格納
//----------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class BossParts : MonoBehaviour
{
    public bool invincible;				// 無敵確認
    public int HP;							// HP
	public Renderer my;				// 自分のレンダー
	public Color originalColor;		// 自分の元の色の格納

    void Start()
    {
		my = GetComponent<Renderer>();
		originalColor = my.material.color;
    }

    void Update()
    {
        if(HP < 1)
        {
            Destroy(gameObject);
        }
    }

	/// <summary>
	/// コライダー
	/// </summary>
	/// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Bullet")
        {
            Destroy(other.gameObject);
			//　自分が無敵でないとき
            if(!invincible)
            {
                StartCoroutine(Effect());
                HP -= (int)other.GetComponent<bullet_status>().attack_damage;
            }
        }
    }

	/// <summary>
	/// ダメージ表現のコルーチン
	/// </summary>
	/// <returns></returns>
    IEnumerator Effect()
    {
		my.material.color = Color.white;
		// ディレイの時間
		yield return new WaitForSeconds(0.1f);
		my.material.color = originalColor;
	}
}