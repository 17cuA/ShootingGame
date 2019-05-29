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
	/*
		//マウス追従
		mousePostion = Input.mousePosition;
		mousePostion.z = 10f;
		destination = Camera.main.ScreenToWorldPoint(mousePostion);
		transform.position = destination;

		*/
		//爆発パターン
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			particleManagementCS.ParticleCreation(0, transform.position + new Vector3(0,0,-1));
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			particleManagementCS.ParticleCreation(1, transform.position + new Vector3(0, 0, -1));
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			particleManagementCS.ParticleCreation(2, transform.position + new Vector3(0, 0, -1));
		}
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            particleManagementCS.ParticleCreation(3, transform.position + new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            particleManagementCS.ParticleCreation(4, transform.position + new Vector3(0, 0, -1));
        }
		else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            particleManagementCS.ParticleCreation(5, transform.position + new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            particleManagementCS.ParticleCreation(6, transform.position + new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            particleManagementCS.ParticleCreation(7, transform.position + new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            particleManagementCS.ParticleCreation(8, transform.position + new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            particleManagementCS.ParticleCreation(9, transform.position + new Vector3(0, 0, -1));
        }
    }
}
