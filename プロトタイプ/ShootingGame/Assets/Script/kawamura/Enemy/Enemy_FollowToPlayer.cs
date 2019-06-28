using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FollowToPlayer : MonoBehaviour
{
	public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);
		gameObject.transform.position += velocity * Time.deltaTime;
	}
}
