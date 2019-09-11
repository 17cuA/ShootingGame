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
	public bool isPlay1Laser = false;
	public bool isPlay2Laser = false;
	public bool IsPlayer1Laser
	{
		get
		{
			return isPlay1Laser;
		}
		set
		{
			isPlay1Laser = value;
		}
	}

	public bool IsPlayer2Laser
	{
		get
		{
			return isPlay2Laser;
		}
		set
		{
			isPlay2Laser = value;
		}
	}

	public Material redMaterial;
	public Material blueMaterial;
	private void Awake()
    {
		this.trailRenderer = GetComponent<TrailRenderer>();
		this.rigidbody = GetComponent<Rigidbody>();
	}

	private new void Update()
    {
		if(isPlay1Laser)
		{
			if (!transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(true);
			if (transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(false);
			if(trailRenderer.material != blueMaterial)
				trailRenderer.material = blueMaterial;
		}

		if (isPlay2Laser)
		{
			if (transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(false);
			if (!transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(true);
			if(trailRenderer.material != redMaterial)
				trailRenderer.material = redMaterial;
		}

		if (transform.position.x >= 25.0f || transform.position.x <= -25.0f
		|| transform.position.y >= 8.5f || transform.position.y <= -8.5f)
		{
			gameObject.SetActive(false);
		}

		//rigidbody.velocity = new Vector3(shot_speed * 40, 0, 0);
		base.Moving_To_Travelling_Direction();
	}
}
