using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;

[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
	private TrailRenderer trailRenderer;
	public TrailRenderer TrailRenderer
	{
		get
		{
			return trailRenderer;
		}
	}

	private new Rigidbody rigidbody;

    private void Awake()
    {
		this.trailRenderer = GetComponent<TrailRenderer>();
		this.rigidbody = GetComponent<Rigidbody>();
	}

	private new void Update()
    {

		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
		|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			gameObject.SetActive(false);
		}

		//rigidbody.velocity = new Vector3(shot_speed * 40, 0, 0);
		base.Moving_To_Travelling_Direction();
	}
}
