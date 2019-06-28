using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
	public float waveSpeed = 0;
	public float defaSpeed;
	public float waveIntervalTime;
	public float k;
	float dec;


	Vector3 velocity;

	bool isInc;
	bool isDec;

	void Start()
    {
		//waveSpeed = defaSpeed;
		dec = -2;
		isInc = true;
    }

    void Update()
    {
		velocity = gameObject.transform.rotation * new Vector3(-5, 0, 0);
		gameObject.transform.position += velocity * Time.deltaTime;
		transform.position = new Vector3(transform.position.x , Mathf.Sin(Time.frameCount*-0.2f), transform.position.z);
		//transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, dec), transform.position.z);

		if (waveSpeed > defaSpeed)
		{
			isDec = true;
			isInc = false;
		}
		else if (waveSpeed < -defaSpeed)
		{
			isInc = true;
			isDec = false;
		}
		
		if(isInc)
		{
			waveSpeed += 1;
		}
		else if(isDec)
		{
			waveSpeed -= 1;
		}

	}
}
