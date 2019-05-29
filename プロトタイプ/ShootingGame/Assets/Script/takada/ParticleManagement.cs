using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManagement : MonoBehaviour
{
	/*
	 public ParticleManagement particleManagementCS;
	 particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	 particleManagementCS.ParticleCreation(0,transform.position);
	 */
	[SerializeField]
	public GameObject[] particle = new GameObject[3];

	//test
	GameObject particleDirection;

	//void Start(){}

	void Update()
	{
		particleDirection = GameObject.Find("ParticleDirection");
	}

	public void ParticleCreation(int particleID, Vector3 objectPosition)
	{
		GameObject particleGameObject = Instantiate(particle[particleID], objectPosition, particle[particleID].transform.rotation);
        if(particleID == 3)
        {
            Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f),0);
            particleGameObject.transform.position += position;
        }
		particleGameObject.transform.parent = particleDirection.transform;
	}
}
