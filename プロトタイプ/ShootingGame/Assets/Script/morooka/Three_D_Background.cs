using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three_D_Background : MonoBehaviour
{
    private Transform[] backs = new Transform[3];
    private float speed = 0.5f;
    private Vector3 reset = new Vector3(60.0f, 0.0f, 0.0f);
    void Start()
    {
        for(int i = 0;i<3;i++)
        {
            backs[i] = transform.GetChild(i).GetComponent<Transform>();
        }
    }

    void Update()
    {
        foreach(Transform t in backs)
        {
            Vector3 temptrans = t.localPosition;
            temptrans.x -= speed;
            t.localPosition = temptrans;

            if(t.localPosition.x <= -120)
            {
                t.localPosition = reset;
            }
        }
    }
}
