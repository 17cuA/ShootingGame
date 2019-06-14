using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	Vector3 pos;
	float a = 5;

    void Start()
    {
		pos = transform.position;
		FacingChange(new Vector3(1.0f,0.0f,0.0f));
    }

    void Update()
    {
		Vector3 vector = transform.position;
		vector.y = (transform.position.x - pos.x) * (transform.position.x - pos.x) + transform.position.y;

		transform.right = vector;
		transform.position = vector;

		Moving_To_Travelling_Direction();
    }
}
