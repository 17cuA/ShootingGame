using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Parts : character_status
{
	new private void Update()
	{
		base.Update();
		if(Died_Judgment())
		{
			foreach(var render in object_material)
			{
				render.enabled = false;
			}
		}
	}
}
