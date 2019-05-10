using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Data : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public Rigidbody Rig;
    public SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(-1, 0, 0);
        speed = 1;
        Rig = GetComponent<Rigidbody>();                    //rigidbodyの情報取得
        sphereCollider = GetComponent<SphereCollider>();  //カプセルコライダーの情報取得
        transform.eulerAngles = new Vector3(0, -180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Rig.velocity = direction * speed;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
