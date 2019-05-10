using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_test_2 : MonoBehaviour
{
    private Rigidbody rb2;
    private float speed = 1.0f;
    private BossAll bossAll;


    int 
    void Start()
    {
        rb2 = GetComponent<Rigidbody>();
        Vector3 vector3 = transform.right.normalized * speed * -1;
        rb2.velocity = vector3;
        bossAll = GetComponent<BossAll>();
    }

    void Update()
    {
        if (transform.position.x <= 8.5f)
        {
            rb2.velocity = Vector3.zero;
        }

        if((Game_Master.MY.Frame_Count % 180) == 0)
        {
            
        }
    }
}
