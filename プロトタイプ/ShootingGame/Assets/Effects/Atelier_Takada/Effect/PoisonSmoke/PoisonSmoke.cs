using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{
	public float time;
	private float elapsedTime;

	void Start()
    {
		elapsedTime = 0.0f;
	}
    void Update()
    {
		if(elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;

			if (elapsedTime > time)
			{
				GetComponent<SphereCollider>().enabled = true;
			}
		}

	}
}
