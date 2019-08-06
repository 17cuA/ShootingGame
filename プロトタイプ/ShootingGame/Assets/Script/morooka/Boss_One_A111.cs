using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_A111 : MonoBehaviour
{
	public Boss_One_A111_Individual[] p;
	public bool Ok { get; private set; }

	public bool CCCCC()
	{
		bool ok = true;
		foreach(Boss_One_A111_Individual system in p)
		{
			ok = system.Completion;
		}

		return ok;
	}

	public void SetUp()
	{
		foreach(Boss_One_A111_Individual bo in p)
		{
			bo.GetComponent<ParticleSystem>().Play();
		}
	}
}
