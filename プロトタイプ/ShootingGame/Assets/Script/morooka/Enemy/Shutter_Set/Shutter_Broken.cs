using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter_Broken : character_status
{
    new void Update()
    {
        if(hp < 1)
		{
			Died_Process();
		}
    }
}