using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caution_Manager : MonoBehaviour
{
	[SerializeField, Header("表示フレーム")] private int a; 

    void Update()
    {
        if(Scene_Manager.Manager.Is_Fade_Finished)
		{
			
		}
    }
}
