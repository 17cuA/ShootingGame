using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test : MonoBehaviour
{
	private Rigidbody rb2;
	private float speed = 1.0f;
    private BossAll ba;
    private List<BossParts> parts;

    private float attack_interval = 20.0f;
    private GameObject bullet;

    void Start()
    {
		rb2 = GetComponent<Rigidbody>();
		Vector3 vector3 = transform.right.normalized * speed * -1;
		rb2.velocity = vector3;
        ba = GetComponent<BossAll>();
        parts = new List<BossParts>();
        foreach(BossParts bp in ba.OwnParts)
        {
            parts.Add(bp);
        }

        bullet = Resources.Load("Enemy_Bullet") as GameObject ;
	}

    void Update()
    {
		if(transform.position.x <= 8.5f)
		{
			rb2.velocity = Vector3.zero;
		}

        if((Game_Master.MY.Frame_Count % attack_interval) == 0)
        {
            foreach (BossParts bp in parts)
            {
                if(!bp.invincible)
                {            
                    Instantiate(bullet,bp.transform.position, bp.transform.rotation);
                }
            }
        }
    }
}
