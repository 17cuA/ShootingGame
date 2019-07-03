using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
	public float existenceHour;
	private float kari;
	private ParticleSystem particle;
	void Start()
	{
		particle = GetComponent<ParticleSystem>();
		kari = existenceHour;
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
				existenceHour = kari;
				gameObject.SetActive(false);
			}
		}

	}
}
