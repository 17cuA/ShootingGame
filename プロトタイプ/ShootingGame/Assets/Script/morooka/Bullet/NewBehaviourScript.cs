using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : bullet_status
{
	public GameObject Person_Who_Shot { get; set; }
	new void Start()
    {
		base.Start();
    }

    new void Update()
    {
		transform.position -= transform.right.normalized * shot_speed;
    }

	new private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != Person_Who_Shot)
		{
			if (other.tag != "Enemy_Bullet" && other.tag != "Player_Bullet")
			{
				gameObject.SetActive(false);
			}
		}
	}
}
