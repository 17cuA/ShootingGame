using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorWave : character_status
{
    public GameObject meteor_One;
    public GameObject meteor_Two;
    public GameObject meteor_Three;
    public GameObject meteor_Four;
    public GameObject meteor_Five;

    public float speedX;
    public float speedY;
    public float rotaZ;
    public float rotaZ_ChangeValue;

    Vector3 velocity;

    public bool isInc = false;
    public bool isDec = false;

    new void Start()
    {
        meteor_One = GameObject.Find("Enemy_Meteor_One");
        meteor_Two = GameObject.Find("Enemy_Meteor_Two");
        meteor_Three = GameObject.Find("Enemy_Meteor_Three");
        meteor_Four = GameObject.Find("Enemy_Meteor_Four");
        meteor_Five = GameObject.Find("Enemy_Meteor_Five");

        rotaZ = transform.position.z;
        base.Start();
    }

    new void Update()
    {
        velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
        gameObject.transform.position += velocity * Time.deltaTime;

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotaZ);

        if (isInc)
        {
            rotaZ += rotaZ_ChangeValue;
        }
        else if(isDec)
        {
            rotaZ -= rotaZ_ChangeValue;
        }

    }
}
