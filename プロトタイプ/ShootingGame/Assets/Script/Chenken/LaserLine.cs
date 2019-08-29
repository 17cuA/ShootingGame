using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
		this.trailRenderer = GetComponent<TrailRenderer>();
	}

	private new void Update()
    {

		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
		|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			gameObject.SetActive(false);
		}

		base.Moving_To_Travelling_Direction();
    }
}
