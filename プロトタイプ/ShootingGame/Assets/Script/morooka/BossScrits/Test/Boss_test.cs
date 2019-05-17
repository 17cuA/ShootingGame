using UnityEngine;
using System.Collections;

public class Boss_test : MonoBehaviour
{
	enum attackshurui
	{
		bullet,
		tosshin,
	}

	private Rigidbody rb2;
	private float speed = 1.0f;
    private BossAll ba;
    private float attack_interval = 20.0f;
    private GameObject bullet;
	private GameObject[] hand = new GameObject[2];
	private GameObject main_body;
	private GameObject playerData;

	bool ata;
	private Vector3 resetLocal;
	private attackshurui shurui;


	int num;
    void Start()
    {
		rb2 = GetComponent<Rigidbody>();
		Vector3 vector3 = transform.right.normalized * speed * -1;
		rb2.velocity = vector3;
        ba = GetComponent<BossAll>();
        bullet = Resources.Load("Enemy_Bullet") as GameObject ;
		foreach (BossParts bp in ba.OwnParts)
		{
			if(bp.name == "Boss_test_tate (2)")
			{
				hand[0] = bp.gameObject;
			}
			else if(bp.name == "Boss_test_tate (1)")
			{
				hand[1] = bp.gameObject;
			}
			else if(bp.name == "Boss_Body")
			{
				main_body = bp.gameObject;
			}
		}

		shurui = attackshurui.bullet;
		playerData = Game_Master.MY.GetComponent<MapCreate>().GetPlayer();
		resetLocal = hand[0].transform.localPosition;

		num = 0;
		ata = false;
	}

    void Update()
    {
		if (rb2.velocity != Vector3.zero)
		{
			if (transform.position.x <= 8.5f)
			{
				rb2.velocity = Vector3.zero;
			}
		}
		else
		{
			switch (shurui)
			{
				case attackshurui.bullet:
					BulletAttack();
					break;
				case attackshurui.tosshin:
					Tosshin();
					break;
				default:
					break;
			}

		}
    }


	/// <summary>
	/// 通常の弾の攻撃
	/// </summary>
	private void BulletAttack()
	{
		if ((Game_Master.MY.Frame_Count % attack_interval) == 0)
		{
			foreach (BossParts bp in ba.OwnParts)
			{
				if (bp.my.isVisible)
				{
					if (!bp.invincible)
					{
						Instantiate(bullet, bp.transform.position, bp.transform.rotation);
					}
				}
			}
		}
		if (!ata)
		{
			StartCoroutine(Effect());
		}
	}

	private void Tosshin()
	{
        if(hand[0] != null)
        {
		    Vector3 vector = (hand[0].transform.position - playerData.transform.position) * -1.0f;
		    if (num == 0)
		    {
			    hand[0].transform.right = vector;
			    num++;
		    }
		    else if(num == 1)
		    {
			    print("GGGGGGGGGG");
			    hand[0].transform.position = hand[0].transform.position + hand[0].transform.right.normalized * 0.1f;

			    if(hand[0].transform.position.y <= -7.0f)
			    {
				    num++;
				    hand[0].transform.localPosition = resetLocal;
				    hand[0].transform.right = new Vector3(-1.0f, 0.0f, 0.0f);
                    shurui = attackshurui.bullet;
                    num = 0;
                }
            }
        }
        else
        {
            shurui = attackshurui.bullet;
        }
    }

	IEnumerator Effect()
	{
		if(!ata)
		{
			ata = true;
		}
		yield return new WaitForSeconds(1.1f);
		ata = false;
		shurui = attackshurui.tosshin;
	}
}
