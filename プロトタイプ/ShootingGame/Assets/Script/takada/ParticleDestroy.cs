using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
	//public float existenceHour = 3.0f;
	private ParticleSystem particle;
	void Start()
	{
		particle = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		//existenceHour -= Time.deltaTime;
		//if(existenceHour < 0.0f && particle.isPlaying)
		if(!particle.isPlaying)
		{
			//Destroy(this.gameObject);
			particle.Stop();
			gameObject.SetActive(false);
		}

	}
}
