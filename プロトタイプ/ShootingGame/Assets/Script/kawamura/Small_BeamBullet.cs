using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_BeamBullet : bullet_status
{
	// Start is called before the first frame update
	private new void Start()
	{
		base.Start();
		//Tag_Change("Enemy");
	}

	private new void Update()
	{
		if (transform.position.x >= 18.5f || transform.position.x <= -18.5f || transform.position.y >= 10.0f || transform.position.y <= -10.0f)
		{
			Destroy(gameObject);
			//gameObject.SetActive(false);

		}
	

		base.Update();
		Moving_To_Facing();
	}
}
