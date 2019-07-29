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

	public float trailMaxWidth = 5;
	public float trailMinWidth;

	public float colliderMaxSize = 5;
	public float colliderMinSize;

	public bool isRotateLaser = false;
	public bool ischangLaserWidth = false;

	private float lifeTimer = 0f;

	private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        base.shot_speed = 0.8f;
        base.attack_damage = 1;
        base.Travelling_Direction = Vector3.right;

		this.trailRenderer = GetComponent<TrailRenderer>();
		this.trailMinWidth = 0;
		this.capsuleCollider = GetComponent<CapsuleCollider>();
		this.capsuleCollider.direction = 0;
	}

	private new void Update()
    {
		if (isRotateLaser)
		{
			if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
			|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
			{
				this.lifeTimer = 0f;
				this.trailRenderer.time = this.trailMaxTime;
				this.ischangLaserWidth = false;
				gameObject.SetActive(false);
			}
		}
		else
		{
			if (transform.position.x >= 30.0f || transform.position.x <= -30.0f
			|| transform.position.y >= 13.5f || transform.position.y <= -13.5f)
			{
				this.lifeTimer = 0f;
				this.trailRenderer.time = this.trailMaxTime;
				this.ischangLaserWidth = false;
				gameObject.SetActive(false);
			}
		}

		this.lifeTimer += Time.deltaTime;

		if(this.lifeTimer > this.lifeTime)
		{
			this.lifeTimer = 0f;
			this.trailRenderer.time = this.trailMaxTime;
			this.ischangLaserWidth = false;
			gameObject.SetActive(false);
		}

		this.trailRenderer.time = Mathf.Lerp(trailMaxTime, trailMinTime, lifeTimer / lifeTime);

		if (isRotateLaser && ischangLaserWidth)
		{
			this.trailRenderer.startWidth = Mathf.Lerp(trailMinWidth, trailMaxWidth, lifeTimer / lifeTime);
			this.trailRenderer.endWidth = Mathf.Lerp(trailMinWidth, trailMaxWidth, lifeTimer / lifeTime);
		}

		base.Moving_To_Travelling_Direction();
    }
}
