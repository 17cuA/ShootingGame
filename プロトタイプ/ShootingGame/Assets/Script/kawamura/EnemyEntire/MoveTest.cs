using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
	Vector3 velocity;
	public float speed;

	public float aliveMax;
	public float deadCnt;
	public bool shinimasuka = false;
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

		if (shinimasuka)
		{
			deadCnt += Time.deltaTime;
			if (deadCnt > aliveMax)
			{
				deadCnt = 0;
				Destroy(gameObject);
			}
		}
	}
}
