//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/04/25
//----------------------------------------------------------------------------------------------
// Bossのパーツの挙動
//----------------------------------------------------------------------------------------------
// 2019/04/25：体パーツの情報
//----------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BossParts : MonoBehaviour
{
	private int AttackInterval { get; set; }                    // 攻撃のインターバル
    public GameObject EffectExplosion { get; private set; }     // エフェクトのプレハブ格納
    public GameObject Bullet { get; private set; }                                  // 攻撃用の弾

    public Material[] materials;
    public MeshRenderer look;

    [SerializeField]
    public bool invincible;                 // 無敵確認
    [SerializeField]
    private int HP;
    [SerializeField]
    private bool ShouldAttack;        // 攻撃するかしないか

    void Start()
    {
        look = GetComponent<MeshRenderer>();
        Bullet = Resources.Load("Enemy_Bullet") as GameObject;
        AttackInterval = 2;
    }

    void Update()
    {
        if(ShouldAttack)
		{
            if((Game_Master.MY.Frame_Count % AttackInterval) == 0)
            {
                Instantiate(Bullet, transform.position, Quaternion.Euler(transform.right));
                print("aaaaa");
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
                StartCoroutine(Effect());
                HP -= (int)other.GetComponent<bullet_status>().attack_damage;
            }
        }
    }

    IEnumerator Effect()
    {
        Debug.Log("A");
        look.materials[0] = materials[1];
        // ディレイの時間
        yield return new WaitForSeconds(1.0f);
        Debug.Log("B");
        look.materials[0] = materials[0];
    }
}