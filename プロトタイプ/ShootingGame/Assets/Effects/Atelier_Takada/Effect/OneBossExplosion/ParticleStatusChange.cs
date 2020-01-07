using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStatusChange : MonoBehaviour
{
	public List<ParticleSystem> ParticleSystemList 
		= new List<ParticleSystem>();


    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void SetStatus(float time,float size)
	{
		for(int i = 0; i < ParticleSystemList.Count; i++)
		{
			ParticleSystemList[i].startDelay = time;

			Vector3 Scale = new Vector3(size, size, 1);
			ParticleSystemList[i].transform.localScale = Scale;
		}
	}

}
