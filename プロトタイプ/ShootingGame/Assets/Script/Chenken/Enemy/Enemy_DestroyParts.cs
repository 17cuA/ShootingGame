using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_DestroyParts : character_status
{
    // Update is called once per frame
    new void Update()
    {
		if (base.hp < 1)
		{
			base.Died_Judgment();
			////
			base.Died_Process();
		}

		base.Update();
	}
}
