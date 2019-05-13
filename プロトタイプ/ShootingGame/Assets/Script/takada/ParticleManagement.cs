using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManagement : MonoBehaviour
{
	/*
	 ParticleManagement particleManagementCS;
	 particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	 particleManagementCS.ParticleCreation(0,transform.position);
	 */
	[SerializeField]
	public GameObject[] particle = new GameObject[3];

	//void Start(){}

	void Update()
	{
	}

	public void ParticleCreation(int particleID,Vector3 objectPosition)
	{
		Instantiate(particle[particleID], objectPosition, transform.rotation);
	}
}
