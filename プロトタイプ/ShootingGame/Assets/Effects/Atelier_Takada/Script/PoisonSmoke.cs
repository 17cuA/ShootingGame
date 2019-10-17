using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : character_status
{


    void Start()
    {
        
    }



    void Update()
    {


		if (transform.position.y <= -30.0f)
		{
			Destroy(this.gameObject);
		}

	}
}
