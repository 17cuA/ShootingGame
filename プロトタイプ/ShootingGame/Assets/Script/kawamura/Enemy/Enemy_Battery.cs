using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Battery : character_status
{
    new void Start()
    {
		HP_Setting();
		base.Start();
    }

    void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}
    }
}
