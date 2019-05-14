using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three_D_Background : MonoBehaviour
{
    private Transform[] backs = new Transform[3];
    private float speed = 0.3f;
    private Vector3 reset = new Vector3(60.0f, 0.0f, 0.0f);

    public Material ma;

    void Start()
    {
        for(int i = 0;i<3;i++)
        {
            backs[i] = transform.GetChild(i).GetComponent<Transform>();
        }

        for(int i = 0;i < transform.childCount; i++)
        {
            for(int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material = ma;
            }
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
