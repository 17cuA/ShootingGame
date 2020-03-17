using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_Stage02SceneTranstion : MonoBehaviour
{
	private bool canCheck = true;

    void Update()
    {
		if (canCheck)
		{
			if (Wireless_sinario.IsFinishWireless())
			{
				GameObject.Find("Scene_Manager").GetComponent<Scene_Manager>().Screen_Transition_To_EndRoll();
				canCheck = false;
			}
		}
    }
}
