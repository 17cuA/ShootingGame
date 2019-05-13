using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
	public ParticleManagement particleManagementCS;
	public Vector3 destination;
	public Vector3 mousePostion;
	void Start()
	{
		particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	}

	// Update is called once per frame
	void Update()
	{
		//マウス追従
		mousePostion = Input.mousePosition;
		mousePostion.z = 10f;
		destination = Camera.main.ScreenToWorldPoint(mousePostion);
		transform.position = destination;

		//爆発パターン
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			particleManagementCS.ParticleCreation(0, transform.position);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			particleManagementCS.ParticleCreation(1, transform.position);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			particleManagementCS.ParticleCreation(2, transform.position);
		}
	}
}
