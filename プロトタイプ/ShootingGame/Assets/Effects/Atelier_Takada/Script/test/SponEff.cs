﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponEff : MonoBehaviour
{
	public GameObject obj;
	public float time;

	// Start is called before the first frame update
	void Start()
	{
		time = 0.0f;

	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;


		if (time > 1.0f)
		{
			Vector3 vec3 = new Vector3(
					Random.Range(-0.5f, 0.5f),
					Random.Range(-0.5f, 0.5f)-1.0f,
					0.0f);

			Instantiate(obj, transform.position + vec3, transform.rotation);
			time = 0.0f;
		}

	}
}
