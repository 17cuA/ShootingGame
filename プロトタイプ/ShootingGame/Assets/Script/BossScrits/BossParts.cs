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
	public bool ShouldAttack { get; private set; }		        // 攻撃するかしないか
	private int AttackInterval { get; set; }                    // 攻撃のインターバル
    public GameObject EffectExplosion { get; private set; }     // エフェクトのプレハブ格納

    public GameObject bullet;                                  // 攻撃用の弾
    [SerializeField]
    public bool invincible;                 // 無敵確認
    [SerializeField]
    private int HP;

    void Start()
    {
    }

    void Update()
    {
        if(ShouldAttack)
		{
            if(Game_Master.MY.Frame_Count % AttackInterval == 0)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(transform.forward));
            }
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
        if (other.tag == "Player_Bullet")
        {
            if (invincible)
            {
                Destroy(other.gameObject);
            }
            else
            {
                HP -= (int)other.GetComponent<bullet_status>().attack_damage;
            }
        }
    }
}