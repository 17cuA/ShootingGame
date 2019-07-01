using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInheritance : character_status
{
    void Start()
    {

	}

	void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}

	}
}
