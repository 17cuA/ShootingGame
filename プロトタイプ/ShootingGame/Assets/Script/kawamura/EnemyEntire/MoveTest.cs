using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
	Vector3 velocity;
	public float speed;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
		gameObject.transform.position += velocity * Time.deltaTime;
    }
}
