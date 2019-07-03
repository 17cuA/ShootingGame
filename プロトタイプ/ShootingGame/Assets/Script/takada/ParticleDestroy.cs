using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
	public float existenceHour = 1.0f;
	private ParticleSystem particle;
	void Start()
	{
		particle = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if(particle.isPlaying)
		{
			existenceHour -= Time.deltaTime;
			if (existenceHour < 0.0f)
			{
				//Destroy(this.gameObject);
				particle.Stop();
				existenceHour = 1.0f;
				gameObject.SetActive(false);
			}
		}

	}
}
