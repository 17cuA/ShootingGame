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

	public float trailMaxTime = 1f;
	public float trailMinTime = 0.1f;
	public float lifeTime = 1.5f;

	private float lifeTimer = 0f;

    private void Awake()
    {
        base.shot_speed = 0.8f;
        base.attack_damage = 1;
        base.Travelling_Direction = Vector3.right;

		this.trailRenderer = GetComponent<TrailRenderer>();

    }

	private new void Update()
    {
		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
		|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			this.lifeTimer = 0f;
			this.trailRenderer.time = this.trailMaxTime;
			gameObject.SetActive(false);
		}

		this.lifeTimer += Time.deltaTime;

		if(this.lifeTimer > this.lifeTime)
		{
			this.lifeTimer = 0f;
			this.trailRenderer.time = this.trailMaxTime;
			gameObject.SetActive(false);
		}

		this.trailRenderer.time = Mathf.Lerp(trailMaxTime, trailMinTime, lifeTimer / lifeTime);

		base.Moving_To_Travelling_Direction();
    }
}
