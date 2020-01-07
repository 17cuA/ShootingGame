using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_Parts : character_status
{
	private void Update()
	{
		// HPが0のとき
		if(hp < 1)
		{
			Died_Judgment();
			enabled = false;
		}
	}
}
