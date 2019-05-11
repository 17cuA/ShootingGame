using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test_2 : MonoBehaviour
{
    private Rigidbody rb2;
    private float speed = 1.0f;
    private BossAll bossAll;

    private int animeindex;

    void Start()
    {
        rb2 = GetComponent<Rigidbody>();
        Vector3 vector3 = transform.right.normalized * speed * -1;
        rb2.velocity = vector3;
        bossAll = GetComponent<BossAll>();
        animeindex = 0;
    }

    void Update()
    {
        if (transform.position.x <= 8.5f)
        {
            rb2.velocity = Vector3.zero;
        }

        if((Game_Master.MY.Frame_Count % 180) == 0)
        {
            if(animeindex == 0)
            {
                bossAll.animationControl.Play("gado");
                //bossAll.animationControl.SetBool("gado", true);
                //bossAll.animationControl.SetBool("nogado", false);
            }
            else if(animeindex == 1)
            {
                bossAll.animationControl.Play("nogado");
                //bossAll.animationControl.SetBool("gado", false);
                //bossAll.animationControl.SetBool("nogado", true);
            }
            else
            {
                //bossAll.animationControl.SetBool("gado", false);
                //bossAll.animationControl.SetBool("nogado", false);
            }

            animeindex++;
            if(animeindex == 3)
            {
                animeindex = 0;
            }
        }
}
}