//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossのパーツの挙動
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの情報
//----------------------------------------------------------------------------------------------

using UnityEngine;

public class BossParts : MonoBehaviour
{
	[SerializeField]
	public bool ShouldAttack { get; private set; }		        // 攻撃するかしないか
	private int AttackInterval { get; set; }                    // 攻撃のインターバル
    public int HP { get; private set; }                         // パーツ自身のHP
    public GameObject EffectExplosion { get; private set; }     // エフェクトのプレハブ格納

    public GameObject bullet;                                  // 攻撃用の弾

    //public bool invincible;                // 無敵確認
    [SerializeField]
    private bool invincible;                 // 無敵確認

    void Start()
    {

    }

    void Update()
    {
        if(ShouldAttack)
		{
            
		}
		else if(!ShouldAttack)
		{

		}

        if(HP == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(invincible)
        {
            if(other.name == "Player_Bullet")
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            HP -= (int)other.GetComponent<bullet_status>().attack_damage;
        }
    }
}