using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	private float bottomSpeed;
	public float intervalChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		bottomSpeed +=  intervalChange * Time.deltaTime;
		transform.position += bottomSpeed * Travelling_Direction.normalized;

		Moving_To_Facing();		
	}
}
