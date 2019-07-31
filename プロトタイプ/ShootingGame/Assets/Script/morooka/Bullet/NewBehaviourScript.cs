using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : bullet_status
{
	public GameObject Person_Who_Shot { get; set; }
	// Start is called before the first frame update
	new void Start()
    {
		base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
		transform.position += transform.right.normalized * shot_speed;
    }

	new private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != Person_Who_Shot)
		{
			gameObject.SetActive(false);
		}
	}
}
