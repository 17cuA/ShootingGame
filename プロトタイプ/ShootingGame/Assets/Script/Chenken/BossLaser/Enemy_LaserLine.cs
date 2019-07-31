using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Enemy_LaserLine : Enemy_Bullet
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
		base.shot_speed = 0.8f;
		base.attack_damage = 1;

		this.trailRenderer = GetComponent<TrailRenderer>();
	}

	private new void Update()
	{
			if (transform.position.x >= 30.0f || transform.position.x <= -30.0f
			|| transform.position.y >= 13.5f || transform.position.y <= -13.5f)
			{
				gameObject.SetActive(false);
			}
		   base.Moving_To_Travelling_Direction();
	}
}
