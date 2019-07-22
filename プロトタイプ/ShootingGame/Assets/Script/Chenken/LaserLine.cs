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
        base.shot_speed = 0.8f;
        base.attack_damage = 1;
        base.Travelling_Direction = Vector3.right;

		this.trailRenderer = GetComponent<TrailRenderer>();

    }

	private new void Update()
    {
		base.Update();

        base.Moving_To_Travelling_Direction();

    }
}
